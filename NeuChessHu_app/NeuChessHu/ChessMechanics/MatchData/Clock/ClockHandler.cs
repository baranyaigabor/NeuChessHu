using ChessMechanics.Authentication.Session;
using ChessMechanics.ChessBoard.Definitions;
using ChessMechanics.MatchData.MatchDatas;
using ChessMechanics.MatchData.MatchDatas.Models;
using ChessMechanics.WebSockets.ChessEngine.Requests;
using System.ComponentModel;

namespace ChessMechanics.MatchData.Clock;

public class ClockHandler : IDisposable
{
    readonly MatchDataStore matchDataStore;
    readonly EngineRequests requests;
    readonly SessionDatas session;

    readonly Lock clocksLock = new();

    Timer? tickTimer;

    double whiteBaseSeconds;
    double blackBaseSeconds;

    long lastSyncMs;
    Side currentSide;

    public ClockHandler(MatchDataStore matchDataStore, EngineRequests requests, SessionDatas session)
    {
        this.matchDataStore = matchDataStore;
        this.requests = requests;
        this.session = session;

        matchDataStore.MatchState.PropertyChanged += OnMatchStateChanged;
    }

    void OnMatchStateChanged(object? s, PropertyChangedEventArgs e)
    {
        if (e.PropertyName is nameof(MatchState.CurrentSide))
        {
            lock (clocksLock)
            {
                currentSide = matchDataStore.MatchState.CurrentSide;

                if (currentSide is Side.None)
                    StopTicking();

                else StartTicking();
            }
        }
    }

    public void SyncFromServer(double whiteRemainingMs, double blackRemainingMs)
    {
        lock (clocksLock)
        {
            whiteBaseSeconds = whiteRemainingMs / 1000.0;
            blackBaseSeconds = blackRemainingMs / 1000.0;

            lastSyncMs = Environment.TickCount64;
            currentSide = matchDataStore.MatchState.CurrentSide;

            StartTicking();
            PushToPlayerDatas(whiteBaseSeconds, blackBaseSeconds);
        }
    }

    void StartTicking()
    {
        tickTimer?.Dispose();

        if (currentSide == Side.None)
            return;

        tickTimer = new Timer(_ => Tick(), null, 0, 50);
    }

    void StopTicking()
    {
        tickTimer?.Dispose();
        tickTimer = null;
    }

    void Tick()
    {
        bool shouldTriggerTimeout = false;
        Side timeoutSide = Side.None;

        double white;
        double black;

        lock (clocksLock)
        {
            long now = Environment.TickCount64;
            double elapsed = (now - lastSyncMs) / 1000.0;

            white = whiteBaseSeconds;
            black = blackBaseSeconds;

            if (currentSide == Side.White)
            {
                white = Math.Max(0, whiteBaseSeconds - elapsed);

                if (white <= 0)
                {
                    shouldTriggerTimeout = true;
                    timeoutSide = Side.White;
                }
            }

            else if (currentSide == Side.Black)
            {
                black = Math.Max(0, blackBaseSeconds - elapsed);

                if (black <= 0)
                {
                    shouldTriggerTimeout = true;
                    timeoutSide = Side.Black;
                }
            }

            UpdateUI(white, black);
        }

        if (shouldTriggerTimeout)
            _ = HandleTimeout(timeoutSide);
    }

    async Task HandleTimeout(Side side)
    {
        if (matchDataStore.PlayingSide != side)
            return;

        await requests.MatchPointRequestAsync(matchDataStore.MatchChannel!,
            (int)session.UserID!, "Timeout");
    }

    void UpdateUI(double white, double black)
    {
        matchDataStore.PlayerDatas[Side.White].Time = TimeFormatter(white);
        matchDataStore.PlayerDatas[Side.Black].Time = TimeFormatter(black);
    }

    void PushToPlayerDatas(double white, double black)
    {
        matchDataStore.PlayerDatas[Side.White].Time = TimeFormatter(white);
        matchDataStore.PlayerDatas[Side.Black].Time = TimeFormatter(black);
    }

    static string TimeFormatter(double totalSeconds)
    {
        totalSeconds = Math.Max(0, totalSeconds);

        int minutes = (int)(totalSeconds / 60);
        double rawSeconds = totalSeconds % 60;
        int wholeSeconds = (int)Math.Floor(rawSeconds);

        if (minutes > 0)
            return $"{minutes:D2}:{wholeSeconds:D2}";

        if (rawSeconds < 10)
        {
            double tenths = Math.Floor(rawSeconds * 10) / 10;
            return $"0:{tenths:00.0}".Replace(',', '.');
        }

        return $"0:{wholeSeconds:D2}";
    }

    public void Dispose()
    {
        matchDataStore.MatchState.PropertyChanged -= OnMatchStateChanged;
        tickTimer?.Dispose();
    }
}