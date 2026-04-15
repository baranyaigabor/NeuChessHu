<?php 

namespace ChessLogic\Moving\Factories;

use ChessLogic\MatchDatas\MatchDataStore;
use ChessLogic\Moving\Factories\MoveComponents\MoveComponents;
use ChessLogic\Moving\Validators\Moves\MoveValidator;

class MoveValidatorFactory
{
    public function create(MatchDataStore $matchDataStore, MoveComponents $components) : MoveValidator
    {
        return new MoveValidator(
            $matchDataStore,
            $components->checkValidator,
            $components->bishop,
            $components->king,
            $components->knight,
            $components->pawn,
            $components->queen,
            $components->rook
        );
    }
}