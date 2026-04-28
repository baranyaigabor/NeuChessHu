<?php

use App\Http\Controllers\Auth\DesktopAuthController;
use Illuminate\Support\Facades\Route;

Route::get('/desktop/authorize', [DesktopAuthController::class, 'authorizeApp']);
Route::get('/oauth/close', [DesktopAuthController::class, 'oauthClose']);