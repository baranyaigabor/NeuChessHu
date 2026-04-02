<?php

namespace App\Providers;

use App\Services\MatchService;
use App\Services\ReadyPlayersRegistry;
use ChessLogic\Moving\Factories\MoveComponents\MoveComponentsFactory;
use ChessLogic\Moving\Factories\MoveFactory;
use ChessLogic\Moving\Factories\MoveValidatorFactory;
use Illuminate\Database\Eloquent\Model;
use Illuminate\Support\ServiceProvider;

class AppServiceProvider extends ServiceProvider
{
    public function register(): void
    {        
        //
    }

    public function boot(): void
    {
        //
    }
}