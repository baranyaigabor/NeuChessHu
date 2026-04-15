<?php

namespace ChessLogic\Moving;

use ChessLogic\ChessBoard\ChessPieces\Definitions\Piece;
use ChessLogic\ChessBoard\ChessPieces\Definitions\Side;
use ChessLogic\ChessBoard\ChessPieces\Pieces\ChessPiece;
use ChessLogic\MatchDatas\MatchDataStore;
use ChessLogic\Moving\Validators\Moves\CheckRules\CheckValidator;
use ChessLogic\Moving\Validators\Moves\CheckRules\Extension\MatrixCloner;
use ChessLogic\Moving\Validators\Moves\MoveValidator;
use ChessLogic\Moving\Validators\Draws\DrawValidator;
use ChessLogic\Moving\Captures\CapturedListsHandler;

class Move
{
    public function __construct(
        private MatchDataStore $matchDataStore,
        private CheckValidator $checks,
        private MoveValidator $moveValidator,
        private DrawValidator $drawValidator
    ) {}

    /**
     * @param array{0:int,1:int} $from
     * @param array{0:int,1:int} $to
     */
    public function movePiece(array $from, array $to, Piece $promotionChoice) : array 
    {
        $pieceMatrix = $this->matchDataStore->MatchState->PieceMatrix;
        $startingMatrix = MatrixCloner::clone($pieceMatrix);

        $fromPiece = $pieceMatrix[$from[0]][$from[1]];
        $toPiece = $pieceMatrix[$to[0]][$to[1]];

        $currentSide = $this->matchDataStore->MatchState->CurrentSide;

        $isEnPassant = $this->isEnPassantMove($from, $to);

        $hasCaptured = $isEnPassant;
        $hasCastled = '';
        $isCheck = false;
        $isCheckMate = false;

        self::capture($pieceMatrix, $toPiece, $to, $hasCaptured);

        self::promotion($pieceMatrix, $fromPiece, $from, $to, $promotionChoice);

        $this->enPassant($pieceMatrix, $currentSide, $fromPiece, $from, $to, $isEnPassant);

        $this->castle($pieceMatrix, $fromPiece, $from, $to, $hasCastled);

        $this->matchDataStore->MatchState->PieceMatrix = $pieceMatrix;

        $this->matchDataStore->swapPieces($from, $to);

        $this->checkAndDrawValidators($fromPiece, $hasCaptured, 
            $hasCastled, $isCheck, $isCheckMate);

        $this->validMove( $toPiece, $from, $to, $startingMatrix, 
            $hasCaptured, $hasCastled, $isCheck, $isCheckMate, 
            $isEnPassant, $currentSide, $promotionChoice);

        MatchDataStore::handleMatchEnd($this->matchDataStore);

        $this->matchDataStore->switchSide();

        return [
            'match_state' => $this->matchDataStore->MatchState->jsonSerialize(),
            'player_datas' => $this->matchDataStore->serializePlayerDatas(),
            'match_points' => $this->matchDataStore->MatchPoints->jsonSerialize(),
            'sound' => $this->determineSoundName($isCheck, $isCheckMate, $hasCaptured,
                $hasCastled, $promotionChoice)
        ]; 
    }

    private function isEnPassantMove(array $from, array $to): bool
    {
        $enPassantTarget = $this->matchDataStore->MatchState->EnPassantTarget;
        $piece = $this->matchDataStore->MatchState->PieceMatrix[$from[0]][$from[1]];

        return $piece->Name === Piece::Pawn
            && $enPassantTarget !== null
            && $to[0] === $enPassantTarget[0]
            && $to[1] === $enPassantTarget[1];
    }

    private static function capture(array &$pieceMatrix, $toPiece, array $to, bool &$hasCaptured) : void 
    {
        if ($toPiece->Name !== Piece::None) {
            $pieceMatrix[$to[0]][$to[1]] = ChessPiece::create(Piece::None, Side::None);
            $hasCaptured = true;
        }
    }

    private static function promotion(array &$pieceMatrix, $fromPiece, array $from,
        array $to, Piece $promotionChoice): void 
    {
        if ($fromPiece->Name === Piece::Pawn && ($to[0] === 0 || $to[0] === 7)) 
        {
            $pieceMatrix[$from[0]][$from[1]] =
                ChessPiece::create($promotionChoice, $fromPiece->Color);
        }
    }

    private function enPassant(array &$pieceMatrix, Side $currentSide,
        $fromPiece, array $from, array $to, bool $isEnPassant) : void
    {
        if ($fromPiece->Name === Piece::Pawn && abs($from[0] - $to[0]) === 2) 
        {
            $this->matchDataStore->setEnPassantTarget([
                (int)(($from[0] + $to[0]) / 2), $to[1]]);
        } 

        elseif ($this->matchDataStore->MatchState->EnPassantTarget !== null) 
        {
            $this->matchDataStore->setEnPassantTarget(null);
        } 

        if ($isEnPassant) 
        {
            $this->enPassantMove( $pieceMatrix,$currentSide, $fromPiece, $to);
        }
    }

