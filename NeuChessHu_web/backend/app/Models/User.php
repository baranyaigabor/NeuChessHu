<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Casts\Attribute;
use Illuminate\Database\Eloquent\Relations\BelongsToMany;
use Illuminate\Foundation\Auth\User as Authenticatable;
use Laravel\Sanctum\HasApiTokens;

class User extends Authenticatable
{
    use HasApiTokens;

    protected $fillable = [
        "nickname",
        "email",
        "password",
        "first_name",
        "last_name",
        "region",
        "profile_picture",
        "date_of_birth",
        "is_active"
    ];

    protected $primaryKey = 'id';
    public $incrementing = true;
    protected $keyType = 'int';

    public function getRouteKeyName()
    {
        return 'nickname';
    }

    protected function profilePicture(): Attribute
    {
        return Attribute::make(
            get: fn($value) => $value ?: null,
        );
    }

    protected $hidden = [
        'password',
    ];

    protected function casts(): array
    {
        return ['password' => 'hashed'];
    }

    public function whiteMatches()
    {
        return $this->hasMany(Matches::class, 'white_id');
    }

    public function blackMatches()
    {
        return $this->hasMany(Matches::class, 'black_id');
    }
}