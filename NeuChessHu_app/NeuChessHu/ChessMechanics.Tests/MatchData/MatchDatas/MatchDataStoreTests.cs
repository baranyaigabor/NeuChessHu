using ChessMechanics.APIs;
using ChessMechanics.Authentication.Session;
using ChessMechanics.ChessBoard.Definitions;
using ChessMechanics.MatchData.MatchDatas;
using NUnit.Framework;

namespace ChessMechanics.Tests.MatchData.MatchDatas;

[TestFixture]
public class MatchDataStoreTests
{
    [Test]
    public void ConstructorCreatesEmptyMatchModelsAndPlayerStores()
    {
        using APIHandlers apiHandlers = new(new SessionDatas());

        MatchDataStore store = new(apiHandlers, new SessionDatas());

        Assert.Multiple(() =>
        {
            Assert.That(store.MatchPoints, Is.Not.Null);
            Assert.That(store.MatchState, Is.Not.Null);
            Assert.That(store.ChatMessageList, Is.Not.Null);
            Assert.That(store.PlayerDatas.Keys, Is.EquivalentTo(new[] { Side.White, Side.Black }));
            Assert.That(store.PlayerDatas[Side.White].Points, Is.EqualTo(0));
            Assert.That(store.PlayerDatas[Side.Black].Time, Is.EqualTo(string.Empty));
        });
    }
}
