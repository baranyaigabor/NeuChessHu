<?php

use Illuminate\Support\Facades\Broadcast;

Broadcast::channel('waiting-queue', function ($user) {
    return true;
});

Broadcast::channel('user.{id}', function ($user, $id) {
    return (int) $user->id === (int) $id;
});


Broadcast::channel('{player1}-{player2}-{timestamp}', function ($user, $player1, $player2, $timestamp) {
    return (int)$user->id === (int)$player1 || (int)$user->id === (int)$player2;
});