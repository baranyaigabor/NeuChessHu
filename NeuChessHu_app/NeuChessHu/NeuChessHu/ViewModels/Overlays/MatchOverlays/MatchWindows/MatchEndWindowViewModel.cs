using ChessMechanics.ChessBoard.Definitions;
using ChessMechanics.Common;
using ChessMechanics.MatchData.MatchDatas;
using ChessMechanics.MatchData.MatchDatas.Models;
using NeuChessHu.CommandUtils;
using NeuChessHu.Converters;
using NeuChessHu.Resources;
using NeuChessHu.UserSettings;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace NeuChessHu.ViewModels.Overlays.MatchOverlays.MatchWindows;

public class MatchEndWindowViewModel : ObservableBase, IDisposable
{
    readonly BindableSettings settings;
    readonly MatchDataStore matchDataStore;

    Side playerSide;
    Side opponentSide;

    string matchEndReason;
    string matchResult;
    ImageSource? playerProfilePicture;
    ImageSource? opponentProfilePicture;
    Style opponentProfilePictureStyle;
    Style playerProfilePictureStyle;
    Brush playerMatchResultBrush;
    Brush opponentMatchResultBrush;
    Visibility opponentMedalVisibility;
    Visibility playerMedalVisibility;

    public string MatchEndReason
    {
        get => matchEndReason;
        private set
        {
            matchEndReason = value;
            RaisePropertyChanged();
        }
    }

    public string? MatchResult
    {
        get => matchResult;
        private set
        {
            matchResult = value!;
            RaisePropertyChanged();
        }
    }

    public ImageSource? PlayerProfilePicture
    {
        get => playerProfilePicture;
        set
        {
            playerProfilePicture = value;
            RaisePropertyChanged();
        }
    }
    public ImageSource? OpponentProfilePicture
    {
        get => opponentProfilePicture;
        set
        {
            opponentProfilePicture = value;
            RaisePropertyChanged();
        }
    }

    public Style OpponentProfilePictureStyle
    {
        get => opponentProfilePictureStyle;
        set
        {
            opponentProfilePictureStyle = value;
            RaisePropertyChanged();
        }
    }

    public Brush PlayerMatchResultBrush
    {
        get => playerMatchResultBrush;
        set
        {
            playerMatchResultBrush = value;
            RaisePropertyChanged();
        }
    }

    public Style PlayerProfilePictureStyle
    {
        get => playerProfilePictureStyle;
        set
        {
            playerProfilePictureStyle = value;
            RaisePropertyChanged();
        }
    }

    public Brush OpponentMatchResultBrush
    {
        get => opponentMatchResultBrush;
        set
        {
            opponentMatchResultBrush = value;
            RaisePropertyChanged();
        }
    }

    public Visibility PlayerMedalVisibility
    {
        get => playerMedalVisibility;
        set
        {
            playerMedalVisibility = value;
            RaisePropertyChanged();
        }
    }

    public Visibility OpponentMedalVisibility
    {
        get => opponentMedalVisibility;
        set
        {
            opponentMedalVisibility = value;
            RaisePropertyChanged();
        }
    }

    public Action OnSwitchMenu { get; set; }
    public Func<Task>? OnPlayAgain { get; set; }
    public Action? OnCloseOverlay { get; set; }

    public ICommand OpenPlayerProfileCommand { get; }
    public ICommand OpenOpponentProfileCommand { get; }
    public ICommand SwitchMenuCommand { get; }
    public ICommand PlayAgainCommand { get; }
    public ICommand CloseOverlayCommand { get; }

    public MatchEndWindowViewModel(BindableSettings settings, MatchDataStore matchDataStore)
    {
        this.settings = settings;
        this.matchDataStore = matchDataStore;

        settings.PropertyChanged += OnSettingsChanged;

        matchDataStore.MatchPoints.PropertyChanged += OnMatchPointsChanged;

        MatchResult = string.Empty;
        MatchEndReason = string.Empty;

        OpenPlayerProfileCommand = new CommandExecuter<object>
            (_ => RedirectToProfile(matchDataStore.PlayerDatas[playerSide].UserData!.Nickname));
        OpenOpponentProfileCommand = new CommandExecuter<object>
            (_ => RedirectToProfile(matchDataStore.PlayerDatas[opponentSide].UserData!.Nickname));

        SwitchMenuCommand = new CommandExecuter<object?>(_ => OnSwitchMenu?.Invoke());
        PlayAgainCommand = new CommandExecuter<object?>(_ => OnPlayAgain?.Invoke());
        CloseOverlayCommand = new CommandExecuter<object?>(_ => OnCloseOverlay?.Invoke());

        _ = ProfilePicturesLoader();
    }

    void OnSettingsChanged(object? s, PropertyChangedEventArgs e)
    {
        if (e.PropertyName is nameof(BindableSettings.DarkMode))
            _ = ProfilePicturesLoader();
    }

