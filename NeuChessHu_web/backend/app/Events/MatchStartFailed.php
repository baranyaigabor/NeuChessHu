<?php

namespace App\Events;

use Illuminate\Broadcasting\PrivateChannel;
use Illuminate\Contracts\Broadcasting\ShouldBroadcastNow;

class MatchStartFailed implements ShouldBroadcastNow
{
    public function __construct(public string $channel) { }

    public function broadcastOn() : PrivateChannel
    {
        return new PrivateChannel(str_replace('private-', '', $this->channel));
    }

    public function broadcastAs() : string
    {
        return 'match-start-failed';
    }
}