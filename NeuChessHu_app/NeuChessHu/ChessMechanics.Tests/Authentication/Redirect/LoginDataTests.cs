using System.Text;
using ChessMechanics.Authentication.Redirect;
using NUnit.Framework;

namespace ChessMechanics.Tests.Authentication.Redirect;

[TestFixture]
public class LoginDataTests
{
    [Test]
    public void ExtractLoginDetailsWithValidEncodedDataReturnsTokenAndUserId()
    {
        string payload = Convert.ToBase64String(Encoding.UTF8.GetBytes(
            """{"token":"Token","user_id":42}"""));

        string url = $"neuchess://callback?data={Uri.EscapeDataString(payload)}";

        Tuple<string, int> result = LoginData.ExtractLoginDetails(url);

        Assert.Multiple(() =>
        {
            Assert.That(result.Item1, Is.EqualTo("Token"));
            Assert.That(result.Item2, Is.EqualTo(42));
        });
    }


    [Test]
    public void ExtractLoginDetailsWhenDataIsMissingThrows()
    {
        Exception? exception = Assert.Throws<Exception>(() =>
            LoginData.ExtractLoginDetails("neuchess://callback?state=ignored"));

        Assert.That(exception!.Message, Is.EqualTo("Parameter 'data' is missing!"));
    }
}