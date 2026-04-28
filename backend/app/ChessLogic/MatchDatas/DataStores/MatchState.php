<?php

namespace ChessLogic\MatchDatas\DataStores;

use ChessLogic\ChessBoard\ChessBoardFactory;
use ChessLogic\ChessBoard\ChessPieces\Definitions\Side;
use ChessLogic\ChessBoard\ChessPieces\Pieces\ChessPiece;
use ChessLogic\Notations\PieceNotations\SANNotationRow;

use JsonSerializable;

class MatchState implements JsonSerializable
{
    public ?string $MatchDuration;

    /** @var ChessPiece[][] */
    public array $PieceMatrix;

    public Side $CurrentSide = Side::White;

    /** @var SANNotationRow[] */
    public array $Notations = [];

    public bool $HasWKMoved = false;
    public bool $HasWRAMoved = false;
    public bool $HasWRHMoved = false;
    public bool $HasBKMoved = false;
    public bool $HasBRAMoved = false;
    public bool $HasBRHMoved = false;

    /** @var array{0:int,1:int}|null */
    public ?array $EnPassantTarget = null;

    public function __construct()
    {
        $this->PieceMatrix = ChessBoardFactory::boardFiller();
    }
       
    public function jsonSerialize() : array
    {
        return [
            'matchDuration' => $this->MatchDuration,
            'currentSide' => $this->CurrentSide->value,
            
            'pieceMatrix' => array_map(fn($row) => array_map(
                    fn($piece) => $piece ? $this->pieceToString($piece) : null,
                    $row
                ),
                $this->PieceMatrix
            ),

            'notations' => array_map(fn($notation) => $this->notationsToArray($notation),
                $this->Notations
            ),

            'hasWKMoved' => $this->HasWKMoved,
            'hasWRAMoved' => $this->HasWRAMoved,
            'hasWRHMoved' => $this->HasWRHMoved,
            'hasBKMoved' => $this->HasBKMoved,
            'hasBRAMoved' => $this->HasBRAMoved,
            'hasBRHMoved' => $this->HasBRHMoved,

            'enPassantTarget' => $this->EnPassantTarget,
        ];
    }

    private function pieceToString(ChessPiece $piece) : string
    {
        return $piece->Name->value . $piece->Color->value;
    }

    private function notationsToArray(SANNotationRow $row) : array
    {
        return [
            'round' => $row->Round,
            'white' => $row->WhitesNotation,
            'black' => $row->BlacksNotation,
        ];
    }

    public static function fromArray(array $data) : MatchState
    {
        $state = new MatchState();

        $state->MatchDuration = $data['matchDuration'] ?? '3 | 2';
        $state->CurrentSide = Side::from($data['currentSide']);
        $state->HasWKMoved = $data['hasWKMoved'] ?? false;
        $state->HasWRAMoved = $data['hasWRAMoved'] ?? false;
        $state->HasWRHMoved = $data['hasWRHMoved'] ?? false;
        $state->HasBKMoved = $data['hasBKMoved'] ?? false;
        $state->HasBRAMoved = $data['hasBRAMoved'] ?? false;
        $state->HasBRHMoved = $data['hasBRHMoved'] ?? false;
        $state->EnPassantTarget = $data['enPassantTarget'] ?? null;

        $state->PieceMatrix = array_map(fn($row) => array_map(
                fn($piece) => $piece ? ChessPiece::fromString($piece) : null,
                $row
            ),
            $data['pieceMatrix']
        );

        $state->Notations = array_map(fn($row) => SANNotationRow::fromArray($row),
            $data['notations'] ?? []
        );

        return $state;
    }
}