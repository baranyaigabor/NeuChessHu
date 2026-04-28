<?php

namespace App\Http\Controllers;

use Illuminate\Http\Request;
use Illuminate\Support\Facades\Cache;

class PendingChannelController extends Controller
{
    public function showPendingChannel(Request $request)
    {
        $userID = $request->user('sanctum')->id;
        $pendingCacheKey = "pending_channel:{$userID}";
        $activeCacheKey = "active_channel:{$userID}";

        $channel = Cache::get($pendingCacheKey) ?? Cache::get($activeCacheKey);

        if (!$channel) {
            return response()->json(['channel' => null]);
        }

        $gameKey = 'game:' . str_replace('private-', '', $channel);

        if (!Cache::has($gameKey)) {
            Cache::forget($pendingCacheKey);
            Cache::forget($activeCacheKey);
            return response()->json(['channel' => null]);
        }

        return response()->json(['channel' => $channel]);
    }
}