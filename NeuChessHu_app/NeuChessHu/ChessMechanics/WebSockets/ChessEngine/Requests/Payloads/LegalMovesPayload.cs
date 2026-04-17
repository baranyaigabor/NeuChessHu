using ChessMechanics.ChessBoard.ChessPieces;
using ChessMechanics.ChessBoard.Definitions;

namespace ChessMechanics.WebSockets.ChessEngine.Requests.Payloads;

internal record LegalMovesPayload(string channel, Tuple<int, int> from,
    ChessPiece[,] pieceMatrix, string playingSide)
{
    internal static LegalMovesPayload CreateLegalMovesPayload(string channel,
        Tuple<int, int> from, ChessPiece[,] pieceMatrix, Side playingSide) =>
        new(channel, from, pieceMatrix, playingSide.ToString());
}