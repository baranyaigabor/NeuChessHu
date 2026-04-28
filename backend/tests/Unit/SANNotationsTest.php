<?php

namespace Tests\Unit;

use ChessLogic\ChessBoard\ChessPieces\Definitions\Piece;
use ChessLogic\ChessBoard\ChessPieces\Definitions\Side;
use ChessLogic\ChessBoard\ChessPieces\Pieces\ChessPiece;
use ChessLogic\Notations\PieceNotations\SANNotations;
use Tests\TestCase;

class SANNotationsTest extends TestCase
{
    private function emptyBoard() : array
    {
        return array_fill(0, 8, array_fill(0, 8,
            ChessPiece::create(Piece::None, Side::None)
        ));
    }

    private function piece(Piece $name, Side $color) : ChessPiece
    {
        return ChessPiece::create($name, $color);
    }

    private function neverCanMove() : callable
    {
        return fn($matrix, $from, $to) => false;
    }

    private function notation(array $board, array $startingBoard, callable $canMove, array $from, 
        array $to, Piece $promoted = Piece::None, bool $captured = false, string $castled = '',
        bool $check = false, bool $checkmate = false) : string 
    {
        return SANNotations::notationDefiner($board, $startingBoard, $canMove,
            $from, $to, $promoted, $captured, $castled, $check, $checkmate);
    }

    public function testPawnSimpleMove() : void
    {
        $board = $this->emptyBoard();
        $board[4][4] = $this->piece(Piece::Pawn, Side::White);

        $result = $this->notation($board, $board, $this->neverCanMove(), [6, 4], [4, 4]);

        $this->assertSame('e5', $result);
    }

    public function testPawnCaptureIncludesFromColumn() : void
    {
        $board = $this->emptyBoard();
        $board[4][5] = $this->piece(Piece::Pawn, Side::White);

        $result = $this->notation($board, $board, $this->neverCanMove(), [5, 4], [4, 5], Piece::None, true);

        $this->assertSame('exf5', $result);
    }

    public function testKnightMoveNotation() : void
    {
        $board = $this->emptyBoard();
        $board[2][5] = $this->piece(Piece::Knight, Side::White);

        $result = $this->notation($board, $board, $this->neverCanMove(), [4, 4], [2, 5]);

        $this->assertSame('Nf3', $result);
    }

    public function testBishopMoveNotation() : void
    {
        $board = $this->emptyBoard();
        $board[4][4] = $this->piece(Piece::Bishop, Side::White);

        $result = $this->notation($board, $board, $this->neverCanMove(), [6, 2], [4, 4]);

        $this->assertSame('Be5', $result);
    }

    public function testRookMoveNotation() : void
    {
        $board = $this->emptyBoard();
        $board[0][4] = $this->piece(Piece::Rook, Side::White);

        $result = $this->notation($board, $board, $this->neverCanMove(), [0, 0], [0, 4]);

        $this->assertSame('Re1', $result);
    }

    public function testQueenMoveNotation() : void
    {
        $board = $this->emptyBoard();
        $board[3][3] = $this->piece(Piece::Queen, Side::White);

        $result = $this->notation($board, $board, $this->neverCanMove(), [7, 3], [3, 3]);

        $this->assertSame('Qd4', $result);
    }

    public function testKingMoveNotation() : void
    {
        $board = $this->emptyBoard();
        $board[4][4] = $this->piece(Piece::King, Side::White);

        $result = $this->notation($board, $board, $this->neverCanMove(), [4, 3], [4, 4]);

        $this->assertSame('Ke5', $result);
    }

    public function testRookCaptureNotation() : void
    {
        $board = $this->emptyBoard();
        $board[0][4] = $this->piece(Piece::Rook, Side::White);

        $result = $this->notation($board, $board, $this->neverCanMove(), [0, 0], [0, 4], Piece::None, true);

        $this->assertSame('Rxe1', $result);
    }
}