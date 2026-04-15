<?php

namespace ChessLogic\Moving\Validators\Moves\CheckRules;

use ChessLogic\ChessBoard\ChessPieces\Definitions\Piece;
use ChessLogic\ChessBoard\ChessPieces\Definitions\Side;
use ChessLogic\ChessBoard\ChessPieces\Pieces\ChessPiece;
use ChessLogic\MatchDatas\MatchDataStore;
use ChessLogic\Moving\Validators\Moves\PieceMoveRules\BishopMove;
use ChessLogic\Moving\Validators\Moves\PieceMoveRules\KnightMove;
use ChessLogic\Moving\Validators\Moves\PieceMoveRules\QueenMove;
use ChessLogic\Moving\Validators\Moves\PieceMoveRules\RookMove;
use ChessLogic\Moving\Validators\Moves\CheckRules\Extension\MatrixCloner;

class CheckValidator
{
    public function __construct(private BishopMove $BishopMove, private KnightMove $KnightMove,
        private QueenMove $QueenMove, private RookMove $RookMove) {}

    /**
     * @param array{0:int,1:int} $from
     * @param array{0:int,1:int} $to
     * @param callable $checkingFunction
     */
    public function isFullyLegalMove(array $from, array $to, array $pieceMatrix,
        MatchDataStore $matchDataStore, callable $checkingFunction) : bool 
    {
        return $checkingFunction($from, $to, $matchDataStore) &&
            $this->isSimulatedMoveSafe($matchDataStore, $pieceMatrix, $from, $to) &&
            $this->isNotSameColor($from, $to, $pieceMatrix);
    }

    /**
     * @param array{0:int,1:int} $from
     * @param array{0:int,1:int} $to
     */
    public function isSimulatedMoveSafe(MatchDataStore $matchDataStore, array $pieceMatrix,
        array $from, array $to) : bool 
    {
        $tempMatrix = self::moveSimulator($pieceMatrix, $from, $to);
        $movingColor = $tempMatrix[$to[0]][$to[1]]->Color;

        $checks = $this->checksChecker($matchDataStore, $tempMatrix);

        return !in_array($movingColor, $checks, true);
    }

    /**
     * @param array{0:int,1:int} $from
     * @param array{0:int,1:int} $to
     */
    
    private static function moveSimulator(array $sourceMatrix, array $from, array $to): array 
    {
        $tempMatrix = MatrixCloner::clone($sourceMatrix);

        $tempMatrix[$to[0]][$to[1]] = $tempMatrix[$from[0]][$from[1]];
        $tempMatrix[$from[0]][$from[1]] = ChessPiece::create(Piece::None, Side::None);

        return $tempMatrix;
    }

    /**
     * @return Side[]
     */
    public function checksChecker(MatchDataStore $matchDataStore, array $pieceMatrix) : array 
    {
        $result = [];

        for ($r = 0; $r < 8; $r++) 
        {
            for ($c = 0; $c < 8; $c++) 
            {
                $piece = $pieceMatrix[$r][$c];

                if ($piece->Name === Piece::King) 
                {
                    if ($piece->Color === Side::White &&
                        $this->isSelectedKingInCheck($matchDataStore, $pieceMatrix, [$r, $c])) 
                    {
                        $result[] = Side::White;
                    }

                    if ($piece->Color === Side::Black &&
                        $this->isSelectedKingInCheck($matchDataStore, $pieceMatrix, [$r, $c])) 
                    {
                        $result[] = Side::Black;
                    }
                }
            }
        }

        return $result;
    }

