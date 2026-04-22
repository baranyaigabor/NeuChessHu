<?php

namespace Tests\Unit;

use ChessLogic\ChessBoard\ChessBoardFactory;
use ChessLogic\ChessBoard\ChessPieces\Definitions\Piece;
use ChessLogic\ChessBoard\ChessPieces\Definitions\Side;
use Tests\TestCase;

class ChessBoardFactoryTest extends TestCase
{
    private array $board;

    protected function setUp() : void
    {
        parent::setUp();
        $this->board = ChessBoardFactory::boardFiller();
    }

    public function testBoardHas8Rows() : void
    {
        $this->assertCount(8, $this->board);
    }

    public function testBoardHas8ColumnsPerRow() : void
    {
        foreach ($this->board as $row)
        {
            $this->assertCount(8, $row);
        }
    }

    public function testBoardHasNoNullCells() : void
    {
        foreach ($this->board as $row) {
            foreach ($row as $cell) {
                $this->assertNotNull($cell, 'A táblán null cella található.');
            }
        }
    }

    public function testBlackBackRankOrder() : void
    {
        $expectedOrder = [
            Piece::Rook,
            Piece::Knight,
            Piece::Bishop,
            Piece::Queen,
            Piece::King,
            Piece::Bishop,
            Piece::Knight,
            Piece::Rook,
        ];

        for ($c = 0; $c < 8; $c++) {
            $piece = $this->board[0][$c];
            $this->assertSame($expectedOrder[$c], $piece->Name, "Helytelen bábu a fekete hátsó sorban ($c. oszlop).");
            $this->assertSame(Side::Black, $piece->Color);
        }
    }

    public function testBlackPawnsOnRow1() : void
    {
        for ($c = 0; $c < 8; $c++) {
            $piece = $this->board[1][$c];
            $this->assertSame(Piece::Pawn, $piece->Name, "Hiányzó fekete gyalog a $c. oszlopban.");
            $this->assertSame(Side::Black, $piece->Color);
        }
    }

    public function testWhitePawnsOnRow6() : void
    {
        for ($c = 0; $c < 8; $c++) {
            $piece = $this->board[6][$c];
            $this->assertSame(Piece::Pawn, $piece->Name, "Hiányzó fehér gyalog a $c. oszlopban.");
            $this->assertSame(Side::White, $piece->Color);
        }
    }

    public function testWhiteBackRankOrder() : void
    {
        $expectedOrder = [
            Piece::Rook,
            Piece::Knight,
            Piece::Bishop,
            Piece::Queen,
            Piece::King,
            Piece::Bishop,
            Piece::Knight,
            Piece::Rook,
        ];

        for ($c = 0; $c < 8; $c++) {
            $piece = $this->board[7][$c];
            $this->assertSame($expectedOrder[$c], $piece->Name, "Helytelen bábu a fehér hátsó sorban ($c. oszlop).");
            $this->assertSame(Side::White, $piece->Color);
        }
    }

    public function testMiddleRowsAreEmptyPieces() : void
    {
        for ($r = 2; $r <= 5; $r++) {
            for ($c = 0; $c < 8; $c++) {
                $piece = $this->board[$r][$c];
                $this->assertSame(Piece::None, $piece->Name, "Sor $r, oszlop $c nem üres.");
                $this->assertSame(Side::None, $piece->Color);
            }
        }
    }

    public function testTotalNonEmptyPiecesIs32() : void
    {
        $count = 0;
        foreach ($this->board as $row) {
            foreach ($row as $piece) {
                if ($piece->Name !== Piece::None) {
                    $count++;
                }
            }
        }
        $this->assertSame(32, $count);
    }

    public function testEachSideHas16Pieces() : void
    {
        $white = $black = 0;
        foreach ($this->board as $row) {
            foreach ($row as $piece) {
                if ($piece->Color === Side::White) $white++;
                if ($piece->Color === Side::Black) $black++;
            }
        }
        $this->assertSame(16, $white);
        $this->assertSame(16, $black);
    }

    public function testEachSideHas8Pawns() : void
    {
        $whitePawns = $blackPawns = 0;
        foreach ($this->board as $row) {
            foreach ($row as $piece) {
                if ($piece->Name === Piece::Pawn && $piece->Color === Side::White) $whitePawns++;
                if ($piece->Name === Piece::Pawn && $piece->Color === Side::Black) $blackPawns++;
            }
        }
        $this->assertSame(8, $whitePawns);
        $this->assertSame(8, $blackPawns);
    }

    public function testEachSideHas2Rooks() : void
    {
        $white = $black = 0;
        foreach ($this->board as $row) {
            foreach ($row as $piece) {
                if ($piece->Name === Piece::Rook && $piece->Color === Side::White) $white++;
                if ($piece->Name === Piece::Rook && $piece->Color === Side::Black) $black++;
            }
        }
        $this->assertSame(2, $white);
        $this->assertSame(2, $black);
    }

    public function testEachSideHas1King() : void
    {
        $white = $black = 0;
        foreach ($this->board as $row) {
            foreach ($row as $piece) {
                if ($piece->Name === Piece::King && $piece->Color === Side::White) $white++;
                if ($piece->Name === Piece::King && $piece->Color === Side::Black) $black++;
            }
        }
        $this->assertSame(1, $white);
        $this->assertSame(1, $black);
    }
}