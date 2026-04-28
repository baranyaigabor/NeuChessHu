using ChessMechanics.APIs;
using ChessMechanics.Authentication.Redirect;
using ChessMechanics.Authentication.Session;
using ChessMechanics.Authentication.User;

namespace ChessMechanics.Authentication;

public record SessionManager(SessionDatas Session, APIHandlers HttpClients)
{
    public async Task OnAuthenticated(string callbackUrl)
    {
        Tuple<string, int> loginDetails = LoginData.ExtractLoginDetails(callbackUrl);

        Session.Token = loginDetails.Item1;
        Session.UserID = loginDetails.Item2;

        Session.User = await UserData.CreateUser(HttpClients, Session.UserID);
    }

    public async Task LogoutAsync()
    {
        if (string.IsNullOrWhiteSpace(Session.Token))
            return;

        try
        {
            await HttpClients.HttpLeaveMatchmakingQueueAsync();
            await HttpClients.HttpLogoutAsync();

            Session.ClearSession();
        }
        catch (Exception ex)
        {
            throw new Exception("An error occurred during logout.", ex);
        }
    }
}