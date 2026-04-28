<?php

namespace App\Http\Controllers;

use App\Events\ChatMessagesUpdated;
use Illuminate\Http\Request;

class ChatUpdateController extends Controller
{
    public function update(Request $request)
    {
        $channel = $request->input('channel');
        $newMessage = $request->input('new_message');
    
        if (!$newMessage)
        {
            return response()->json(['status' => 'skipped']);
        }
           
        broadcast(new ChatMessagesUpdated($channel, $newMessage));

        return response()->json(['status' => 'ok']);
    }
}