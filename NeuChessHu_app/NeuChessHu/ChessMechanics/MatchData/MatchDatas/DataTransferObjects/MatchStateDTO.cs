using ChessMechanics.ChessBoard.ChessPieces;
using ChessMechanics.ChessBoard.Definitions;
using ChessMechanics.MatchData.MatchDatas.Models.DomainModels;
using System.Collections.ObjectModel;

namespace ChessMechanics.MatchData.MatchDatas.DataTransferObjects;

public class MatchStateDTO
{
    public string? MatchID { get; set; }

    public Side? CurrentSide { get; set; }
    public ChessPiece[,]? PieceMatrix { get; set; }

    public string? MatchDuration { get; set; }
    public ObservableCollection<SANNotationRow>? Notations { get; set; }

    public bool? HasWKMoved { get; set; } 
    public bool? HasWRAMoved { get; set; }
    public bool? HasWRHMoved { get; set; }
    public bool? HasBKMoved { get; set; } 
    public bool? HasBRAMoved { get; set; }
    public bool? HasBRHMoved { get; set; }

    public Tuple<int, int>? EnPassantTarget { get; set; }
}