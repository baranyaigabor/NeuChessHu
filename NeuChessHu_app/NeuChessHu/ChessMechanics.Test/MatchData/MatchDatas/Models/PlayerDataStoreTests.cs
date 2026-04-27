using ChessMechanics.ChessBoard.Definitions;
using ChessMechanics.MatchData.MatchDatas.DataTransferObjects;
using ChessMechanics.MatchData.MatchDatas.Models;
using ChessMechanics.MatchData.MatchDatas.Patching;
using NUnit.Framework;

namespace ChessMechanics.Test.MatchData.MatchDatas.Models;

[TestFixture]
public class PlayerDataStoreTests
{
    [Test]
    public void PointsWhenPatchedRaisesPropertyChanged()
    {
        Dictionary<Side, PlayerDataStore> playerDatas = CreatePlayerDatas();

        string? propertyName = null;
        playerDatas[Side.White].PropertyChanged += (_, e) => propertyName = e.PropertyName;

        Patcher.PatchPlayerDatas(new PlayerDatasDTO { Side = Side.White, Points = 8 },
            playerDatas, new ImmediateSynchronizationContext());

        Assert.That(propertyName, Is.EqualTo(nameof(PlayerDataStore.Points)));
    }

    [Test]
    public void TimeWhenPatchedRaisesPropertyChanged()
    {
        Dictionary<Side, PlayerDataStore> playerDatas = CreatePlayerDatas();

        string? propertyName = null;
        playerDatas[Side.Black].PropertyChanged += (_, e) => propertyName = e.PropertyName;

        Patcher.PatchPlayerDatas(new PlayerDatasDTO { Side = Side.Black, Time = "00:30" },
            playerDatas, new ImmediateSynchronizationContext());

        Assert.That(propertyName, Is.EqualTo(nameof(PlayerDataStore.Time)));
    }

    static Dictionary<Side, PlayerDataStore> CreatePlayerDatas() => new()
    {
        [Side.White] = new PlayerDataStore(null, null, [], 0, string.Empty),
        [Side.Black] = new PlayerDataStore(null, null, [], 0, string.Empty)
    };
}
