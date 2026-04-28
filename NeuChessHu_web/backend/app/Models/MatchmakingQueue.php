<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;

class MatchmakingQueue extends Model
{
    protected $fillable = [
        'player_id',
        'match_duration',
        'joined_at',
    ];

    public $timestamps = false;
}