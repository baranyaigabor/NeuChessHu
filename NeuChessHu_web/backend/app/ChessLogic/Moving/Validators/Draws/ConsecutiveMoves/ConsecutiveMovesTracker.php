<?php

namespace ChessLogic\Moving\Validators\Draws\ConsecutiveMoves;

use ChessLogic\ChessBoard\ChessPieces\Definitions\Piece;
use ChessLogic\ChessBoard\ChessPieces\Pieces\ChessPiece;
use ChessLogic\MatchDatas\MatchDataStore;

class ConsecutiveMovesTracker
{
    public static function isConsecutiveMovesLimit(MatchDataStore $matchDataStore,
        ChessPiece $piece, bool $hasCaptured) : bool 
    {
        if ($piece->Name === Piece::Pawn || $hasCaptured)
        {
            self::resetConsecutiveMoves($matchDataStore);
        }

        else
        {
            self::addConsecutiveMoves($matchDataStore);
        }

        return self::isFiftyOrSeventyFiveConsecutive($matchDataStore);
    }

    private static function isFiftyOrSeventyFiveConsecutive(MatchDataStore $matchDataStore): bool
    {
        $moves = $matchDataStore->DrawTrackers->ConsecutiveMoves;
        return $moves === 100 || $moves === 150;
    }

    private static function addConsecutiveMoves(MatchDataStore $matchDataStore): void
    {
        $matchDataStore->DrawTrackers->ConsecutiveMoves++;
    }

    private static function resetConsecutiveMoves(MatchDataStore $matchDataStore): void
    {
        $matchDataStore->DrawTrackers->ConsecutiveMoves = 0;
    }
}