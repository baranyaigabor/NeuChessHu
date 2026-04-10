<?php

namespace App\Http\Requests;

use App\Rules\Base64Image;
use App\Rules\ClamAVScan;
use Illuminate\Foundation\Http\FormRequest;

class UpdateUserRequest extends FormRequest
{
    public function authorize(): bool
    {
        return true;
    }

    public function rules(): array
    {
        return [
            'nickname' => 'sometimes|string|unique:users,nickname,' . $this->route('user')->id,
            'email' => 'sometimes|email|unique:users,email,' . $this->route('user')->id,
            'first_name' => 'sometimes|string',
            'last_name' => 'sometimes|string',
            'region' => 'sometimes|string',
            'date_of_birth' => 'sometimes|date',
            'profile_picture' => ['sometimes', 'string', new Base64Image(), new ClamAVScan()],
        ];
    }
}