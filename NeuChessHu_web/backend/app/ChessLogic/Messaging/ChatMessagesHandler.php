<?php

namespace ChessLogic\Messaging;

use App\Rules\ChatMessageRule;
use ChessLogic\MatchDatas\DataStores\DomainModels\ChatMessageRow;
use ChessLogic\MatchDatas\MatchDataStore;
use Illuminate\Support\Facades\Validator;

class ChatMessagesHandler
{
    public function __construct(private MatchDataStore $matchDataStore) {}

    public function handleMessage(int $userID, string $message)
    { 
        $validatedMessage = Validator::make(
            ['message' => $message],
            ['message' => [new ChatMessageRule()]]
        );

        if ($validatedMessage->passes()) 
        {
            $newMessage = new ChatMessageRow($userID, $message);
            array_push($this->matchDataStore->ChatMessages->ChatMessages, $newMessage);

            return [
                'Status' => 'Success',
                'NewMessage' => [
                    'UserID' => $userID,
                    'Message' => $message,
                ]
            ];
        }

        return ['Status' => 'Violation'];
    }
}