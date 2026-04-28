<?php

namespace ChessLogic\Moving\Validators\Moves\PieceMoveRules;

use ChessLogic\ChessBoard\ChessPieces\Definitions\Piece;
use ChessLogic\ChessBoard\ChessPieces\Definitions\Side;
use ChessLogic\MatchDatas\MatchDataStore;

class PawnMove implements IMoveValidators
{
    /**
     * @param array{0:int,1:int} $from
     * @param array{0:int,1:int} $to
     */
    public function isMoveLegal(array $from, array $to, MatchDataStore $matchDataStore): bool 
    {
        $pawn = $matchDataStore->MatchState->PieceMatrix[$from[0]][$from[1]];
        $target = $matchDataStore->MatchState->PieceMatrix[$to[0]][$to[1]];

        if ($from[1] === $to[1] && $target->Name === Piece::None) {
            if (self::manhattanDistance($from, $to) === 1) {
                if ($pawn->Color === Side::White && $to[0] < $from[0]) {
                    return true;
                }
                if ($pawn->Color === Side::Black && $to[0] > $from[0]) {
                    return true;
                }
            }
            if (self::manhattanDistance($from, $to) === 2) {
                if ($pawn->Color === Side::Black && $from[0] === 1) {
                    return true;
                }
                if ($pawn->Color === Side::White && $from[0] === 6) {
                    return true;
                }
            }
        }

        if (abs($from[0] - $to[0]) === abs($from[1] - $to[1]) 
            && self::manhattanDistance($from, $to) === 2 &&
            (
                $target->Name !== Piece::None ||
                self::canBeEnPassant($matchDataStore->MatchState->EnPassantTarget, $to)
            )
        ) {
            if ($pawn->Color === Side::White && $from[0] > $to[0]) {
                return true;
            }
            if ($pawn->Color === Side::Black && $from[0] < $to[0]) {
                return true;
            }
        }

        return false;
    }

    private static function manhattanDistance(array $from, array $to): int
    {
        return abs($to[0] - $from[0]) + abs($to[1] - $from[1]);
    }

    private static function canBeEnPassant(?array $enPassantTarget, array $to): bool
    {
        return $enPassantTarget !== null &&
               $to[0] === $enPassantTarget[0] &&
               $to[1] === $enPassantTarget[1];
    }
}