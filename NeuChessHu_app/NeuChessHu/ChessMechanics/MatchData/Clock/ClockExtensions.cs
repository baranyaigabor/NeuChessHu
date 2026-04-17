namespace ChessMechanics.MatchData.Clock;

internal static class ClockExtensions
{
    internal static void Start(this Timer timer, double seconds)
    {
        int firstInterval = GetFirstInterval(seconds);
        int period = seconds <= 10 ? 100 : 1000;

        timer.Change(firstInterval, period);
    }

    internal static void Stop(this Timer clock) =>
        clock.Change(Timeout.Infinite, Timeout.Infinite);

    static int GetFirstInterval(double secondsLeft)
    {
        int ms = Convert.ToInt32((secondsLeft - Math.Floor(secondsLeft)) * 1000);

        if (secondsLeft <= 10)
        {
            int sub = ms % 100;
            return sub is 0 ? 100 : sub;
        }

        return ms is 0 ? 1000 : ms;
    }
}
