using ChessMechanics.Authentication.Session;
using System.ComponentModel;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace ChessMechanics.APIs;

public class APIHandlers : IDisposable
{
    const string baseUrl = "http://backend.vm2.test/api/";

    readonly SessionDatas session;

    HttpClient? httpClient;

    HttpClient HttpClient => httpClient ??= CreateHttpClient();

    public APIHandlers(SessionDatas session)
    {
        this.session = session;
        session.PropertyChanged += OnSessionChanged;
    }

    void OnSessionChanged(object? s, PropertyChangedEventArgs e)
    {
        if (e.PropertyName is nameof(SessionDatas.Token))
            CreateHttpClient();
    }

    HttpClient CreateHttpClient() => new()
    {
        Timeout = TimeSpan.FromSeconds(10),
        DefaultRequestHeaders =
        {
            Authorization = new AuthenticationHeaderValue("Bearer", session.Token),
            Accept = { new MediaTypeWithQualityHeaderValue("application/json") }
        }
    };

    internal async Task<string> HttpGetUserAsync(int? userID) =>
        await HttpClient.GetStringAsync($"{baseUrl}users/{userID}");

    public async Task HttpJoinMatchmakingQueueAsync(string matchDuration) =>
        await HttpClient.PostAsJsonAsync($"{baseUrl}join/matchmakingqueue",
            new { playerID = session.UserID, matchDuration }).ConfigureAwait(false);

    public async Task HttpLeaveMatchmakingQueueAsync() =>
        await HttpClient.PostAsJsonAsync($"{baseUrl}leave/matchmakingqueue",
            new { playerID = session.UserID }).ConfigureAwait(false);

    internal async Task HttpLogoutAsync() =>
        await HttpClient.PostAsJsonAsync($"{baseUrl}desktop/logout",
            new { playerID = session.UserID }).ConfigureAwait(false);

    internal async Task<string> HttpAuthenticateAsync(string socketID, string channel) =>
        await (await HttpClient.PostAsJsonAsync($"{baseUrl}broadcasting/auth",
            new { socket_id = socketID, channel_name = channel })).Content.ReadAsStringAsync();

    internal async Task<string> HttpMatchReadyAsync(string channel) =>
        await (await HttpClient.PostAsJsonAsync($"{baseUrl}match/ready",
            new { channel_name = channel })).Content.ReadAsStringAsync();

    internal async Task<string?> HttpGetPendingChannelAsync() =>
        await HttpClient.GetStringAsync($"{baseUrl}match/pendingchannel");

    public void Dispose()
    {
        httpClient?.Dispose();
        httpClient = null;
    }
}