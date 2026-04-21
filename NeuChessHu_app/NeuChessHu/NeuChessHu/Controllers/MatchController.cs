using ChessMechanics.MatchData.Clock;
using ChessMechanics.MatchData.MatchDatas;
using ChessMechanics.MatchData.MatchDatas.DataTransferObjects;
using ChessMechanics.MatchData.MatchDatas.Patching;
using ChessMechanics.WebSockets.Pusher;

namespace NeuChessHu.Controllers;

public class MatchController(PusherClientService Pusher, MatchDataStore MatchDataStore,
    ClockHandler clocks)
{
    internal SynchronizationContext UIContext { get; set; }

    public Func<Task>? OnPlayAgain { get; internal set; }

    internal async Task SubscribeMatchChannelsAsync(Action? onInitialized = null)
    {
        Pusher.Bind<InitializerDTO>("match-start", async x =>
        {
            await MatchDataStore.InitializeAsync(x, clocks);
            UIContext.Post(_ => onInitialized?.Invoke(), null);
        });

        Pusher.Bind<MatchPointsDTO>("match-points", async x =>
        {
            await MatchDataStore.Initialize;

            UIContext.Post(_ => Patcher.PatchMatchPoints(x, MatchDataStore.MatchPoints), null);
        });

        Pusher.Bind<MatchStateDTO>("match-state", async x =>
        {
            await MatchDataStore.Initialize;

            UIContext.Post(_ =>
                Patcher.PatchMatchState(x, MatchDataStore, MatchDataStore.MatchState, UIContext), null);
        });

        Pusher.Bind<PlayerDatasDTO>("player-datas", async x =>
        {
            await MatchDataStore.Initialize;

            UIContext.Post(_ => Patcher.PatchPlayerDatas(x, MatchDataStore.PlayerDatas, UIContext), null);
        });

        Pusher.Bind<ChatMessagesDTO>("chat-messages", async x =>
        {
            await MatchDataStore.Initialize;

            UIContext.Post(_ => Patcher.PatchChatMessages(x, MatchDataStore.ChatMessageList, UIContext), null);
        });

        Pusher.Bind<ClocksDTO>("clocks", async x =>
        {
            await MatchDataStore.Initialize;

            UIContext.Post(_ => Patcher.PatchClocks(x, clocks), null);
        });

        Pusher.Bind<object>("match-start-failed", async _ =>
        {
            Pusher.ResetMatchChannel();
            await OnPlayAgain!.Invoke();
        });
    }
}