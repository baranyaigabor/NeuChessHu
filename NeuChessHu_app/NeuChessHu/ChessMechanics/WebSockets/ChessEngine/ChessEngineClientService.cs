using System.Net.WebSockets;
using Websocket.Client;

namespace ChessMechanics.WebSockets.ChessEngine;

public class ChessEngineClientService : IAsyncDisposable
{
    readonly ChessEngineTasks tasks;
    readonly WebsocketClient webSocketClient;

    public event Action<string>? MessageReceived;

    public ChessEngineClientService(ChessEngineTasks tasks)
    {
        this.tasks = tasks;

        webSocketClient = CreateWebSocketClient(new("ws://10.1.3.33:7001"));
    }

    public async Task InitializeChessEngineClientAsync()
    {
        if (webSocketClient.IsRunning)
            return;

        await webSocketClient.Start();
    }

    public async Task DisconnectChessEngineAsync()
    {
        TaskCompletionSource chessEngineShutdown = new();

        using IDisposable subscribeDisconnect = webSocketClient.DisconnectionHappened
            .Subscribe(_ => chessEngineShutdown.TrySetResult());

        await webSocketClient.Stop(WebSocketCloseStatus.NormalClosure, "Shutdown");
        await Task.WhenAny(chessEngineShutdown.Task, Task.Delay(3000));
    }

    WebsocketClient CreateWebSocketClient(Uri socketUri)
    {
        WebsocketClient webSocketClient = new(socketUri)
        {
            ReconnectTimeout = TimeSpan.FromSeconds(60)
        };

        webSocketClient.MessageReceived.Subscribe(x =>
        {
            if (x.Text is null)
                return;
        });

        return webSocketClient;
    }

    public async Task SendAsync(string message) =>
        await webSocketClient.SendInstant(message);

    public async ValueTask DisposeAsync()
    {
        await DisconnectChessEngineAsync();
        webSocketClient.Dispose();
    }
}