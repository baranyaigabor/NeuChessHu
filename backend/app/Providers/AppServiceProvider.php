<?php

namespace App\Providers;

use App\Models\User;
use App\Services\MatchService;
use App\Services\ReadyPlayersRegistryService;
use ChessLogic\Messaging\ChatMessagesHandlerFactory;
use ChessLogic\Moving\Factories\MoveComponents\MoveComponentsFactory;
use ChessLogic\Moving\Factories\MoveFactory;
use ChessLogic\Moving\Factories\MoveValidatorFactory;
use Illuminate\Database\Eloquent\Model;
use Illuminate\Support\Facades\Gate;
use Illuminate\Support\ServiceProvider;

class AppServiceProvider extends ServiceProvider
{
    public function register(): void
    {        
        $this->app->singleton(ReadyPlayersRegistryService::class, fn() => new ReadyPlayersRegistryService());
        $this->app->singleton(MatchService::class, fn() => new MatchService());

        $this->app->singleton(MoveComponentsFactory::class);
        $this->app->singleton(MoveValidatorFactory::class);
        $this->app->singleton(MoveFactory::class);
        $this->app->singleton(ChatMessagesHandlerFactory::class);
    }

    public function boot(): void
    {
        Gate::define('manage-users', fn (User $user): bool => $user->role === 'admin');

        Gate::define('update-user', fn (User $user, User $targetUser) : bool =>
            $user->role === 'admin' || $user->is($targetUser));

        Gate::define('destroy-user', fn (User $user, User $targetUser) : bool => 
            $user->role === 'admin' || $user->is($targetUser));

        Gate::define('store-match', fn (User $user) : bool => $user->role === 'admin');

        Gate::define('logout', fn (User $user): bool => 
            in_array($user->role, ['user', 'admin'], true));

        Model::shouldBeStrict();
    }
}