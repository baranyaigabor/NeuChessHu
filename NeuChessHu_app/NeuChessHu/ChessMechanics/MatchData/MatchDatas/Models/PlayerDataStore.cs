using ChessMechanics.Authentication.User;
using ChessMechanics.ChessBoard.Definitions;
using ChessMechanics.Common;
using System.Collections.ObjectModel;

namespace ChessMechanics.MatchData.MatchDatas.Models;

public class PlayerDataStore(int? id, UserData? userData, ObservableCollection<Piece> capturedPieces,
    int points, string time) : ObservableBase
{
    private UserData? userData = userData;

    public int? ID { get; internal set; } = id;

    public UserData? UserData 
    { 
        get => userData;
        internal set
        {
            userData = value;
            RaisePropertyChanged();
        }
    }

    public ObservableCollection<Piece> CapturedPieces 
    { 
        get => capturedPieces;
        internal set
        {
            capturedPieces = value;
            RaisePropertyChanged();
        }
    }
    public int Points
    {
        get => points;
        internal set { points = value; RaisePropertyChanged(); }
    }

    public string Time
    {
        get => time!;
        set { time = value; RaisePropertyChanged(); }
    }

    internal static PlayerDataStore CreatePlayerDataStore() =>
        new(id: null, userData: null, capturedPieces: [], points: 0, time: string.Empty);
}