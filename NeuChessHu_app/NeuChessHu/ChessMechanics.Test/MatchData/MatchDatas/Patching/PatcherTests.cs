using ChessMechanics.APIs;
using ChessMechanics.Authentication.Session;
using ChessMechanics.ChessBoard.ChessPieces;
using ChessMechanics.ChessBoard.Definitions;
using ChessMechanics.MatchData.MatchDatas;
using ChessMechanics.MatchData.MatchDatas.DataTransferObjects;
using ChessMechanics.MatchData.MatchDatas.Models;
using ChessMechanics.MatchData.MatchDatas.Models.DomainModels;
using ChessMechanics.MatchData.MatchDatas.Patching;
using NUnit.Framework;

namespace ChessMechanics.Tests.MatchData.MatchDatas.Patching;

[TestFixture]
public class PatcherTests
{
    readonly ImmediateSynchronizationContext uiContext = new();

    [Test]
    public void PatchMatchStateWithValuesUpdatesScalarStateAndFlipsBlackMatrix()
    {
        using APIHandlers apiHandlers = new(new SessionDatas());

        MatchDataStore store = new(apiHandlers, new SessionDatas())
        {
            PlayingSide = Side.Black
        };

        ChessPiece[,]? matrix = EmptyMatrix();
        matrix[0, 1] = ChessPiece.Create(Piece.Knight, Side.White);
        MatchState state = new();

        Patcher.PatchMatchState(new MatchStateDTO
        {
            MatchID = "MatchIDTest",
            CurrentSide = Side.Black,
            PieceMatrix = matrix,
            MatchDuration = "Rapid",
            HasWKMoved = true,
            HasBRHMoved = true,
            EnPassantTarget = Tuple.Create(3, 4)
        }, store, state, uiContext);

        Assert.Multiple(() =>
        {
            Assert.That(state.MatchID, Is.EqualTo("MatchIDTest"));
            Assert.That(state.CurrentSide, Is.EqualTo(Side.Black));
            Assert.That(state.MatchDuration, Is.EqualTo("Rapid"));
            Assert.That(state.PieceMatrix[7, 6], Is.EqualTo(ChessPiece.Create(Piece.Knight, Side.White)));
            Assert.That(state.HasWKMoved, Is.True);
            Assert.That(state.HasBRHMoved, Is.True);
            Assert.That(state.EnPassantTarget, Is.EqualTo(Tuple.Create(3, 4)));
        });
    }

    [Test]
    public void PatchMatchStateWithLatestNotationReplacesSameRoundOrAddsNewRound()
    {
        using APIHandlers apiHandlers = new(new SessionDatas());
        MatchDataStore store = new(apiHandlers, new SessionDatas());
        MatchState state = new();
        state.Notations.Add(new SANNotationRow("1", "e4", null));

        Patcher.PatchMatchState(new MatchStateDTO
        {
            Notations = [new SANNotationRow("1", "e4", "e5")]
        }, store, state, uiContext);

        Patcher.PatchMatchState(new MatchStateDTO
        {
            Notations = [new SANNotationRow("2", "Nf3", null)]
        }, store, state, uiContext);

        Assert.Multiple(() =>
        {
            Assert.That(state.Notations, Has.Count.EqualTo(2));
            Assert.That(state.Notations[0], Is.EqualTo(new SANNotationRow("1", "e4", "e5")));
            Assert.That(state.Notations[1], Is.EqualTo(new SANNotationRow("2", "Nf3", null)));
        });
    }

    [Test]
    public void PatchPlayerDatasWithScalarValuesUpdatesSelectedPlayer()
    {
        Dictionary<Side, PlayerDataStore> playerDatas = CreatePlayerDatas();

        Patcher.PatchPlayerDatas(new PlayerDatasDTO
        {
            Side = Side.White,
            ID = 10,
            Points = 3,
            Time = "05:00"
        }, playerDatas, uiContext);

        Assert.Multiple(() =>
        {
            Assert.That(playerDatas[Side.White].ID, Is.EqualTo(10));
            Assert.That(playerDatas[Side.White].Points, Is.EqualTo(3));
            Assert.That(playerDatas[Side.White].Time, Is.EqualTo("05:00"));
            Assert.That(playerDatas[Side.Black].Points, Is.EqualTo(0));
        });
    }

