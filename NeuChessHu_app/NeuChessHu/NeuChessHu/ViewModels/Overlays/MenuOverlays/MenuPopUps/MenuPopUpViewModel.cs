using ChessMechanics.Authentication.Session;
using NeuChessHu.CommandUtils;
using NeuChessHu.UserSettings;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;

namespace NeuChessHu.ViewModels.Overlays.MenuOverlays.MenuPopUps;

public class MenuPopUpViewModel
{
    internal Action? OnOpenPreferencesPanel { get; set; }
    internal Action? OnLogout { get; set; }
    internal Action? OnCloseOverlay { get; set; }

    public ICommand OpenProfileSettingsCommand { get; }
    public ICommand OpenPreferencesCommand { get; }
    public ICommand LogoutCommand { get; }
    public ICommand QuitCommand { get; }
    public ICommand GoBackCommand { get; }

    public MenuPopUpViewModel(SessionDatas session)
    {
        OpenProfileSettingsCommand = new CommandExecuter<object?>(_ => OpenProfileSettings(session.User!.Nickname));
        OpenPreferencesCommand = new CommandExecuter<object?>(_ => OnOpenPreferencesPanel?.Invoke());
        LogoutCommand = new CommandExecuter<object?>(_ => OnLogout?.Invoke());
        QuitCommand = new CommandExecuter<object?>(_ => QuitApplication());
        GoBackCommand = new CommandExecuter<object?>(_ => OnCloseOverlay?.Invoke());
    }

    static void OpenProfileSettings(string nickname) => Process.Start(startInfo: new ProcessStartInfo
    {
        FileName = "http://frontend.vm2.test/" + nickname,
        UseShellExecute = true
    });

    void QuitApplication() =>
        Application.Current.Shutdown();
}