<?php

namespace App\Services;

use App\Events\MatchStartFailed;
use App\Http\Controllers\QueueController;
use Exception;
use Illuminate\Contracts\Cache\LockTimeoutException;
use Illuminate\Support\Facades\Cache;
use Illuminate\Support\Facades\Log;

class ReadyPlayersRegistryService
{
    public function __construct() { }

    public function markPlayerReady(string $channel, int $playerId) : void
    {
        $cacheKey = "ready_players:{$channel}";
        $shouldStart = false;

        try 
        {
            Cache::lock("match_lock:{$channel}", 10)->block(5, function () 
                use ($cacheKey, $channel, $playerId, &$shouldStart) 
            {
                $players = Cache::get($cacheKey, []);

                if (isset($players[$playerId]))
                {
                    return;
                }

                $players[$playerId] = true;
                Cache::put($cacheKey, $players, now()->addMinutes(10));

                if (count($players) >= 2)
                {
                    $shouldStart = true;
                }
            });
        } 
        catch (LockTimeoutException $e) 
        {
            Log::warning("match_lock timeout for channel {$channel}, player {$playerId}");
            return;
        }

        if ($shouldStart) 
        {
            try
            {
                MatchService::startMatch($channel);
                Cache::forget($cacheKey);
            }
            catch (Exception $e)
            {
                $this->recoverPlayers($channel);
            }
        }
    }

    private function recoverPlayers(string $channel): void
    {
        $data = MatchService::getMatchFromCache($channel);

        if (!$data) 
        {
            return;
        }

        Cache::forget("ready_players:{$channel}");
        MatchService::removeMatchFromCache($channel);

        app(QueueController::class)->enqueuePlayer($data['white_id'], $data['match_duration']);
        app(QueueController::class)->enqueuePlayer($data['black_id'], $data['match_duration']);

        broadcast(new MatchStartFailed($channel));
    }
}