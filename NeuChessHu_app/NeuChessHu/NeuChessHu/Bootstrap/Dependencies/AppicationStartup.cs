using Microsoft.Extensions.DependencyInjection;
using NeuChessHu.Bootstrap.Dependencies.AppServiceRegistrations;

namespace NeuChessHu.Bootstrap.Dependencies;

internal static class AppicationStartup
{
    internal static IServiceCollection AddServiceCollections(this IServiceCollection services) =>
        services.AddSettings()
                .AddUI()
                .AddMenu()
                .AddMatch()
                .AddWebSocketLayer();
}