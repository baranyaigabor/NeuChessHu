<?php

namespace ChessLogic\MatchDatas\Initializers;

use ChessLogic\ChessBoard\ChessPieces\Definitions\Side;
use ChessLogic\MatchDatas\DataStores\PlayerDatas;
use ChessLogic\MatchDatas\Initializers\Extension\SideRandomizerExtension;

class MatchInitializer
{
    /**
     * @return array<Side, PlayerDatas>
     */
    public static function initializePlayers(string $ID1, string $ID2, ?Side $firstPlayerSide = null) : array 
    {
        $randomizedSides = $firstPlayerSide
            ? self::sidesWithFirstPlayer($firstPlayerSide)
            : self::randomizedSides();

        return [
            $randomizedSides[0]->value => self::createPlayer($ID1),
            $randomizedSides[1]->value => self::createPlayer($ID2),
        ];
    }

    private static function createPlayer(string $id) : PlayerDatas
    {
        $player = new PlayerDatas();
        $player->ID = $id;
        $player->CapturedPieces = [];
        $player->Points = 0;

        return $player;
    }

    /**
     * @return Side[]
     */
    private static function randomizedSides() : array
    {
        $sides = [Side::White, Side::Black];
        return SideRandomizerExtension::randomize($sides);
    }

    /**
     * @return Side[]
     */
    private static function sidesWithFirstPlayer(Side $firstPlayerSide) : array
    {
        return $firstPlayerSide === Side::White
            ? [Side::White, Side::Black]
            : [Side::Black, Side::White];
    }
}