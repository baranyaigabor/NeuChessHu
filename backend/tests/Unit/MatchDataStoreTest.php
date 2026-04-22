<?php

namespace Tests\Unit;

use ChessLogic\MatchDatas\MatchDataStore;
use DateTime;
use Tests\TestCase;

class MatchDataStoreTest extends TestCase
{
    public function testCreateDataStoreSetsMatchID() : void
    {
        $store = MatchDataStore::createDataStore('1', '2', '3 | 2', 'matchid-1');

        $this->assertSame('3|2-matchid-1', $store->MatchID);
    }

    public function testCreateDataStorePlayedAtIsRecent() : void
    {
        $before = new DateTime();
        $store = MatchDataStore::createDataStore('1', '2', '5|0', 'ch');
        $after = new DateTime();

        $this->assertGreaterThanOrEqual($before->getTimestamp(), $store->PlayedAt->getTimestamp());
        $this->assertLessThanOrEqual($after->getTimestamp(), $store->PlayedAt->getTimestamp());
    }

    public function testCreateDataStoreInitializesEmptyChatMessages() : void
    {
        $store = MatchDataStore::createDataStore('1', '2', '3|2', 'ch');

        $this->assertEmpty($store->ChatMessages->ChatMessages);
    }

    public function testCreateDataStoreSetsMatchDurationOnMatchState() : void
    {
        $store = MatchDataStore::createDataStore('1', '2', '5 | 3', 'ch');

        $this->assertSame('5|3', $store->MatchState->MatchDuration);
    }

    public function testCreateDataStoreCreatesTwoPlayers() : void
    {
        $store = MatchDataStore::createDataStore('42', '99', '3|2', 'ch');

        $this->assertCount(2, $store->PlayerDatas);

        $ids = array_map(fn($p) => $p->ID, $store->PlayerDatas);

        $this->assertContains('42', $ids);
        $this->assertContains('99', $ids);
    }

    public function testCreateDataStoreClocksMatchDuration() : void
    {
        $store = MatchDataStore::createDataStore('1', '2', '3|2', 'ch');

        $expectedBaseMs = 3 * 60 * 1000;
        $expectedIncrementMs = 2 * 1000;

        $this->assertSame($expectedBaseMs, $store->Clocks->WhiteRemainingMs);
        $this->assertSame($expectedBaseMs, $store->Clocks->BlackRemainingMs);
        $this->assertSame($expectedIncrementMs, $store->Clocks->IncrementMs);
    }
}