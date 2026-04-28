<?php

namespace ChessLogic\Moving\Validators\Draws\Stalemate;

use ChessLogic\ChessBoard\ChessPieces\Pieces\ChessPiece;
use ChessLogic\MatchDatas\MatchDataStore;

class StalemateValidator
{
    /**
     * @param callable $currentLegalMovesWithSelectedPiece function(ChessPiece[][], array{0:int,1:int}, Side): bool[][]
     */
    public static function isStalemate(MatchDataStore $matchDataStore,
        callable $currentLegalMovesWithSelectedPiece) : bool 
    {
        $board = $matchDataStore->MatchState->PieceMatrix;
        
        for ($r = 0; $r < 8; $r++) {
            for ($c = 0; $c < 8; $c++) {

                /** @var ChessPiece $piece */
                $piece = $board[$r][$c];

                if ($matchDataStore->MatchState->CurrentSide === $piece->Color) 
                {
                    continue;
                }

                $moves = $currentLegalMovesWithSelectedPiece($board, [$r, $c]);

                foreach ($moves as $row) 
                {
                    foreach ($row as $canMove) 
                    {
                        if ($canMove === true) 
                        {
                            return false;
                        }
                    }
                }
            }
        }

        return true;
    }
}