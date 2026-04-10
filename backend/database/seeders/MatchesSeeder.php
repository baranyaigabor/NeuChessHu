<?php

namespace Database\Seeders;

use Illuminate\Database\Seeder;
use Illuminate\Support\Facades\DB;
use Carbon\Carbon;

class MatchesSeeder extends Seeder
{
    public function run(): void
    {
        DB::table("matches")->insert([
            ['match_id'=>"teszt1", 'white_id'=>1, 'black_id'=>2, 'gamemode'=>"Blitz", 'match_duration'=>"3 | 2", 'played_at'=>$this->randomDate(), 'moves'=>null, 'match_end_result'=>'Checkmate', 'winner_id'=>null, 'winner_time'=>null],
            ['match_id'=>"teszt2", 'white_id'=>2, 'black_id'=>1, 'gamemode'=>"Blitz", 'match_duration'=>"5", 'played_at'=>$this->randomDate(), 'moves'=>null, 'match_end_result'=>'Checkmate', 'winner_id'=>2, 'winner_time'=>null],
        
            ['match_id'=>"teszt3", 'white_id'=>3, 'black_id'=>4, 'gamemode'=>"Blitz", 'match_duration'=>"3", 'played_at'=>$this->randomDate(), 'moves'=>null, 'match_end_result'=>'Checkmate', 'winner_id'=>null, 'winner_time'=>null],
            ['match_id'=>"teszt4", 'white_id'=>5, 'black_id'=>6, 'gamemode'=>"Rapid", 'match_duration'=>"10", 'played_at'=>$this->randomDate(), 'moves'=>null, 'match_end_result'=>'Checkmate', 'winner_id'=>5, 'winner_time'=>null],
            ['match_id'=>"teszt5", 'white_id'=>7, 'black_id'=>8, 'gamemode'=>"Rapid", 'match_duration'=>"15 | 10", 'played_at'=>$this->randomDate(), 'moves'=>null, 'match_end_result'=>'Checkmate', 'winner_id'=>8, 'winner_time'=>null],
            ['match_id'=>"teszt6", 'white_id'=>9, 'black_id'=>10, 'gamemode'=>"Blitz", 'match_duration'=>"3 | 2", 'played_at'=>$this->randomDate(), 'moves'=>null, 'match_end_result'=>'Checkmate', 'winner_id'=>null, 'winner_time'=>null],
            ['match_id'=>"teszt7", 'white_id'=>11, 'black_id'=>12, 'gamemode'=>"Rapid", 'match_duration'=>"15", 'played_at'=>$this->randomDate(), 'moves'=>null, 'match_end_result'=>'Checkmate', 'winner_id'=>11, 'winner_time'=>null],
        
            ['match_id'=>"teszt8", 'white_id'=>13, 'black_id'=>14, 'gamemode'=>"Blitz", 'match_duration'=>"5", 'played_at'=>$this->randomDate(), 'moves'=>null, 'match_end_result'=>'Checkmate', 'winner_id'=>14, 'winner_time'=>null],
            ['match_id'=>"teszt9", 'white_id'=>15, 'black_id'=>16, 'gamemode'=>"Rapid", 'match_duration'=>"10", 'played_at'=>$this->randomDate(), 'moves'=>null, 'match_end_result'=>'Checkmate', 'winner_id'=>null, 'winner_time'=>null],
            ['match_id'=>"teszt10", 'white_id'=>17, 'black_id'=>18, 'gamemode'=>"Rapid", 'match_duration'=>"15", 'played_at'=>$this->randomDate(), 'moves'=>null, 'match_end_result'=>'Checkmate', 'winner_id'=>17, 'winner_time'=>null],
            ['match_id'=>"teszt11", 'white_id'=>19, 'black_id'=>20, 'gamemode'=>"Blitz", 'match_duration'=>"3", 'played_at'=>$this->randomDate(), 'moves'=>null, 'match_end_result'=>'Checkmate', 'winner_id'=>20, 'winner_time'=>null],
            ['match_id'=>"teszt12", 'white_id'=>21, 'black_id'=>22, 'gamemode'=>"Rapid", 'match_duration'=>"15", 'played_at'=>$this->randomDate(), 'moves'=>null, 'match_end_result'=>'Checkmate', 'winner_id'=>null, 'winner_time'=>null],
        
            ['match_id'=>"teszt13", 'white_id'=>23, 'black_id'=>24, 'gamemode'=>"Rapid", 'match_duration'=>"10", 'played_at'=>$this->randomDate(), 'moves'=>null, 'match_end_result'=>'Checkmate', 'winner_id'=>23, 'winner_time'=>null],
            ['match_id'=>"teszt14", 'white_id'=>25, 'black_id'=>26, 'gamemode'=>"Blitz", 'match_duration'=>"5", 'played_at'=>$this->randomDate(), 'moves'=>null, 'match_end_result'=>'Checkmate', 'winner_id'=>26, 'winner_time'=>null],
            ['match_id'=>"teszt15", 'white_id'=>27, 'black_id'=>28, 'gamemode'=>"Rapid", 'match_duration'=>"15", 'played_at'=>$this->randomDate(), 'moves'=>null, 'match_end_result'=>'Checkmate', 'winner_id'=>null, 'winner_time'=>null],
            ['match_id'=>"teszt16", 'white_id'=>29, 'black_id'=>30, 'gamemode'=>"Rapid", 'match_duration'=>"10", 'played_at'=>$this->randomDate(), 'moves'=>null, 'match_end_result'=>'Checkmate', 'winner_id'=>29, 'winner_time'=>null],
            ['match_id'=>"teszt17", 'white_id'=>31, 'black_id'=>32, 'gamemode'=>"Blitz", 'match_duration'=>"3 | 2", 'played_at'=>$this->randomDate(), 'moves'=>null, 'match_end_result'=>'Checkmate', 'winner_id'=>31, 'winner_time'=>null],
        
            ['match_id'=>"teszt18", 'white_id'=>1, 'black_id'=>3, 'gamemode'=>"Blitz", 'match_duration'=>"5", 'played_at'=>$this->randomDate(), 'moves'=>null, 'match_end_result'=>'Checkmate', 'winner_id'=>null, 'winner_time'=>null],
            ['match_id'=>"teszt19", 'white_id'=>2, 'black_id'=>4, 'gamemode'=>"Rapid", 'match_duration'=>"10", 'played_at'=>$this->randomDate(), 'moves'=>null, 'match_end_result'=>'Checkmate', 'winner_id'=>2, 'winner_time'=>null],
            ['match_id'=>"teszt20", 'white_id'=>5, 'black_id'=>1, 'gamemode'=>"Rapid", 'match_duration'=>"15", 'played_at'=>$this->randomDate(), 'moves'=>null, 'match_end_result'=>'Checkmate', 'winner_id'=>1, 'winner_time'=>null],
            ['match_id'=>"teszt21", 'white_id'=>6, 'black_id'=>2, 'gamemode'=>"Blitz", 'match_duration'=>"3", 'played_at'=>$this->randomDate(), 'moves'=>null, 'match_end_result'=>'Checkmate', 'winner_id'=>null, 'winner_time'=>null],
            ['match_id'=>"teszt22", 'white_id'=>1, 'black_id'=>7, 'gamemode'=>"Rapid", 'match_duration'=>"10", 'played_at'=>$this->randomDate(), 'moves'=>null, 'match_end_result'=>'Checkmate', 'winner_id'=>7, 'winner_time'=>null],
        
            ['match_id'=>"teszt23", 'white_id'=>8, 'black_id'=>2, 'gamemode'=>"Rapid", 'match_duration'=>"15", 'played_at'=>$this->randomDate(), 'moves'=>null, 'match_end_result'=>'Checkmate', 'winner_id'=>null, 'winner_time'=>null],
            ['match_id'=>"teszt24", 'white_id'=>9, 'black_id'=>1, 'gamemode'=>"Blitz", 'match_duration'=>"5", 'played_at'=>$this->randomDate(), 'moves'=>null, 'match_end_result'=>'Checkmate', 'winner_id'=>1, 'winner_time'=>null],
            ['match_id'=>"teszt25", 'white_id'=>2, 'black_id'=>10, 'gamemode'=>"Rapid", 'match_duration'=>"10", 'played_at'=>$this->randomDate(), 'moves'=>null, 'match_end_result'=>'Checkmate', 'winner_id'=>2, 'winner_time'=>null],
            ['match_id'=>"teszt26", 'white_id'=>11, 'black_id'=>12, 'gamemode'=>"Rapid", 'match_duration'=>"15", 'played_at'=>$this->randomDate(), 'moves'=>null, 'match_end_result'=>'Checkmate', 'winner_id'=>null, 'winner_time'=>null],
            ['match_id'=>"teszt27", 'white_id'=>13, 'black_id'=>1, 'gamemode'=>"Blitz", 'match_duration'=>"3 | 2", 'played_at'=>$this->randomDate(), 'moves'=>null, 'match_end_result'=>'Checkmate', 'winner_id'=>13, 'winner_time'=>null],
        
            ['match_id'=>"teszt28", 'white_id'=>2, 'black_id'=>14, 'gamemode'=>"Rapid", 'match_duration'=>"10", 'played_at'=>$this->randomDate(), 'moves'=>null, 'match_end_result'=>'Checkmate', 'winner_id'=>null, 'winner_time'=>null],
            ['match_id'=>"teszt29", 'white_id'=>15, 'black_id'=>16, 'gamemode'=>"Rapid", 'match_duration'=>"15", 'played_at'=>$this->randomDate(), 'moves'=>null, 'match_end_result'=>'Checkmate', 'winner_id'=>16, 'winner_time'=>null],
            ['match_id'=>"teszt30", 'white_id'=>1, 'black_id'=>2, 'gamemode'=>"Blitz", 'match_duration'=>"5", 'played_at'=>$this->randomDate(), 'moves'=>null, 'match_end_result'=>'Checkmate', 'winner_id'=>1, 'winner_time'=>null],
            ['match_id'=>"teszt31", 'white_id'=>17, 'black_id'=>18, 'gamemode'=>"Rapid", 'match_duration'=>"15", 'played_at'=>$this->randomDate(), 'moves'=>null, 'match_end_result'=>'Checkmate', 'winner_id'=>null, 'winner_time'=>null],
            ['match_id'=>"teszt32", 'white_id'=>19, 'black_id'=>20, 'gamemode'=>"Rapid", 'match_duration'=>"10", 'played_at'=>$this->randomDate(), 'moves'=>null, 'match_end_result'=>'Checkmate', 'winner_id'=>19, 'winner_time'=>null]
        ]);
    }

    private function randomDate()
    {
        return Carbon::now()->subDays(rand(1, 365))->subMinutes(rand(0, 1440));
    }
}
