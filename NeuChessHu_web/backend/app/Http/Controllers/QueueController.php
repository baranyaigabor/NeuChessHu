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
        $playerID = $request->user()->id;
        $matchDuration = $request->input('matchDuration');
        $stockfishDepth = $request->input('stockfishDepth', $request->input('StockfishDepth'));

        if ($stockfishDepth !== null) 
        {
            MatchmakingQueue::where('player_id', $playerID)->delete();

            $channel = $this->matchMaker->startStockfishMatch(
                $playerID,
                $matchDuration,
                (int) $stockfishDepth
            );

            return response()->json([
                'status' => 'match-found',
                'channel' => $channel,
                'stockfish' => true,
            ]);
        }

        $this->enqueuePlayer($playerID, $matchDuration);

        $channel = $this->matchMaker->tryStartMatch($matchDuration);

        return response()->json([
            'status' => $channel ? 'match-found' : 'waiting',
            'channel' => $channel,
        ]);
    }

    public function leave(QueueRequest $request)
    {
        $playerId = $request->user()->id;

        $player = MatchmakingQueue::where('player_id', $playerId)->first();

        if (!$player) 
        {
            return response()->json([
                'status' => 'not-in-queue',
                'message' => 'Player is not currently in the matchmaking queue.',
            ]);
        }

        $player->delete();

        return response()->json([
            'status' => 'left',
            'message' => 'Player successfully removed from the matchmaking queue.',
        ]);
    }

    public function enqueuePlayer(int $playerID, string $matchDuration) : void
    {
        MatchmakingQueue::updateOrCreate(
            ['player_id' => $playerID],
            ['match_duration' => $matchDuration, 'joined_at' => now()]
        );
    }

    public function matchPlayersFromDB(string $matchDuration) : ?array
    {
        return DB::transaction(function () use ($matchDuration) : ?array 
        {
            $players = MatchmakingQueue::where('match_duration', $matchDuration)
                                       ->orderBy('joined_at')
                                       ->lockForUpdate()
                                       ->limit(2)
                                       ->get();

            if ($players->count() < 2) 
            {
                return null;
            }

            $player1 = (int) $players[0]->player_id;
            $player2 = (int) $players[1]->player_id;

            MatchmakingQueue::whereIn('player_id', [$player1, $player2])->delete();

            return [$player1, $player2];
        });
    }
}