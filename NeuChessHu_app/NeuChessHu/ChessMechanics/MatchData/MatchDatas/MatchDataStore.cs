using ChessMechanics.ChessBoard.Definitions;
using ChessMechanics.MatchData.MatchDatas.Models;

namespace ChessMechanics.MatchData.MatchDatas;

public class MatchDataStore
{
    public string? MatchChannel { get; set; }

    public MatchPoints MatchPoints { get; }
    public MatchState MatchState { get; }
    public Dictionary<Side, PlayerDataStore> PlayerDatas { get; }
    public ChatMessages ChatMessageList { get; set; }

    public MatchDataStore()
    {
        MatchPoints = MatchPoints.CreateMatchPoints();
        MatchState = MatchState.CreateMatchState();

        PlayerDatas = Enum.GetValues<Side>().Cast<Side>().Take(2)
            .ToDictionary(x => x, x => PlayerDataStore.CreatePlayerDataStore());

        ChatMessageList = ChatMessages.CreateChatMessages();
    }
}