using System.Text;
using System.Text.Json;
using System.Web;

namespace ChessMechanics.Authentication.Redirect;

public static class LoginData
{
    public static Tuple<string, int> ExtractLoginDetails(string url)
    {
        string base64 = HttpUtility.ParseQueryString(new Uri(url).Query)["data"]!;

        if (string.IsNullOrEmpty(base64))
            throw new Exception("Parameter 'data' is missing!");

        string json = Encoding.UTF8.GetString(Convert.FromBase64String(base64));
        using JsonDocument doc = JsonDocument.Parse(json);

        string token = doc.RootElement.GetProperty("token").ToString();
        string userID = doc.RootElement.GetProperty("user_id").ToString();

        if (!int.TryParse(userID, out int parsedUserID))
            throw new Exception("'userID' is invalid");

        return Tuple.Create(token, parsedUserID);
    }
}
