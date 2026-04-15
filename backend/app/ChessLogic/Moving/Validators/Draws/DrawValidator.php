<?php

namespace ChessLogic\Moving\Validators\Draws;

use ChessLogic\ChessBoard\ChessPieces\Definitions\Side;
use ChessLogic\ChessBoard\ChessPieces\Pieces\ChessPiece;
use ChessLogic\MatchDatas\MatchDataStore;
use ChessLogic\Moving\Validators\Draws\ConsecutiveMoves\ConsecutiveMovesTracker;
use ChessLogic\Moving\Validators\Draws\InsufficentMaterials\InsufficentMaterialsValidator;
use ChessLogic\Moving\Validators\Draws\Repetitions\RepetitionTracker;
use ChessLogic\Moving\Validators\Draws\Repetitions\Hashing\Zobrist;
use ChessLogic\Moving\Validators\Draws\Stalemate\StalemateValidator;

class DrawValidator
{
    public MatchDataStore $MatchDataStore;

    public function __construct(MatchDataStore $matchDataStore)
    {
        $this->MatchDataStore = $matchDataStore;
    }

    /**
     * @param callable $currentLegalMovesWithSelectedPiece function(ChessPiece[][], array{0:int,1:int}, Side): bool[][]
     */
    public function isDrawn(callable $currentLegalMovesWithSelectedPiece,
        ChessPiece $piece, bool $hasCaptured, string $hasCastled) : bool 
    {
        $hash = Zobrist::hashComputer($this->MatchDataStore);
        $matchDataStore = $this->MatchDataStore;

        if (StalemateValidator::isStalemate($this->MatchDataStore, 
            $currentLegalMovesWithSelectedPiece)) 
        {
            $matchDataStore->MatchPoints->setMatchPointsReason("Stalemate", true);
            return true;
        }

        if (ConsecutiveMovesTracker::isConsecutiveMovesLimit($this->MatchDataStore, 
            $piece, $hasCaptured))
        {
            if ($matchDataStore->DrawTrackers->ConsecutiveMoves === 100) 
            {
                $matchDataStore->MatchPoints->setMatchPointsReason("50 Consecutive moves", true);
                return true;
            }

            $matchDataStore->MatchPoints->setMatchPointsReason("75 Consecutive moves", true);
            return true;
        }

        if (InsufficentMaterialsValidator::isInsufficentMaterialsOccured($matchDataStore->MatchState->PieceMatrix)) 
        {
            $matchDataStore->MatchPoints->setMatchPointsReason("Insufficent materials", true);
            return true;
        }

        if (RepetitionTracker::isRepetitive($this->MatchDataStore, $hash, 
            $piece, $hasCaptured, $hasCastled)) 
        {
            if ($matchDataStore->DrawTrackers->Repetitions[$hash] === 3) 
            {
                $matchDataStore->MatchPoints->setMatchPointsReason("Threefold-repetition", true);
                return true;
            }

            $matchDataStore->MatchPoints->setMatchPointsReason("Fivefold-repetition", true);
            return true;
        }

        return false;
    }
}