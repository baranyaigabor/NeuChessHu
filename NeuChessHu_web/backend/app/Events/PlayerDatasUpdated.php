<?php

namespace App\Events;

use Illuminate\Broadcasting\PrivateChannel;
use Illuminate\Contracts\Broadcasting\ShouldBroadcastNow;

class PlayerDatasUpdated implements ShouldBroadcastNow
{
    public function __construct(public string $channel, public array $playerDatas) {}

    public function broadcastOn()
    {
        return new PrivateChannel($this->channel);
    }

    public function broadcastAs()
    {
        return 'player-datas';
    }

    public function broadcastWith()
    {
        return $this->playerDatas;
    }
}