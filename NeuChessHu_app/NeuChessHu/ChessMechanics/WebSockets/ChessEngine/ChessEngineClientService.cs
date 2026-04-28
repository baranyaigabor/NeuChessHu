using System.Net.WebSockets;
using System.Text.Json;
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

            MessageHandler(x.Text);
        });

        return webSocketClient;
    }

    public async Task SendAsync(string message) =>
        await webSocketClient.SendInstant(message);

    void MessageHandler(string rawMessage)
    {
        try
        {
            MessageReceived?.Invoke(rawMessage);
        }
        catch (Exception) { }

        try
        {
            JsonDocument jsonDoc = JsonDocument.Parse(rawMessage);
            JsonElement root = jsonDoc.RootElement;

            if (!root.TryGetProperty("requestID", out var idProp))
                return;

            string requestID = idProp.GetString()!;

            if (root.TryGetProperty("error", out var errorProp))
            {
                if (tasks.PendingRequests.TryRemove(requestID, out var errorTcs))
                    errorTcs.SetException(new Exception(errorProp.GetString()));

                return;
            }

            JsonElement payload = root.GetProperty("payload");

            if (tasks.PendingRequests.TryRemove(requestID, out var taskCompletionSource))
                taskCompletionSource.SetResult(payload);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async ValueTask DisposeAsync()
    {
        await DisconnectChessEngineAsync();
        webSocketClient.Dispose();
    }
}