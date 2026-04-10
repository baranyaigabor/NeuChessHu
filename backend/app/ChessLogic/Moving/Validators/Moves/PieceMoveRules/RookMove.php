<?php

namespace ChessLogic\Moving\Validators\Moves\PieceMoveRules;

use ChessLogic\MatchDatas\MatchDataStore;

class RookMove implements IMoveValidators
{
    /**
     * @param array{0:int,1:int} $from
     * @param array{0:int,1:int} $to
     */
    public function isMoveLegal(array $from, array $to, MatchDataStore $matchDataStore): bool
    {
        return $from[0] === $to[0] || $from[1] === $to[1];
    }
}