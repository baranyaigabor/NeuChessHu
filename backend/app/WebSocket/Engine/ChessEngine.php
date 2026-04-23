<?php

namespace App\WebSocket\Engine;

use App\Services\MatchService;
use App\Services\StockfishService;
use ChessLogic\ChessBoard\ChessPieces\Definitions\Piece;
use ChessLogic\ChessBoard\ChessPieces\Definitions\Side;
use ChessLogic\ChessBoard\ChessPieces\Pieces\ChessPiece;
use ChessLogic\MatchDatas\DataStores\Clocks;
use ChessLogic\MatchDatas\MatchDataStore;
use ChessLogic\Messaging\ChatMessagesHandlerFactory;
use ChessLogic\Moving\Factories\MoveFactory;
use ChessLogic\Moving\Factories\MoveValidatorFactory;
use ChessLogic\Moving\Factories\MoveComponents\MoveComponentsFactory;
use Exception;
use Illuminate\Support\Facades\Log;
use RuntimeException;
use Throwable;

class ChessEngine
{
    public function __construct(private MoveFactory $moveFactory,
        private MoveValidatorFactory $moveValidatorFactory,
        private MoveComponentsFactory $componentsFactory,
        private ChatMessagesHandlerFactory $chatMessagesHandler,
        private StockfishService $stockfishService
    ) {}

    private array $matchDataStores = [];
    private array $validators = [];

    private function methodsFactory(string $channel) : array
    {
        if (!isset($this->validators[$channel]))
        {
            $data = MatchService::getMatchFromCache($channel);

            if (!$data)
            {
                throw new Exception(message: "Game not found for channel: $channel");
            }

            $matchDataStore = MatchDataStore::fromCache($data);
            $components = $this->componentsFactory->create();

            $this->matchDataStores[$channel] = $matchDataStore;
            $this->validators[$channel] = $this->moveValidatorFactory->create($matchDataStore, $components);
        }

        $matchDataStore = $this->matchDataStores[$channel];
        $moveValidator = $this->validators[$channel];

        $move = $this->moveFactory->create($matchDataStore, $moveValidator);
        $chatMessage = $this->chatMessagesHandler->create($matchDataStore);

        return [$matchDataStore, $moveValidator, $move, $chatMessage];
    }
    
    public function onMessage(string $rawMessage) : string
    {
        $data = json_decode($rawMessage, true);
        
        if (!$data)
        {
            return json_encode(['error' => 'Invalid JSON']);
        }        
        
        $type = $data['type'] ?? null;
        $requestID = $data['requestID'] ?? null;
        $payload = $data['payload'] ?? [];
        $channel = $payload['channel'];
        
        try 
        {
            $responsePayload = match ($type) {
                'request-move-piece' => $this->movePiece($payload),
                'request-is-legal-move' => $this->isLegalMove($payload),
                'request-legal-moves' => $this->currentLegalMovesWithSelectedPiece($payload),
                'request-chat-message' => $this->chatMessage($payload),
                'request-match-point' => $this->matchPointSetter($payload),
                'request-draw-response' => $this->drawResponseSetter($payload),
                default => throw new Exception("Unknown request type: $type")
            };

            $store = $this->matchDataStores[$channel];

            if ($store->MatchPoints->MatchEnded) 
            {
                MatchService::saveMatchToDB($channel);
                $this->removeMatchFromMemory($channel);
            }

            return json_encode([
                'requestID' => $requestID,
                'payload' => $responsePayload
            ]);
        } 
        catch (Throwable $e) 
        {
            return json_encode([
                'requestID' => $requestID,
                'error' => $e->getMessage()
            ]);
        }
    }

    public function removeMatchFromMemory(string $channel) : void
    {
        unset($this->matchDataStores[$channel]);
        unset($this->validators[$channel]);
        MatchService::removeMatchFromCache($channel);
    }

    private function isLegalMove(array $payload) : bool
    {
        [, $validator] = $this->methodsFactory($payload['channel']);
        return $validator->isLegalMove($payload['from'], $payload['to']);
    }

