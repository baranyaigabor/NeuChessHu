<?php

namespace ChessLogic\Notations\PieceNotations;

use ChessLogic\ChessBoard\ChessPieces\Definitions\Side;
use ChessLogic\MatchDatas\MatchDataStore;

class NotationsExtension
{
    /**
     * @param SANNotationRow[] &$moves
     */
    public static function notationAdder(array &$moves, 
        MatchDataStore $matchDataStore, string $notation): void
    {
        $currentSide = $matchDataStore->MatchState->CurrentSide;

        if ($currentSide === Side::Black) 
        {
            $lastMove = $moves[count($moves) - 1];

            $moves[count($moves) - 1] = new SANNotationRow(
                Round: $lastMove->Round,
                WhitesNotation: $lastMove->WhitesNotation,
                BlacksNotation: $notation
            );
        }
        else 
        {
            $newRound = new SANNotationRow(Round: (string)(count($moves) + 1) . '.',
                WhitesNotation: $notation,
                BlacksNotation: null
            );

            $moves[] = $newRound;
        }
    }
}