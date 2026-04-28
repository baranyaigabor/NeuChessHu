<?php

namespace ChessLogic\Notations\PieceNotations;

use ChessLogic\ChessBoard\ChessPieces\Definitions\Piece;
use ChessLogic\ChessBoard\ChessPieces\Pieces\ChessPiece;

class SANNotations
{
    /**
     * @param array{0:int,1:int} $from
     * @param array{0:int,1:int} $to
     */
    public static function notationDefiner(array $pieceMatrix, array $startingMatrix,
        callable $canMoveThere, array $from, array $to,
        Piece $promotedPiece, bool $hasCaptured, string $hasCastled,
        bool $isCheck, bool $isCheckMate): string 
    {
        $notation = '';

        $piece = $pieceMatrix[$to[0]][$to[1]];

        $pieceNotation = ($promotedPiece !== Piece::None)
            ? ''
            : match ($piece->Name) {
                Piece::Pawn => '',
                Piece::Knight => 'N',
                default => substr($piece->Name->name, 0, 1)
            };

        $toSquare =
            self::columnDefiner($to[1]) .
            self::rowDefiner( $to[0]);

        $promotedPieceNotation = ($promotedPiece === Piece::None)
            ? ''
            : match ($promotedPiece) {
                Piece::Knight => 'N',
                default => substr($piece->Name->name, 0, 1)
            };

        if ($hasCastled !== '') {
            $notation .= ($hasCastled === 'QueenSide' ? 'O-O-O' : 'O-O');
            $notation .= $isCheckMate ? '#' : ($isCheck ? '+' : '');
            return $notation;
        }

        $notation .=
            $pieceNotation .
            self::specifiedFromSquareDefiner($startingMatrix, $canMoveThere, $piece,
                $from, $to, $hasCaptured, $promotedPiece ) .
            ($hasCaptured ? 'x' : '') .
            $toSquare .
            ($promotedPiece !== Piece::None ? '=' . $promotedPieceNotation : '') .
            ($isCheckMate ? '#' : ($isCheck ? '+' : ''));

        return $notation;
    }

    /**
     * @param array{0:int,1:int} $from
     * @param array{0:int,1:int} $to
     */
    private static function specifiedFromSquareDefiner(array $startingMatrix,
        callable $canMoveThere, ChessPiece $piece, array $from, array $to,
        bool $hasCaptured, Piece $promotedPiece): string 
    {
        if (($piece->Name === Piece::Pawn || $promotedPiece !== Piece::None) && $hasCaptured) {
            return self::columnDefiner( $from[1]);
        }

        $matching = [];
        $specified = '';

        for ($r = 0; $r < 8; $r++) {
            for ($c = 0; $c < 8; $c++) {

                $candidate = $startingMatrix[$r][$c];

                if ($piece->Name !== Piece::Pawn &&
                    $candidate->Name === $piece->Name &&
                    $candidate->Color === $piece->Color &&
                    $canMoveThere($startingMatrix, [$r, $c], $to)) 
                {
                    $matching[] = [$r, $c];
                }
            }
        }

        if (count($matching) > 1) {
            $sameColumnCount = 0;
            foreach ($matching as $pos) {
                if ($pos[1] === $from[1]) {
                    $sameColumnCount++;
                }
            }

            if ($sameColumnCount > 1) 
            {
                $specified .= self::rowDefiner($from[0]);
            } 
            else 
            {
                $specified .= self::columnDefiner($from[1]);
            }
        }

        return $specified;
    }

    private static function columnDefiner(int $col) : string
    {
        $letters = ['a','b','c','d','e','f','g','h'];

        return $letters[$col];
    }

    private static function rowDefiner(int $row) : string
    {
        //return (string)($row + 1);
        return (string)(8 - $row);
    }
}