<?php

namespace ChessLogic\Moving\Validators\Draws\InsufficentMaterials;

use ChessLogic\ChessBoard\ChessPieces\Definitions\Piece;
use ChessLogic\ChessBoard\ChessPieces\Definitions\Side;
use ChessLogic\ChessBoard\ChessPieces\Pieces\ChessPiece;

class InsufficentMaterialsValidator
{
    /**
     * @param ChessPiece[][] $pieceMatrix
     */
    public static function isInsufficentMaterialsOccured(array $pieceMatrix) : bool
    {
        $piecesCoordinates = self::allTheRemainingPiecesCoordinates($pieceMatrix);

        $pieces = array_map(fn($coord) => $pieceMatrix[$coord[0]][$coord[1]]->Name,
            $piecesCoordinates);

        if (count($piecesCoordinates) === 0) 
        {
            return true;
        }

        if (count($piecesCoordinates) === 1) 
        {
            $piece = $pieces[0];

            if ($piece === Piece::Knight || $piece === Piece::Bishop) 
            {
                return true;
            }
        }

        if (count($piecesCoordinates) === 2 && self::areOpponentPieces($pieceMatrix, $piecesCoordinates) &&
            $pieces[0] === Piece::Bishop && $pieces[1] === Piece::Bishop &&
            self::squaresColorCalculator($piecesCoordinates[0]) === self::squaresColorCalculator($piecesCoordinates[1])) 
        {
            return true;
        }

        return false;
    }

    /**
     * @param ChessPiece[][] $pieceMatrix
     * @return array{0:int,1:int}[]
     */
    private static function allTheRemainingPiecesCoordinates(array $pieceMatrix) : array
    {
        $coords = [];

        for ($r = 0; $r < 8; $r++) 
        {
            for ($c = 0; $c < 8; $c++) 
            {
                $piece = $pieceMatrix[$r][$c];

                if ($piece->Name !== Piece::None && $piece->Name !== Piece::King) 
                {
                    $coords[] = [$r, $c];
                }
            }
        }

        return $coords;
    }

    /**
     * @param array{0:int,1:int} $piece
     */
    private static function squaresColorCalculator(array $piece): Side
    {
        return (($piece[0] + $piece[1]) % 2 === 1)
            ? Side::White
            : Side::Black;
    }

    /**
     * @param ChessPiece[][] $pieceMatrix
     * @param array{0:int,1:int}[] $coords
     */
    private static function areOpponentPieces(array $pieceMatrix, array $coords): bool
    {
        [$r1, $c1] = $coords[0];
        [$r2, $c2] = $coords[1];

        return $pieceMatrix[$r1][$c1]->Color !== $pieceMatrix[$r2][$c2]->Color;
    }
}