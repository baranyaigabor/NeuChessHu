using ChessMechanics.APIs;
using ChessMechanics.Authentication.Session;
using ChessMechanics.MatchData.MatchDatas;
using ChessMechanics.MatchData.MatchDatas.DataTransferObjects;
using ChessMechanics.WebSockets.ChessEngine;
using ChessMechanics.WebSockets.Pusher;
using Microsoft.Extensions.DependencyInjection;
using NeuChessHu.Collections.Contexts;
using NeuChessHu.Controllers;
using NeuChessHu.UserSettings;
using Newtonsoft.Json.Linq;
using System.Windows;

namespace NeuChessHu.Services.MatchServices;

public record LookingForMatchService(IServiceProvider Provider, SessionDatas Session,
    APIHandlers HttpClients, PusherClientService Pusher, ChessEngineClientService ChessEngineClient,
    BindableSettings Settings) : IAsyncDisposable
{
    bool isLookingForMatch = false;
    IServiceScope? matchScope;
    Action? onConnectedHandler;
    CancellationTokenSource? lookingForMatchCancellationTokenSource;

    internal IServiceScope? MatchScope => matchScope;
    public Action? OnSwitchToMatch { get; internal set; }

    public async Task LookingForMatchAsync()
    {
        Pusher.Bind<ChannelAssignmentDTO>("assign-channel", HandleAssignChannelAsync);
        await StartWebsocketsAsync();
    }

    async Task HandleAssignChannelAsync(ChannelAssignmentDTO channelAssignmentDTO)
    {
        if (channelAssignmentDTO.PlayerID != Session.UserID.ToString())
            return;

        lookingForMatchCancellationTokenSource?.Cancel();

        Pusher.Unbind("assign-channel");
        await HttpClients.HttpLeaveMatchmakingQueueAsync();
        isLookingForMatch = false;

        matchScope ??= Provider.CreateScope();
        matchScope!.ServiceProvider.GetRequiredService<MatchContext>();

        await Application.Current.Dispatcher.InvokeAsync(async () =>
            await SubscribeMatchChannelAsync(channelAssignmentDTO.Channel));
    }

    async Task HandlePendingMatchChannelAsync(string channel)
    {
        lookingForMatchCancellationTokenSource?.Cancel();

        Pusher.Unbind("assign-channel");
        isLookingForMatch = false;

        await Application.Current.Dispatcher.InvokeAsync(async () =>
            await SubscribeMatchChannelAsync(channel));
    }

    async Task StartWebsocketsAsync()
    {
        if (onConnectedHandler is not null)
        {
            Pusher.OnConnected -= onConnectedHandler;
            onConnectedHandler = null;
        }

        isLookingForMatch = true;

        onConnectedHandler = async () =>
        {
            try
            {
                if (!isLookingForMatch)
                    return;

                string? pendingResult = await HttpClients.HttpGetPendingChannelAsync();
                string? pendingChannel = ExtractPendingChannel(pendingResult);

                if (pendingChannel is not null)
                {
                    await HandlePendingMatchChannelAsync(pendingChannel);
                    return;
                }

                await HttpClients.HttpJoinMatchmakingQueueAsync(Settings.LastMatchDuration,
                    Settings.LastMatchStockfish ? 12 : null);
            }
            catch
            {
                isLookingForMatch = false;
            }
        };

        Pusher.OnConnected += onConnectedHandler;
        await Pusher.InitializePusherClientAsync();
        await ChessEngineClient.InitializeChessEngineClientAsync();
    }

    static string? ExtractPendingChannel(string? pendingResult)
    {
        if (string.IsNullOrWhiteSpace(pendingResult))
            return null;

        JToken? channelToken = JObject.Parse(pendingResult)["channel"];

        if (channelToken is null || channelToken.Type is JTokenType.Null)
            return null;

        string? channel = channelToken.ToString();

        return string.IsNullOrWhiteSpace(channel) ? null : channel;
    }

    async Task SubscribeMatchChannelAsync(string channel)
    {
        string privateChannel = channel.StartsWith("private-")
            ? channel
            : $"private-{channel}";

        matchScope ??= Provider.CreateScope();
        matchScope!.ServiceProvider.GetRequiredService<MatchContext>();
        matchScope!.ServiceProvider.GetRequiredService<MatchDataStore>().MatchChannel = privateChannel;

        MatchController matchController = matchScope!.ServiceProvider.GetRequiredService<MatchController>();
        matchController.UIContext = SynchronizationContext.Current!;

        await matchController.SubscribeMatchChannelsAsync(OnSwitchToMatch);
        await Pusher.SubscribeMatchChannelAsync(privateChannel);
    }

    internal CancellationTokenSource CreateLookingForMatchCts()
    {
        lookingForMatchCancellationTokenSource?.Dispose();
        lookingForMatchCancellationTokenSource = new CancellationTokenSource();
        return lookingForMatchCancellationTokenSource;
    }

    internal async Task DisposeMatchAsync()
    {
        isLookingForMatch = false;

        if (onConnectedHandler is not null)
        {
            Pusher.OnConnected -= onConnectedHandler;
            onConnectedHandler = null;
        }

        lookingForMatchCancellationTokenSource?.Cancel();
        lookingForMatchCancellationTokenSource?.Dispose();
        lookingForMatchCancellationTokenSource = null;

        await Pusher.StopPusherAsync();
        await ChessEngineClient.DisconnectChessEngineAsync();

        matchScope?.Dispose();
        matchScope = null;
    }

    public async ValueTask DisposeAsync() =>
        await DisposeMatchAsync();
}