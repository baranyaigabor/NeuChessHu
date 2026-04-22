using Microsoft.Extensions.DependencyInjection;
using NeuChessHu.Properties;
using NeuChessHu.UserSettings;
using NeuChessHu.ViewModels.Overlays.SettingsPopUp;

namespace NeuChessHu.Bootstrap.Dependencies.AppServiceRegistrations;

internal static class SettingServices
{
    internal static IServiceCollection AddSettings(this IServiceCollection services) =>
        services.AddSingleton(Settings.Default)
                .AddSingleton<BindableSettings>()
                .AddTransient<SettingsPopUpViewModel>();
}
