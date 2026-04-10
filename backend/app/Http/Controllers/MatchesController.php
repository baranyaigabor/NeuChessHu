<?php

namespace App\Http\Controllers;

use App\Http\Requests\StoreMatchesRequest;
use App\Http\Requests\UpdateMatchesRequest;
use App\Http\Resources\MatchesResource;
use App\Models\Matches;

class MatchesController
{
    public function index()
    {
        return MatchesResource::collection(Matches::all());
    }

    public function store(StoreMatchesRequest $request)
    {
        $data = $request->validated();
        $matches = Matches::create($data);
        return new MatchesResource($matches);
    }

    public function show(Matches $match)
    {
        return new MatchesResource($match);
    }

    public function update(UpdateMatchesRequest $request, Matches $match)
    {
        $data = $request->validated();
        $match->update($data);
        return new MatchesResource($match);
    }

    public function destroy(Matches $match)
    {
        return $match->delete() ? response()->noContent() : abort(500);
    }
}
