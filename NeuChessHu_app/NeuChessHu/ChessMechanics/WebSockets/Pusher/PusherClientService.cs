using ChessMechanics.APIs;
using ChessMechanics.Authentication.Session;
using ChessMechanics.MatchData.MatchDatas.ComplexTypeJSONConverters;
using ChessMechanics.WebSocketss.Pusher.Config;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.WebSockets;
using System.Reactive.Linq;
using Websocket.Client;

namespace ChessMechanics.WebSockets.Pusher;

public class PusherClientService : IAsyncDisposable
{
    readonly APIHandlers apiHandlers;
    readonly SessionDatas session;
    string? socketId;

    string? pendingMatchChannel;
    string? activeMatchChannel;

    readonly Dictionary<string, Func<JObject, Task>> eventHandlers;
    readonly Dictionary<string, Func<JObject, Task>> subscriptionBinds = [];
    readonly HashSet<string> subscribedChannels = [];

    readonly WebsocketClient webSocketClient;
    readonly IDisposable? messageSubscription;

    bool isManualDisconnect = false;

    public event Action? OnConnected;
    public event Action? OnDisconnected;

    public PusherClientService(PusherConfig config, APIHandlers apiHandlers, SessionDatas session)
    {
        this.apiHandlers = apiHandlers;
        this.session = session;

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

        eventHandlers = PusherEventHandlers();

        messageSubscription = webSocketClient.MessageReceived
            .Where(x => x.Text is not null)
            .Subscribe(async x => await MessageHandlerAsync(x.Text!));
    }

    public async Task InitializePusherClientAsync()
    {
        if (!webSocketClient.IsRunning)
        {
            isManualDisconnect = false;
            await webSocketClient.Start();
        }
    }

    Dictionary<string, Func<JObject, Task>> PusherEventHandlers() => new()
    {
        ["pusher:connection_established"] = async x =>
        {
            socketId = JObject.Parse(x["data"]!.ToString())["socket_id"]!.ToString();
            subscribedChannels.Clear();

            await AuthenticateAndSubscribeAsync("waiting-queue");
            await AuthenticateAndSubscribeAsync($"private-user.{session.UserID}");

            if (pendingMatchChannel is not null)
            {
                await AuthenticateAndSubscribeAsync(pendingMatchChannel);
                pendingMatchChannel = null;
            }

            else if (activeMatchChannel is not null)
                await AuthenticateAndSubscribeAsync(activeMatchChannel);
        },
        ["pusher_internal:subscription_succeeded"] = async x =>
        {
            string channel = x["channel"]!.ToString();

            if (channel == "waiting-queue")
            {
                OnConnected?.Invoke();
                return;
            }

            if (channel.StartsWith("private-user."))
                return;

            string result = await apiHandlers.HttpMatchReadyAsync(channel);
        }
    };

    public async Task SubscribeMatchChannelAsync(string channel)
    {
        activeMatchChannel = channel;

        if (socketId is not null)
            await AuthenticateAndSubscribeAsync(channel);

        else pendingMatchChannel = channel;
    }

    public void Bind<T>(string eventName, Func<T, Task> handler)
    {
        subscriptionBinds[eventName] = async json =>
        {
            JsonSerializerSettings settings = new()
            {
                Converters = 
                {
                    new ChessPieceConverter(),
                    new ChessPieceMatrixConverter(),
                    new TupleConverter(),
                    new SideConverter(),
                    new PieceConverter()
                }
            };

            JToken? dataToken = json["data"];
            T dto = dataToken!.Type == JTokenType.String
                ? JsonConvert.DeserializeObject<T>(dataToken.ToString(), settings)!
                : JsonConvert.DeserializeObject<T>(dataToken.ToString(), settings)!;

            await handler(dto);
        };
    }

    public void Unbind(string eventName) =>
        subscriptionBinds.Remove(eventName);

    async Task MessageHandlerAsync(string json)
    {
        JObject jsonObject = JObject.Parse(json);
        string eventMessage = jsonObject["event"]?.ToString() ?? "";

        if (eventHandlers.TryGetValue(eventMessage, out var internalHandler))
        {
            await internalHandler(jsonObject);
            return;
        }

        if (subscriptionBinds.TryGetValue(eventMessage, out var subscription))
            await subscription(jsonObject);
    }
    async Task AuthenticateAndSubscribeAsync(string channel)
    {
        if (socketId is null || !subscribedChannels.Add(channel))
            return;

        string responseJson = await apiHandlers.HttpAuthenticateAsync(socketId, channel);

        JObject json = JObject.Parse(responseJson);

        string? auth = json["auth"]?.ToString();

        if (auth is null)
            return;

        string? channelData = json["channel_data"]?.ToString();

        webSocketClient.Send(JsonConvert.SerializeObject(new
        {
            @event = "pusher:subscribe",
            data = new { channel, auth, channel_data = channelData }
        }));
    }

    public void ResetMatchChannel()
    {
        activeMatchChannel = null;
        pendingMatchChannel = null;
        subscriptionBinds.Clear();
    }

    public async Task StopPusherAsync()
    {
        isManualDisconnect = true;
        subscribedChannels.Clear();
        ResetMatchChannel();

        TaskCompletionSource pusherShutdown = new();

        using IDisposable subribeDisconnect = webSocketClient.DisconnectionHappened
            .Subscribe(_ => pusherShutdown.TrySetResult());

        await webSocketClient.Stop(WebSocketCloseStatus.NormalClosure, "Shutdown");
        await Task.WhenAny(pusherShutdown.Task, Task.Delay(3000));

        pendingMatchChannel = null;
    }

    public async ValueTask DisposeAsync()
    {
        await StopPusherAsync();
        messageSubscription?.Dispose();
        webSocketClient.Dispose();
    }
}