<?php

namespace ChessLogic\Moving\Validators\Moves\PieceMoveRules;

use ChessLogic\MatchDatas\MatchDataStore;

class BishopMove implements IMoveValidators
{ 
    /**
     * @param array{0:int,1:int} $from
     * @param array{0:int,1:int} $to
     */
    public function isMoveLegal(array $from, array $to, MatchDataStore $matchDataStore): bool
    {    
        return abs($from[0] - $to[0]) === abs($from[1] - $to[1]);
    }
}