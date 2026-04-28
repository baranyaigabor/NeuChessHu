namespace ChessMechanics.WebSockets.ChessEngine.Requests.Payloads;

internal record IsLegalMovePayload(string channel, Tuple<int, int> from, Tuple<int, int> to)
{
    internal static IsLegalMovePayload CreateIsLegalMovePayload(string channel,
        Tuple<int, int> from, Tuple<int, int> to) =>
        new(channel, from, to);
}