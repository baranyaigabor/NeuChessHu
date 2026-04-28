<?php

namespace App\Events;

use Illuminate\Broadcasting\PrivateChannel;
use Illuminate\Contracts\Broadcasting\ShouldBroadcastNow;

class ChannelAssignment implements ShouldBroadcastNow
{
    public function __construct(public string $channel, public string $playerID) {}

    public function broadcastOn()
    {
        return new PrivateChannel('user.' . $this->playerID);
    }

    public function broadcastAs()
    {
        return 'assign-channel';
    }

    public function broadcastWith()
    {
        return [
            'Channel' => $this->channel,
            'PlayerID' => $this->playerID
        ];
    }
}