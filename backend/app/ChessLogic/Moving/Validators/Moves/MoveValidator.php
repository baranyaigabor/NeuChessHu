<?php

namespace ChessLogic\Moving\Validators\Moves;

use ChessLogic\ChessBoard\ChessPieces\Definitions\Piece;
use ChessLogic\MatchDatas\MatchDataStore;
use ChessLogic\Moving\Validators\Moves\CheckRules\CheckValidator;
use ChessLogic\Moving\Validators\Moves\PieceMoveRules\BishopMove;
use ChessLogic\Moving\Validators\Moves\PieceMoveRules\KingMove;
use ChessLogic\Moving\Validators\Moves\PieceMoveRules\KnightMove;
use ChessLogic\Moving\Validators\Moves\PieceMoveRules\PawnMove;
use ChessLogic\Moving\Validators\Moves\PieceMoveRules\QueenMove;
use ChessLogic\Moving\Validators\Moves\PieceMoveRules\RookMove;

class MoveValidator
{
    private array $LegalMoves;

    public function __construct(private MatchDataStore $MatchDataStore, private CheckValidator $Checks,
        private BishopMove $BishopMove, private KingMove $KingMove, private KnightMove $KnightMove,
        private PawnMove $PawnMove, private QueenMove $QueenMove, private RookMove $RookMove)
    {
        $this->LegalMoves = array_fill(0, 8, array_fill(0, 8, false));
    }

    /**
     * @param array{0:int,1:int} $from
     * @param array{0:int,1:int} $to
     */
    public function isLegalMove(array $from, array $to) : bool
    {
        $pieceMatrix = $this->MatchDataStore->MatchState->PieceMatrix;

        return self::isFromToNotEqual($pieceMatrix, $from, $to) 
            && CheckValidator::isNotSameColor($from, $to, $pieceMatrix) 
            && $this->LegalMoves[$to[0]][$to[1]];
    }

    private static function isFromToNotEqual(array $pieceMatrix, array $from, array $to) : bool
    {
        return $pieceMatrix[$from[0]][$from[1]] !== $pieceMatrix[$to[0]][$to[1]];
    }

    /**
     * @param array{0:int,1:int} $from
     * @return array<int,array<int,bool>>
     */
    public function currentLegalMovesWithSelectedPiece(array $pieceMatrix, array $from) : array 
    {
        $legalMoves = array_fill(0, 8, array_fill(0, 8, false));

        $piece = $pieceMatrix[$from[0]][$from[1]];

        $checkingFunction = match ($piece->Name) {
            Piece::Rook => [$this->RookMove,   'isMoveLegal'],
            Piece::Knight => [$this->KnightMove, 'isMoveLegal'],
            Piece::Bishop => [$this->BishopMove, 'isMoveLegal'],
            Piece::King => [$this->KingMove,   'isMoveLegal'],
            Piece::Queen => [$this->QueenMove,  'isMoveLegal'],
            Piece::Pawn => [$this->PawnMove,   'isMoveLegal'],
            Piece::None => null,
            default => throw new \RuntimeException("Invalid piece type: {$piece->Name->name}")
        };

        if ($checkingFunction === null) 
        {
            return $legalMoves;
        }

        if ($piece->Name === Piece::Knight) 
        {
            for ($r = 0; $r < 8; $r++) 
            {
                for ($c = 0; $c < 8; $c++) 
                {
                    if ($this->Checks->isFullyLegalMove($from, [$r, $c], $pieceMatrix,
                        $this->MatchDataStore, $checkingFunction)) 
                    {
                        $legalMoves[$r][$c] = true;
                    }
                }
            }
        }

        else
        {
            $isBlocked = false;
            for ($r = $from[0] - 1; $r >= 0; $r--) 
            {
                $this->currentDirectionChecker($isBlocked, $pieceMatrix, $from, [$r, $from[1]],
                    $this->MatchDataStore, $legalMoves, $checkingFunction);
            }

            $isBlocked = false;
            for ($r = $from[0] + 1; $r < 8; $r++) 
            {
                $this->currentDirectionChecker($isBlocked, $pieceMatrix, $from,
                    [$r, $from[1]], $this->MatchDataStore, $legalMoves, $checkingFunction);
            }

            $isBlocked = false;
            for ($c = $from[1] - 1; $c >= 0; $c--) 
            {
                $this->currentDirectionChecker($isBlocked, $pieceMatrix, $from,
                    [$from[0], $c], $this->MatchDataStore, $legalMoves, $checkingFunction);
            }

            $isBlocked = false;
            for ($c = $from[1] + 1; $c < 8; $c++) 
            {
                $this->currentDirectionChecker($isBlocked, $pieceMatrix, $from,
                    [$from[0], $c], $this->MatchDataStore, $legalMoves, $checkingFunction);
            }

            $isBlocked = false;
            for ($r = $from[0] - 1, $c = $from[1] - 1; $r >= 0 && $c >= 0; $r--, $c--) 
            {
                $this->currentDirectionChecker($isBlocked, $pieceMatrix, $from,
                    [$r, $c], $this->MatchDataStore, $legalMoves, $checkingFunction);
            }

            $isBlocked = false;
            for ($r = $from[0] - 1, $c = $from[1] + 1; $r >= 0 && $c < 8; $r--, $c++) 
            {
                $this->currentDirectionChecker($isBlocked, $pieceMatrix, $from,
                    [$r, $c], $this->MatchDataStore, $legalMoves, $checkingFunction);
            }

            $isBlocked = false;
            for ($r = $from[0] + 1, $c = $from[1] - 1; $r < 8 && $c >= 0; $r++, $c--) 
            {
                $this->currentDirectionChecker($isBlocked, $pieceMatrix, $from,
                    [$r, $c], $this->MatchDataStore, $legalMoves, $checkingFunction);
            }

            $isBlocked = false;
            for ($r = $from[0] + 1, $c = $from[1] + 1; $r < 8 && $c < 8; $r++, $c++) 
            {
                $this->currentDirectionChecker($isBlocked, $pieceMatrix, $from,
                    [$r, $c], $this->MatchDataStore, $legalMoves, $checkingFunction);
            }
        }

        $this->LegalMoves = $legalMoves;
        return $legalMoves;
    }

    private function currentDirectionChecker(bool &$isBlocked, array $pieceMatrix,
        array $from, array $to, MatchDataStore $matchDataStore, array &$legalMoves,
        callable $checkingFunction) : void 
    {
        $targetPiece = $pieceMatrix[$to[0]][$to[1]];

        if ($targetPiece->Name !== Piece::None && !$isBlocked &&
            !CheckValidator::isNotSameColor($from, $to, $pieceMatrix)) 
        {
            $isBlocked = true;
        }

        if (!$isBlocked && $this->Checks->isFullyLegalMove($from, $to,
                $pieceMatrix, $matchDataStore, $checkingFunction))
        {
            $legalMoves[$to[0]][$to[1]] = true;
        }

        if ($targetPiece->Name !== Piece::None && !$isBlocked &&
            CheckValidator::isNotSameColor($from, $to, $pieceMatrix)) 
        {
            $isBlocked = true;
        }
    }

    public function canMoveThere(array $pieceMatrix, array $from, array $to) : bool 
    {
        $piece = $pieceMatrix[$from[0]][$from[1]]->Name;

        if (!in_array($piece, [Piece::Rook, Piece::Knight, Piece::Bishop, Piece::Queen], true)) 
        {
            return false;
        }

        $moves = $this->currentLegalMovesWithSelectedPiece($pieceMatrix, $from);
        return $moves[$to[0]][$to[1]];
    }
}