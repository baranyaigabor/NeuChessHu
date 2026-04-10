<?php

namespace App\Http\Requests;

use Illuminate\Foundation\Http\FormRequest;

class UpdateMatchesRequest extends FormRequest
{
    public function authorize(): bool
    {
        return true;
    }

    public function rules(): array
    {
        return [
            "match_id"=> ["string"],
            "white_id"=> ["string"],
            "black_id"=> ["string"],
            "game_mode"=> ["string"],
            "match_duration"=> ["string"],
            
            "moves"=> ["nullable", "string"],
            "match_end_result" => ["required", "string"],
            "winner_id"=> ["nullable", "string"],
            "winner_time"=> ["nullable", "string"]
        ];
    }
}
