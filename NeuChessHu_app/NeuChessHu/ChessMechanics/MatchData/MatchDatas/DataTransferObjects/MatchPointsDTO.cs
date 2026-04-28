namespace ChessMechanics.MatchData.MatchDatas.DataTransferObjects;

public class MatchPointsDTO
{
    public string? MatchPointsReason { get; set; }
    public bool? ClaimForDraw { get; set; }
    public bool? MatchEnded { get; set; }
    public int? WinnerID { get; set; }
}