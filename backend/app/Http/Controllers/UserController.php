<?php

namespace App\Http\Controllers;

use App\Http\Requests\StoreUserRequest;
use App\Http\Requests\UpdateUserRequest;
use App\Http\Resources\UserResource;
use App\Models\User;
use Illuminate\Support\Facades\Auth;

class UserController
{
    public function index()
    {
        $users = User::select('id', 'nickname', 'email', 'first_name', 'last_name',
            'region', 'date_of_birth', 'is_active', 'created_at', 'updated_at' )->get();

        return UserResource::collection($users->load(["whiteMatches", "blackMatches"]));
    }

    public function store(StoreUserRequest $request)
    {
        $data = $request->validated();
        $user = User::create($data);
        return new UserResource($user);
    }

    public function show(string $identifier)
    {
        $user = User::where('role', 'user')->where(function ($query) use ($identifier) 
        {
            $query->where('nickname', $identifier);

            if (is_numeric($identifier)) {
                $query->orWhere('id', (int) $identifier);
            }
        })->firstOrFail();
    
        return new UserResource($user->load(["whiteMatches", "blackMatches"]));
    }

    public function showCurrent()
    {
        $user = Auth::user();

        return new UserResource($user);
    }

    public function update(UpdateUserRequest $request, User $user)
    {
        $data = $request->validated();

        $user->update($data);

        return new UserResource($user);
    }

    public function destroy(User $user)
    {
        return $user->delete() ? response()->noContent() : abort(500);
    }
}