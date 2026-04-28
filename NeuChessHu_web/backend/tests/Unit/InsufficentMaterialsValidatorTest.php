<?php

namespace Tests\Unit;

use ChessLogic\ChessBoard\ChessPieces\Definitions\Piece;
use ChessLogic\ChessBoard\ChessPieces\Definitions\Side;
use ChessLogic\ChessBoard\ChessPieces\Pieces\ChessPiece;
use ChessLogic\Moving\Validators\Draws\InsufficentMaterials\InsufficentMaterialsValidator;
use PHPUnit\Framework\TestCase;

class InsufficentMaterialsValidatorTest extends TestCase
{
    private function emptyMatrix() : array
    {
        $matrix = [];
        for ($r = 0; $r < 8; $r++) {
            for ($c = 0; $c < 8; $c++) {
                $matrix[$r][$c] = ChessPiece::create(Piece::None, Side::None);
            }
        }
        return $matrix;
    }

    private function place(array &$matrix, int $r, int $c, Piece $p, Side $s) : void
    {
        $matrix[$r][$c] = ChessPiece::create($p, $s);
    }

    public function testKingsOnlyIsDraw() : void
    {
        $matrix = $this->emptyMatrix();

        $this->assertTrue(
            InsufficentMaterialsValidator::isInsufficentMaterialsOccured($matrix)
        );
    }

    public function testOneKnightIsDraw() : void
    {
        $matrix = $this->emptyMatrix();
        $this->place($matrix, 4, 4, Piece::Knight, Side::White);

        $this->assertTrue(
            InsufficentMaterialsValidator::isInsufficentMaterialsOccured($matrix)
        );
    }

    public function testOneBishopIsDraw() : void
    {
        $matrix = $this->emptyMatrix();
        $this->place($matrix, 4, 4, Piece::Bishop, Side::White);

        $this->assertTrue(
            InsufficentMaterialsValidator::isInsufficentMaterialsOccured($matrix)
        );
    }

    public function testTwoBishopsOnSameColoredSquaresIsDraw(): void
    {
        $matrix = $this->emptyMatrix();

        $this->place($matrix, 4, 4, Piece::Bishop, Side::White);

        $this->place($matrix, 6, 6, Piece::Bishop, Side::Black);
        $this->assertTrue(
            InsufficentMaterialsValidator::isInsufficentMaterialsOccured($matrix)
        );
    }

    public function testTwoBishopsOnDifferentColoredSquaresIsNotDraw() : void
    {
        $matrix = $this->emptyMatrix();

        $this->place($matrix, 4, 4, Piece::Bishop, Side::White);
        $this->place($matrix, 4, 5, Piece::Bishop, Side::Black);

        $this->assertFalse(
            InsufficentMaterialsValidator::isInsufficentMaterialsOccured($matrix)
        );
    }

    public function testPawnOnBoardIsNotDraw() : void
    {
        $matrix = $this->emptyMatrix();
        $this->place($matrix, 6, 0, Piece::Pawn, Side::White);

        $this->assertFalse(
            InsufficentMaterialsValidator::isInsufficentMaterialsOccured($matrix)
        );
    }

    public function testRookOnBoardIsNotDraw() : void
    {
        $matrix = $this->emptyMatrix();
        $this->place($matrix, 0, 0, Piece::Rook, Side::Black); 

        $this->assertFalse(
            InsufficentMaterialsValidator::isInsufficentMaterialsOccured($matrix)
        );
    }

    public function testQueenOnBoardIsNotDraw() : void
    {
        $matrix = $this->emptyMatrix();
        $this->place($matrix, 0, 3, Piece::Queen, Side::White);

        $this->assertFalse(
            InsufficentMaterialsValidator::isInsufficentMaterialsOccured($matrix)
        );
    }

    public function testTwoBishopsSameSideSameSquareColorIsNotDraw() : void
    {
        $matrix = $this->emptyMatrix();

        $this->place($matrix, 4, 4, Piece::Bishop, Side::White);
        $this->place($matrix, 6, 6, Piece::Bishop, Side::White);

        $this->assertFalse(
            InsufficentMaterialsValidator::isInsufficentMaterialsOccured($matrix)
        );
    }
}