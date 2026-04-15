<?php

namespace ChessLogic\MatchDatas;

use Carbon\Carbon;
use ChessLogic\MatchDatas\DataStores\ChatMessages;
use DateTime;
use ChessLogic\ChessBoard\ChessPieces\Definitions\Side;
use ChessLogic\MatchDatas\DataStores\Clocks;
use ChessLogic\MatchDatas\DataStores\DrawTrackers;
use ChessLogic\MatchDatas\DataStores\MatchPoints;
use ChessLogic\MatchDatas\DataStores\MatchState;
use ChessLogic\MatchDatas\DataStores\PlayerDatas;
use ChessLogic\MatchDatas\Initializers\MatchInitializer;
use InvalidArgumentException;
use React\Http\Io\Clock;

class MatchDataStore
{
    public string $MatchID;
    public DateTime $PlayedAt;

    public MatchPoints $MatchPoints;
    public MatchState $MatchState;
    
    /** @var array<int, PlayerDatas> */
    public array $PlayerDatas;

    public DrawTrackers $DrawTrackers;
    public Clocks $Clocks;
    public ChatMessages $ChatMessages;

    public static function createDataStore(string $ID1, string $ID2, string $matchDuration,
        string $channel) 
    {
        $store = new self();

        $matchDuration = str_replace(' ', '', $matchDuration);

        $store->MatchID = "{$matchDuration}-{$channel}";
        $store->PlayedAt = Carbon::now();

        $store->MatchPoints = new MatchPoints();
        $store->MatchState = new MatchState();
        $store->PlayerDatas = MatchInitializer::initializePlayers($ID1, $ID2);
        $store->DrawTrackers = new DrawTrackers();
        $store->Clocks = Clocks::fromMatchDuration($matchDuration);
        $store->ChatMessages = new ChatMessages();

        $store->MatchState->MatchDuration = $matchDuration;

        return $store;
    }

    public static function fromCache(array $data)
    {
        $store = new self();

        $store->MatchID = $data['match_id'];
        $store->PlayedAt = new DateTime($data['played_at'] ?? 'now');

        $store->DrawTrackers = new DrawTrackers();

        $store->MatchPoints = MatchPoints::fromArray(
            $data['match_points'] ?? []
        );

        $matchStateData = $data['match_state'];
        $matchStateData['matchDuration'] ??= $data['match_duration'] ?? null;

        $store->MatchState = MatchState::fromArray($matchStateData);

        $store->PlayerDatas = [
            Side::White->value => PlayerDatas::fromArray(
                $data['player_datas'][Side::White->value] ?? [],
                (string)$data['white_id']
            ),
            Side::Black->value => PlayerDatas::fromArray(
                $data['player_datas'][Side::Black->value] ?? [],
                (string)$data['black_id']
            ),
        ];

        $store->DrawTrackers = DrawTrackers::fromArray(
            $data['draw_trackers'] ?? []
        );

        $store->Clocks = Clocks::fromArray(
            $data['clocks'] ?? [])
        ;

        $store->ChatMessages = ChatMessages::fromArray(
            $data['chat_messages'] ?? []
        );

        return $store;
    }

    public static function handleMatchEnd(self $store)
    {
        $matchPoints = $store->MatchPoints;

        if (!$matchPoints->shouldEndMatch()) {
            return;
        }

        $matchPoints->markMatchEnded();

        $reason = $matchPoints->getMatchPointsReason();

        if ($reason === 'Checkmate') 
        {
            $side = $store->MatchState->CurrentSide;

            if ($side !== Side::None) 
            {
                $winnerRemainingMs = $side === Side::White
                    ? $store->Clocks->WhiteRemainingMs
                    : $store->Clocks->BlackRemainingMs;

                $matchPoints->setWinner(
                    $store->PlayerDatas[$side->value]->ID,
                    Clocks::formatTime($winnerRemainingMs)
                );
            }
        }

        $store->MatchState->CurrentSide = Side::None;
    }

    public static function matchPointSetter(self $store, int $userID, string $reason)
    {
        $matchPoints = $store->MatchPoints;

        if($reason === 'Abort')
        {
            $matchPoints->markMatchEnded();

            $matchPoints->setMatchPointsReason($reason, isForcedDraw: false);

            $store->MatchState->CurrentSide = Side::None;
        }

        else if($reason === 'Draw')
        {
            $store->MatchPoints->ClaimForDraw = true;
            $store->DrawTrackers->DrawAgreements = [];
        }

        else if($reason === 'Resign' || $reason === 'Timeout')
        {
            if ($reason === 'Timeout' && !Clocks::isTimeoutValid($store))
                return;
        
            $matchPoints->markMatchEnded();
            $matchPoints->setMatchPointsReason($reason, isForcedDraw: false);

            $winningSide = $reason === 'Resign'
                ? ($store->PlayerDatas[Side::White->value]->ID == $userID 
                    ? Side::Black 
                    : Side::White)
                : ($store->MatchState->CurrentSide === Side::White 
                    ? Side::Black 
                    : Side::White);

            $winnerRemainingMs = $winningSide === Side::White
                ? $store->Clocks->WhiteRemainingMs
                : $store->Clocks->BlackRemainingMs;

            $matchPoints->setWinner(
                $store->PlayerDatas[$winningSide->value]->ID,
                Clocks::formatTime($winnerRemainingMs)
            );

            $store->MatchState->CurrentSide = Side::None;
        }

        else 
        {
            throw new InvalidArgumentException();
        }
    }

    public static function drawResponseSetter(self $store, int $userID, bool $drawResponse)
    {
        $matchPoints = $store->MatchPoints;
        $drawTrackers = $store->DrawTrackers;

        $drawTrackers->drawResponseSetter((string)$userID, $drawResponse);

        if(count($drawTrackers->DrawAgreements) == 2)
        {
            $matchPoints->ClaimForDraw = false;
        }

        if (DrawTrackers::bothAgreedToDraw($drawTrackers))
        {
            $matchPoints->setMatchPointsReason('Mutual Agreement', isForcedDraw: false);
            self::handleMatchEnd($store);
        }
    }

    public function switchSide(): void
    {
        if ($this->MatchState->CurrentSide !== Side::None) 
        {
            $this->MatchState->CurrentSide = $this->MatchState->CurrentSide === Side::White
                ? Side::Black
                : Side::White;
        }
    }

    /**
     * @param array{0:int,1:int} $from
     * @param array{0:int,1:int} $to
     */
    public function swapPieces(array $from, array $to): void
    {
        $temp = $this->MatchState->PieceMatrix[$from[0]][$from[1]];

        $this->MatchState->PieceMatrix[$from[0]][$from[1]] =
            $this->MatchState->PieceMatrix[$to[0]][$to[1]];

        $this->MatchState->PieceMatrix[$to[0]][$to[1]] = $temp;
    }

    public function setEnPassantTarget(?array $target): void
    {
        $this->MatchState->EnPassantTarget = $target;
    }

    public function serializePlayerDatas(): array
    {
        $result = [];

        foreach ($this->PlayerDatas as $side => $player) {
            $result[(string)$side] = $player->jsonSerialize();
        }

        return $result;
    }
}