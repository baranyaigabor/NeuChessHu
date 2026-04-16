<?php

use App\Http\Controllers\Auth\AuthController;
use App\Http\Controllers\Auth\BroadcastAuthController;
use App\Http\Controllers\Auth\DesktopAuthController;
use App\Http\Controllers\ChatUpdateController;
use App\Http\Controllers\UserController;
use App\Http\Controllers\MatchesController;
use App\Http\Controllers\MatchUpdateController;
use App\Http\Controllers\QueueController;
use Illuminate\Support\Facades\Route;

Route::apiResource("users", UserController::class);
Route::get('/users/{identifier}', [UserController::class, 'show']);
Route::get('/user', [UserController::class, 'showCurrent'])
    ->middleware('auth:sanctum');

Route::apiResource("matches", MatchesController::class);

Route::post('/signin', [AuthController::class, 'webLogin']);
Route::post('/logout', [AuthController::class, 'webLogout']);

Route::post('/desktop/logout', [DesktopAuthController::class, 'desktopLogout'])
    ->middleware('auth:sanctum');

Route::post('/join/matchmakingqueue', [QueueController::class, 'join'])
    ->middleware('auth:sanctum');
Route::post('/leave/matchmakingqueue', [QueueController::class, 'leave'])
    ->middleware('auth:sanctum');

Route::post('/broadcasting/auth', [BroadcastAuthController::class, 'broadcastAuthenticate'])
    ->middleware('auth:sanctum');

Route::post('match/state-update', [MatchUpdateController::class, 'update']);
Route::post('match/chat-update', [ChatUpdateController::class, 'update']);