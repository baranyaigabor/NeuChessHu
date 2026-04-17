using ChessMechanics.MatchData.Clock;
using ChessMechanics.MatchData.MatchDatas;
using ChessMechanics.MatchData.MatchDatas.DataTransferObjects;
using ChessMechanics.MatchData.MatchDatas.Patching;
using ChessMechanics.WebSockets.Pusher;

namespace NeuChessHu.Controllers;

public class MatchController(PusherClientService Pusher, MatchDataStore MatchDataStore,
    ClockHandler clocks)
{
    public Func<Task>? OnPlayAgain { get; internal set; }

    internal async Task SubscribeMatchChannelsAsync(Action? onInitialized = null)
    {
        SynchronizationContext uiContext = SynchronizationContext.Current!;

        Pusher.Bind<InitializerDTO>("match-start", async x =>
        {
            await MatchDataStore.InitializeAsync(x, clocks);
            uiContext.Post(_ => onInitialized?.Invoke(), null);
        });

        Pusher.Bind<MatchPointsDTO>("match-points", async x =>
        {
            await MatchDataStore.Initialize;

            uiContext.Post(_ => Patcher.PatchMatchPoints(x, MatchDataStore.MatchPoints), null);
        });

        Pusher.Bind<MatchStateDTO>("match-state", async x =>
        {
            await MatchDataStore.Initialize;

            uiContext.Post(_ =>
                Patcher.PatchMatchState(x, MatchDataStore, MatchDataStore.MatchState, uiContext), null);
        });

        Pusher.Bind<PlayerDatasDTO>("player-datas", async x =>
        {
            await MatchDataStore.Initialize;

            uiContext.Post(_ => Patcher.PatchPlayerDatas(x, MatchDataStore.PlayerDatas, uiContext), null);
        });

        Pusher.Bind<ChatMessagesDTO>("chat-messages", async x =>
        {
            await MatchDataStore.Initialize;

            uiContext.Post(_ => Patcher.PatchChatMessages(x, MatchDataStore.ChatMessageList, uiContext), null);
        });

        Pusher.Bind<ClocksDTO>("clocks", async x =>
        {
            await MatchDataStore.Initialize;

            uiContext.Post(_ => Patcher.PatchClocks(x, clocks), null);
        });

        Pusher.Bind<object>("match-start-failed", async _ =>
        {
            Pusher.ResetMatchChannel();
            await OnPlayAgain!.Invoke();
        });
    }
}