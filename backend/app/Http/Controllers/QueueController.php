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
        $this->enqueuePlayer($request->playerID, $request->matchDuration);
    }

    public function enqueuePlayer(int $playerID, string $match_duration) : void
    {
        MatchmakingQueue::updateOrCreate(
            ['player_id' => $playerID],
            ['match_duration' => $match_duration, 'joined_at' => now()]
        );
    }

    public function leave(QueueRequest $request)
    {
        $playerId = $request->playerID;

        $player = MatchmakingQueue::where('player_id', $playerId)->first();

        if (!$player) 
        {
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
}