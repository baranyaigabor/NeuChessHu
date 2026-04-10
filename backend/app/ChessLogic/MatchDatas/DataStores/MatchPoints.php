<?php

namespace ChessLogic\MatchDatas\DataStores;

use JsonSerializable;

class MatchPoints implements JsonSerializable
{
    public bool $ClaimForDraw = false;
    public bool $ForcedDraw = false;
    public bool $MatchEnded = false;

    private ?string $matchPointsReason = null;

    public ?string $WinnerID = null;
    public ?string $WinnerTime = null;

    public function setMatchPointsReason(string $reason, bool $isForcedDraw) : void
    {
        $this->ForcedDraw = $isForcedDraw;
        $this->matchPointsReason = $reason;
    }

    public function getMatchPointsReason() : ?string
    {
        return $this->matchPointsReason;
    }

    public function shouldEndMatch() : bool
    {
        if ($this->matchPointsReason === null) {
            return false;
        }

        return $this->ForcedDraw
            || $this->matchPointsReason === 'Checkmate'
            || $this->matchPointsReason === 'Timeout';
    }

    public function markMatchEnded() 
    {
        $this->MatchEnded = true;
    }

    public function setWinner(string $playerID, string $time)
    {
        $this->WinnerID = $playerID;
        $this->WinnerTime = $time;
    }

    public function jsonSerialize() : array
    {
        return [
            'claimForDraw' => $this->ClaimForDraw,
            'forcedDraw' => $this->ForcedDraw,
            'matchPointsReason' => $this->matchPointsReason,
            'matchEnded' => $this->MatchEnded,
            'winnerID' => $this->WinnerID,
            'winnerTime' => $this->WinnerTime
        ];
    }

    public static function fromArray(array $data) 
    {
        $instance = new self();

        $instance->ClaimForDraw = $data['claimForDraw'] ?? false;
        $instance->ForcedDraw = $data['forcedDraw'] ?? false;
        $instance->matchPointsReason = $data['matchPointsReason'] ?? null;
        $instance->MatchEnded = $data['matchEnded'] ?? false;
        $instance->WinnerID = $data['winnerID'] ?? null;
        $instance->WinnerTime = $data['winnerTime'] ?? null;

        return $instance;
    }
}