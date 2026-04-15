<?php

namespace ChessLogic\Moving\Validators\Moves\PieceMoveRules;

use ChessLogic\ChessBoard\ChessPieces\Definitions\Piece;
use ChessLogic\ChessBoard\ChessPieces\Definitions\Side;
use ChessLogic\MatchDatas\MatchDataStore;
use ChessLogic\Moving\Validators\Moves\CheckRules\CheckValidator;

class KingMove implements IMoveValidators
{
    public function __construct(private CheckValidator $checkValidator) {}

    /**
     * @param array{0:int,1:int} $from
     * @param array{0:int,1:int} $to
     */
    public function isMoveLegal(array $from, array $to, MatchDataStore $matchDataStore): bool
    {
        $pieceMatrix = $matchDataStore->MatchState->PieceMatrix;

        if (abs($from[0] - $to[0]) <= 1 && abs($from[1] - $to[1]) <= 1 &&
            self::arentKingsAdjacents($pieceMatrix, $from, $to)) 
        {
            return $this->checkValidator->isSimulatedMoveSafe($matchDataStore,
                $pieceMatrix, $from, $to);
        }

        return $this->isLegalCastling($from, $to, $matchDataStore);
    }

    private function isLegalCastling(array $from, array $to, MatchDataStore $matchDataStore): bool
    {
        $pieceMatrix = $matchDataStore->MatchState->PieceMatrix;

        if ($from[0] !== $to[0]) {
            return false;
        }

        if (!$matchDataStore->MatchState->HasWKMoved && $from[0] === 7) 
        {
            if (!$matchDataStore->MatchState->HasWRAMoved && ($from[1] - $to[1]) === 2 &&
                $this->isKingToRookClearAndSafe($matchDataStore, $pieceMatrix, $from, $to)) 
            {
                return true;
            }

            if (!$matchDataStore->MatchState->HasWRHMoved && ($from[1] - $to[1]) === -2 &&
                $this->isKingToRookClearAndSafe($matchDataStore, $pieceMatrix, $from, $to)) 
            {
                return true;
            }
        }
        if (!$matchDataStore->MatchState->HasBKMoved && $from[0] === 0) 
        {
            if (!$matchDataStore->MatchState->HasBRAMoved && ($from[1] - $to[1]) === 2 &&
                $this->isKingToRookClearAndSafe($matchDataStore, $pieceMatrix, $from, $to)) 
            {
                return true;
            }

            if (!$matchDataStore->MatchState->HasBRHMoved && ($from[1] - $to[1]) === -2 &&
                $this->isKingToRookClearAndSafe($matchDataStore, $pieceMatrix, $from, $to))
            {
                return true;
            }
        }

        return false;
    }

    private function isKingToRookClearAndSafe(MatchDataStore $matchDataStore,
        array $pieceMatrix, array $from, array $to): bool 
    {
        if ($this->checkValidator->isSelectedKingInCheck($matchDataStore, $pieceMatrix, $from)) 
        {
            return false;
        }

        $direction = ($to[1] < $from[1]) ? -1 : 1;

        for ($s = 1; $s <= 2; $s++) {
            $col = $from[1] + $direction * $s;

            if ($col < 0 || $col > 7) {
                return false;
            }

            if ($pieceMatrix[$from[0]][$col]->Name !== Piece::None) {
                return false;
            }

            if (!$this->checkValidator->isSimulatedMoveSafe($matchDataStore,
                    $pieceMatrix, $from, [$from[0], $col])) 
            {
                return false;
            }

            if (!self::arentKingsAdjacents($pieceMatrix, $from, [$from[0], $col])) {
                return false;
            }
        }

        return true;
    }

    private static function arentKingsAdjacents(array $pieceMatrix, array $from,
        array $to): bool 
    {
        $oppColor = ($pieceMatrix[$from[0]][$from[1]]->Color === Side::White)
            ? Side::Black
            : Side::White;

        for ($r = 0; $r < 8; $r++) {
            for ($c = 0; $c < 8; $c++) {

                $piece = $pieceMatrix[$r][$c];

                if (
                    $piece->Name === Piece::King &&
                    $piece->Color === $oppColor &&
                    abs($to[0] - $r) <= 1 &&
                    abs($to[1] - $c) <= 1
                ) {
                    return false;
                }
            }
        }

        return true;
    }
}