namespace ChessMechanics.WebSockets.ChessEngine.Requests.Payloads;

internal record ChatMessagePayload(string channel, int userID, string message)
{
    internal static ChatMessagePayload Create(string channel, int userID, string message) =>
        new(channel, userID, message);
}