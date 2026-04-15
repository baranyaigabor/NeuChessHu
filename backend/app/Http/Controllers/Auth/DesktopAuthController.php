<?php

namespace App\Http\Controllers\Auth;

use App\Http\Controllers\Controller;
use App\Models\User;
use Illuminate\Http\Request;
use Illuminate\Support\Facades\Auth;
use Illuminate\Support\Facades\Session;
use Laravel\Sanctum\PersonalAccessToken;

class DesktopAuthController extends Controller
{
    public function authorizeApp(Request $request)
    {
        $redirectUri = $request->query('redirect_uri');
        $this->validateRedirectUri($redirectUri);

        Session::put('desktop_oauth_redirect', $redirectUri);

        return Auth::check() ? $this->finishAuthorization()
            : redirect('http://frontend.vm2.test/signin?redirect_uri=' . urlencode($redirectUri));
    }

    private function validateRedirectUri(string $redirectUri): void
    {
        if (!$redirectUri || !str_starts_with($redirectUri, 'neuchesshu://')) {
            abort(400, 'Invalid redirect_uri');
        }
    }

    public function oauthClose(Request $request)
    {
        $token = $request->query('token');

        $tokenModel = PersonalAccessToken::findToken($token);
        $user = $tokenModel?->tokenable;

        if (!$user) {
            abort(401, 'Invalid token');
        }

        $payload = $this->buildPayload($token, $user->id);

        return view('oauth-close', compact('payload'));
    }

    public function finishAuthorizationa()
    {
        $redirectUri = Session::get('desktop_oauth_redirect');

        if (!$redirectUri) {
            return redirect('http://frontend.vm2.test/signin');
        }

        Session::forget('desktop_oauth_redirect');

        $user = User::find(Auth::id());

        $user->update(['is_active' => true]);

        $token = $user->createToken('desktop-app')->plainTextToken;

        $payload = $this->buildPayload($token, $user->id);

        return redirect("neuchesshu://auth/callback?data={$payload}");
    }

    public function finishAuthorization()
    {
        $redirectUri = Session::get('desktop_oauth_redirect');

        if (!$redirectUri) 
        {
            return redirect('http://frontend.vm2.test/signin');
        }

        Session::forget('desktop_oauth_redirect');

        $user = User::find(Auth::id());

        $user->update(['is_active' => true]);

        $token = $user->createToken('desktop-app')->plainTextToken;

        $payload = $this->buildPayload($token, $user->id);

        Auth::logout();
        Session::invalidate();
        Session::regenerateToken();
        Session::save();

        return redirect("neuchesshu://auth/callback?data={$payload}");
    }

    private function buildPayload(string $token, int $userId): string
    {
        return base64_encode(json_encode([
            'token' => $token,
            'user_id' => $userId,
        ]));
    }

    public function desktopLogout(Request $request)
    {
        $user = $request->user();

        $user->update(['is_active' => false]);

        $user->currentAccessToken()?->delete();

        return response()->json(['message' => 'Logged out successfully']);
    }
}