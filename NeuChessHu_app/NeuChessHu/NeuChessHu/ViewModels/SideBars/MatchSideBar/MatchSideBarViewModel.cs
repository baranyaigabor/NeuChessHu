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
    Thickness chatButtonThickness;

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

    public ICommand OpenCloseChatCommand { get; }
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

        ChatVisibility = Visibility.Collapsed;
        UnreadMessageNotificationVisibility = Visibility.Collapsed;
        ViolationNotificationVisibility = Visibility.Collapsed;
        ResignDrawConfirmationPanelVisibility = Visibility.Collapsed;

        ShouldResignDrawConfirmationPanelBeVisible = false;

        ChatButtonThickness = new Thickness(0.5, 0.5, 0.5, 1);

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
        });
    }

    void OnSettingsChanged(object? s, PropertyChangedEventArgs e)
    {
        if (e.PropertyName is nameof(BindableSettings.DarkMode))
            UsersInfosLoader();

        else if (e.PropertyName is nameof(BindableSettings.Language))
            ResignDrawConfirmationText = ResignDrawConfirmationText.StartsWith('D')
                ? AppResources.Get<string>("DrawConfirmationText")
                : AppResources.Get<string>("ResignConfirmationText");
    }

    void OnPlayerDataChanged(object? s, PropertyChangedEventArgs e)
    {
        if (e.PropertyName is not nameof(PlayerDataStore.Time))
            return;

        if (s == matchDataStore.PlayerDatas[playerSide])
            PlayerClock = matchDataStore.PlayerDatas[playerSide].Time;
        else OpponentClock = matchDataStore.PlayerDatas[opponentSide].Time;
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
}