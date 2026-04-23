<?php

namespace App\Http\Controllers;

use App\Http\Requests\QueueRequest;
use App\Models\MatchmakingQueue;
use App\Services\MatchService;
use Illuminate\Support\Facades\DB;

class QueueController
{
    public function __construct(private MatchService $matchMaker) {}

    public function join(QueueRequest $request)
    {
        $stockfishDepth = $request->input('stockfishDepth', $request->input('StockfishDepth'));

        if ($stockfishDepth !== null) {
            MatchmakingQueue::where('player_id', $request->playerID)->delete();

            $channel = $this->matchMaker->startStockfishMatch(
                $request->playerID,
                $request->matchDuration,
                (int)$stockfishDepth
            );

            return response()->json([
                'status' => 'match-found',
                'channel' => $channel,
                'stockfish' => true,
            ]);
        }

        $this->enqueuePlayer($request->playerID, $request->matchDuration);
                
        $channel = $this->matchMaker->tryStartMatch($request->matchDuration);
     
        return response()->json([
            'status' => $channel ? 'match-found' : 'waiting',
            'channel' => $channel
        ]);
    }

    public function leave(QueueRequest $request)
    {
        $playerId = $request->playerID;

        $player = MatchmakingQueue::where('player_id', $playerId)->first();

        if (!$player) {
            return response()->json([
                'status' => 'not-in-queue',
                'message' => 'Player is not currently in the matchmaking queue.'
            ]);
        }

        $player->delete();

        return response()->json([
            'status' => 'left',
            'message' => 'Player successfully removed from the matchmaking queue.'
        ]);
    }

    public function enqueuePlayer(int $playerID, string $match_duration) : void
    {
        MatchmakingQueue::updateOrCreate(
            ['player_id' => $playerID],
            ['match_duration' => $match_duration, 'joined_at' => now()]
        );
    }

    public function matchPlayersFromDB(string $match_duration) : ?array
    {
        return DB::transaction(function () use ($match_duration): ?array {
            $players = MatchmakingQueue::where('match_duration', $match_duration)
                ->orderBy('joined_at')
                ->lockForUpdate()
                ->limit(2)
                ->get();

            if ($players->count() < 2) {
                return null;
            }

            $player1 = (int)$players[0]->player_id;
            $player2 = (int)$players[1]->player_id;

            MatchmakingQueue::whereIn('player_id', [$player1, $player2])->delete();

            return [$player1, $player2];
        });
    }
}