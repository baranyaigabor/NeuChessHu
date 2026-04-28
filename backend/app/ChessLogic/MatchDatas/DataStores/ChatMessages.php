<?php

namespace ChessLogic\MatchDatas\DataStores;

use ChessLogic\MatchDatas\DataStores\DomainModels\ChatMessageRow;

use JsonSerializable;

class ChatMessages implements JsonSerializable
{
    /** @var ChatMessageRow[] */
    public array $ChatMessages = [];

    public function jsonSerialize(): array
    {
        return [
            'ChatMessageList' => array_map(fn($msg) => [
                'UserID'  => $msg->UserID,
                'Message' => $msg->Message,
            ], $this->ChatMessages)
        ];
    }

    public static function fromArray(array $data) : ChatMessages
    {
        $instance = new self();

        $messages = $data['chat_messages'] ?? [];

        if(!empty($messages))
        {
            foreach ($messages as $msg) 
            {
                array_push($instance->ChatMessages, ChatMessageRow::fromArray($msg));
            }
        }

        return $instance;
    }
}