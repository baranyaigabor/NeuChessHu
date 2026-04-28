using ChessMechanics.Authentication.Session;
using ChessMechanics.ChessBoard.Definitions;
using ChessMechanics.Common;
using ChessMechanics.MatchData.MatchDatas;
using ChessMechanics.MatchData.MatchDatas.Models;
using ChessMechanics.MatchData.MatchDatas.Models.DomainModels;
using ChessMechanics.WebSockets.ChessEngine.Requests;
using NeuChessHu.CommandUtils;
using NeuChessHu.Converters;
using NeuChessHu.Resources;
using NeuChessHu.Resources.Types;
using NeuChessHu.UserSettings;
using NeuChessHu.ViewModels.SideBars.MatchSideBar.Displays;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace NeuChessHu.ViewModels.SideBars.MatchSideBar;

public class MatchSideBarViewModel : ObservableBase
{
    readonly BindableSettings settings;
    readonly SessionDatas session;
    readonly MatchDataStore matchDataStore;
    readonly EngineRequests requests;

    Side opponentSide;
    Side playerSide;

    bool isDrawOfferPending;

    string opponentNickname;
    string playerNickname;
    ImageSource? opponentProfilePicture;
    ImageSource? playerProfilePicture;
    Style opponentProfilePictureStyle;
    Style playerProfilePictureStyle;

    string opponentClock;
    string playerClock;
    string opponentPoints;
    string playerPoints;

    string messageInput;
    ScrollTo notationsAndChatScrollDirection;
    Visibility notationsVisibility;
    Visibility chatVisibility;
    Visibility chatMessagePlaceholderTextVisibility;
    Visibility unreadMessageNotificationVisibility;
    Visibility violationNotificationVisibility;
    Visibility resignDrawConfirmationPanelVisibility;
    Visibility inputRowVisibility;
    Visibility chatImageVisibility;
    Thickness chatButtonThickness;
    ICommand? openCloseChatCommand;

    string resignDrawConfirmationText;

    public string OpponentNickname
    {
        get => opponentNickname;
        private set
        {
            opponentNickname = value;
            RaisePropertyChanged();
        }
    }
    public string PlayerNickname
    {
        get => playerNickname;
        private set
        {
            playerNickname = value;
            RaisePropertyChanged();
        }
    }
    public ImageSource? OpponentProfilePicture
    {
        get => opponentProfilePicture;
        private set
        {
            opponentProfilePicture = value;
            RaisePropertyChanged();
        }
    }
    public ImageSource? PlayerProfilePicture
    {
        get => playerProfilePicture;
        private set
        {
            playerProfilePicture = value;
            RaisePropertyChanged();
        }
    }

    public Style OpponentProfilePictureStyle
    {
        get => opponentProfilePictureStyle;
        private set
        {
            opponentProfilePictureStyle = value;
            RaisePropertyChanged();
        }
    }
    public Style PlayerProfilePictureStyle
    {
        get => playerProfilePictureStyle;
        private set
        {
            playerProfilePictureStyle = value;
            RaisePropertyChanged();
        }
    }

    public string OpponentClock
    {
        get => opponentClock;
        private set { opponentClock = value; RaisePropertyChanged(); }
    }
    public string PlayerClock
    {
        get => playerClock;
        private set { playerClock = value; RaisePropertyChanged(); }
    }

    public string OpponentPoints
    {
        get => opponentPoints;
        private set { opponentPoints = value; RaisePropertyChanged(); }
    }

    public string PlayerPoints
    {
        get => playerPoints;
        private set { playerPoints = value; RaisePropertyChanged(); }
    }

    public ObservableCollection<CapturedPiecesDisplay> OpponentPieces { get; } = [];
    public ObservableCollection<CapturedPiecesDisplay> PlayerPieces { get; } = [];

    public ObservableCollection<SANNotationRow> Notations => matchDataStore.MatchState.Notations!;
    public ObservableCollection<ChatMessageDisplay> ChatMessageDisplays { get; } = [];

