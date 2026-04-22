<?php

namespace Tests\Unit;

use ChessLogic\Moving\Validators\Moves\PieceMoveRules\RookMove;
use PHPUnit\Framework\TestCase;
use ChessLogic\MatchDatas\MatchDataStore;

class RookMoveTest extends TestCase
{
    private RookMove $rook;
    private MatchDataStore $matchDataStore;

    protected function setUp(): void
    {
        $this->rook = new RookMove();
        $this->matchDataStore = new MatchDataStore();
    }

    public function testHorizontalMoveIsLegal(): void
    {
        $this->assertTrue($this->rook->isMoveLegal([4, 0], [4, 7], $this->matchDataStore));
        $this->assertTrue($this->rook->isMoveLegal([0, 3], [0, 0], $this->matchDataStore));
    }

    public function testVerticalMoveIsLegal(): void
    {
        $this->assertTrue($this->rook->isMoveLegal([0, 4], [7, 4], $this->matchDataStore));
        $this->assertTrue($this->rook->isMoveLegal([7, 3], [0, 3], $this->matchDataStore));
    }

    public function testDiagonalMoveIsIllegal(): void
    {
        $this->assertFalse($this->rook->isMoveLegal([4, 4], [5, 5], $this->matchDataStore));
        $this->assertFalse($this->rook->isMoveLegal([0, 0], [7, 7], $this->matchDataStore));
    }

    public function testKnightShapedMoveIsIllegal(): void
    {
        $this->assertFalse($this->rook->isMoveLegal([4, 4], [6, 5], $this->matchDataStore));
    }
}