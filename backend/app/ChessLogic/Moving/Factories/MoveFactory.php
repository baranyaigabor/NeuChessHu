<?php

namespace ChessLogic\Moving\Factories;

use ChessLogic\MatchDatas\MatchDataStore;
use ChessLogic\Moving\Factories\MoveComponents\MoveComponentsFactory;
use ChessLogic\Moving\Move;
use ChessLogic\Moving\Validators\Draws\DrawValidator;
use ChessLogic\Moving\Validators\Moves\MoveValidator;

class MoveFactory
{
    public function __construct(
        private MoveComponentsFactory $componentsFactory,
        private MoveValidatorFactory $moveValidatorFactory
    ) {}

    public function create(MatchDataStore $matchDataStore, MoveValidator $moveValidator) : Move
    {
        $components = $this->componentsFactory->create();

        return new Move($matchDataStore, $components->checkValidator, $moveValidator,
            new DrawValidator($matchDataStore)
        );
    }
}