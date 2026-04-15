<?php

namespace App\Events;

use Illuminate\Broadcasting\PrivateChannel;
use Illuminate\Contracts\Broadcasting\ShouldBroadcastNow;

class MatchPointsUpdated implements ShouldBroadcastNow
{
    public function __construct(public string $channel, public array $matchPoints) {}

    public function broadcastOn()
    {
        return new PrivateChannel($this->channel);
    }

    public function broadcastAs()
    {
        return 'match-points';
    }

    public function broadcastWith()
    {
        return $this->matchPoints;
    }
}