using ChessMechanics.ChessBoard.ChessPieces;
using ChessMechanics.ChessBoard.Definitions;
using ChessMechanics.Common;
using ChessMechanics.MatchData.MatchDatas.Models.DomainModels;
using System.Collections.ObjectModel;

namespace ChessMechanics.MatchData.MatchDatas.Models;

public class MatchState : ObservableBase
{
    ChessPiece[,]? pieceMatrix;
    Side currentSide;
    string? matchDuration;

    public string? MatchID { get; internal set; }

    public string MatchDuration
    {
        get => matchDuration!;
        set { matchDuration = value; RaisePropertyChanged(); }
    }

    public ChessPiece[,] PieceMatrix
    {
        get => pieceMatrix!;
        set { pieceMatrix = value; RaisePropertyChanged(); }
    }

    public Side CurrentSide
    {
        get => currentSide;
        internal set { currentSide = value; RaisePropertyChanged(); }
    }

    public ObservableCollection<SANNotationRow> Notations { get; internal set; } = [];

    public bool HasWKMoved { get; internal set; }
    public bool HasWRAMoved { get; internal set; }
    public bool HasWRHMoved { get; internal set; }
    public bool HasBKMoved { get; internal set; }
    public bool HasBRAMoved { get; internal set; }
    public bool HasBRHMoved { get; internal set; }

    public Tuple<int, int>? EnPassantTarget { get; internal set; }

    internal static MatchState CreateMatchState() => new();
}