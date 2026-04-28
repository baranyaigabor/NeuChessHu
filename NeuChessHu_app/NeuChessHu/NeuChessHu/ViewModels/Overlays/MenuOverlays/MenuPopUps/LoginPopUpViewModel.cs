using ChessMechanics.Common;
using NeuChessHu.CommandUtils;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;

namespace NeuChessHu.ViewModels.Overlays.MenuOverlays.MenuPopUps;

public class LoginPopUpViewModel : ObservableBase
{
    bool shouldNotify;
    int panelHeight;
    Visibility loginNotificationVisibility;

    public bool ShouldNotify 
    { 
        get => shouldNotify; 
        internal set
        {
            shouldNotify = value;
            SetLoginNotification();
        } 
    }

    public int PanelHeight
    {
        get => panelHeight;
        private set
        {
            panelHeight = value;
            RaisePropertyChanged();
        }
    }
    public Visibility LoginNotificationVisibility
    {
        get => loginNotificationVisibility;
        private set
        {
            loginNotificationVisibility = value;
            RaisePropertyChanged();
        }
    }

    public Action? OnCloseOverlay { get; set; }

    public ICommand OpenLoginPanelCommand { get; }
    public ICommand GoBackCommand { get; }

    public LoginPopUpViewModel()
    {
        SetLoginNotification();

        OpenLoginPanelCommand = new CommandExecuter<object?>(_ => OpenLoginPanel());
        GoBackCommand = new CommandExecuter<object?>(_ => OnCloseOverlay?.Invoke());
    }

    void SetLoginNotification()
    {
        if (ShouldNotify)
        {
            PanelHeight = 120;
            LoginNotificationVisibility = Visibility.Visible;
        }
        else
        {
            PanelHeight = 82;
            LoginNotificationVisibility = Visibility.Collapsed;
        }
    }

    static void OpenLoginPanel() => Process.Start(startInfo: new ProcessStartInfo
    {
        FileName = "http://backend.vm2.test/desktop/authorize?redirect_uri=neuchesshu://auth/callback",
        UseShellExecute = true
    });
}