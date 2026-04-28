<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;

class Matches extends Model
{
    protected $fillable = [
        "match_id", 
        "white_id", 
        "black_id", 
        "gamemode", 
        'match_duration',
        "played_at", 
        "moves",
        "match_end_result",
        "winner_id", 
        "winner_time"
    ];

    protected $primaryKey = 'match_id';
    public $incrementing = false;
    protected $keyType = 'string';

    public $timestamps = false;

    public function getRouteKeyName()
    {
        return 'match_id';
    }

    protected $casts = [
        'moves' => 'array',
    ];

    public function white()
    {
        return $this->belongsTo(User::class, 'white_id');
    }

    public function black()
    {
        return $this->belongsTo(User::class, 'black_id');
    }
}