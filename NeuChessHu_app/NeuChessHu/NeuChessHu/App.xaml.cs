using ChessMechanics.Authentication;
using ChessMechanics.Authentication.Session;
using Microsoft.Extensions.DependencyInjection;
using NeuChessHu.Bootstrap.Protocols;
using NeuChessHu.Callback;
using NeuChessHu.Configs;
using NeuChessHu.Resources;
using NeuChessHu.Resources.Images.Register.Icons;
using NeuChessHu.Resources.Images.Register.Images.Statics;
using NeuChessHu.Resources.Styles;
using NeuChessHu.Resources.Triggers;
using NeuChessHu.Services.SoundServices;
using NeuChessHu.Templates;
using NeuChessHu.UserSettings.SettingManagers;
using NeuChessHu.ViewModels.MainWindow;
using System.Windows;
using System.Windows.Media.Imaging;
using Microsoft.Extensions.Hosting;
using NeuChessHu.Bootstrap.Dependencies;

namespace NeuChessHu;

public partial class App : Application
{
    internal static IHost AppHost { get; private set; } = default!;

    protected override async void OnStartup(StartupEventArgs e)
    {
        Current.ThemeMode = ThemeMode.System;

        base.OnStartup(e);

        if (!SingleInstanceManager.CanCreateNewInstance(this, e))
            return;

        InitializeAppEnvironment();
        await InitializeUIEnvironment(e);
    }

    protected override async void OnExit(ExitEventArgs e)
    {
        if (AppHost is not null)
        {
            if (AppHost.Services.GetRequiredService<SessionDatas>()?.Token is not null)
                await AppHost.Services.GetRequiredService<SessionManager>().LogoutAsync();

            await AppHost.StopAsync();
            AppHost.Dispose();
        }

        SingleInstanceManager.Dispose();

        base.OnExit(e);
    }

    static void InitializeAppEnvironment()
    {
        LanguageManager.ApplyLanguage(LanguageManager.Decode());

        AppThemeManager.ApplyTheme(AppThemeManager.Decode());

        BoardThemeManager.ApplyTheme(BoardThemeManager.Decode());

        UITemplates.MergeTemplates();

        Triggers.MergeTriggers();
        Styles.MergeStyles();

        StaticImages.Register();

        Sounds.LoadToMemory();

        Protocols.RegisterAppURL();

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

        builder.Services.AddServiceCollections();
        AppHost = builder.Build();

        await AppHost.StartAsync();
    }
}