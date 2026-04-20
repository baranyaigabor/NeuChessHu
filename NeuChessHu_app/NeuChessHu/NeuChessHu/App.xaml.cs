using ChessMechanics.Authentication;
using ChessMechanics.Authentication.Session;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NeuChessHu.Configs;
using NeuChessHu.Resources;
using NeuChessHu.Resources.Images.Register.Icons;
using NeuChessHu.Resources.Images.Register.Images.Statics;
using NeuChessHu.Services.SoundServices;
using NeuChessHu.Templates;
using NeuChessHu.UserSettings.SettingManagers;
using NeuChessHu.ViewModels.MainWindow;
using System.Windows;
using System.Windows.Media.Imaging;

namespace NeuChessHu;

public partial class App : Application
{
    internal static IHost AppHost { get; private set; } = default!;

    protected override async void OnStartup(StartupEventArgs e)
    {
        Current.ThemeMode = ThemeMode.System;

        base.OnStartup(e);

        InitializeAppEnvironment();
        await InitializeUIEnvironment(e);
    }

    protected override async void OnExit(ExitEventArgs e)
    {
        if (AppHost?.Services.GetRequiredService<SessionDatas>()?.Token is not null)
            await AppHost.Services.GetRequiredService<SessionManager>().LogoutAsync();

        await AppHost!.StopAsync();
        AppHost.Dispose();

        base.OnExit(e);
    }

    static void InitializeAppEnvironment()
    {
        LanguageManager.ApplyLanguage(LanguageManager.Decode());

        AppThemeManager.ApplyTheme(AppThemeManager.Decode());

        BoardThemeManager.ApplyTheme(BoardThemeManager.Decode());

        UITemplates.MergeTemplates();

        StaticImages.Register();

        Sounds.LoadToMemory();

        AppIcon.Register();
    }

    static async Task InitializeUIEnvironment(StartupEventArgs e)
    {
        await CreateAppHost();

        if (e.Args.Length > 0 && e.Args[0].StartsWith("neuchesshu://auth/callback"))
            await AppHost.Services.GetRequiredService<SessionManager>().OnAuthenticated(e.Args[0]);

        MainWindow mainWindow = AppHost.Services.GetRequiredService<MainWindow>().WindowConfig(x =>
        {
            x.DataContext = AppHost.Services.GetRequiredService<MainWindowViewModel>();
            x.ResizeMode = ResizeMode.CanResize;
            x.WindowState = WindowState.Normal;
            x.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            x.Title = "NeuChess.hu";
            x.Height = 660;
            x.Width = 900;
            x.Icon = AppResources.Get<BitmapImage>("AppIcon");
            x.Show();
        });
    }

    static async Task CreateAppHost()
    {
        HostApplicationBuilder builder = Host.CreateApplicationBuilder();

        AppHost = builder.Build();

        await AppHost.StartAsync();
    }
}