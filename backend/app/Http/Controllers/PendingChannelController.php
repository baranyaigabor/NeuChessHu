<?php

namespace App\Http\Controllers;

use Illuminate\Http\Request;
use Illuminate\Support\Facades\Cache;

class PendingChannelController extends Controller
{
    public function showPendingChannel(Request $request)
    {
        $cacheKey = "pending_channel:{$request->user('sanctum')->id}";
        return response()->json(['channel' => Cache::get($cacheKey)]);
    }
}