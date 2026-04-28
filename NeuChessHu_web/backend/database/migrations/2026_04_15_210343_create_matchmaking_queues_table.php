<?php

use Illuminate\Database\Migrations\Migration;
use Illuminate\Database\Schema\Blueprint;
use Illuminate\Support\Facades\Schema;

return new class extends Migration
{
    public function up()
    {
        Schema::create('matchmaking_queues', function (Blueprint $table) {
            $table->id();
            $table->string('player_id')->unique();
            $table->string('match_duration');
            $table->timestamp('joined_at')->useCurrent();
        });
    }
    
    public function down(): void
    {
        Schema::dropIfExists('matchmaking_queues');
    }
};
