<?php

namespace App\Http\Controllers;

use App\Services\ReadyPlayersRegistryService;
use Illuminate\Http\Request;

class MatchReadyController extends Controller
{
    public function __construct(private ReadyPlayersRegistryService $readyPlayersRegistry) { }

    public function ready(Request $request)
    {
        $channel = $request->input('channel_name');
        
        if (!$channel) {
            return response()->json(['error' => 'Missing channel'], 422);
        }

        $userID = $request->user('sanctum')->id;

        $this->readyPlayersRegistry->markPlayerReady($channel, $userID);

        return response()->json(['status' => 'ok']);
    }
}