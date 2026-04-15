<?php

namespace ChessLogic\Moving\Validators\Draws\Repetitions\Hashing;

use ChessLogic\ChessBoard\ChessPieces\Definitions\Piece;
use ChessLogic\ChessBoard\ChessPieces\Definitions\Side;
use ChessLogic\ChessBoard\ChessPieces\Pieces\ChessPiece;
use ChessLogic\MatchDatas\MatchDataStore;
use Random\Engine\Mt19937;
use Random\Randomizer;

class Zobrist
{
    /** @var int[][][] */
    private static array $PieceSquare = [];

    private static int $CurrentlyPlayingSide;

    /** @var int[] */
    private static array $CastlingRights = [];

    /** @var int[] */
    private static array $EnPassantFile = [];

    public static function init() : void
    {
        if (!empty(self::$PieceSquare)) {
            return;
        }

        $rng = new Mt19937(20260430);
        $random = new Randomizer($rng);

        for ($color = 0; $color < 2; $color++) 
        {
            self::$PieceSquare[$color] = [];

            for ($piece = 0; $piece < 6; $piece++) 
            {
                self::$PieceSquare[$color][$piece] = [];

                for ($square = 0; $square < 64; $square++) 
                {
                    self::$PieceSquare[$color][$piece][$square] = self::random64($random);
                }
            }
        }

        self::$CurrentlyPlayingSide = self::random64($random);

        for ($i = 0; $i < 16; $i++)
        {
            self::$CastlingRights[$i] = self::random64($random);
        }

        for ($i = 0; $i < 8; $i++) 
        {
            self::$EnPassantFile[$i] = self::random64($random);
        }
    }

    public static function hashComputer(MatchDataStore $matchDataStore) : int
    {
        self::init();

        $hash = 0;

        $board = $matchDataStore->MatchState->PieceMatrix;

        for ($r = 0; $r < 8; $r++) {
            for ($c = 0; $c < 8; $c++) {
                /** @var ChessPiece $piece */
                $piece = $board[$r][$c];

                if ($piece->Name === Piece::None) 
                {
                    continue;
                }

                $colorNumber = ($piece->Color === Side::White) ? 0 : 1;

                $pieceNumber = match($piece->Name) 
                {
                    Piece::Pawn => 0,
                    Piece::Knight => 1,
                    Piece::Bishop => 2,
                    Piece::Rook => 3,
                    Piece::Queen => 4,
                    Piece::King => 5,
                    default => null
                };

                if ($pieceNumber === null)
                {
                    continue;
                }

                $square = $r * 8 + $c;

                $hash ^= self::$PieceSquare[$colorNumber][$pieceNumber][$square];
            }
        }

        if ($matchDataStore->MatchState->CurrentSide === Side::Black) 
        {
            $hash ^= self::$CurrentlyPlayingSide;
        }

        $mask = self::getCastlingRightsMask($matchDataStore);
        $hash ^= self::$CastlingRights[$mask];

        if ($matchDataStore->MatchState->EnPassantTarget !== null) 
        {
            $file = $matchDataStore->MatchState->EnPassantTarget[1];
            $hash ^= self::$EnPassantFile[$file];
        }

        return $hash;
    }

    private static function random64(Randomizer $random) : int
    {
        $bytes = $random->getBytes(8);
        $arr = unpack("P", $bytes);
        return $arr[1];
    }

    private static function getCastlingRightsMask(MatchDataStore $matchDataStore) : int
    {
        $state = $matchDataStore->MatchState;
        $mask = 0;

        if (!$state->HasWKMoved && !$state->HasWRHMoved) 
        {
            $mask |= 1;
        }

        if (!$state->HasWKMoved && !$state->HasWRAMoved) 
        {
            $mask |= 2;
        }

        if (!$state->HasBKMoved && !$state->HasBRHMoved) 
        {
            $mask |= 4;
        }

        if (!$state->HasBKMoved && !$state->HasBRAMoved) 
        {
            $mask |= 8;
        }

        return $mask;
    }
}