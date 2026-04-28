<?php

namespace ChessLogic\Messaging;

use ChessLogic\MatchDatas\MatchDataStore;

class ChatMessagesHandlerFactory
{
    public function create(MatchDataStore $matchDataStore) : ChatMessagesHandler
    {
        return new ChatMessagesHandler($matchDataStore);
    }
}