    private function currentLegalMovesWithSelectedPiece(array $payload) : array
    {
        [, $validator] = $this->methodsFactory($payload['channel']);

        $pieceMatrix = array_map(
            fn($row) => array_map(
                fn($piece) => $piece ? ChessPiece::fromString($piece) : null,
                $row
            ),
            $payload['pieceMatrix']
        );

        $side = Side::from($payload['playingSide']);

        return $validator->currentLegalMovesWithSelectedPiece(
            $pieceMatrix,
            $payload['from'],
            $side
        );
    }

    private function movePiece(array $payload) : string
    {
        $promotionChoice = Piece::from($payload['promotionChoice']);

        $result = $this->applyMove(
            $payload['channel'],
            $payload['from'],
            $payload['to'],
            $promotionChoice
        );

        $this->updateMatchToClients($payload['channel'], $result, false);
        $this->playStockfishTurnIfNeeded($payload['channel']);

        return $result['sound'];
    }

    private function applyMove(string $channel, array $from, array $to, Piece $promotionChoice) : array
    {
        [$matchDataStore,, $move] = $this->methodsFactory($channel);

        $currentSide = $matchDataStore->MatchState->CurrentSide;
        $timedOut = !$matchDataStore->Clocks->onMove($currentSide);
        
        $moveResult = $move->movePiece($from, $to, $promotionChoice);
        
        if ($timedOut)
        {
            $winningSide = $currentSide === Side::White ? Side::Black : Side::White;
            $matchDataStore->MatchPoints->markMatchEnded();
            $matchDataStore->MatchPoints->setMatchPointsReason('Timeout', isForcedDraw: false);

            $winningSide = $currentSide === Side::White ? Side::Black : Side::White;

            $winnerRemainingMs = $winningSide === Side::White
                ? $matchDataStore->Clocks->WhiteRemainingMs
                : $matchDataStore->Clocks->BlackRemainingMs;

            $matchDataStore->MatchPoints->setWinner(
                $matchDataStore->PlayerDatas[$winningSide->value]->ID, 
                Clocks::formatTime($winnerRemainingMs)
            );

            $matchDataStore->MatchState->CurrentSide = Side::None;
        }

        $result = [
            'sound' => $moveResult['sound'],
            'match_state' => $matchDataStore->MatchState->jsonSerialize(),
            'player_datas' => $matchDataStore->serializePlayerDatas(),
            'match_points' => $matchDataStore->MatchPoints->jsonSerialize(),
            'draw_trackers' => $matchDataStore->DrawTrackers->jsonSerialize(),
            'clocks' => $matchDataStore->Clocks->jsonSerialize()
        ];

        return $result;
    }

    private function playStockfishTurnIfNeeded(string $channel) : void
    {
        [$matchDataStore, $validator] = $this->methodsFactory($channel);
        $config = $this->stockfishConfig($channel);

        if (!$config || $matchDataStore->MatchPoints->MatchEnded) 
        {
            return;
        }

        $currentSide = $matchDataStore->MatchState->CurrentSide;

        if ($currentSide === Side::None) 
        {
            return;
        }

        $currentPlayer = $matchDataStore->PlayerDatas[$currentSide->value]->ID ?? null;

        if ((string)$currentPlayer !== (string)$config['player_id']) 
        {
            return;
        }

        sleep(1);

        try {
            $stockfishMove = $this->stockfishService->bestMove($matchDataStore, (int)$config['depth']);
            $legalMoves = $validator->currentLegalMovesWithSelectedPiece(
                $matchDataStore->MatchState->PieceMatrix,
                $stockfishMove['from']
            );

            if (!$legalMoves[$stockfishMove['to'][0]][$stockfishMove['to'][1]]) {
                throw new RuntimeException("Stockfish returned illegal move {$stockfishMove['uci']}");
            }

            $result = $this->applyMove(
                $channel,
                $stockfishMove['from'],
                $stockfishMove['to'],
                $stockfishMove['promotionChoice']
            );

            $this->updateMatchToClients($channel, $result, false);
        } 
        catch (Throwable $e) 
        {
            Log::warning("Stockfish move failed for channel {$channel}: {$e->getMessage()}");
        }
    }

