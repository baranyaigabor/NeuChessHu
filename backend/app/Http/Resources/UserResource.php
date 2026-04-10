<?php

namespace App\Http\Resources;

use Illuminate\Http\Request;
use Illuminate\Http\Resources\Json\JsonResource;

class UserResource extends JsonResource
{
    public function toArray(Request $request): array
    {
        return[
            "nickname" => $this->nickname,
            "profile_picture" => $this->profile_picture ?? "Unknown",
            "email" => $this->email,
            "full_name" => $this->first_name . " " . $this->last_name ?? "Unknown",
            "region" => $this->region ?? "Unknown",
            "date_of_birth" => $this->date_of_birth ?? "Unknown",
            "is_active" => $this->is_active ? "Online" : "Offline",
            "white_matches" => MatchesResource::collection($this->whenLoaded("whiteMatches")),
            "black_matches" => MatchesResource::collection($this->whenLoaded("blackMatches")),
            "created_at" => $this->created_at,
            "updated_at" => $this->updated_at,
            #"tournaments" => TournamentResource::collection($this->whenLoaded("tournaments"))
        ];
    }
}
