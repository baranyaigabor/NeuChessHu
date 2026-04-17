namespace ChessMechanics.WebSockets.ChessEngine.Requests.Payloads;

internal record MatchPointPayload(string channel, int userID, string matchPointReason)
{
    internal static MatchPointPayload Create(string channel, int userID, string matchPointReason) =>
        new(channel, userID, matchPointReason);
}