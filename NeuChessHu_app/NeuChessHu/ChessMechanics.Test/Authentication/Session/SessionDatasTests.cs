using ChessMechanics.Authentication.Session;
using ChessMechanics.Authentication.User;
using NUnit.Framework;

namespace ChessMechanics.Test.Authentication.Session;

[TestFixture]
public class SessionDatasTests
{
    [Test]
    public void UserWhenSetRaisesPropertyChanged()
    {
        SessionDatas session = new();

        string? propertyName = null;

        session.PropertyChanged += (_, e) => propertyName = e.PropertyName;

        session.User = new UserData("Player", null);

        Assert.That(propertyName, Is.EqualTo(nameof(SessionDatas.User)));
    }

    [Test]
    public void ClearSessionWithExistingValuesRemovesTokenUserAndUserId()
    {
        SessionDatas session = new()
        {
            Token = "Token",
            UserID = 15,
            User = new UserData("User", "Picture")
        };

        session.ClearSession();

        Assert.Multiple(() =>
        {
            Assert.That(session.Token, Is.Null);
            Assert.That(session.UserID, Is.Null);
            Assert.That(session.User, Is.Null);
        });
    }
}
