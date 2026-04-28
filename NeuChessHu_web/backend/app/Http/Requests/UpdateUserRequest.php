<?php

namespace App\Http\Requests;

use App\Rules\Base64ImageRule;
use App\Rules\ClamAVScanRule;
use Illuminate\Foundation\Http\FormRequest;
use Illuminate\Support\Facades\Gate;

class UpdateUserRequest extends FormRequest
{
    public function authorize(): bool
    {
        return Gate::allows('update-user', $this->route('user'));
    }

    public function rules(): array
    {
        return [
            'nickname' => 'sometimes|string|unique:users,nickname,' . $this->route('user')->id,
            'email' => 'sometimes|email|unique:users,email,' . $this->route('user')->id,
            'first_name' => 'sometimes|nullable|string',
            'last_name' => 'sometimes|nullable|string',
            'region' => 'sometimes|nullable|string',
            'date_of_birth' => 'sometimes|nullable|date',
            'profile_picture' => ['sometimes', 'nullable', 'string', new Base64ImageRule(), new ClamAVScanRule()],
        ];
    }
}