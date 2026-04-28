<?php 

namespace ChessLogic\Moving\Factories\MoveComponents;

use ChessLogic\Moving\Factories\MoveComponents\MoveComponents;
use ChessLogic\Moving\Validators\Moves\CheckRules\CheckValidator;
use ChessLogic\Moving\Validators\Moves\PieceMoveRules\{
    BishopMove, KingMove, KnightMove, PawnMove, QueenMove, RookMove
};

class MoveComponentsFactory
{
    public function create(): MoveComponents
    {
        $bishop = new BishopMove();
        $knight = new KnightMove();
        $pawn = new PawnMove();
        $queen = new QueenMove();
        $rook = new RookMove();

        $checkValidator = new CheckValidator($bishop, $knight, $queen, $rook);
        $king = new KingMove($checkValidator);

        return new MoveComponents(
            bishop: $bishop,
            knight: $knight,
            pawn: $pawn,
            queen: $queen,
            rook: $rook,
            king: $king,
            checkValidator: $checkValidator
        );
    }
}