    public string MessageInput
    {
        get => messageInput;
        set
        {
            messageInput = string.IsNullOrWhiteSpace(value)
                ? value
                : char.ToUpper(value[0]) + value.Substring(1);

            ChatMessagePlaceholderTextVisibility = string.IsNullOrWhiteSpace(value)
                ? Visibility.Visible
                : Visibility.Collapsed;

            RaisePropertyChanged();
        }
    }

    public ScrollTo NotationsScrollDirection
    {
        get => notationsAndChatScrollDirection;
        private set
        {
            notationsAndChatScrollDirection = value;
            RaisePropertyChanged();
        }
    }

    public ScrollTo ChatScrollDirection
    {
        get => notationsAndChatScrollDirection;
        private set
        {
            notationsAndChatScrollDirection = value;
            RaisePropertyChanged();
        }
    }

    public Visibility NotationsVisibility
    {
        get => notationsVisibility;
        private set
        {
            notationsVisibility = value;
            RaisePropertyChanged();
        }
    }

    public Visibility ChatVisibility
    {
        get => chatVisibility;
        internal set
        {
            chatVisibility = value;
            RaisePropertyChanged();
        }
    }

    public Visibility ChatMessagePlaceholderTextVisibility
    {
        get => chatMessagePlaceholderTextVisibility;
        private set
        {
            chatMessagePlaceholderTextVisibility = value;
            RaisePropertyChanged();
        }
    }

    public Visibility UnreadMessageNotificationVisibility
    {
        get => unreadMessageNotificationVisibility;
        private set
        {
            unreadMessageNotificationVisibility = value;
            RaisePropertyChanged();
        }
    }

    public Visibility ViolationNotificationVisibility
    {
        get => violationNotificationVisibility;
        private set
        {
            violationNotificationVisibility = value;
            RaisePropertyChanged();
        }
    }

    public Visibility ResignDrawConfirmationPanelVisibility
    {
        get => resignDrawConfirmationPanelVisibility;
        internal set
        {
            resignDrawConfirmationPanelVisibility = value;
            RaisePropertyChanged();
        }
    }

    public Visibility InputRowVisibility
    {
        get => inputRowVisibility;
        internal set
        {
            inputRowVisibility = value;
            RaisePropertyChanged();
        }
    }

    public Visibility ChatImageVisibility
    {
        get => chatImageVisibility;
        private set
        {
            chatImageVisibility = value;
            RaisePropertyChanged();
        }
    }

    public Thickness ChatButtonThickness
    {
        get => chatButtonThickness;
        private set
        {
            chatButtonThickness = value;
            RaisePropertyChanged();
        }
    }

    public string ResignDrawConfirmationText
    {
        get => resignDrawConfirmationText;
        internal set
        {
            resignDrawConfirmationText = value;
            RaisePropertyChanged();
        }
    }

    public bool ShouldResignDrawConfirmationPanelBeVisible { get; internal set; }

    public Action? OnOpenOptions { get; internal set; }

    public ICommand? OpenCloseChatCommand
    {
        get => openCloseChatCommand;
        private set
        {
            openCloseChatCommand = value;
            RaisePropertyChanged();
        }
    }

    public ICommand SendChatMessageCommand { get; }
    public ICommand ConfirmOrCancelCommand { get; }
    public ICommand OpenOptionsCommand { get; }

