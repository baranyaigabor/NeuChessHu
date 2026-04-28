<?php
namespace App\Http\Controllers\Auth;

use App\Http\Controllers\Controller;
use Illuminate\Http\Request;
use Illuminate\Support\Facades\Auth;
use Illuminate\Support\Facades\Broadcast;

class BroadcastAuthController extends Controller
{
    public function broadcastAuthenticate(Request $request)
    {
        $user = $request->user('sanctum');

        if (!$user) {
            return response()->json(['message' => 'Unauthorized'], 401);
        }

        Auth::setUser($user);

        $result = Broadcast::auth($request);

        return response()->json($result);
    }
}