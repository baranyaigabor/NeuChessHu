<?php

namespace Tests\Unit;

use ChessLogic\Moving\Validators\Moves\PieceMoveRules\QueenMove;
use PHPUnit\Framework\TestCase;
use ChessLogic\MatchDatas\MatchDataStore;


class QueenMoveTest extends TestCase
{
    private QueenMove $queen;
    private MatchDataStore $matchDataStore;

    protected function setUp(): void
    {
        $this->queen = new QueenMove();
        $this->matchDataStore = new MatchDataStore();
    }

    public function testHorizontalMoveIsLegal(): void
    {
        $this->assertTrue($this->queen->isMoveLegal([4, 0], [4, 7], $this->matchDataStore));
    }

    public function testVerticalMoveIsLegal(): void
    {
        $this->assertTrue($this->queen->isMoveLegal([0, 4], [7, 4], $this->matchDataStore));
    }

    public function testDiagonalMoveIsLegal(): void
    {
        $this->assertTrue($this->queen->isMoveLegal([0, 0], [7, 7], $this->matchDataStore));
        $this->assertTrue($this->queen->isMoveLegal([4, 4], [1, 7], $this->matchDataStore));
    }

    public function testKnightShapedMoveIsIllegal(): void
    {
        $this->assertFalse($this->queen->isMoveLegal([4, 4], [6, 5], $this->matchDataStore));
        $this->assertFalse($this->queen->isMoveLegal([4, 4], [5, 6], $this->matchDataStore));
    }

    public function testRandomDirectionIsIllegal(): void
    {
        $this->assertFalse($this->queen->isMoveLegal([4, 4], [6, 7], $this->matchDataStore));
    }
}