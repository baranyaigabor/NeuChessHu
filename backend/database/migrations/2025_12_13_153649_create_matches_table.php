<?php

use Illuminate\Database\Migrations\Migration;
use Illuminate\Database\Schema\Blueprint;
use Illuminate\Support\Facades\Schema;

return new class extends Migration
{
    public function up(): void
    {
        Schema::create('matches', function (Blueprint $table) {
            $table->string("match_id")->primary();
            
            $table->foreignId('white_id')->constrained('users');
            $table->foreignId('black_id')->constrained('users');

            $table->string("gamemode");
            $table->string('match_duration');
            $table->dateTime("played_at");

            $table->json("moves")->nullable();
            $table->string("match_end_result");
            $table->string("winner_id")->nullable();
            $table->string("winner_time")->nullable();
        });
    }
    
    public function down(): void
    {
        Schema::dropIfExists('matches');
    }
};
