<?php

namespace App\Services;

use App\Events\ChannelAssignment;
use ChessLogic\ChessBoard\ChessPieces\Definitions\Side;
use ChessLogic\MatchDatas\MatchDataStore;
use Illuminate\Support\Facades\Cache;

class MatchService 
{
    private function createChannel(int $player1, int $player2) : string
    {
        $timestamp = now()->format('YmdHis');
        return "{$player1}-{$player2}-{$timestamp}";
    }
    
    private function createMatch(int $player1, int $player2, string $match_duration,
        string $channel) : void
    {
        $matchDataStore = MatchDataStore::createDataStore($player1, $player2, $match_duration, $channel);

        Cache::put("game:{$channel}", 
        [
            'match_id' => $matchDataStore->MatchID,
            'white_id' => $matchDataStore->PlayerDatas[Side::White->value]->ID,
            'black_id' => $matchDataStore->PlayerDatas[Side::Black->value]->ID,
            'match_state' => $matchDataStore->MatchState->jsonSerialize(),
            'player_datas' => $matchDataStore->serializePlayerDatas(),
            'match_points' => $matchDataStore->MatchPoints->jsonSerialize(),
            'draw_trackers' => $matchDataStore->DrawTrackers->jsonSerialize(),
            'clocks' => $matchDataStore->Clocks->jsonSerialize(),
            'chat_messages' => $matchDataStore->ChatMessages->jsonSerialize(),
            'played_at' => $matchDataStore->PlayedAt->format('Y-m-d H:i:s'),
            'match_duration' => $match_duration
        ], now()->addMinutes(30));

        $this->broadcastMatch($player1, $player2, $channel);
    }

    
    private function broadcastMatch(int $player1, int $player2, string $channel) : void
    {
        broadcast(new ChannelAssignment($channel, $player1));
        broadcast(new ChannelAssignment($channel, $player2));
    }
}