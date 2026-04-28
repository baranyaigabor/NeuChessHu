using ChessMechanics.Authentication.Session;
using ChessMechanics.Authentication.User;
using ChessMechanics.ChessBoard.Definitions;
using ChessMechanics.Common;
using ChessMechanics.MatchData.MatchDatas;
using ChessMechanics.MatchData.MatchDatas.Models;
using ChessMechanics.MatchData.MatchDatas.Models.DomainModels;
using ChessMechanics.WebSockets.ChessEngine.Requests;
using NeuChessHu.CommandUtils;
using NeuChessHu.Resources;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace NeuChessHu.ViewModels.Overlays.MatchOverlays.MatchPopUps;

public class OptionsPopUpViewModel : ObservableBase, IDisposable
{
    readonly MatchDataStore matchDataStore;
    readonly EngineRequests requests;
    readonly SessionDatas session;

    bool canAbort;

    string abortResignQuitButtonContent;
    Visibility offerDrawButtonVisibility;
    int optionsMenuHeight;

    public string AbortResignQuitButtonContent
    {
        get => abortResignQuitButtonContent;
        private set { abortResignQuitButtonContent = value; RaisePropertyChanged(); }
    }

    public int OptionsMenuHeight
    {
        get => optionsMenuHeight;
        private set { optionsMenuHeight = value; RaisePropertyChanged(); }
    }

    public Visibility OfferDrawButtonVisibility
    {
        get => offerDrawButtonVisibility;
        private set { offerDrawButtonVisibility = value; RaisePropertyChanged(); }
    }

    public Action<string>? OnShowConfirmationPanel { get; set; }
    public Action? OnQuit { get; internal set; }
    public Action? OnOpenInGameSettings { get; internal set; }
    public Action? OnCloseOverlay { get; internal set; }

    public ICommand AbortResignQuitCommand { get; }
    public ICommand OffersDrawCommand { get; }
    public ICommand OpenInGameSettignsCommand { get; }
    public ICommand GoBackCommand { get; }

    public OptionsPopUpViewModel(MatchDataStore matchDataStore, EngineRequests requests, SessionDatas session)
    {
        this.matchDataStore = matchDataStore;
        this.requests = requests;
        this.session = session;

        foreach (Side side in new[] { Side.White, Side.Black })
            matchDataStore.PlayerDatas[side].PropertyChanged += OnPlayerDatasChanged;

        matchDataStore.MatchState.Notations.CollectionChanged += OnNotationsCollectionChanged;
        matchDataStore.MatchPoints.PropertyChanged += OnMatchPointsChanged;

        OptionsMenuHeight = 158;
        RefreshAbortResignQuitButton();

        AbortResignQuitCommand = new CommandExecuter<object?>(async x => await AbortResignQuitButtonAction());
        OffersDrawCommand = new CommandExecuter<object?>(async x => await OffersDraw());
        OpenInGameSettignsCommand = new CommandExecuter<object?>(_ => OnOpenInGameSettings?.Invoke());
        GoBackCommand = new CommandExecuter<object?>(_ => OnCloseOverlay?.Invoke());
    }

    void OnPlayerDatasChanged(object? s, PropertyChangedEventArgs e)
    {
        if (e.PropertyName is nameof(PlayerDataStore.UserData))
            DrawButtonVisibilityOnStockfish();
    }

    void OnMatchPointsChanged(object? s, PropertyChangedEventArgs e)
    {
        if (matchDataStore.MatchPoints.MatchEnded)
        {
            OfferDrawButtonVisibility = Visibility.Collapsed;
            OptionsMenuHeight = 118;
        }

        RefreshAbortResignQuitButton();
    }

    void OnNotationsCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e) => RefreshAbortResignQuitButton();

    void RefreshAbortResignQuitButton()
    {
        ObservableCollection<SANNotationRow> notations = matchDataStore.MatchState.Notations;

        if (matchDataStore.MatchPoints.MatchEnded)
        {
            AbortResignQuitButtonContent = AppResources.Get<string>("QuitToMenuButtonText");
            canAbort = false;
            return;
        }

        if (!notations.Any() || notations.Count is 1 && notations.Last().Black is null)
        {
            AbortResignQuitButtonContent = AppResources.Get<string>("AbortText");
            canAbort = true;
            return;
        }

        AbortResignQuitButtonContent = AppResources.Get<string>("ResignText");
        canAbort = false;
    }

    void DrawButtonVisibilityOnStockfish()
    {
        string?[] nicknames = [
            matchDataStore.PlayerDatas[Side.White].UserData?.Nickname,
            matchDataStore.PlayerDatas[Side.Black].UserData?.Nickname
        ];

        if (nicknames.Any(x => x is "Stockfish"))
        {
            OptionsMenuHeight = 118;
            OfferDrawButtonVisibility = Visibility.Collapsed;
        }
    }

    async Task AbortResignQuitButtonAction()
    {
        if (canAbort)
            await requests.MatchPointRequestAsync(matchDataStore.MatchChannel!, (int)session.UserID!, "Abort");

        else if (!matchDataStore.MatchPoints.MatchEnded)
            OnShowConfirmationPanel?.Invoke("Resign");

        else OnQuit?.Invoke();

        OnCloseOverlay?.Invoke();
    }

    async Task OffersDraw()
    {
        OnShowConfirmationPanel?.Invoke("Draw");
        OnCloseOverlay?.Invoke();

        await requests.MatchPointRequestAsync(matchDataStore.MatchChannel!, (int)session.UserID!, "Draw");
    }

    public void Dispose()
    {
        foreach (Side side in new[] { Side.White, Side.Black })
            matchDataStore.PlayerDatas[side].PropertyChanged -= OnPlayerDatasChanged;

        matchDataStore.MatchState.Notations.CollectionChanged -= OnNotationsCollectionChanged;
        matchDataStore.MatchPoints.PropertyChanged -= OnMatchPointsChanged;
    }
}