    public MatchSideBarViewModel(BindableSettings settings, SessionDatas session,
        MatchDataStore matchDataStore, EngineRequests requests)
    {
        this.settings = settings;
        this.session = session;
        this.matchDataStore = matchDataStore;
        this.requests = requests;

        settings.PropertyChanged += OnSettingsChanged;

        matchDataStore.MatchPoints.PropertyChanged += OnMatchPointsChanged;

        foreach (Side side in new[] { Side.White, Side.Black })
        {
            matchDataStore.PlayerDatas[side].PropertyChanged += OnPlayerDatasChanged;
            matchDataStore.PlayerDatas[side].CapturedPieces.CollectionChanged += OnCapturedPiecesChanged;
        }

        Notations.CollectionChanged += OnNotationsChanged;

        matchDataStore.ChatMessageList.ChatMessageList.CollectionChanged += OnChatMessagesChanged;

        ChatVisibility = Visibility.Collapsed;
        UnreadMessageNotificationVisibility = Visibility.Collapsed;
        ViolationNotificationVisibility = Visibility.Collapsed;
        ResignDrawConfirmationPanelVisibility = Visibility.Collapsed;

        ShouldResignDrawConfirmationPanelBeVisible = false;

        ChatButtonThickness = new Thickness(0.5, 0.5, 0.5, 1);

        OpenCloseChatCommand = new CommandExecuter<object?>(_ => SwapNotationsChatPanel());
        SendChatMessageCommand = new CommandExecuter<string>(async x => await SendMessage());
        ConfirmOrCancelCommand = new CommandExecuter<bool>(async args => await OnConfirmOrCancel(args));
        OpenOptionsCommand = new CommandExecuter<object?>(_ => OnOpenOptions?.Invoke());

        _ = InitializeAsync();
    }

    async Task InitializeAsync()
    {
        await matchDataStore.Initialize;

        await Application.Current.Dispatcher.InvokeAsync(async () =>
        {
            playerSide = matchDataStore.PlayingSide;
            opponentSide = playerSide is Side.White ? Side.Black : Side.White;

            PlayerClock = matchDataStore.PlayerDatas[playerSide].Time;
            OpponentClock = matchDataStore.PlayerDatas[opponentSide].Time;

            UsersInfosLoader();
            ProfilePictureStyleSetter();
        });
    }

    void OnSettingsChanged(object? s, PropertyChangedEventArgs e)
    {
        if (e.PropertyName is nameof(BindableSettings.DarkMode))
            UsersInfosLoader();

        else if (e.PropertyName is nameof(BindableSettings.Language) && ResignDrawConfirmationText is not null)
        {
            ResignDrawConfirmationText = ResignDrawConfirmationText.StartsWith('D')
                ? AppResources.Get<string>("DrawConfirmationText")
                : AppResources.Get<string>("ResignConfirmationText");
        }
    }

    void OnPlayerDatasChanged(object? s, PropertyChangedEventArgs e)
    {

        if (e.PropertyName is nameof(PlayerDataStore.UserData))
        {
            UpdateChatAvailability();
            return;
        }

        if (e.PropertyName is nameof(PlayerDataStore.Time))
        {
            if (s == matchDataStore.PlayerDatas[playerSide])
                PlayerClock = matchDataStore.PlayerDatas[playerSide].Time;
            else OpponentClock = matchDataStore.PlayerDatas[opponentSide].Time;
        }
    }

    void OnNotationsChanged(object? sender, NotifyCollectionChangedEventArgs e)
    {
        NotationsScrollDirection = ScrollTo.Top;
        NotationsScrollDirection = ScrollTo.Bottom;
    }

