<?php

namespace Tests\Unit;

use ChessLogic\Moving\Validators\Moves\PieceMoveRules\KnightMove;
use PHPUnit\Framework\TestCase;
use ChessLogic\MatchDatas\MatchDataStore;

class KnightMoveTest extends TestCase
{
    private KnightMove $knight;
    private MatchDataStore $matchDataStore;

    protected function setUp(): void
    {
        $this->knight = new KnightMove();
        $this->matchDataStore = new MatchDataStore();
    }

    public function testTwoRowsOneColumnMovesAreLegal(): void
    {
        $this->assertTrue($this->knight->isMoveLegal([4, 4], [2, 5], $this->matchDataStore));
        $this->assertTrue($this->knight->isMoveLegal([4, 4], [2, 3], $this->matchDataStore));
        $this->assertTrue($this->knight->isMoveLegal([4, 4], [6, 5], $this->matchDataStore));
        $this->assertTrue($this->knight->isMoveLegal([4, 4], [6, 3], $this->matchDataStore));
    }

    public function testOneRowTwoColumnMovesAreLegal(): void
    {
        $this->assertTrue($this->knight->isMoveLegal([4, 4], [3, 6], $this->matchDataStore));
        $this->assertTrue($this->knight->isMoveLegal([4, 4], [5, 6], $this->matchDataStore));
        $this->assertTrue($this->knight->isMoveLegal([4, 4], [3, 2], $this->matchDataStore));
        $this->assertTrue($this->knight->isMoveLegal([4, 4], [5, 2], $this->matchDataStore));
    }

    public function testDiagonalMoveIsIllegal(): void
    {
        $this->assertFalse($this->knight->isMoveLegal([4, 4], [5, 5], $this->matchDataStore));
        $this->assertFalse($this->knight->isMoveLegal([4, 4], [6, 6], $this->matchDataStore));
    }

    public function testStraightMovesAreIllegal(): void
    {
        $this->assertFalse($this->knight->isMoveLegal([4, 4], [4, 6], $this->matchDataStore));
        $this->assertFalse($this->knight->isMoveLegal([4, 4], [6, 4], $this->matchDataStore));
    }

    public function testTwoByTwoMoveIsIllegal(): void
    {
        $this->assertFalse($this->knight->isMoveLegal([4, 4], [2, 2], $this->matchDataStore));
        $this->assertFalse($this->knight->isMoveLegal([4, 4], [6, 6], $this->matchDataStore));
    }

    public function testThreeOneMoveIsIllegal(): void
    {
        $this->assertFalse($this->knight->isMoveLegal([4, 4], [7, 5], $this->matchDataStore));
    }
}