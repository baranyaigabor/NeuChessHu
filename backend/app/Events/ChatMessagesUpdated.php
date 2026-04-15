<?php

namespace App\Events;

use Illuminate\Broadcasting\PrivateChannel;
use Illuminate\Contracts\Broadcasting\ShouldBroadcastNow;

class ChatMessagesUpdated implements ShouldBroadcastNow
{
    public function __construct(public string $channel, public array $newMessage) {}

    public function broadcastOn()
    {
        return new PrivateChannel($this->channel);
    }

    public function broadcastAs()
    {
        return 'chat-messages';
    }

    public function broadcastWith()
    {
        return [
            'Status' => 'Success',
            'NewMessage' => $this->newMessage,
        ];
    }
}