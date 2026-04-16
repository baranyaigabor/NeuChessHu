<?php

use App\Http\Controllers\Auth\AuthController;
use App\Http\Controllers\Auth\DesktopAuthController;
use App\Http\Controllers\UserController;
use App\Http\Controllers\MatchesController;
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