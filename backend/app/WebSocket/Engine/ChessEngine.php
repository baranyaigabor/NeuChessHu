<?php

namespace App\WebSocket\Engine;

use App\Services\MatchService;
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
use Throwable;

class ChessEngine
{
    public function __construct(private MoveFactory $moveFactory,
        private MoveValidatorFactory $moveValidatorFactory,
        private MoveComponentsFactory $componentsFactory,
        private ChatMessagesHandlerFactory $chatMessagesHandler
    ) {}

    private array $matchDataStores = [];
    private array $validators = [];

    private function methodsFactory(string $channel): array
    {
        if (!isset($this->validators[$channel]))
        {
            $data = MatchService::getMatchFromCache($channel);

            if (!$data)
                throw new Exception(message: "Game not found for channel: $channel");

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

        return $validator->currentLegalMovesWithSelectedPiece($pieceMatrix, $payload['from'], $side);
    }



    private function updateMatchToClients(string $channel, array $data, bool $isChat): void
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