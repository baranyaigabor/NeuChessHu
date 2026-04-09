<?php

namespace ChessLogic\MatchDatas\DataStores;

class DrawTrackers
{
    public function __construct() {}
    public int $ConsecutiveMoves = 0;

    /** @var array<int,int> */
    public array $Repetitions = [];

    /** @var array<string, bool> */
    public array $DrawAgreements = [];

    public function drawResponseSetter(string $userID, bool $drawResponse) : void
    {
        $this->DrawAgreements[$userID] = $drawResponse;
    }

    public static function bothAgreedToDraw(self $drawTrackers) : bool
    {
        if(count($drawTrackers->DrawAgreements) == 2)
        {
            if(count(array_filter($drawTrackers->DrawAgreements)))
            {
                return true;
            }

            $drawTrackers->DrawAgreements = [];
        }

        return false;
    }

    public function jsonSerialize() : array
    {
        return [
            'consecutiveMoves' => $this->ConsecutiveMoves,
            'repetitions' => $this->Repetitions,
            'drawAgreements' => $this->DrawAgreements,
        ];
    }

    public static function fromArray(array $data) : self
    {
        $drawTrackers = new self();

        $drawTrackers->ConsecutiveMoves = $data['consecutiveMoves'] ?? 0;
        $drawTrackers->Repetitions = $data['repetitions'] ?? [];
        $drawTrackers->DrawAgreements = $data['drawAgreements'] ?? [];

        return $drawTrackers;
    }
}