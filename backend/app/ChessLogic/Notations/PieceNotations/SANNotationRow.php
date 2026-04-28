<?php

namespace ChessLogic\Notations\PieceNotations;

class SANNotationRow
{
    public function __construct(
        public readonly string $Round,
        public readonly string $WhitesNotation,
        public readonly ?string $BlacksNotation
    ) {}

    public static function fromArray(array $data): SANNotationRow
    {
        return new SANNotationRow(
            $data['round'],
            $data['white'],
            $data['black']
        );
    }
}