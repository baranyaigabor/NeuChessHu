<?php

namespace App\Http\Requests;

use Illuminate\Foundation\Http\FormRequest;

class StoreMatchesRequest extends FormRequest
{
    public function authorize(): bool
    {
        return true;
    }

    public function rules(): array
    {
        return [
            "match_id"=> ["required", "string"],
            "white_id"=> ["required", "string"],
            "black_id"=> ["required", "string"],
            "gamemode"=> ["required", "string"],
            "match_duration"=> ["required", "string"],
            
            "moves"=> ["nullable", "string"],
            "match_end_result" => ["required", "string"],
            "winner_id"=> ["nullable", "string"],
            "winner_time"=> ["nullable", "string"]
        ];
    }
}
