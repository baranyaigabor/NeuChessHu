using Microsoft.Extensions.DependencyInjection;
using NeuChessHu.ViewModels.MainWindow;

namespace NeuChessHu.Bootstrap.Dependencies.AppServiceRegistrations;

internal static class UIServices
{
    internal static IServiceCollection AddUI(this IServiceCollection services) =>
        services.AddSingleton<MainWindowViewModel>()
                .AddSingleton<MainWindow>();
}