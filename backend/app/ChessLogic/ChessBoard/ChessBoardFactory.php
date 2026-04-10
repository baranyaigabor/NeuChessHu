<?php

namespace ChessLogic\ChessBoard;

use ChessLogic\ChessBoard\ChessPieces\Definitions\Piece;
use ChessLogic\ChessBoard\ChessPieces\Definitions\Side;
use ChessLogic\ChessBoard\ChessPieces\Pieces\ChessPiece;

class ChessBoardFactory
{
    private static array $backRank = [
        Piece::Rook,
        Piece::Knight,
        Piece::Bishop,
        Piece::Queen,
        Piece::King,
        Piece::Bishop,
        Piece::Knight,
        Piece::Rook
    ];

    public static function boardFiller(): array
    {
        $board = array_fill(0, 8, array_fill(0, 8, null));
 
        $blackBackRank = 0;
        $blackPawns = 1;

        $whitePawns = 6;
        $whiteBackRank = 7;

        $backRanks = self::$backRank;

        for ($c = 0; $c < 8; $c++) {
            $board[$blackBackRank][$c] = ChessPiece::create($backRanks[$c], Side::Black);
        }

        for ($c = 0; $c < 8; $c++) {
            $board[$blackPawns][$c] = ChessPiece::create(Piece::Pawn, Side::Black);
        }
        for ($c = 0; $c < 8; $c++) {
            $board[$whitePawns][$c] = ChessPiece::create(Piece::Pawn, Side::White);
        }

        for ($c = 0; $c < 8; $c++) {
            $board[$whiteBackRank][$c] = ChessPiece::create($backRanks[$c], Side::White);
        }

        for ($r = 0; $r < 8; $r++) {
            for ($c = 0; $c < 8; $c++) {
                if ($board[$r][$c] === null) {
                    $board[$r][$c] = ChessPiece::create(Piece::None, Side::None);
                }
            }
        }

        return $board;
    }
}