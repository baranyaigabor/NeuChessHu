<?php

use App\Http\Controllers\Auth\AuthController;
use App\Http\Controllers\Auth\BroadcastAuthController;
use App\Http\Controllers\Auth\DesktopAuthController;
use App\Http\Controllers\ChatUpdateController;
use App\Http\Controllers\UserController;
use App\Http\Controllers\MatchesController;
use App\Http\Controllers\MatchReadyController;
use App\Http\Controllers\MatchUpdateController;
use App\Http\Controllers\PendingChannelController;
use App\Http\Controllers\QueueController;
use Illuminate\Support\Facades\Route;

Route::apiResource('users', UserController::class)
    ->middlewareFor('index', ['auth:sanctum', 'can:manage-users'])
    ->middlewareFor('update', ['auth:sanctum', 'can:update-user,user'])
    ->middlewareFor('destroy', ['auth:sanctum', 'can:destroy-user,user']);

Route::get('/user', [UserController::class, 'showCurrent'])
    ->middleware('auth:sanctum');

Route::apiResource('matches', MatchesController::class)
    ->middlewareFor('store', ['auth:sanctum', 'can:store-match']);

Route::post('/signin', [AuthController::class, 'webLogin']);
Route::post('/logout', [AuthController::class, 'webLogout'])
    ->middleware(['auth:sanctum', 'can:logout']);

Route::post('/desktop/logout', [DesktopAuthController::class, 'desktopLogout'])
    ->middleware(['auth:sanctum', 'can:logout']);

Route::post('/join/matchmakingqueue', [QueueController::class, 'join'])
    ->middleware('auth:sanctum');
Route::post('/leave/matchmakingqueue', [QueueController::class, 'leave'])
    ->middleware('auth:sanctum');

Route::post('/broadcasting/auth', [BroadcastAuthController::class, 'broadcastAuthenticate'])
    ->middleware('auth:sanctum');

Route::post('/match/ready', [MatchReadyController::class, 'ready'])
    ->middleware('auth:sanctum');
Route::get('/match/pendingchannel', [PendingChannelController::class, 'showPendingChannel'])
    ->middleware('auth:sanctum');

Route::post('match/state-update', [MatchUpdateController::class, 'update']);
Route::post('match/chat-update', [ChatUpdateController::class, 'update']);