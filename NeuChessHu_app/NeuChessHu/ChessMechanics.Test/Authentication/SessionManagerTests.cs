using ChessMechanics.APIs;
using ChessMechanics.Authentication;
using ChessMechanics.Authentication.Session;
using ChessMechanics.Authentication.User;
using NUnit.Framework;

namespace ChessMechanics.Tests.Authentication;

[TestFixture]
public class SessionManagerTests
{
    [Test]
    public async Task LogoutAsyncWhenTokenIsMissingDoesNotClearCurrentSession()
    {
        SessionDatas session = new()
        {
            UserID = 20,
            User = new UserData("User", null)
        };

        using APIHandlers apiHandlers = new(session);
        SessionManager manager = new(session, apiHandlers);

        await manager.LogoutAsync();

        Assert.Multiple(() =>
        {
            Assert.That(session.UserID, Is.EqualTo(20));
            Assert.That(session.User, Is.EqualTo(new UserData("User", null)));
        });
    }
}