<?php

use App\Http\Controllers\UserController;
use App\Http\Controllers\MatchesController;
use Illuminate\Support\Facades\Route;

Route::apiResource("users", UserController::class);
Route::get('/users/{identifier}', [UserController::class, 'show']);
Route::get('/user', [UserController::class, 'showCurrent'])
    ->middleware('auth:sanctum');

Route::apiResource("matches", MatchesController::class);