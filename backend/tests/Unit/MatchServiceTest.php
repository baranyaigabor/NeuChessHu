<?php

namespace Tests\Unit;

use App\Services\MatchService;
use Illuminate\Support\Facades\Cache;
use Tests\TestCase;

class MatchServiceTest extends TestCase
{
    public function testGetMatchFromCacheReturnsExistingData() : void
    {
        $channel = 'private-1-2-20260101120000';
        $data = ['match_id' => 1, 'white_id' => 1, 'black_id' => 2];

        Cache::put('game:1-2-20260101120000', $data, 60);

        $result = MatchService::getMatchFromCache($channel);
        $this->assertEquals($data, $result);
    }

    public function testGetMatchFromCacheReturnsNullWhenMissing() : void
    {
        $channel = 'private-nonexistent-channel';

        $result = MatchService::getMatchFromCache($channel);

        $this->assertNull($result);
    }

    public function testUpdateMatchInCacheStoresUpdatedData() : void
    {
        $channel = 'private-3-4-20260101130000';
        $original = ['match_id' => 5];
        $updated  = ['match_id' => 5, 'status' => 'in_progress'];

        Cache::put('game:3-4-20260101130000', $original, 60);
        MatchService::updateMatchInCache($channel, $updated);

        $result = Cache::get('game:3-4-20260101130000');
        $this->assertEquals($updated, $result);
    }

    public function testRemoveMatchFromCacheDeletesEntry() : void
    {
        $channel = 'private-5-6-20260101140000';
        Cache::put('game:5-6-20260101140000', ['dummy' => true], 60);

        $result = MatchService::removeMatchFromCache($channel);

        $this->assertTrue($result);
        $this->assertNull(Cache::get('game:5-6-20260101140000'));
    }

    public function testRemoveMatchFromCacheOnNonexistentKeyDoesNotThrow() : void
    {
        $result = MatchService::removeMatchFromCache('private-no-such-channel');

        $this->assertIsBool($result);
    }
}