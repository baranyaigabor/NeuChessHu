using ChessMechanics.Authentication.Session;
using ChessMechanics.Common;
using ChessMechanics.MatchData.MatchDatas;
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
    private Visibility offerDrawButtonVisibility;
    private int optionsMenuHeight;

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
        private set {  offerDrawButtonVisibility = value;  RaisePropertyChanged(); }
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

        matchDataStore.MatchState.Notations.CollectionChanged += OnNotationsCollectionChanged;
        matchDataStore.MatchPoints.PropertyChanged += OnMatchPointsChanged;

        AbortResignQuitButtonContent = AppResources.Get<string>("AbortText");
        canAbort = true;

        OptionsMenuHeight = 158;

        AbortResignQuitCommand = new CommandExecuter<object?>(async x => await AbortResignQuitButtonAction());
        OffersDrawCommand = new CommandExecuter<object?>(async x => await OffersDraw());
        OpenInGameSettignsCommand = new CommandExecuter<object?>(_ => OnOpenInGameSettings?.Invoke());
        GoBackCommand = new CommandExecuter<object?>(_ => OnCloseOverlay?.Invoke());
    }

    void OnMatchPointsChanged(object? s, PropertyChangedEventArgs e)
    {
        if (matchDataStore.MatchPoints.MatchEnded)
        {
            OfferDrawButtonVisibility = Visibility.Collapsed;
            OptionsMenuHeight = 118;
        }
    }

    void OnNotationsCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
    {
        ObservableCollection<SANNotationRow> notations = matchDataStore.MatchState.Notations;

        if (!notations.Any())
            return;

        SANNotationRow latestRow = e.Action is NotifyCollectionChangedAction.Replace
            ? (SANNotationRow)e.NewItems![0]!
            : notations.Last();

        if (notations.Count is 1 && latestRow.Black is null)
            AbortResignQuitButtonContent = AppResources.Get<string>("AbortText");

        else if(!matchDataStore.MatchPoints.MatchEnded)
        {
            AbortResignQuitButtonContent = AppResources.Get<string>("ResignText");
            canAbort = false;
        }

        else AbortResignQuitButtonContent = AppResources.Get<string>("QuitToMenuButtonText");
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
        matchDataStore.MatchState.Notations.CollectionChanged -= OnNotationsCollectionChanged;
        matchDataStore.MatchPoints.PropertyChanged += OnMatchPointsChanged;
    }
}