using System.Reflection;
using ChessMechanics.APIs;
using ChessMechanics.Authentication.Session;
using ChessMechanics.ChessBoard.Definitions;
using ChessMechanics.MatchData.Clock;
using ChessMechanics.MatchData.MatchDatas;
using ChessMechanics.WebSockets.ChessEngine;
using ChessMechanics.WebSockets.ChessEngine.Requests;
using NUnit.Framework;

namespace ChessMechanics.Tests.MatchData.Clock;

[TestFixture]
public class ClockHandlerTests
{
    [Test]
    public void TimeFormatterWithMidGameSecondsReturnsWholeSeconds()
    {
        MethodInfo? method = typeof(ClockHandler).GetMethod("TimeFormatter",
            BindingFlags.NonPublic | BindingFlags.Static)!;

        string formatted = (string)method.Invoke(null, [12.7])!;

        Assert.That(formatted, Is.EqualTo("0:12"));
    }
}