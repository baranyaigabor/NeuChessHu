<?php

namespace App\Events;

use Illuminate\Broadcasting\PrivateChannel;
use Illuminate\Contracts\Broadcasting\ShouldBroadcastNow;

class MatchStateUpdated implements ShouldBroadcastNow
{
    public function __construct(public string $channel, public array $matchState) {}

    public function broadcastOn()
    {
        return new PrivateChannel($this->channel);
    }

    public function broadcastAs()
    {
        return 'match-state';
    }

    public function broadcastWith()
    {
        return $this->matchState; 
    }
}