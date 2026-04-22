<?php

namespace Tests\Unit;

use ChessLogic\MatchDatas\DataStores\ChatMessages;
use ChessLogic\MatchDatas\MatchDataStore;
use ChessLogic\Messaging\ChatMessagesHandler;
use Tests\TestCase;

class ChatMessagesHandlerTest extends TestCase
{
    private MatchDataStore $store;
    private ChatMessagesHandler $handler;

    protected function setUp(): void
    {
        parent::setUp();

        $this->store = new MatchDataStore();
        $this->store->ChatMessages = new ChatMessages();

        $this->handler = new ChatMessagesHandler($this->store);
    }

    public function testValidMessageReturnsSuccessStatus(): void
    {
        $result = $this->handler->handleMessage(1, 'Hello!');

        $this->assertSame('Success', $result['Status']);
    }

    public function testValidMessageReturnsNewMessageData(): void
    {
        $result = $this->handler->handleMessage(42, 'Nice move');

        $this->assertSame(42, $result['NewMessage']['UserID']);
        $this->assertSame('Nice move',  $result['NewMessage']['Message']);
    }

    public function testValidMessageIsAppendedToChatMessages(): void
    {
        $this->handler->handleMessage(1, 'First');
        $this->handler->handleMessage(2, 'Second');

        $messages = $this->store->ChatMessages->ChatMessages;

        $this->assertCount(2, $messages);
        $this->assertSame('First',  $messages[0]->Message);
        $this->assertSame('Second', $messages[1]->Message);
    }

    public function testValidMessageStoresCorrectUserId(): void
    {
        $this->handler->handleMessage(99, 'Szia');

        $this->assertSame(99, $this->store->ChatMessages->ChatMessages[0]->UserID);
    }

    public function testSingleCharacterMessageIsValid(): void
    {
        $result = $this->handler->handleMessage(1, 'a');

        $this->assertSame('Success', $result['Status']);
    }

    public function testMessageExceeding100CharsReturnsViolation(): void
    {
        $message = str_repeat('a', 101);

        $result = $this->handler->handleMessage(1, $message);

        $this->assertSame('Violation', $result['Status']);
    }

    public function testBannedEnglishWordReturnsViolation(): void
    {
        $result = $this->handler->handleMessage(1, 'what the fuck is this');

        $this->assertSame('Violation', $result['Status']);
    }

    public function testBannedHungarianWordReturnsViolation(): void
    {
        $result = $this->handler->handleMessage(1, 'k*rva jó lépés');

        $this->assertSame('Violation', $result['Status']);
    }

    public function testMultipleValidMessagesAccumulate(): void
    {
        for ($i = 1; $i <= 5; $i++) 
        {
            $this->handler->handleMessage($i, "Message $i");
        }

        $this->assertCount(5, $this->store->ChatMessages->ChatMessages);
    }
}