    private function stockfishConfig(string $channel) : ?array
    {
        $data = MatchService::getMatchFromCache($channel);
        $config = $data['stockfish'] ?? null;

        if (!is_array($config) || empty($config['enabled'])) 
        {
            return null;
        }

        return $config;
    }

    private function chatMessage(array $payload) : array
    {
        [,,, $chatMessage] = $this->methodsFactory($payload['channel']);
        
        $result = $chatMessage->handleMessage($payload['userID'], $payload['message']);
        
        if ($result['Status'] === 'Success')
        {
            $this->updateMatchToClients($payload['channel'], 
                ['new_message' => $result['NewMessage']], isChat: true);
        }

        return $result;
    }

    private function matchPointSetter(array $payload) : string
    {
        [$matchDataStore] = $this->methodsFactory($payload['channel']);

        MatchDataStore::matchPointSetter($matchDataStore, $payload['userID'], $payload['matchPointReason']);
    
        $result = [
            'match_state' => $matchDataStore->MatchState->jsonSerialize(),
            'player_datas' => $matchDataStore->serializePlayerDatas(),
            'match_points' => $matchDataStore->MatchPoints->jsonSerialize(),
            'draw_trackers' => $matchDataStore->DrawTrackers->jsonSerialize(),
            'clocks' => $matchDataStore->Clocks->jsonSerialize()
        ];

        $this->updateMatchToClients($payload['channel'], $result, false);

        return "Ok";
    }

    private function drawResponseSetter(array $payload)
    {
        [$matchDataStore] = $this->methodsFactory($payload['channel']);

        MatchDataStore::drawResponseSetter($matchDataStore, $payload['userID'], $payload['drawResponse']);

        $result = [
            'match_state' => $matchDataStore->MatchState->jsonSerialize(),
            'player_datas' => $matchDataStore->serializePlayerDatas(),
            'match_points' => $matchDataStore->MatchPoints->jsonSerialize(),
            'draw_trackers' => $matchDataStore->DrawTrackers->jsonSerialize(),
            'clocks' => $matchDataStore->Clocks->jsonSerialize()
        ];

        $this->updateMatchToClients($payload['channel'], $result, false);

        return "Ok";
    }

    private function updateMatchToClients(string $channel, array $data, bool $isChat) : void
    {
        $cacheKey = str_replace('private-', '', $channel);
        $existing = MatchService::getMatchFromCache($channel);

        if ($existing) 
        {
            $basePath = 'http://webserver/api/match/';

            if ($isChat)
            {
                $existingMessages = $existing['chat_messages'] ?? [];
                
                MatchService::updateMatchInCache($channel, array_merge($existing, [
                    'chat_messages' => array_merge($existingMessages, [$data['new_message']]),
                ]));

                $payload = json_encode([
                    'channel' => $cacheKey,
                    'new_message' => $data['new_message'],
                ]);

                $endpoint = $basePath . 'chat-update';
            }

            else
            {
                MatchService::updateMatchInCache($channel, array_merge($existing, [
                    'match_state' => $data['match_state'],
                    'player_datas' => $data['player_datas'],
                    'match_points' => $data['match_points'],
                    'draw_trackers' => $data['draw_trackers'],
                    'clocks' => $data['clocks'],
                ]));

                $payload = json_encode([
                    'channel' => $cacheKey,
                    'match_state' => $data['match_state'],
                    'player_datas' => $data['player_datas'],
                    'match_points' => $data['match_points'],
                    'draw_trackers' => $data['draw_trackers'],
                    'clocks' => $data['clocks'],
                ]);

                $endpoint = $basePath . 'state-update';
            }

            $context = stream_context_create([
                'http' => [
                    'method' => 'POST',
                    'header' => "Content-Type: application/json\r\nHost: backend.vm2.test\r\n",
                    'content' => $payload,
                    'timeout' => 5,
                ]
            ]);

            @file_get_contents($endpoint, false, $context);
        }
    }
}