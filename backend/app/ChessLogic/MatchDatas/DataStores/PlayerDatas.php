<?php

namespace ChessLogic\MatchDatas\DataStores;

use JsonSerializable;

class PlayerDatas implements JsonSerializable
{
    public string $ID;
    public array $CapturedPieces = [];
    public int $Points = 0;
    public string $Time = "";

    public function jsonSerialize() : array
    {
        return [
            'id' => $this->ID,
            'capturedPieces' => array_map(fn($piece) => $piece->value, $this->CapturedPieces),
            'points' => $this->Points
        ];
    }

    public static function fromArray(array $data, string $id) : PlayerDatas
    {
        $player = new PlayerDatas();
        $player->ID = $id;
        $player->Points = $data['points'] ?? 0;
        $player->CapturedPieces = $data['capturedPieces'] ?? [];
        return $player;
    }
}