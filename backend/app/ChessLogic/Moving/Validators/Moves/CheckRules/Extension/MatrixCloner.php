<?php

namespace ChessLogic\Moving\Validators\Moves\CheckRules\Extension;

use ChessLogic\ChessBoard\ChessPieces\Definitions\Piece;
use ChessLogic\ChessBoard\ChessPieces\Definitions\Side;
use ChessLogic\ChessBoard\ChessPieces\Pieces\ChessPiece;

class MatrixCloner
{
    /**
     * @param ChessPiece[][] $pieceMatrix
     * @return ChessPiece[][]
     */
    public static function clone(array $pieceMatrix): array
    {
        $cloned = [];

        for ($r = 0; $r < 8; $r++) 
        {
            $cloned[$r] = [];

            for ($c = 0; $c < 8; $c++) 
            {
                $piece = $pieceMatrix[$r][$c];

                $cloned[$r][$c] = ChessPiece::create($piece->Name, $piece->Color);
            }
        }

        return $cloned;
    }
}