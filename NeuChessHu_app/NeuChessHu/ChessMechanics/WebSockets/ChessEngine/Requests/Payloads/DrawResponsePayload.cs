namespace ChessMechanics.WebSockets.ChessEngine.Requests.Payloads;

internal record DrawResponsePayload(string channel, int userID, bool drawResponse)
{
    internal static DrawResponsePayload Create(string channel, int userID, bool drawResponse) =>
        new(channel, userID, drawResponse);
}