using ChessMechanics.MatchData.Clock;
using NUnit.Framework;

namespace ChessMechanics.Tests.MatchData.Clock;

[TestFixture]
public class ClockExtensionsTests
{
    [Test]
    public void StartWithShortTimeUsesSubsecondTicking()
    {
        using var ticked = new ManualResetEventSlim();
        using var timer = new Timer(_ => ticked.Set(), null, Timeout.Infinite, Timeout.Infinite);

        timer.Start(0.05);

        Assert.That(ticked.Wait(TimeSpan.FromSeconds(1)), Is.True);
    }

    [Test]
    public void StopWhenTimerIsRunningPreventsFurtherTicks()
    {
        int ticks = 0;
        using Timer timer = new(_ => Interlocked.Increment(ref ticks), null, Timeout.Infinite, Timeout.Infinite);

        timer.Start(5);
        timer.Stop();
        Thread.Sleep(150);

        Assert.That(Volatile.Read(ref ticks), Is.EqualTo(0));
    }
}
