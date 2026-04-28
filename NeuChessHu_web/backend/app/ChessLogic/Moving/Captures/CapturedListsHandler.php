<?php

namespace ChessLogic\Moving\Captures;

use ChessLogic\ChessBoard\ChessPieces\Definitions\Piece;
use ChessLogic\ChessBoard\ChessPieces\Definitions\Side;
use ChessLogic\MatchDatas\MatchDataStore;
use RuntimeException;

class CapturedListsHandler
{
    public static function appendList(MatchDataStore $matchDataStore, Side $currentSide,
        Piece $capturedPiece): void 
    {
        $matchDataStore->PlayerDatas[$currentSide->value]->Points += self::pointsCounter($capturedPiece);

        if ($matchDataStore->PlayerDatas[$currentSide->value]->CapturedPieces !== null) {
            self::addPiece($matchDataStore->PlayerDatas[$currentSide->value]->CapturedPieces, $capturedPiece);
        }
    }

    private static function pointsCounter(Piece $piece): int
    {
        return match ($piece) {
            Piece::Pawn => 1,
            Piece::Knight => 3,
            Piece::Bishop => 3,
            Piece::Rook => 5,
            Piece::Queen => 9,
            default => throw new RuntimeException("Unhandled piece type"),
        };
    }

    /**
     * @param Piece[] $pieces
     */
    private static function addPiece(array &$pieces, Piece $capturedPiece): void
    {
        $pieces[] = $capturedPiece;

        usort($pieces, function (Piece $a, Piece $b) {
            $pa = self::pointsCounter($a);
            $pb = self::pointsCounter($b);

            if ($pa !== $pb) {
                return $pb <=> $pa; 
            }

            $isKnightA = $a === Piece::Knight ? 1 : 0;
            $isKnightB = $b === Piece::Knight ? 1 : 0;

            return $isKnightB <=> $isKnightA;
        });
    }
}