<?php

namespace ChessLogic\MatchDatas\Initializers\Extension;

use ChessLogic\ChessBoard\ChessPieces\Definitions\Side;

class SideRandomizerExtension
{
    /**
     * @param Side[] $sides
     * @return Side[]
     */
    public static function randomize(array $sides): array
    {
        $random = random_int(0, 20260430);

        if ($random % 2 === 0) {
            return array_reverse($sides);
        }

        return $sides;
    }
}