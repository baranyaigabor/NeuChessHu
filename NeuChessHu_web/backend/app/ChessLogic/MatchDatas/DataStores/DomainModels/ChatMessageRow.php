<?php

namespace ChessLogic\MatchDatas\DataStores\DomainModels;

class ChatMessageRow
{
    public function __construct(
        public int $UserID,
        public string $Message
    ) {}

    public static function fromArray(array $data) : ChatMessageRow
    {
        return new ChatMessageRow(
            $data['userID'],
            $data['message']
        );
    }
}