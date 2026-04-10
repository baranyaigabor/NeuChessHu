<?php

namespace ChessLogic\ChessBoard\ChessPieces\Definitions;

enum Piece : string
{
    case Pawn = 'Pawn';
    case Knight = 'Knight';
    case Bishop = 'Bishop';
    case Rook = 'Rook';
    case Queen = 'Queen';
    case King = 'King';
    case None = 'None';
}