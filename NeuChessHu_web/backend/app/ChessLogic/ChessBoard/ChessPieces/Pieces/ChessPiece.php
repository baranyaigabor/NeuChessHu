<?php

namespace ChessLogic\ChessBoard\ChessPieces\Pieces;

use ChessLogic\ChessBoard\ChessPieces\Definitions\Piece;
use ChessLogic\ChessBoard\ChessPieces\Definitions\Side;
use InvalidArgumentException;

class ChessPiece
{
    public function __construct(
        public Piece $Name,
        public Side $Color
    ) {}

    public static function create(Piece $name, Side $color): ChessPiece
    {
        return new ChessPiece($name, $color);
    }

    public static function fromString(string $value): ChessPiece
    {
        foreach (Side::cases() as $side) {
            if (str_ends_with($value, $side->value)) {
                $pieceName = substr($value, 0, strlen($value) - strlen($side->value));
                $piece = Piece::from($pieceName);
                return new ChessPiece($piece, $side);
            }
        }

        throw new InvalidArgumentException("Cannot parse ChessPiece from '$value'");
    }
}