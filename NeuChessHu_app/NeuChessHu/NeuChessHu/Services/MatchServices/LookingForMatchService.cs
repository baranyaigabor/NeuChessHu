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
using System.Windows;

namespace NeuChessHu.Services.MatchServices;

public record LookingForMatchService(IServiceProvider Provider, SessionDatas Session,
    APIHandlers HttpClients, PusherClientService Pusher, ChessEngineClientService ChessEngineClient,
    BindableSettings Settings) : IAsyncDisposable
{
    bool isLookingForMatch = false;

    IServiceScope? matchScope;
    Action? onConnectedHandler;

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

        Pusher.Unbind("assign-channel");
        await HttpClients.HttpLeaveMatchmakingQueueAsync();

        isLookingForMatch = false;

        matchScope ??= Provider.CreateScope();

        matchScope!.ServiceProvider.GetRequiredService<MatchContext>();

        await Application.Current.Dispatcher.InvokeAsync(async () =>
            await SubscribeMatchChannelAsync(channelAssignmentDTO.Channel));
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
            if (isLookingForMatch)
                await HttpClients.HttpJoinMatchmakingQueueAsync(Settings.LastMatchDuration);
        };
        Pusher.OnConnected += onConnectedHandler;

        await Pusher.InitializePusherClientAsync();
        await ChessEngineClient.InitializeChessEngineClientAsync();
    }

    async Task SubscribeMatchChannelAsync(string channel)
    {
        matchScope!.ServiceProvider.GetRequiredService<MatchDataStore>().MatchChannel = $"private-{channel}";

        MatchController matchController = matchScope!.ServiceProvider.GetRequiredService<MatchController>();

        matchController.UIContext = SynchronizationContext.Current!;
        await matchController.SubscribeMatchChannelsAsync(OnSwitchToMatch);

        await Pusher.SubscribeMatchChannelAsync($"private-{channel}");
    }

    internal async Task DisposeMatchAsync()
    {
        isLookingForMatch = false;

        if (onConnectedHandler is not null)
        {
            Pusher.OnConnected -= onConnectedHandler;
            onConnectedHandler = null;
        }

        await Pusher.StopPusherAsync();
        await ChessEngineClient.DisconnectChessEngineAsync();

        matchScope?.Dispose();
        matchScope = null;
    }

    public async ValueTask DisposeAsync() =>
        await DisposeMatchAsync();
}