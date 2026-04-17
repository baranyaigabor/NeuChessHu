using Microsoft.Extensions.Configuration;

namespace ChessMechanics.WebSocketss.Pusher.Config;

internal static class ConfigLoader
{
    internal static IConfigurationRoot Load(string configFile) =>
        new ConfigurationBuilder().SetBasePath(AppContext.BaseDirectory)
                                  .AddJsonFile(configFile, optional: true, reloadOnChange: true)
                                  .Build();
}