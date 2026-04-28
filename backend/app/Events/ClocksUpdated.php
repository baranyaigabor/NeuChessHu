<?php

namespace App\Events;

use Illuminate\Broadcasting\PrivateChannel;
use Illuminate\Contracts\Broadcasting\ShouldBroadcastNow;

class ClocksUpdated implements ShouldBroadcastNow
{
    public function __construct(public string $channel, public array $clocks) {}

    public function broadcastOn()
    {
        return new PrivateChannel($this->channel);
    }

    public function broadcastAs()
    {
        return 'clocks';
    }

    public function broadcastWith()
    {
        return $this->clocks;
    }
}