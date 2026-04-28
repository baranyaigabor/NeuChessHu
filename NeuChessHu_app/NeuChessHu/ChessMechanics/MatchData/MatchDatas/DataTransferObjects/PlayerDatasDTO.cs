using ChessMechanics.ChessBoard.Definitions;

namespace ChessMechanics.MatchData.MatchDatas.DataTransferObjects;

public class PlayerDatasDTO
{
    public Side Side { get; set; }
    public int? ID { get; set; }
    public int? Points { get; set; }
    public string? Time { get; set; }
    public List<Piece>? CapturedPieces { get; set; }
}