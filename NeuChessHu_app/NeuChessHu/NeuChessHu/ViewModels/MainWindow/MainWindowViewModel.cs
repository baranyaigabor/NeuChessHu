using ChessMechanics.Authentication;
using ChessMechanics.Authentication.Session;
using ChessMechanics.Common;
using ChessMechanics.MatchData.MatchDatas;
using ChessMechanics.WebSockets.Pusher;
using Microsoft.Extensions.DependencyInjection;
using NeuChessHu.Collections.Contexts;
using NeuChessHu.Controllers;
using NeuChessHu.Resources;
using NeuChessHu.Services.MatchServices;
using NeuChessHu.Services.SoundServices;
using NeuChessHu.UserSettings;
using NeuChessHu.ViewModels.NavBar;
using NeuChessHu.ViewModels.Overlays.MenuOverlays.MenuWindows;
using NeuChessHu.ViewModels.SideBars.MatchSideBar;
using System.Windows;
using System.Windows.Threading;

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
            WindowStyleOnMainOverlayInteraction();
            RaisePropertyChanged();
        }
    }
    public object? BoardOverlay
    {
        get => boardOverlay;
        private set
        {
            boardOverlay = value;
            WindowStyleOnBoardOverlayInteraction();
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

        lookingForMatchService.OnSwitchToMatch = SwitchToMatch;

        SwitchToMenu();
    }

    void SwitchToMenu()
    {
        if (MainOverlay is not null)
            MainOverlay = null;

        WindowStyleOnMainOverlayInteraction();

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

    void SwitchToMatch()
    {
        MainOverlay = null;

        matchContext = lookingForMatchService.MatchScope?.ServiceProvider
            .GetRequiredService<MatchContext>()
            ?? throw new Exception("MatchContext not found");

        MatchController matchController = lookingForMatchService.MatchScope?.ServiceProvider
            .GetRequiredService<MatchController>()
            ?? throw new Exception("MatchController not found");

        matchController.OnPlayAgain = PlayAgain;

        MatchDataStore matchDataStore = lookingForMatchService.MatchScope!.ServiceProvider
            .GetRequiredService<MatchDataStore>();

        matchDataStore.MatchPoints.OnMatchEnd = MatchEnd;

        navBar.ProfilePictureVisibility = Visibility.Hidden;
        navBar.FlagVisibility = Visibility.Hidden;

        CurrentBoard = matchContext.Board;
        CurrentSidebar = matchContext.MatchSideBar;

        matchContext.MatchSideBar.OnOpenOptions = () =>
        {
            MainOverlay = matchContext.MatchPopUpPanels.OptionsPopUp;
        };

        matchContext.MatchPopUpPanels.OptionsPopUp.OnOpenInGameSettings = () =>
        {
            MainOverlay = matchContext.MatchPopUpPanels.SettingsPopUp;
        };

        matchContext.Board.InteractionHandler.OnOpenPromotionWindow = () =>
        {
            BoardOverlay = matchContext.Board.InteractionHandler.PromotionWindow;
        };

        matchContext.MatchPopUpPanels.OptionsPopUp.OnCloseOverlay = CloseMainOverlay;
        matchContext.MatchPopUpPanels.SettingsPopUp.OnCloseOverlay = CloseMainOverlay;
        matchContext.MatchWindows.PromotionWindow.OnClosePromotionWindow = CloseBoardOverlay;
        matchContext!.MatchWindows.MatchEndWindow.OnCloseOverlay = CloseMainOverlay;

        matchContext!.MatchWindows.MatchEndWindow.OnSwitchMenu = SwitchToMenu;
        matchContext!.MatchWindows.MatchEndWindow.OnPlayAgain = PlayAgain;

        matchContext.MatchPopUpPanels.OptionsPopUp.OnQuit = SwitchToMenu;

        matchContext.MatchPopUpPanels.OptionsPopUp.OnShowConfirmationPanel = ShowConfirmationPanel;
    }

    void ShowConfirmationPanel(string confirming)
    {
        MatchSideBarViewModel matchSideBar = matchContext!.MatchSideBar;

        matchSideBar.ResignDrawConfirmationText = confirming is "Resign"
            ? AppResources.Get<string>("ResignConfirmationText")
            : AppResources.Get<string>("DrawConfirmationText");

        matchSideBar.ResignDrawConfirmationPanelVisibility = Visibility.Visible;
        matchSideBar.ShouldResignDrawConfirmationPanelBeVisible = true;

        if (matchSideBar.NotationsVisibility is not Visibility.Visible)
            matchSideBar.SwapNotationsChatPanel();
    }

    void ShouldLogin()
    {
        MainOverlay = menu.MenuPopUpPanels.LoginPopUp;
        menu.MenuPopUpPanels.LoginPopUp.ShouldNotify = true;
    }

    async Task PlayAgain()
    {
        pusher.ResetMatchChannel();

        MainOverlay = null;
        CurrentBoard = menu.Board;
        CurrentSidebar = menu.MenuSideBar;

        await StartLookingForMatchAsync();
    }

    async Task StartLookingForMatchAsync()
    {
        await pusher.InitializePusherClientAsync();

        LookingForMatchWindowViewModel lookingForMatchWindow = menu.MenuWindows.LookingForMatchWindow;

        MainOverlay = lookingForMatchWindow;

        lookingForMatchWindow.StartTimer();

        lookingForMatchWindow.MatchDuration = settings.LastMatchStockfish
            ? AppResources.Get<string>("AgainstStockfishText")
            : settings.LastMatchDuration + $"{(settings.LastMatchDuration.Contains('|')
                ? string.Empty
                : " " + AppResources.Get<string>("MinuteText"))}";

        CancellationTokenSource cancellationTokenSource = lookingForMatchService.CreateLookingForMatchCts();

        _ = Task.Run(async () =>
        {
            try
            {
                await Task.Delay(TimeSpan.FromMinutes(5), cancellationTokenSource.Token);

                Application.Current.Dispatcher.Invoke(CloseMainOverlay);

                await lookingForMatchService.DisposeMatchAsync();
            }
            catch (OperationCanceledException) { }
        });

        await lookingForMatchService.LookingForMatchAsync();
    }

    async Task MatchEnd()
    {
        await lookingForMatchService.DisposeMatchAsync();

        await Application.Current.Dispatcher.InvokeAsync(() =>
        {
            Sounds.Play("MatchEnd");

            if (BoardOverlay is not null)
                BoardOverlay = null;

            matchContext!.MatchSideBar.InputRowVisibility = Visibility.Collapsed;

            MainOverlay = matchContext!.MatchWindows.MatchEndWindow;
        });
    }

    void WindowStyleOnBoardOverlayInteraction()
    {
        if (BoardOverlay is null)
            MainOverlayStyle = AppResources.Get<Style>("ClosedPromotionWindowStyle");

        else BoardOverlayStyle = AppResources.Get<Style>("OpenPromotionWindowStyle");
    }

    void WindowStyleOnMainOverlayInteraction()
    {
        if (MainOverlay is null)
        {
            MainWindowStyle = AppResources.Get<Style>("MainWindowStyle");
            MainOverlayStyle = AppResources.Get<Style>("MainOverlayStyle");
        }
        else
        {
            MainWindowStyle = AppResources.Get<Style>("MainWindowOpenOverlayStyle");
            MainOverlayStyle = AppResources.Get<Style>("MainOpenOverlayStyle");
        }
    }

    void CloseBoardOverlay() =>
        BoardOverlay = null;

    internal void CloseMainOverlay() =>
        MainOverlay = null;
}