<?php

namespace ChessLogic\Moving\Validators\Draws\Repetitions;

use ChessLogic\ChessBoard\ChessPieces\Definitions\Piece;
use ChessLogic\ChessBoard\ChessPieces\Pieces\ChessPiece;
use ChessLogic\MatchDatas\MatchDataStore;

class RepetitionTracker
{
    public static function isRepetitive(MatchDataStore $matchDataStore, int $hash,
        ChessPiece $piece, bool $hasCaptured, string $hasCastled) : bool 
    {
        if ($piece->Name === Piece::Pawn || $hasCaptured || $hasCastled !== "") 
        {
            self::reset($matchDataStore);
        }

        self::add($matchDataStore, $hash);

        return self::isRepeated($matchDataStore, $hash);
    }

    private static function isRepeated(MatchDataStore $matchDataStore, int $hash) : bool
    {
        $reps = $matchDataStore->DrawTrackers->Repetitions;

        if (!array_key_exists($hash, $reps)) 
        {
            return false;
        }

        $count = $reps[$hash];

        return ($count === 3 || $count >= 5);
    }

    private static function add(MatchDataStore $matchDataStore, int $hash) : void
    {
        if (!array_key_exists($hash, $matchDataStore->DrawTrackers->Repetitions)) 
        {
            $matchDataStore->DrawTrackers->Repetitions[$hash] = 0;
        }

        $matchDataStore->DrawTrackers->Repetitions[$hash]++;
    }

    private static function reset(MatchDataStore $matchDataStore) : void
    {
        $matchDataStore->DrawTrackers->Repetitions = [];
    }
}