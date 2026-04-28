using ChessMechanics.ChessBoard.Definitions;
using ChessMechanics.MatchData.Clock;
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
            List<SANNotationRow> incomingNotations = matchStateDTO.Notations.ToList();
            uiContext?.Post(_ =>
            {
                if (matchState.Notations.Count is 0 || incomingNotations.Count > matchState.Notations.Count + 1)
                {
                    matchState.Notations.Clear();

                    foreach (SANNotationRow notation in incomingNotations)
                        matchState.Notations.Add(notation);

                    return;
                }

                SANNotationRow latest = incomingNotations.Last();

                if (matchState.Notations.Last().Round == latest.Round)
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

    public static void PatchPlayerDatas(PlayerDatasDTO playerDatasDTO,
        Dictionary<Side, PlayerDataStore> playerDatas, SynchronizationContext uiContext)
    {
        PlayerDataStore player = playerDatas[playerDatasDTO.Side];

        if (playerDatasDTO.ID is not null)
            player.ID = playerDatasDTO.ID;

        if (playerDatasDTO.Points is not null)
            player.Points = playerDatasDTO.Points.Value;

        if (playerDatasDTO.Time is not null)
            player.Time = playerDatasDTO.Time;

        if (playerDatasDTO.CapturedPieces is not null)
        {
            List<Piece> capturedPiecesList = playerDatasDTO.CapturedPieces.ToList();
            uiContext.Post(_ =>
            {
                player.CapturedPieces.Clear();
                foreach (Piece piece in capturedPiecesList)
                    player.CapturedPieces.Add(piece);
            }, null);
        }
    }

    public static void PatchMatchPoints(MatchPointsDTO matchPointsDTO, MatchPoints matchPoints)
    {
        if (matchPointsDTO.MatchPointsReason is not null)
            matchPoints.MatchPointsReason = matchPointsDTO.MatchPointsReason;

        if (matchPointsDTO.ClaimForDraw is not null)
            matchPoints.ClaimForDraw = (bool)matchPointsDTO.ClaimForDraw;

        if (matchPointsDTO.MatchEnded is not null)
            matchPoints.MatchEnded = (bool)matchPointsDTO.MatchEnded;

        if (matchPointsDTO.WinnerID is not null)
            matchPoints.WinnerID = (int)matchPointsDTO.WinnerID;
    }

    public static void PatchChatMessages(ChatMessagesDTO chatMessagesDTO,
        ChatMessages chatMessages, SynchronizationContext uiContext)
    {
        if (chatMessagesDTO.Status is not null)
            chatMessages.Status = chatMessagesDTO.Status;

        if (chatMessagesDTO.NewMessage is not null)
            uiContext.Post(_ => chatMessages.ChatMessageList.Add(chatMessagesDTO.NewMessage), null);
    }

    public static void PatchClocks(ClocksDTO clocksDTO, ClockHandler clocks) =>
        clocks.SyncFromServer(clocksDTO.WhiteRemainingMs, clocksDTO.BlackRemainingMs);
}