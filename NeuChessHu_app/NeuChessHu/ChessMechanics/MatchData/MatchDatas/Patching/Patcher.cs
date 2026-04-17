using ChessMechanics.MatchData.MatchDatas.DataTransferObjects;
using ChessMechanics.MatchData.MatchDatas.Models;
using ChessMechanics.MatchData.MatchDatas.Models.DomainModels;
using ChessMechanics.MatchData.MatchDatas.Patching.Orientation;

namespace ChessMechanics.MatchData.MatchDatas.Patching;

public static class Patcher
{
    public static void PatchMatchState(MatchStateDTO matchStateDTO, MatchDataStore matchDataStore,
        MatchState matchState, SynchronizationContext uiContext)
    {
        if (matchStateDTO.MatchID is not null)
            matchState.MatchID = matchStateDTO.MatchID;

        if (matchStateDTO.CurrentSide is not null)
            matchState.CurrentSide = matchStateDTO.CurrentSide.Value;

        if (matchStateDTO.PieceMatrix is not null)
            matchState.PieceMatrix = MatrixOrientation.ClientMatrix(matchDataStore.PlayingSide, matchStateDTO.PieceMatrix);

        if (matchStateDTO.MatchDuration is not null)
            matchState.MatchDuration = matchStateDTO.MatchDuration;

        if (matchStateDTO.Notations is not null && matchStateDTO.Notations.Count > 0)
        {
            SANNotationRow latest = matchStateDTO.Notations.Last();
            uiContext.Post(_ =>
            {
                if (matchState.Notations.Count > 0 && matchState.Notations.Last().Round == latest.Round)
                    matchState.Notations[matchState.Notations.Count - 1] = latest;
                else matchState.Notations.Add(latest);
            }, null);
        }

        if (matchStateDTO.HasWKMoved is not null)
            matchState.HasWKMoved = (bool)matchStateDTO.HasWKMoved;

        if (matchStateDTO.HasWRAMoved is not null)
            matchState.HasWRAMoved = (bool)matchStateDTO.HasWRAMoved;

        if (matchStateDTO.HasWRHMoved is not null)
            matchState.HasWRHMoved = (bool)matchStateDTO.HasWRHMoved;

        if (matchStateDTO.HasBKMoved is not null)
            matchState.HasBKMoved = (bool)matchStateDTO.HasBKMoved;

        if (matchStateDTO.HasBRAMoved is not null)
            matchState.HasBRAMoved = (bool)matchStateDTO.HasBRAMoved;

        if (matchStateDTO.HasBRHMoved is not null)
            matchState.HasBRHMoved = (bool)matchStateDTO.HasBRHMoved;

        if (matchStateDTO.EnPassantTarget is not null)
            matchState.EnPassantTarget = matchStateDTO.EnPassantTarget;
    }
}