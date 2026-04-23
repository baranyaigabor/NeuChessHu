<?php

namespace App\Services;

use App\Events\MatchPointsUpdated;
use App\Events\MatchStarted;
use App\Events\PlayerDatasUpdated;
use App\Events\ChannelAssignment;
use App\Http\Controllers\QueueController;
use App\Models\Matches;
use App\Models\User;
use ChessLogic\MatchDatas\MatchDataStore;
use ChessLogic\ChessBoard\ChessPieces\Definitions\Side;
use Illuminate\Support\Facades\Cache;
use Illuminate\Support\Facades\Hash;
use Illuminate\Support\Str;

class MatchService 
{
    private const STOCKFISH_EMAIL = 'stockfish@neuchess.local';
    private const STOCKFISH_NICKNAME = 'Stockfish';

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

    public function startStockfishMatch(int $player, string $matchDuration, int $stockfishDepth) : string
    {
        $stockfish = $this->stockfishUser();
        $channel = $this->createChannel($player, $stockfish->id);

        $this->createMatch($player, $stockfish->id, $matchDuration, $channel, [
            'stockfish' => [
                'enabled' => true,
                'player_id' => $stockfish->id,
                'depth' => $stockfishDepth,
            ],
        ], Side::White, $player);

        return $channel;
    }

    public static function startMatch(string $channel) : void
    {
        $cacheKey = str_replace('private-', '', $channel);
        $data = Cache::get("game:{$cacheKey}");

        if (!$data) {
            return;
        }

        Cache::forget("pending_channel:{$data['white_id']}");
        Cache::forget("pending_channel:{$data['black_id']}");
    
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
        string $channel, array $metadata = [], ?Side $firstPlayerSide = null,
        ?int $assignmentPlayer = null) : void
    {
        $matchDataStore = MatchDataStore::createDataStore(
            (string)$player1,
            (string)$player2,
            $match_duration,
            $channel,
            $firstPlayerSide
        );

        Cache::put("game:{$channel}", array_merge([
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
        ], $metadata), now()->addMinutes(30));

        $this->broadcastMatch($player1, $player2, $channel, $assignmentPlayer);
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

    private function stockfishUser(): User
    {
        $user = User::query()
            ->where('email', self::STOCKFISH_EMAIL)
            ->orWhere('nickname', self::STOCKFISH_NICKNAME)
            ->first();

        if ($user) {
            return $user;
        }

        return User::create([
            'nickname' => self::STOCKFISH_NICKNAME,
            'email' => self::STOCKFISH_EMAIL,
            'password' => Hash::make(Str::random(32)),
            'first_name' => 'Stockfish',
            'last_name' => 'Engine',
            'is_active' => true,
        ]);
    }

    private function broadcastMatch(int $player1, int $player2, string $channel,
        ?int $assignmentPlayer = null) : void
    {
        $players = $assignmentPlayer ? [$assignmentPlayer] : [$player1, $player2];

        foreach ($players as $player) {
            Cache::put("pending_channel:{$player}", "private-{$channel}", now()->addMinutes(5));
            broadcast(new ChannelAssignment($channel, $player));
        }
    }
}