    void OnMatchPointsChanged(object? sender, PropertyChangedEventArgs e)
    {
        MatchPoints points = matchDataStore.MatchPoints;

        if (!points.MatchEnded || points.MatchPointsReason is null)
            return;

        MatchResultAppearanceSetter();
    }

    async Task ProfilePicturesLoader()
    {
        await matchDataStore.Initialize;

        playerSide = matchDataStore.PlayingSide;
        opponentSide = playerSide is Side.White ? Side.Black : Side.White;

        Application.Current.Dispatcher.Invoke(() =>
        {
            string playerProfilePictureString = matchDataStore.PlayerDatas[playerSide].UserData!.ProfilePicture!;
            string opponentProfilePictureString = matchDataStore.PlayerDatas[opponentSide].UserData!.ProfilePicture!;

            PlayerProfilePicture = playerProfilePictureString is not "Unknown"
                ? ImageConverters.LoadProfilePicture(playerProfilePictureString)
                : AppResources.Get<ImageSource>("DefaultProfilePictureImage");

            OpponentProfilePicture = opponentProfilePictureString is not "Unknown"
                ? ImageConverters.LoadProfilePicture(opponentProfilePictureString)
                : AppResources.Get<ImageSource>("DefaultProfilePictureImage");

            ProfilePictureStyleSetter();
        });
    }

    void MatchResultAppearanceSetter()
    {
        if (matchDataStore.MatchPoints.WinnerID == matchDataStore.PlayerDatas[playerSide].ID)
        {
            PlayerMatchResultBrush = Brushes.Green;
            OpponentMatchResultBrush = Brushes.Red;

            PlayerMedalVisibility = Visibility.Visible;
            OpponentMedalVisibility = Visibility.Collapsed;

            MatchResultAndReasonSetter(result: "Won");
        }
        else if (matchDataStore.MatchPoints.WinnerID == matchDataStore.PlayerDatas[opponentSide].ID)
        {
            PlayerMatchResultBrush = Brushes.Red;
            OpponentMatchResultBrush = Brushes.Green;

            PlayerMedalVisibility = Visibility.Collapsed;
            OpponentMedalVisibility = Visibility.Visible;

            MatchResultAndReasonSetter(result: "Lost");
        }
        else
        {
            PlayerMatchResultBrush = Brushes.Gray;
            OpponentMatchResultBrush = Brushes.Gray;

            PlayerMedalVisibility = Visibility.Collapsed;
            OpponentMedalVisibility = Visibility.Collapsed;

            MatchResultAndReasonSetter(result: null);
        }
    }

    void MatchResultAndReasonSetter(string? result)
    {
        MatchPoints points = matchDataStore.MatchPoints;
        string? reason = points.MatchPointsReason;

        if (reason is "Abort")
        {
            MatchResult = AppResources.Get<string>("MatchAbortedText");
            return;
        }

        if (reason is "Checkmate" || reason is "Resign" || reason is "Timeout")
        {
            MatchResult = AppResources.Get<string>(result is "Lost" 
                ? "MatchResultLostText" 
                : "MatchResultWonText");

            MatchEndReason = reason switch
            {
                "Checkmate" => AppResources.Get<string>("CheckmateText"),
                "Resign" => AppResources.Get<string>("ResignationText"),
                "Timeout" => AppResources.Get<string>("TimedoutText"),
                _ => throw new NotImplementedException(reason)
            };

            if (reason is "Timeout")
            {
                Side losingSide = matchDataStore.MatchPoints.WinnerID == matchDataStore.PlayerDatas[playerSide].ID
                    ? playerSide 
                    : opponentSide;

                matchDataStore.PlayerDatas[losingSide].Time = "0:00.0";
            }

            return;
        }

        MatchResult = AppResources.Get<string>("MatchResultDrawnText");

        MatchEndReason = reason switch
        {
            "Mutual Agreement" => AppResources.Get<string>("MutualAgreementText"),
            "Stalemate" => AppResources.Get<string>("StalemateText"),
            "50 Consecutive moves" => AppResources.Get<string>("FiftyConsecutiveMovesTexts"),
            "75 Consecutive moves" => AppResources.Get<string>("SeventyFiveConsecutiveMovesTexts"),
            "Insufficent materials" => AppResources.Get<string>("InsufficentMaterialText"),
            "Threefold-repetition" => AppResources.Get<string>("ThreefoldRepetition"),
            "Fivefold-repetition" => AppResources.Get<string>("FivefoldRepetition"),
            _ => throw new NotImplementedException()
        };
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
            ? "DefaultProfilePictureOnMatchEndWindowStyle"
            : "ProfilePictureStyle");

    static void RedirectToProfile(string nickname) => Process.Start(startInfo: new ProcessStartInfo
    {
        FileName = $"http://frontend.vm2.test/user/{nickname}",
        UseShellExecute = true
    });

    public void Dispose()
    {
        settings.PropertyChanged -= OnSettingsChanged;

        matchDataStore.MatchPoints.PropertyChanged -= OnMatchPointsChanged;
    }
}