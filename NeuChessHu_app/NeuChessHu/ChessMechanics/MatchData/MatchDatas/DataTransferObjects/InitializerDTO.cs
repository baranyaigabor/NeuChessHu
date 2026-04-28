namespace ChessMechanics.MatchData.MatchDatas.DataTransferObjects;

public record InitializerDTO(string WhiteID, string BlackID, 
    MatchStateDTO InitialState, ClocksDTO Clocks);