using Microsoft.Extensions.Configuration;

namespace ChessMechanics.WebSocketss.Pusher.Config;

public record PusherConfig(string AppKey, string Cluster, bool Encrypted)
{
    static readonly IConfiguration configBuilder;

    static PusherConfig() =>
        configBuilder = ConfigLoader.Load("Resources/ConfigFiles/PusherConfig.json");

    public static PusherConfig Create() => new(
        configBuilder["pusher:AppKey"] ?? 
            throw new Exception("pusher's 'AppKey' could not be found in the configBuilder file!"),
        configBuilder["pusher:Cluster"] ?? 
            throw new Exception("pusher's 'Cluster' could not be found in the configBuilder file!"),
        bool.Parse(configBuilder["pusher:Encrypted"] ?? 
            throw new Exception("pusher's 'Encrypted' variable could not be found in the configBuilder file!"))
    );
}