    /**
     * @param array{0:int,1:int} $kingPos
     */
    public function isSelectedKingInCheck(MatchDataStore $matchDataStore,
        array $pieceMatrix, array $kingPos) : bool 
    {
        if ($this->isKnightCheckingKing($kingPos, $matchDataStore, $pieceMatrix) 
            || $this->isPawnCheckingKing($kingPos, $pieceMatrix)) 
        {
            return true;
        }

        $isBlocked = false;
        for ($r = $kingPos[0] - 1; $r >= 0; $r--) 
        {
            if (!$isBlocked && $pieceMatrix[$r][$kingPos[1]]->Name !== Piece::None) 
            {
                if ($this->isSelectedKingInCheckOrthogonally($kingPos, $r, $kingPos[1], $pieceMatrix, $matchDataStore)) 
                {
                    return true;
                }
                $isBlocked = true;
            }
        }

        $isBlocked = false;
        for ($r = $kingPos[0] + 1; $r < 8; $r++) 
        {
            if (!$isBlocked && $pieceMatrix[$r][$kingPos[1]]->Name !== Piece::None) 
            {
                if ($this->isSelectedKingInCheckOrthogonally($kingPos, $r, $kingPos[1], $pieceMatrix, $matchDataStore)) 
                {
                    return true;
                }
                $isBlocked = true;
            }
        }

        $isBlocked = false;
        for ($c = $kingPos[1] - 1; $c >= 0; $c--) 
        {
            if (!$isBlocked && $pieceMatrix[$kingPos[0]][$c]->Name !== Piece::None) 
            {
                if ($this->isSelectedKingInCheckOrthogonally($kingPos, $kingPos[0], $c, $pieceMatrix, $matchDataStore)) 
                {
                    return true;
                }
                $isBlocked = true;
            }
        }

        $isBlocked = false;
        for ($c = $kingPos[1] + 1; $c < 8; $c++) 
        {
            if (!$isBlocked && $pieceMatrix[$kingPos[0]][$c]->Name !== Piece::None) 
            {
                if ($this->isSelectedKingInCheckOrthogonally($kingPos, $kingPos[0], $c, $pieceMatrix, $matchDataStore)) 
                {
                    return true;
                }
                $isBlocked = true;
            }
        }

        $isBlocked = false;
        for ($r = $kingPos[0] - 1, $c = $kingPos[1] - 1; $r >= 0 && $c >= 0; $r--, $c--) 
        {
            if (!$isBlocked && $pieceMatrix[$r][$c]->Name !== Piece::None) 
            {
                if ($this->isSelectedKingInCheckDiagonally($kingPos, $r, $c, $pieceMatrix, $matchDataStore)) 
                {
                    return true;
                }
                $isBlocked = true;
            }
        }

        $isBlocked = false;
        for ($r = $kingPos[0] - 1, $c = $kingPos[1] + 1; $r >= 0 && $c < 8; $r--, $c++) 
        {
            if (!$isBlocked && $pieceMatrix[$r][$c]->Name !== Piece::None) 
            {
                if ($this->isSelectedKingInCheckDiagonally($kingPos, $r, $c, $pieceMatrix, $matchDataStore)) 
                {
                    return true;
                }
                $isBlocked = true;
            }
        }

        $isBlocked = false;
        for ($r = $kingPos[0] + 1, $c = $kingPos[1] - 1; $r < 8 && $c >= 0; $r++, $c--) 
        {
            if (!$isBlocked && $pieceMatrix[$r][$c]->Name !== Piece::None)
            {
                if ($this->isSelectedKingInCheckDiagonally($kingPos, $r, $c, $pieceMatrix, $matchDataStore)) 
                {
                    return true;
                }
                $isBlocked = true;
            }
        }

        $isBlocked = false;
        for ($r = $kingPos[0] + 1, $c = $kingPos[1] + 1; $r < 8 && $c < 8; $r++, $c++) 
        {
            if (!$isBlocked && $pieceMatrix[$r][$c]->Name !== Piece::None) 
            {
                if ($this->isSelectedKingInCheckDiagonally($kingPos, $r, $c, $pieceMatrix, $matchDataStore)) 
                {
                    return true;
                }
                $isBlocked = true;
            }
        }

        return false;
    }

    public static function isNotSameColor(array $from, array $to, array $matrix) : bool
    {
        return $matrix[$from[0]][$from[1]]->Color !== $matrix[$to[0]][$to[1]]->Color;
    }

    private function isKnightCheckingKing(array $kingPos, MatchDataStore $matchDataStore,
        array $pieceMatrix) : bool 
    {
        for ($r = 0; $r < 8; $r++) 
        {
            for ($c = 0; $c < 8; $c++) 
            {
                $piece = $pieceMatrix[$r][$c];

                if ($piece->Name === Piece::Knight && $piece->Color !== Side::None &&
                    $piece->Color !== $pieceMatrix[$kingPos[0]][$kingPos[1]]->Color &&
                    $this->KnightMove->isMoveLegal([$r, $c], $kingPos, $matchDataStore)) 
                {
                    return true;
                }
            }
        }
        return false;
    }

