using ChessMechanics.Common;

namespace ChessMechanics.MatchData.MatchDatas.Models;

public class MatchPoints : ObservableBase
{
    bool matchEnded;
    string? matchPointsReason;
    int? winnerID;
    private bool claimForDraw;

    public string? MatchPointsReason
    {
        get => matchPointsReason;
        internal set
        {
            matchPointsReason = value;
            RaisePropertyChanged();
        }
    }

    public bool ClaimForDraw 
    { 
        get => claimForDraw;
        internal set 
        {
            claimForDraw = value;
            RaisePropertyChanged();
        }
    }

    public bool MatchEnded
    {
        get => matchEnded;
        internal set
        {
            matchEnded = value;

            if (value && OnMatchEnd is not null)
                Task.Run(OnMatchEnd!);

            RaisePropertyChanged();
        }
    }

    public int? WinnerID
    {
        get => winnerID;
        internal set
        {
            winnerID = value;
            RaisePropertyChanged();
        }
    }

    public Func<Task>? OnMatchEnd { get; set; }

    internal static MatchPoints CreateMatchPoints() => new();
}