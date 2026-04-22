using ChessMechanics.APIs;
using ChessMechanics.Authentication.Session;
using ChessMechanics.ChessBoard.Definitions;
using ChessMechanics.MatchData.MatchDatas;
using ChessMechanics.MatchData.MatchDatas.DataTransferObjects;
using ChessMechanics.MatchData.MatchDatas.Models;
using ChessMechanics.MatchData.MatchDatas.Patching;
using NUnit.Framework;

namespace ChessMechanics.Tests.MatchData.MatchDatas.Models;

[TestFixture]
public class MatchStateTests
{
    [Test]
    public void MatchDurationWhenPatchedRaisesPropertyChanged()
    {
        using APIHandlers apiHandlers = new(new SessionDatas());
        MatchDataStore store = new(apiHandlers, new SessionDatas());

        MatchState state = new();
        string? propertyName = null;
        state.PropertyChanged += (_, e) => propertyName = e.PropertyName;

        Patcher.PatchMatchState(new MatchStateDTO { MatchDuration = "Bullet" },
            store, state, new ImmediateSynchronizationContext());

        Assert.That(propertyName, Is.EqualTo(nameof(MatchState.MatchDuration)));
    }
    
    [Test]
    public void CurrentSideWhenPatchedRaisesPropertyChanged()
    {
        using APIHandlers apiHandlers = new(new SessionDatas());
        MatchDataStore store = new(apiHandlers, new SessionDatas());

        MatchState state = new();
        string? propertyName = null;
        state.PropertyChanged += (_, e) => propertyName = e.PropertyName;

        Patcher.PatchMatchState(new MatchStateDTO { CurrentSide = Side.Black },
            store, state, new ImmediateSynchronizationContext());

        Assert.That(propertyName, Is.EqualTo(nameof(MatchState.CurrentSide)));
    }
}