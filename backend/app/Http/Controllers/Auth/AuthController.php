<?php

namespace App\Http\Controllers\Auth;

use App\Http\Controllers\Controller;
use App\Http\Requests\AuthRequest;
use Illuminate\Http\Request;
use Illuminate\Support\Facades\Auth;

class AuthController extends Controller
{
    public function webLogin(AuthRequest $request)
    {
        $credentials = $request->validated();

        if (!Auth::attempt($credentials)) 
        {
            return response()->json(['message' => 'Invalid credentials'], 401);
        }

        /** @var \App\Models\User|\Laravel\Sanctum\HasApiTokens $user */
        $user = Auth::user();

        $user->update(['is_active' => true]);

        $token = $user->createToken('desktop-app', ['*'], now()->addHours(4))->plainTextToken;

        return response()->json([
            'token' => $token,
            'user' => $user->load(['whiteMatches', 'blackMatches']),
        ]);
    }

    public function webLogout(Request $request)
    {
        $user = $request->user();

        $user->currentAccessToken()?->delete();

        $user->update(['is_active' => false]);

        return response()->json(['message' => 'Logged out successfully']);
    }
}