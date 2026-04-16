<?php

namespace App\Http\Controllers;

use App\Http\Requests\QueueRequest;
use App\Models\MatchmakingQueue;
use App\Services\MatchService;
use Illuminate\Support\Facades\DB;

class QueueController
{
    public function __construct(private MatchService $matchMaker) {}

    public function enqueuePlayer(int $playerID, string $match_duration) : void
    {
        MatchmakingQueue::updateOrCreate(
            ['player_id' => $playerID],
            ['match_duration' => $match_duration, 'joined_at' => now()]
        );
    }
}