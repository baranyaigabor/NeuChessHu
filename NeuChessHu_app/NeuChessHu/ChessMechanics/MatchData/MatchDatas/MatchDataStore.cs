using ChessMechanics.APIs;
using ChessMechanics.Authentication.Session;
using ChessMechanics.Authentication.User;
using ChessMechanics.ChessBoard.Definitions;
using ChessMechanics.MatchData.Clock;
using ChessMechanics.MatchData.MatchDatas.DataTransferObjects;
using ChessMechanics.MatchData.MatchDatas.Models;
using ChessMechanics.MatchData.MatchDatas.Patching;

namespace ChessMechanics.MatchData.MatchDatas;

public class MatchDataStore
{
    readonly APIHandlers apiHandlers;
    readonly SessionDatas session;
    readonly TaskCompletionSource readyTaskCompletionSource;

    public string? MatchChannel { get; set; }

    public Side PlayingSide { get; set; }
    public MatchPoints MatchPoints { get; }
    public MatchState MatchState { get; }
    public Dictionary<Side, PlayerDataStore> PlayerDatas { get; }
    public ChatMessages ChatMessageList { get; set; }

    public Task Initialize => readyTaskCompletionSource.Task;

    public MatchDataStore(APIHandlers apiHandlers, SessionDatas session)
    {
        this.apiHandlers = apiHandlers;
        this.session = session;

        readyTaskCompletionSource = new();

        MatchPoints = MatchPoints.CreateMatchPoints();
        MatchState = MatchState.CreateMatchState();

        PlayerDatas = Enum.GetValues<Side>().Cast<Side>().Take(2)
            .ToDictionary(x => x, x => PlayerDataStore.CreatePlayerDataStore());

        ChatMessageList = ChatMessages.CreateChatMessages();
    }

    public async Task InitializeAsync(InitializerDTO initializerDTO, ClockHandler clocks)
    {
        PlayerDatas[Side.White].ID = int.Parse(initializerDTO.WhiteID);
        PlayerDatas[Side.Black].ID = int.Parse(initializerDTO.BlackID);

        PlayingSide = PlayerDatas[Side.White].ID == session.UserID ? Side.White : Side.Black;

        SynchronizationContext uiContext = SynchronizationContext.Current!;

        Patcher.PatchMatchState(initializerDTO.InitialState, this, MatchState, uiContext);
        Patcher.PatchClocks(initializerDTO.Clocks, clocks);

        await SetUserDatas();

        readyTaskCompletionSource.TrySetResult();
    }

    async Task SetUserDatas()
    {
        Side opponentSide = PlayingSide is Side.White ? Side.Black : Side.White;

        PlayerDatas[PlayingSide].UserData = session.User!;
        PlayerDatas[opponentSide].UserData = await UserData.CreateUser(apiHandlers, PlayerDatas[opponentSide].ID);
    }
}