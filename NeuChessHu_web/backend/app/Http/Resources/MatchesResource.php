<?php

namespace App\Http\Resources;

use Illuminate\Http\Request;
use Illuminate\Http\Resources\Json\JsonResource;

class MatchesResource extends JsonResource
{
    public function toArray(Request $request): array
    {
        return[
            "match_id" => $this->match_id,
            'white_id' => $this->white_id,
            'black_id' => $this->black_id,
            
            "gamemode" => $this->gamemode,
            "match_durations" => $this->match_duration,
            'played_at' => $this->played_at,

            "moves"=> $this->moves ?? "Unknown",
            "match_end_result" => $this->match_end_result,
            "winner_id"=> $this->winner_id ?? "Unknown",
            "winner_time"=> $this->winner_time ?? "Unknown"
        ];
    }
}