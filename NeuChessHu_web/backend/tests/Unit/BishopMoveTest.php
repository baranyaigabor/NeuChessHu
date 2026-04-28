<?php

namespace Tests\Unit;

use ChessLogic\Moving\Validators\Moves\PieceMoveRules\BishopMove;
use PHPUnit\Framework\TestCase;
use ChessLogic\MatchDatas\MatchDataStore;

class BishopMoveTest extends TestCase
{
    private BishopMove $bishop;
    private MatchDataStore $matchDataStore;

    protected function setUp(): void
    {
        $this->bishop = new BishopMove();
        $this->matchDataStore = new MatchDataStore();
    }

    public function testDiagonalMoveOneStepIsLegal(): void
    {
        $this->assertTrue($this->bishop->isMoveLegal([4, 4], [5, 5], $this->matchDataStore));
        $this->assertTrue($this->bishop->isMoveLegal([4, 4], [3, 3], $this->matchDataStore));
        $this->assertTrue($this->bishop->isMoveLegal([4, 4], [5, 3], $this->matchDataStore));
        $this->assertTrue($this->bishop->isMoveLegal([4, 4], [3, 5], $this->matchDataStore));
    }

    public function testDiagonalMoveMultipleStepsIsLegal(): void
    {
        $this->assertTrue($this->bishop->isMoveLegal([0, 0], [7, 7], $this->matchDataStore));
        $this->assertTrue($this->bishop->isMoveLegal([7, 0], [0, 7], $this->matchDataStore));
        $this->assertTrue($this->bishop->isMoveLegal([3, 3], [6, 0], $this->matchDataStore));
    }

    public function testHorizontalMoveIsIllegal(): void
    {
        $this->assertFalse($this->bishop->isMoveLegal([4, 4], [4, 6], $this->matchDataStore));
        $this->assertFalse($this->bishop->isMoveLegal([4, 4], [4, 0], $this->matchDataStore));
    }

    public function testVerticalMoveIsIllegal(): void
    {
        $this->assertFalse($this->bishop->isMoveLegal([4, 4], [6, 4], $this->matchDataStore));
        $this->assertFalse($this->bishop->isMoveLegal([4, 4], [0, 4], $this->matchDataStore));
    }

    public function testKnightShapedMoveIsIllegal(): void
    {
        $this->assertFalse($this->bishop->isMoveLegal([4, 4], [6, 5], $this->matchDataStore));
        $this->assertFalse($this->bishop->isMoveLegal([4, 4], [5, 6], $this->matchDataStore));
    }
}
