using ChessMechanics.APIs;
using System.Diagnostics;
using System.Text.Json;

namespace ChessMechanics.Authentication.User;

public record UserData(string Nickname, string? ProfilePicture)
{
    internal static async Task<UserData?> CreateUser(APIHandlers apiHandlers, int? userID)
    {
        if (userID is null)
            return null;

        string json = await apiHandlers.HttpGetUserAsync(userID);
        JsonElement data = JsonDocument.Parse(json).RootElement.GetProperty("data");

        string nickname = data.GetProperty("nickname").ToString();

        JsonElement picture = data.GetProperty("profile_picture");
        string? profilePictureBase64 = picture.ValueKind is not JsonValueKind.Null
            ? picture.ToString()
            : null;

        if (profilePictureBase64 != null)
            Debug.WriteLine("\n\n\n\n" + profilePictureBase64 + "\n\n\n\n");

        return new UserData(nickname, profilePictureBase64);
    }
};