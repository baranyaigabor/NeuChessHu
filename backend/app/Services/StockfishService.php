<?php

namespace App\Services;

use ChessLogic\ChessBoard\ChessPieces\Definitions\Piece;
use ChessLogic\ChessBoard\ChessPieces\Definitions\Side;
use ChessLogic\ChessBoard\ChessPieces\Pieces\ChessPiece;
use ChessLogic\MatchDatas\MatchDataStore;
use RuntimeException;

class StockfishService
{
    /**
     * @return array{uci:string,from:array{0:int,1:int},to:array{0:int,1:int},promotionChoice:Piece}
     */
    public function bestMove(MatchDataStore $store, int $depth) : array
    {
        $response = $this->postJson('/bestmove', [
            'fen' => $this->toFen($store),
            'depth' => $depth,
        ]);

        $move = $response['move'] ?? null;

        if (!is_string($move)) 
        {
            throw new RuntimeException('Stockfish response did not include a move.');
        }

        return $this->uciToMove($move);
    }

    public function toFen(MatchDataStore $store) : string
    {
        return implode(' ', [
            $this->pieceMatrixToFen($store->MatchState->PieceMatrix),
            $store->MatchState->CurrentSide === Side::Black ? 'b' : 'w',
            $this->castlingRights($store),
            $this->enPassantSquare($store),
            (string)$store->DrawTrackers->ConsecutiveMoves,
            (string)$this->fullMoveNumber($store),
        ]);
    }

    /**
     * @return array{uci:string,from:array{0:int,1:int},to:array{0:int,1:int},promotionChoice:Piece}
     */
    public function uciToMove(string $move) : array
    {
        if (!preg_match('/^[a-h][1-8][a-h][1-8][qrbn]?$/', $move)) 
        {
            throw new RuntimeException("Invalid Stockfish move: {$move}");
        }

        return [
            'uci' => $move,
            'from' => $this->squareToCoordinates(substr($move, 0, 2)),
            'to' => $this->squareToCoordinates(substr($move, 2, 2)),
            'promotionChoice' => strlen($move) === 5
                ? $this->promotionChoice($move[4])
                : Piece::None,
        ];
    }

    /**
     * @param ChessPiece[][] $board
     */
    public function pieceMatrixToFen(array $board) : string
    {
        $ranks = [];

        foreach ($board as $rank) 
        {
            $empty = 0;
            $fenRank = '';

            foreach ($rank as $piece) 
            {
                if ($piece->Name === Piece::None) 
                {
                    $empty++;
                    continue;
                }

                if ($empty > 0)
                {
                    $fenRank .= (string)$empty;
                    $empty = 0;
                }

                $fenRank .= $this->pieceToFen($piece);
            }

            if ($empty > 0) 
            {
                $fenRank .= (string)$empty;
            }

            $ranks[] = $fenRank;
        }

        return implode('/', $ranks);
    }

    /**
     * @return array{0:int,1:int}
     */
    public function squareToCoordinates(string $square) : array
    {
        return [
            8 - (int)$square[1],
            ord($square[0]) - ord('a'),
        ];
    }

    public function coordinatesToSquare(int $row, int $column) : string
    {
        return chr(ord('a') + $column) . (string)(8 - $row);
    }

    private function pieceToFen(ChessPiece $piece) : string
    {
        $letter = match ($piece->Name) {
            Piece::Pawn => 'p',
            Piece::Knight => 'n',
            Piece::Bishop => 'b',
            Piece::Rook => 'r',
            Piece::Queen => 'q',
            Piece::King => 'k',
            Piece::None => '',
        };

        return $piece->Color === Side::White ? strtoupper($letter) : $letter;
    }

    private function castlingRights(MatchDataStore $store) : string
    {
        $rights = '';
        $board = $store->MatchState->PieceMatrix;

        if (!$store->MatchState->HasWKMoved) 
        {
            if (!$store->MatchState->HasWRHMoved && $this->hasRook($board, 7, 7, Side::White)) 
            {
                $rights .= 'K';
            }

            if (!$store->MatchState->HasWRAMoved && $this->hasRook($board, 7, 0, Side::White)) 
            {
                $rights .= 'Q';
            }
        }

        if (!$store->MatchState->HasBKMoved) 
        {
            if (!$store->MatchState->HasBRHMoved && $this->hasRook($board, 0, 7, Side::Black)) 
            {
                $rights .= 'k';
            }

            if (!$store->MatchState->HasBRAMoved && $this->hasRook($board, 0, 0, Side::Black)) 
            {
                $rights .= 'q';
            }
        }

        return $rights === '' ? '-' : $rights;
    }

    /**
     * @param ChessPiece[][] $board
     */
    private function hasRook(array $board, int $row, int $column, Side $side): bool
    {
        $piece = $board[$row][$column] ?? null;

        return $piece instanceof ChessPiece
            && $piece->Name === Piece::Rook
            && $piece->Color === $side;
    }

    private function enPassantSquare(MatchDataStore $store): string
    {
        $target = $store->MatchState->EnPassantTarget;

        return $target === null 
            ? '-' 
            : $this->coordinatesToSquare($target[0], $target[1]);
    }

    private function fullMoveNumber(MatchDataStore $store): int
    {
        $notationRows = count($store->MatchState->Notations);

        return $store->MatchState->CurrentSide === Side::Black
            ? max(1, $notationRows)
            : $notationRows + 1;
    }

    private function promotionChoice(string $piece): Piece
    {
        return match ($piece) {
            'q' => Piece::Queen,
            'r' => Piece::Rook,
            'b' => Piece::Bishop,
            'n' => Piece::Knight,
        };
    }

    private function postJson(string $path, array $payload) : array
    {
        $url = rtrim((string)config('services.stockfish.url', 'http://stockfish:8001'), '/') . $path;
        $body = json_encode($payload);

        if ($body === false) 
        {
            throw new RuntimeException('Could not encode Stockfish request.');
        }

        $context = stream_context_create([
            'http' => [
                'method' => 'POST',
                'header' => "Content-Type: application/json\r\n",
                'content' => $body,
                'timeout' => 10,
                'ignore_errors' => true,
            ],
        ]);

        $response = @file_get_contents($url, false, $context);

        if ($response === false) 
        {
            throw new RuntimeException('Stockfish service is unavailable.');
        }

        $data = json_decode($response, true);

        if (!is_array($data)) 
        {
            throw new RuntimeException('Stockfish service returned invalid JSON.');
        }

        if (isset($data['error'])) 
        {
            throw new RuntimeException((string)$data['error']);
        }

        return $data;
    }
}