    private function enPassantMove(array &$pieceMatrix, Side $currentSide, $piece, array $to ): void 
    {
        if ($piece->Color === Side::White) 
        {
            $pieceMatrix[$to[0] + 1][$to[1]] = ChessPiece::create(Piece::None, Side::None);
        } 
        else 
        {
            $pieceMatrix[$to[0] - 1][$to[1]] = ChessPiece::create(Piece::None, Side::None);
        }
    }

    private function castle(array &$pieceMatrix, $fromPiece,
        array $from, array $to, string &$hasCastled): void 
    {
        if ($fromPiece->Name === Piece::King) 
        {
            $hasCastled = $this->kingMoved($pieceMatrix, $from, $to);
        }
        if ($fromPiece->Name === Piece::Rook) 
        {
            $this->rookMoved($pieceMatrix, $from);
        }
    }

    private function kingMoved(array &$pieceMatrix, array $from, array $to): string 
    {
        if ($pieceMatrix[$from[0]][$from[1]]->Color === Side::White)
        {
            $this->matchDataStore->MatchState->HasWKMoved = true;    
        }
        else 
        {
            $this->matchDataStore->MatchState->HasBKMoved = true;
        }

        if (abs($from[0] - $to[0]) === 0 && ($from[1] - $to[1] === 2)) 
        {
            $tempPiece = clone $pieceMatrix[$to[0]][$to[1] - 2]; 

            if (($tempPiece->Color === Side::White && !$this->matchDataStore->MatchState->HasWRAMoved) ||
                ($tempPiece->Color === Side::Black && !$this->matchDataStore->MatchState->HasBRAMoved)) 
            {
                $pieceMatrix[$to[0]][$to[1] - 2] = ChessPiece::create(Piece::None, Side::None);
                $pieceMatrix[$to[0]][$to[1] + 1] = $tempPiece;
                return "QueenSide";
            }
        }

        if (abs($from[0] - $to[0]) === 0 && ($to[1] - $from[1] === 2)) 
        {
            $tempPiece = clone $pieceMatrix[$to[0]][$to[1] + 1];

            if (($tempPiece->Color === Side::White && !$this->matchDataStore->MatchState->HasWRHMoved) ||
                ($tempPiece->Color === Side::Black && !$this->matchDataStore->MatchState->HasBRHMoved)) 
            {
                $pieceMatrix[$to[0]][$to[1] + 1] = ChessPiece::create(Piece::None, Side::None);
                $pieceMatrix[$to[0]][$to[1] - 1] = $tempPiece; 
                return "KingSide";
            }
        }

        return "";
    }

    private function rookMoved(array &$pieceMatrix, array $from): void 
    {
        if ($from[1] === 0) 
        {
            if ($pieceMatrix[$from[0]][$from[1]]->Color === Side::White) 
            {
                $this->matchDataStore->MatchState->HasWRAMoved = true;
            } 
            else
            {
                $this->matchDataStore->MatchState->HasBRAMoved = true;
            }   
        }

        if ($from[1] === 7) 
        {
            if ($pieceMatrix[$from[0]][$from[1]]->Color === Side::White) 
            {
                $this->matchDataStore->MatchState->HasWRHMoved = true;
            } 
            else 
            {
                $this->matchDataStore->MatchState->HasBRHMoved = true;
            }
        }
    }

    private function checkAndDrawValidators(ChessPiece $fromPiece, bool $hasCaptured,
        string $hasCastled, bool &$isCheck, bool &$isCheckMate): void 
    {
        if ($this->checks->isCheckMate($this->matchDataStore,
            [$this->moveValidator, 'currentLegalMovesWithSelectedPiece'])) 
        {
            $this->matchDataStore->MatchPoints->setMatchPointsReason("Checkmate", false);
            $isCheckMate = true;
        }
        else 
        {
            $this->drawValidator->isDrawn([$this->moveValidator, 'currentLegalMovesWithSelectedPiece'],
                $fromPiece, $hasCaptured, $hasCastled);
        }

        if (!empty($this->checks->checksChecker($this->matchDataStore, $this->matchDataStore->MatchState->PieceMatrix)))
        {
            $isCheck = true;
        }
    }

    private function validMove(ChessPiece $toPiece, array $from, array $to, array $startingMatrix, 
        bool $hasCaptured, string $hasCastled, bool $isCheck, bool $isCheckMate, 
        bool $isEnPassant, Side $currentSide, Piece $promotionChoice): void 
    {
        if ($toPiece->Name !== Piece::None) 
        {
            CapturedListsHandler::appendList($this->matchDataStore, $currentSide, $toPiece->Name);
        }
        
        else if($isEnPassant)
        {
            CapturedListsHandler::appendList($this->matchDataStore, $currentSide, Piece::Pawn);
        }
    }

    private function determineSoundName(bool $isCheck, bool $isCheckMate,
        bool $hasCaptured, string $hasCastled, Piece $promotionChoice): string
    {
        if ($isCheck && !$isCheckMate) 
        {
            return 'Check';
        }

        if ($hasCastled !== '') 
        {
            return 'Castle';
        }

        if ($promotionChoice != Piece::None)
        {
            return 'Promotion';
        }

        if ($hasCaptured) 
        {
            return 'Capture';
        }

        return 'Move';
    }
}