    private static function isPawnCheckingKing(array $kingPos, array $pieceMatrix) : bool 
    {
        $kingColor = $pieceMatrix[$kingPos[0]][$kingPos[1]]->Color;

        return ($kingColor === Side::White)
            ? self::isPawnCheckingKingFromAbove($kingPos, $pieceMatrix)
            : self::isPawnCheckingKingFromBelow($kingPos, $pieceMatrix);
    }

    private static function isPawnCheckingKingFromAbove(array $kingPos, array $pieceMatrix) : bool
    {
        $kingColor = $pieceMatrix[$kingPos[0]][$kingPos[1]]->Color;

        if ($kingPos[0] === 0) 
        {
            return false;
        }

        $left = ($kingPos[1] > 0) ? $pieceMatrix[$kingPos[0] - 1][$kingPos[1] - 1] : null;
        $right = ($kingPos[1] < 7) ? $pieceMatrix[$kingPos[0] - 1][$kingPos[1] + 1] : null;

        if ($left !== null && $left->Name === Piece::Pawn && $left->Color !== $kingColor) 
        {
            return true;
        }

        if ($right !== null && $right->Name === Piece::Pawn && $right->Color !== $kingColor) 
        {
            return true;
        }

        return false;
    }

    private static function isPawnCheckingKingFromBelow(array $kingPos, array $pieceMatrix) : bool
    {
        $kingColor = $pieceMatrix[$kingPos[0]][$kingPos[1]]->Color;

        if ($kingPos[0] === 7) 
        {
            return false;
        }

        $left = ($kingPos[1] > 0) ? $pieceMatrix[$kingPos[0] + 1][$kingPos[1] - 1] : null;
        $right = ($kingPos[1] < 7) ? $pieceMatrix[$kingPos[0] + 1][$kingPos[1] + 1] : null;

        if ($left !== null && $left->Name === Piece::Pawn && $left->Color !== $kingColor) 
        {
            return true;
        }
        if ($right !== null && $right->Name === Piece::Pawn && $right->Color !== $kingColor) 
        {
            return true;
        }

        return false;
    }

    private function isSelectedKingInCheckOrthogonally(array $kingPos, int $r, int $c,
        array $pieceMatrix, MatchDataStore $matchDataStore) : bool 
    {
        if ($this->isNotSameColor($kingPos, [$r, $c], $pieceMatrix)) 
        {
            $piece = $pieceMatrix[$r][$c];

            $checkingFunction = match ($piece->Name) {
                Piece::Rook => [$this->RookMove, 'isMoveLegal'],
                Piece::Queen => [$this->QueenMove, 'isMoveLegal'],
                default => null
            };

            if ($checkingFunction !== null && $checkingFunction([$r, $c], $kingPos, $matchDataStore)) 
            {
                return true;
            }
        }

        return false;
    }

    private function isSelectedKingInCheckDiagonally(array $kingPos, int $r, int $c,
        array $pieceMatrix, MatchDataStore $matchDataStore) : bool 
    {
        if ($this->isNotSameColor($kingPos, [$r, $c], $pieceMatrix)) 
        {
            $piece = $pieceMatrix[$r][$c];

            $checkingFunction = match ($piece->Name) {
                Piece::Bishop => [$this->BishopMove, 'isMoveLegal'],
                Piece::Queen => [$this->QueenMove, 'isMoveLegal'],
                default => null
            };

            if ($checkingFunction !== null && $checkingFunction([$r, $c], $kingPos, $matchDataStore)) 
            {
                return true;
            }
        }

        return false;
    }

    public function isCheckMate(MatchDataStore $matchDataStore, callable $currentLegalMovesWithSelectedPiece) : bool 
    {
        $pieceMatrix = $matchDataStore->MatchState->PieceMatrix;

        $checkingSide = $this->checksChecker($matchDataStore, $pieceMatrix);

        if (!empty($checkingSide)) 
        {
            $attackerColor = $checkingSide[0];

            for ($r = 0; $r < 8; $r++) 
            {
                for ($c = 0; $c < 8; $c++) 
                {
                    if ($pieceMatrix[$r][$c]->Color === $attackerColor) 
                    {
                        $moves = $currentLegalMovesWithSelectedPiece($pieceMatrix, [$r, $c]);

                        foreach ($moves as $row) 
                        {
                            foreach ($row as $canMove) 
                            {
                                if ($canMove === true) 
                                {
                                    return false;
                                }
                            }
                        }
                    }
                }
            }

            return true;
        }

        return false;
    }
}