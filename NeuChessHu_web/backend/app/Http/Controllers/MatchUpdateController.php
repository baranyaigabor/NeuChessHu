<?php

namespace App\Http\Controllers;

use App\Events\ClocksUpdated;
use App\Events\MatchPointsUpdated;
use App\Events\MatchStateUpdated;
use App\Events\PlayerDatasUpdated;
use Illuminate\Http\Request;

class MatchUpdateController extends Controller
{
    public function update(Request $request)
    {
        $channel = $request->input('channel');
        $playerDatas = $request->input('player_datas');
        $matchState = $request->input('match_state');
        $matchPoints = $request->input('match_points');
        $clocks = $request->input('clocks');

        if (!$matchState || !$playerDatas || !$matchPoints)
        {
            return response()->json(['status' => 'skipped']);
        }
    
        foreach ($playerDatas as $side => $playerData)
        {
            broadcast(new PlayerDatasUpdated($channel, array_merge($playerData, ['side' => $side])));
        }
    
        broadcast(new MatchStateUpdated($channel, $matchState));
        broadcast(new MatchPointsUpdated($channel, $matchPoints));
        broadcast(new ClocksUpdated($channel, $clocks));
    
        return response()->json(['status' => 'ok']);
    }
}