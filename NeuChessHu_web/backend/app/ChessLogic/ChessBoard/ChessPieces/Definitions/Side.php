<?php

namespace ChessLogic\ChessBoard\ChessPieces\Definitions;

enum Side : string
{
    case White = 'White';
    case Black = 'Black';
    case Random = 'Random';
    case None = 'None';
}