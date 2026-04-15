<?php

namespace App\Services;

use Illuminate\Support\Facades\Cache;

class ReadyPlayersRegistryService
{
    public function __construct() { }

    public function markPlayerReady(string $channel, int $playerId): void
    {
        $cacheKey = "ready_players:{$channel}";
        $lock = Cache::lock("match_lock:{$channel}", 10);

        if (!$lock->get()) 
        {
            return;
        }

        try 
        {
            $players = Cache::get($cacheKey, []);

            $players[$playerId] = true;

            Cache::put($cacheKey, $players, now()->addMinutes(10));

            if (count($players) >= 2) {
                Cache::forget($cacheKey);
                //start    
            }
        } 
        finally 
        {
            $lock->release();
        }
    }
}