<?php

namespace App\Services;

use App\Events\ChannelAssignment;
use App\Events\MatchPointsUpdated;
use App\Events\MatchStarted;
use App\Events\PlayerDatasUpdated;
use App\Http\Controllers\QueueController;
use App\Models\Matches;
use ChessLogic\ChessBoard\ChessPieces\Definitions\Side;
use ChessLogic\MatchDatas\MatchDataStore;
use Illuminate\Support\Facades\Cache;

class MatchService 
{
    public function tryStartMatch(string $match_duration) : ?string
    {
        $matchedPlayers = app(QueueController::class)->matchPlayersFromDB($match_duration);

        if (!$matchedPlayers)
        {
            return null;
        }

        [$player1, $player2] = $matchedPlayers;

        $channel = $this->createChannel($player1, $player2);
        $this->createMatch($player1, $player2, $match_duration, $channel);

        return $channel;
    }

    public static function startMatch(string $channel) : void
    {
        $cacheKey = str_replace('private-', '', $channel);
        $data = Cache::get("game:{$cacheKey}");

        if (!$data) {
            return;
        }
    
        broadcast(new MatchStarted($cacheKey, [
            'WhiteID' => $data['white_id'],
            'BlackID' => $data['black_id'],
            'InitialState' => $data['match_state'],
            'Clocks' => $data['clocks'],
        ]));
    
        broadcast(new PlayerDatasUpdated($cacheKey, $data['player_datas']));
        broadcast(new MatchPointsUpdated($cacheKey, $data['match_points']));
    }

    private function createChannel(int $player1, int $player2) : string
    {
        $timestamp = now()->format('YmdHis');
        return "{$player1}-{$player2}-{$timestamp}";
    }
    
    private function createMatch(int $player1, int $player2, string $match_duration,
        string $channel) : void
    {
        $matchDataStore = MatchDataStore::createDataStore($player1, $player2, $match_duration, $channel);

        Cache::put("game:{$channel}", 
        [
            'match_id' => $matchDataStore->MatchID,
            'white_id' => $matchDataStore->PlayerDatas[Side::White->value]->ID,
            'black_id' => $matchDataStore->PlayerDatas[Side::Black->value]->ID,
            'match_state' => $matchDataStore->MatchState->jsonSerialize(),
            'player_datas' => $matchDataStore->serializePlayerDatas(),
            'match_points' => $matchDataStore->MatchPoints->jsonSerialize(),
            'draw_trackers' => $matchDataStore->DrawTrackers->jsonSerialize(),
            'clocks' => $matchDataStore->Clocks->jsonSerialize(),
            'chat_messages' => $matchDataStore->ChatMessages->jsonSerialize(),
            'played_at' => $matchDataStore->PlayedAt->format('Y-m-d H:i:s'),
            'match_duration' => $match_duration
        ], now()->addMinutes(30));

        $this->broadcastMatch($player1, $player2, $channel);
    }

    public static function getMatchFromCache(string $channel) : ?array
    {
        $cacheKey = 'game:' . str_replace('private-', '', $channel);
        return Cache::get($cacheKey);
    }

    public static function updateMatchInCache(string $channel, array $data) : void
    {
        $cacheKey = 'game:' . str_replace('private-', '', $channel);
        Cache::put($cacheKey, $data, now()->addMinutes(30));
    }

    public static function removeMatchFromCache(string $channel) : bool
    {
        $cacheKey = 'game:' . str_replace('private-', '', $channel);
        return Cache::forget($cacheKey);
    }

    public static function saveMatchToDB(string $channel) : void
    {
        $data = self::getMatchFromCache($channel);

        if (!$data) {
            return;
        }

        $store = MatchDataStore::fromCache($data);

        if ($store->MatchPoints->getMatchPointsReason() !== 'Abort')
        {
            $moves = collect($store->MatchState->Notations)->map(fn($row) => [
                'round' => $row->Round,
                'white' => $row->WhitesNotation,
                'black' => $row->BlacksNotation,
            ])->toJson();

            $fields = [
                'match_id' => $store->MatchID,
                'white_id' => $store->PlayerDatas[Side::White->value]->ID,
                'black_id' => $store->PlayerDatas[Side::Black->value]->ID,
                'gamemode' => self::getGameMode($store->MatchState->MatchDuration),
                'match_duration' => $store->MatchState->MatchDuration,
                'played_at' => $store->PlayedAt,
                'moves' => $moves,
                'match_end_result' => $store->MatchPoints->getMatchPointsReason(),
                'winner_id' => $store->MatchPoints->WinnerID ?: null,
                'winner_time' => $store->MatchPoints->WinnerTime ?: null,
            ];

            Matches::create($fields);
        }
    }

    private static function getGameMode(string $match_duration) : string 
    {
        $base = (int)$match_duration;

        if ($base < 3) {
            return 'Bullet';
        }

        if ($base >= 3 && $base <= 5) {
            return 'Blitz';
        }

        return 'Rapid';
    }

    private function broadcastMatch(int $player1, int $player2, string $channel) : void
    {
        broadcast(new ChannelAssignment($channel, $player1));
        broadcast(new ChannelAssignment($channel, $player2));
    }
}