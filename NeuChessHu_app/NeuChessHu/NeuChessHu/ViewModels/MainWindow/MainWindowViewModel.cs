using ChessMechanics.Authentication;
using ChessMechanics.Authentication.Session;
using ChessMechanics.Common;
using ChessMechanics.WebSockets.Pusher;
using NeuChessHu.Collections.Contexts;
using NeuChessHu.Resources;
using NeuChessHu.Services.MatchServices;
using NeuChessHu.UserSettings;
using NeuChessHu.ViewModels.NavBar;
using NeuChessHu.ViewModels.Overlays.MenuOverlays.MenuWindows;
using System.Windows;

namespace NeuChessHu.ViewModels.MainWindow;

public class MainWindowViewModel : ObservableBase
{
    readonly BindableSettings settings;
    readonly SessionDatas session;
    readonly SessionManager sessionManager;
    readonly NavBarViewModel navBar;
    readonly MenuContext menu;
    readonly LookingForMatchService lookingForMatchService;
    readonly PusherClientService pusher;

    object? currentSidebar;
    object? currentBoard;
    object? mainOverlay;
    object? boardOverlay;
    Style? mainWindowStyle;
    Style? mainOverlayStyle;
    Style? boardOverlayStyle;
    MatchContext? matchContext;

    public NavBarViewModel NavBar => navBar;

    public object? CurrentSidebar
    {
        get => currentSidebar;
        private set { currentSidebar = value; RaisePropertyChanged(); }
    }
    public object? CurrentBoard
    {
        get => currentBoard;
        private set { currentBoard = value; RaisePropertyChanged(); }
    }
    public object? MainOverlay
    {
        get => mainOverlay;
        private set
        {
            mainOverlay = value;
            RaisePropertyChanged();
        }
    }
    public object? BoardOverlay
    {
        get => boardOverlay;
        private set
        {
            boardOverlay = value;
            RaisePropertyChanged();
        }
    }

    public Style? MainWindowStyle
    {
        get => mainWindowStyle;
        private set { mainWindowStyle = value; RaisePropertyChanged(); }
    }
    public Style? MainOverlayStyle
    {
        get => mainOverlayStyle;
        private set { mainOverlayStyle = value; RaisePropertyChanged(); }
    }
    public Style? BoardOverlayStyle
    {
        get => boardOverlayStyle;
        private set { boardOverlayStyle = value; RaisePropertyChanged(); }
    }

    public MainWindowViewModel(BindableSettings settings, SessionDatas session,
        SessionManager sessionManager, NavBarViewModel navBar, MenuContext menu,
        LookingForMatchService lookingForMatchService, PusherClientService pusher)
    {
        this.settings = settings;
        this.session = session;
        this.sessionManager = sessionManager;
        this.navBar = navBar;
        this.menu = menu;
        this.lookingForMatchService = lookingForMatchService;
        this.pusher = pusher;

        SwitchToMenu();
    }

    void SwitchToMenu()
    {
        if (MainOverlay is not null)
            MainOverlay = null;

        navBar.ProfilePictureVisibility = Visibility.Visible;
        navBar.FlagVisibility = Visibility.Visible;

        CurrentSidebar = menu.MenuSideBar;
        CurrentBoard = menu.Board;

        navBar.OnShowMenuPopUpCommand = () =>
        {
            if (session.UserID is null)
            {
                MainOverlay = menu.MenuPopUpPanels.LoginPopUp;
                menu.MenuPopUpPanels.LoginPopUp.ShouldNotify = false;
            }
            else MainOverlay = menu.MenuPopUpPanels.MenuPopUp;
        };

        menu.MenuPopUpPanels.MenuPopUp.OnOpenPreferencesPanel = () =>
        {
            MainOverlay = menu.MenuPopUpPanels.SettingsPopUp;
        };

        menu.MenuPopUpPanels.MenuPopUp.OnLogout = async () =>
        {
            await sessionManager.LogoutAsync();

            MainOverlay = null;
        };

        menu.MenuSideBar.OnOpenTimeSetter = () =>
        {
            if (session.UserID is not null)
                MainOverlay = menu.MenuWindows.TimeSetterWindow;

            else ShouldLogin();
        };

        menu.MenuPopUpPanels.LoginPopUp.OnCloseOverlay = CloseMainOverlay;
        menu.MenuPopUpPanels.MenuPopUp.OnCloseOverlay = CloseMainOverlay;
        menu.MenuPopUpPanels.SettingsPopUp.OnCloseOverlay = CloseMainOverlay;

        menu.MenuWindows.TimeSetterWindow.OnCloseOverlay = CloseMainOverlay;
        menu.MenuWindows.LookingForMatchWindow.OnStopLookingForMatch = SwitchToMenu;

        menu.MenuSideBar.OnStartMatch = async () =>
        {
            if (session.UserID is null)
                ShouldLogin();
            else await StartLookingForMatchAsync();
        };
    }

    void ShouldLogin()
    {
        MainOverlay = menu.MenuPopUpPanels.LoginPopUp;
        menu.MenuPopUpPanels.LoginPopUp.ShouldNotify = true;
    }

    async Task StartLookingForMatchAsync()
    {
        await pusher.InitializePusherClientAsync();

        LookingForMatchWindowViewModel lookingForMatchWindow = menu.MenuWindows.LookingForMatchWindow;

        MainOverlay = lookingForMatchWindow;

        lookingForMatchWindow.StartTimer();
        lookingForMatchWindow.MatchDuration = settings.LastMatchDuration +
            $"{(settings.LastMatchDuration.Contains('|')
            ? string.Empty
            : " " + AppResources.Get<string>("MinuteText"))}";

        await lookingForMatchService.LookingForMatchAsync();
    }
    internal void CloseMainOverlay() =>
        MainOverlay = null;
}