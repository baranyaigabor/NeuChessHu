<?php
namespace App\Http\Controllers\Auth;

use App\Http\Controllers\Controller;
use App\Http\Requests\AuthRequest;
use Illuminate\Http\Request;
use Illuminate\Support\Facades\Auth;
use Laravel\Sanctum\PersonalAccessToken;

class AuthController extends Controller
{
    public function webLogin(AuthRequest $request)
    {
        $credentials = $request->validated();

        if (!Auth::attempt($credentials)) {
            return response()->json(['message' => 'Invalid credentials'], 401);
        }

        /** @var \App\Models\User|\Laravel\Sanctum\HasApiTokens $user */
        $user = Auth::user();
        
        $user->update(['is_active' => true]);

        $tokenName = $request->has('redirect_uri') ? 'desktop' : 'api';
        $token = $user->createToken($tokenName)->plainTextToken;

        return response()->json([
            'token' => $token,
            'user' => $user
        ]);
    }

    public function webLogout(Request $request)
    {
        $user = $request->user();

        if (!$user) 
        {
            $token = $request->query('token');
            $tokenModel = PersonalAccessToken::findToken($token);
            $user = $tokenModel?->tokenable;
            $tokenModel?->delete();
        }

        else 
        {
            $request->user()->currentAccessToken()->delete();
        }
                
        $user?->update(['is_active' => false]);
        return response()->json(['message' => 'Logged out successfully']);
    }
}