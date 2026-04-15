<?php

namespace ChessLogic\Moving\Factories\MoveComponents;

use ChessLogic\Moving\Validators\Moves\CheckRules\CheckValidator;
use ChessLogic\Moving\Validators\Moves\PieceMoveRules\{
    BishopMove, KingMove, KnightMove, PawnMove, QueenMove, RookMove
};

class MoveComponents
{
    public function __construct(
        public BishopMove $bishop,
        public KnightMove $knight,
        public PawnMove $pawn,
        public QueenMove $queen,
        public RookMove $rook,
        public KingMove $king,
        public CheckValidator $checkValidator
    ) {}
}