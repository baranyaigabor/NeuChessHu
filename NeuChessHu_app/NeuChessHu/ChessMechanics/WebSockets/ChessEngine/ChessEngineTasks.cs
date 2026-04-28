using System.Collections.Concurrent;
using System.Text.Json;

namespace ChessMechanics.WebSockets.ChessEngine;

public class ChessEngineTasks
{
    internal ConcurrentDictionary<string, TaskCompletionSource<JsonElement>> PendingRequests { get; }

    public ChessEngineTasks() =>
        PendingRequests = new(StringComparer.Ordinal);
}