    void OnMatchPointsChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName is nameof(MatchPoints.ClaimForDraw) && matchDataStore.MatchPoints.ClaimForDraw
            && !isDrawOfferPending)
        {
            isDrawOfferPending = true;

            if (NotationsVisibility is not Visibility.Visible)
                SwapNotationsChatPanel();

            ResignDrawConfirmationText = AppResources.Get<string>("DrawConfirmationText");
            ResignDrawConfirmationPanelVisibility = Visibility.Visible;
            ShouldResignDrawConfirmationPanelBeVisible = true;
        }

        else if (e.PropertyName is nameof(MatchPoints.ClaimForDraw) && !matchDataStore.MatchPoints.ClaimForDraw)
            isDrawOfferPending = false;
    }

    void OnCapturedPiecesChanged(object? sender, NotifyCollectionChangedEventArgs e)
    {
        Side capturingSide = sender == matchDataStore.PlayerDatas[Side.White].CapturedPieces
            ? Side.White
            : Side.Black;

        UpdateCapturedPieces(capturingSide);
    }

    void UpdateCapturedPieces(Side capturingSide)
    {
        int opponentPoints = matchDataStore.PlayerDatas[opponentSide].Points;
        int playerPoints = matchDataStore.PlayerDatas[matchDataStore.PlayingSide].Points;

        if (opponentPoints > playerPoints)
        {
            opponentPoints -= playerPoints;
            playerPoints = 0;
        }
        else if (playerPoints > opponentPoints)
        {
            playerPoints -= opponentPoints;
            opponentPoints = 0;
        }
        else
        {
            opponentPoints = 0;
            playerPoints = 0;
        }

        OpponentPoints = opponentPoints > 0 ? "+" + opponentPoints : string.Empty;
        PlayerPoints = playerPoints > 0 ? "+" + playerPoints : string.Empty;

        RefreshCapturedImagesOrder(capturingSide);
    }

    void RefreshCapturedImagesOrder(Side capturingSide)
    {
        if (capturingSide == Side.White)
        {
            if (playerSide is Side.White)
                CapturedPiecesDisplay.Add(PlayerPieces, matchDataStore.PlayerDatas[Side.White].CapturedPieces, opponentSide, settings);
            else CapturedPiecesDisplay.Add(OpponentPieces, matchDataStore.PlayerDatas[Side.White].CapturedPieces, playerSide, settings);
        }
        else if (capturingSide == Side.Black)
        {
            if (playerSide is Side.Black)
                CapturedPiecesDisplay.Add(PlayerPieces, matchDataStore.PlayerDatas[Side.Black].CapturedPieces, opponentSide, settings);
            else CapturedPiecesDisplay.Add(OpponentPieces, matchDataStore.PlayerDatas[Side.Black].CapturedPieces, playerSide, settings);
        }
    }

    internal void SwapNotationsChatPanel()
    {
        if (NotationsVisibility is Visibility.Visible)
        {
            NotationsVisibility = Visibility.Collapsed;

            ChatVisibility = Visibility.Visible;
            ChatScrollDirection = ScrollTo.Top;
            ChatScrollDirection = ScrollTo.Bottom;

            ChatButtonThickness = new Thickness(0.5, 0.5, 0.5, 0);

            if (UnreadMessageNotificationVisibility is Visibility.Visible)
                UnreadMessageNotificationVisibility = Visibility.Collapsed;

            if (ResignDrawConfirmationPanelVisibility is Visibility.Visible)
                ResignDrawConfirmationPanelVisibility = Visibility.Collapsed;
        }
        else
        {
            ChatVisibility = Visibility.Collapsed;

            NotationsVisibility = Visibility.Visible;
            NotationsScrollDirection = ScrollTo.Top;
            NotationsScrollDirection = ScrollTo.Bottom;

            ChatButtonThickness = new Thickness(0.5, 0.5, 0.5, 1);

            if (ShouldResignDrawConfirmationPanelBeVisible)
                ResignDrawConfirmationPanelVisibility = Visibility.Visible;
        }
    }

    void OnChatMessagesChanged(object? s, NotifyCollectionChangedEventArgs e)
    {
        if (e.NewItems is not null)
        {
            foreach (ChatMessageRow messageRow in e.NewItems)
                ChatMessageDisplay.Add(ChatMessageDisplays, messageRow, (int)session.UserID!, playerSide);

            if (ChatVisibility is Visibility.Collapsed)
                UnreadMessageNotificationVisibility = Visibility.Visible;

            ChatScrollDirection = ScrollTo.Top;
            ChatScrollDirection = ScrollTo.Bottom;
        }
    }

    void UsersInfosLoader()
    {
        PlayerNickname = matchDataStore.PlayerDatas[playerSide].UserData!.Nickname;
        OpponentNickname = matchDataStore.PlayerDatas[opponentSide].UserData!.Nickname;

        string playerProfilePictureString = matchDataStore.PlayerDatas[playerSide].UserData!.ProfilePicture!;
        string opponentProfilePictureString = matchDataStore.PlayerDatas[opponentSide].UserData!.ProfilePicture!;

        PlayerProfilePicture = playerProfilePictureString is not "Unknown"
            ? ImageConverters.LoadProfilePicture(playerProfilePictureString)
            : AppResources.Get<ImageSource>("DefaultProfilePictureImage");

        OpponentProfilePicture = opponentProfilePictureString is not "Unknown"
            ? ImageConverters.LoadProfilePicture(opponentProfilePictureString)
            : AppResources.Get<ImageSource>("DefaultProfilePictureImage");
    }

    void ProfilePictureStyleSetter()
    {
        OpponentProfilePictureStyle =
            GetStyle(matchDataStore.PlayerDatas[opponentSide].UserData!.ProfilePicture!);
        PlayerProfilePictureStyle =
            GetStyle(matchDataStore.PlayerDatas[playerSide].UserData!.ProfilePicture!);
    }

    static Style GetStyle(string profilePicture) =>
        AppResources.Get<Style>(profilePicture is "Unknown"
            ? "DefaultProfilePictureStyle"
            : "ProfilePictureStyle");

    async Task SendMessage()
    {
        if (string.IsNullOrWhiteSpace(MessageInput))
            return;

        string response = await requests.ChatMessageRequestAsync(matchDataStore.MatchChannel!,
            MessageInput, (int)session.UserID!);

        MessageInput = string.Empty;

        if (response is "Violation")
            ShowViolationNotification();
    }

    void ShowViolationNotification()
    {
        ViolationNotificationVisibility = Visibility.Visible;

        _ = Task.Run(async () =>
        {
            await Task.Delay(5000);
            Application.Current.Dispatcher.Invoke(() =>
            {
                ViolationNotificationVisibility = Visibility.Collapsed;
            });
        });
    }

    void UpdateChatAvailability()
    {
        string?[] nicknames = [
            matchDataStore.PlayerDatas[Side.White].UserData?.Nickname,
            matchDataStore.PlayerDatas[Side.Black].UserData?.Nickname
        ];

        if (nicknames.Any(x => x is "Stockfish"))
        {
            ChatImageVisibility = Visibility.Collapsed;
            OpenCloseChatCommand = null;

            if (ChatVisibility is Visibility.Visible)
            {
                ChatVisibility = Visibility.Collapsed;
                NotationsVisibility = Visibility.Visible;
                ChatButtonThickness = new Thickness(0.5, 0.5, 0.5, 1);
            }

            return;
        }

        ChatImageVisibility = Visibility.Visible;
        OpenCloseChatCommand ??= new CommandExecuter<object?>(_ => SwapNotationsChatPanel());
    }

    async Task OnConfirmOrCancel(bool isConfirmed)
    {
        ResignDrawConfirmationPanelVisibility = Visibility.Collapsed;
        ShouldResignDrawConfirmationPanelBeVisible = false;

        if (ResignDrawConfirmationText == AppResources.Get<string>("ResignConfirmationText"))
        {
            if (isConfirmed)
                await requests.MatchPointRequestAsync(matchDataStore.MatchChannel!, (int)session.UserID!, "Resign");
            else return;
        }

        else await requests.DrawResponseRequestAsync(matchDataStore.MatchChannel!, (int)session.UserID!, isConfirmed);
    }

    public void Dispose()
    {
        settings.PropertyChanged -= OnSettingsChanged;

        foreach (Side side in new[] { Side.White, Side.Black })
        {
            matchDataStore.PlayerDatas[side].PropertyChanged -= OnPlayerDatasChanged;
            matchDataStore.PlayerDatas[side].CapturedPieces.CollectionChanged -= OnCapturedPiecesChanged;
        }

        matchDataStore.MatchPoints.PropertyChanged -= OnMatchPointsChanged;
        Notations.CollectionChanged -= OnNotationsChanged;
        matchDataStore.ChatMessageList.ChatMessageList.CollectionChanged -= OnChatMessagesChanged;
    }
}
