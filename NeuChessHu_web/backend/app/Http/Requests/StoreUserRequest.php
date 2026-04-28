<?php

namespace App\Http\Requests;

use Illuminate\Foundation\Http\FormRequest;

class StoreUserRequest extends FormRequest
{
    public function authorize(): bool
    {
        return true;
    }

    public function rules(): array
    {
        return [
            "nickname"=> ["required", "string", "between:1,14"],
            'email' => ['required', 'email', 'max:255', 'unique:users,email'],
            "password"=> ["required", "string", "between:1,100"],
            "first_name"=> ["nullable", "string"],
            "last_name"=> ["nullable", "string"],
            "region"=> ["nullable", "string"],
            "date_of_birth"=> ["nullable", "date"]
        ];
    }
}
