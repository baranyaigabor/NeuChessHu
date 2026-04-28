using ChessMechanics.MatchData.Clock;
using NUnit.Framework;
using System.Reflection;

namespace ChessMechanics.Test.MatchData.Clock;

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