<?php

namespace App\Http\Requests;

use Illuminate\Foundation\Http\FormRequest;

class QueueRequest extends FormRequest
{
    public function authorize(): bool
    {
        return true;
    }

    public function rules(): array
    {
        return [
            "playerID" => ["required", "integer", "exists:users,id"],
            "matchDuration" => ["required", "string"]
        ];
    }
}