    [Test]
    public void PatchPlayerDatasWithCapturedPiecesReplacesCapturedPieceCollection()
    {
        Dictionary<Side, PlayerDataStore> playerDatas = CreatePlayerDatas();
        playerDatas[Side.Black].CapturedPieces.Add(Piece.Pawn);

        Patcher.PatchPlayerDatas(new PlayerDatasDTO
        {
            Side = Side.Black,
            CapturedPieces = [Piece.Queen, Piece.Rook]
        }, playerDatas, uiContext);

        Assert.That(playerDatas[Side.Black].CapturedPieces,
            Is.EqualTo(new[] { Piece.Queen, Piece.Rook }));
    }

    [Test]
    public void PatchMatchPointsWithValuesUpdatesMatchPointState()
    {
        MatchPoints matchPoints = new();

        Patcher.PatchMatchPoints(new MatchPointsDTO
        {
            MatchPointsReason = "Checkmate",
            ClaimForDraw = true,
            MatchEnded = true,
            WinnerID = 7
        }, matchPoints);

        Assert.Multiple(() =>
        {
            Assert.That(matchPoints.MatchPointsReason, Is.EqualTo("Checkmate"));
            Assert.That(matchPoints.ClaimForDraw, Is.True);
            Assert.That(matchPoints.MatchEnded, Is.True);
            Assert.That(matchPoints.WinnerID, Is.EqualTo(7));
        });
    }

    [Test]
    public async Task PatchMatchPointsWhenMatchEndsRunsOnMatchEndCallback()
    {
        TaskCompletionSource callbackTaskCompletionSource = new(TaskCreationOptions.RunContinuationsAsynchronously);

        MatchPoints matchPoints = new()
        {
            OnMatchEnd = () =>
            {
                callbackTaskCompletionSource.SetResult();
                return Task.CompletedTask;
            }
        };

        Patcher.PatchMatchPoints(new MatchPointsDTO { MatchEnded = true }, matchPoints);

        Task task = await Task.WhenAny(callbackTaskCompletionSource.Task, Task.Delay(1000));
        Assert.That(task, Is.SameAs(callbackTaskCompletionSource.Task));
    }

    [Test]
    public void PatchChatMessagesWithStatusUpdatesStatus()
    {
        ChatMessages chatMessages = new();

        Patcher.PatchChatMessages(new ChatMessagesDTO { Status = "Sent" }, chatMessages, uiContext);

        Assert.That(chatMessages.Status, Is.EqualTo("Sent"));
    }

    [Test]
    public void PatchChatMessagesWithNewMessageAppendsMessage()
    {
        ChatMessages chatMessages = new();

        Patcher.PatchChatMessages(new ChatMessagesDTO
        {
            NewMessage = new ChatMessageRow(12, "Szia")
        }, chatMessages, uiContext);

        Assert.That(chatMessages.ChatMessageList,
            Is.EqualTo(new[] { new ChatMessageRow(12, "Szia") }));
    }

    static Dictionary<Side, PlayerDataStore> CreatePlayerDatas() => new()
    {
        [Side.White] = new PlayerDataStore(null, null, [], 0, string.Empty),
        [Side.Black] = new PlayerDataStore(null, null, [], 0, string.Empty)
    };

    static ChessPiece[,] EmptyMatrix()
    {
        ChessPiece[,]? matrix = new ChessPiece[8, 8];

        for (int row = 0; row < 8; row++)
            for (int column = 0; column < 8; column++)
                matrix[row, column] = ChessPiece.Create(Piece.None, Side.None);

        return matrix;
    }
}
