<?php

namespace App\Events;

use Illuminate\Broadcasting\PrivateChannel;
use Illuminate\Contracts\Broadcasting\ShouldBroadcastNow;

class MatchStarted implements ShouldBroadcastNow
{
    public function __construct(public string $channel, public array $initializerDTO) { }

    public function broadcastOn(): PrivateChannel
    {
        return new PrivateChannel($this->channel);
    }

    public function broadcastAs(): string
    {
        return 'match-start';
    }

    public function broadcastWith(): array
    {
        return $this->initializerDTO;
    }
}