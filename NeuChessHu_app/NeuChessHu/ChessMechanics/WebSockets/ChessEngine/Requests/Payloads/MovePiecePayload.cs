using ChessMechanics.ChessBoard.Definitions;

namespace ChessMechanics.WebSockets.ChessEngine.Requests.Payloads;

internal record MovePiecePayload(string channel, Tuple<int, int> from,
    Tuple<int, int> to, string promotionChoice)
{
    internal static MovePiecePayload CreateMovePiecePayload(string channel, 
        Tuple<int, int> from, Tuple<int, int> to, Piece promotionChoice) =>
        new(channel, from, to, promotionChoice.ToString());
}