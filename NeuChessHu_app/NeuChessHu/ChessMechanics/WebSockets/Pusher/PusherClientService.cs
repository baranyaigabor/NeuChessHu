using ChessMechanics.WebSocketss.Pusher.Config;
using System.Reactive.Linq;
using Websocket.Client;

namespace ChessMechanics.WebSockets.Pusher;

public class PusherClientService
{
    readonly WebsocketClient webSocketClient;

    bool isManualDisconnect = false;

    public event Action? OnConnected;
    public event Action? OnDisconnected;

    public PusherClientService(PusherConfig config)
    {
        webSocketClient = new WebsocketClient(
            new($"ws://10.1.3.33:6001/app/{config.AppKey}?protocol=7&client=dotnet"))
        {
            ReconnectTimeout = TimeSpan.FromSeconds(60)
        };

        webSocketClient.DisconnectionHappened.Subscribe(_ =>
        {
            if (!isManualDisconnect)
                OnDisconnected?.Invoke();
        });
    }
}