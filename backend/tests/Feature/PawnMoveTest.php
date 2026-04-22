<?php

namespace Tests\Unit;

use ChessLogic\ChessBoard\ChessPieces\Definitions\Piece;
use ChessLogic\ChessBoard\ChessPieces\Definitions\Side;
use ChessLogic\ChessBoard\ChessPieces\Pieces\ChessPiece;
use ChessLogic\MatchDatas\DataStores\MatchState;
use ChessLogic\MatchDatas\MatchDataStore;
use ChessLogic\Moving\Validators\Moves\PieceMoveRules\PawnMove;
use PHPUnit\Framework\TestCase;

class PawnMoveTest extends TestCase
{
    private PawnMove $pawnMove;

    protected function setUp(): void
    {
        $this->pawnMove = new PawnMove();
    }

    private function buildState(array $from, Side $color, ?array $extraPiece = null,
        ?array $enPassantTarget = null): MatchDataStore
    {

        $matrix = [];
        for ($r = 0; $r < 8; $r++) {
            for ($c = 0; $c < 8; $c++) {
                $matrix[$r][$c] = ChessPiece::create(Piece::None, Side::None);
            }
        }


        $matrix[$from[0]][$from[1]] = ChessPiece::create(Piece::Pawn, $color);


        if ($extraPiece !== null) {
            [$er, $ec, $ePiece, $eColor] = $extraPiece;
            $matrix[$er][$ec] = ChessPiece::create($ePiece, $eColor);
        }

        $matchState = new MatchState();
        $matchState->PieceMatrix = $matrix;
        $matchState->EnPassantTarget = $enPassantTarget;

        $store = new MatchDataStore();
        $store->MatchState = $matchState;

        return $store;
    }

    public function testBlackPawnSingleStepForwardIsLegal(): void
    {
        $store = $this->buildState([3, 3], Side::Black);
        $this->assertTrue($this->pawnMove->isMoveLegal([3, 3], [4, 3], $store));
    }

    public function testBlackPawnDoubleStepFromStartingRowIsLegal(): void
    {
        $store = $this->buildState([1, 3], Side::Black);
        $this->assertTrue($this->pawnMove->isMoveLegal([1, 3], [3, 3], $store));
    }

    public function testBlackPawnBackwardMoveIsIllegal(): void
    {
        $store = $this->buildState([4, 3], Side::Black);
        $this->assertFalse($this->pawnMove->isMoveLegal([4, 3], [3, 3], $store));
    }

    public function testBlackPawnDiagonalCaptureIsLegal(): void
    {
        $store = $this->buildState(
            [4, 3], Side::Black,
            [5, 4, Piece::Pawn, Side::White]
        );
        $this->assertTrue($this->pawnMove->isMoveLegal([4, 3], [5, 4], $store));
    }
}
