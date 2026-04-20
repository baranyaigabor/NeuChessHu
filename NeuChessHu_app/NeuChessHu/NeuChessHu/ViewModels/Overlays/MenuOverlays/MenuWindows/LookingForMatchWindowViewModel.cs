using ChessMechanics.Common;
using NeuChessHu.CommandUtils;
using NeuChessHu.Resources;
using System.Windows.Input;
using System.Windows.Threading;

namespace NeuChessHu.ViewModels.Overlays.MenuOverlays.MenuWindows;

public class LookingForMatchWindowViewModel : ObservableBase, IDisposable
{
    List<string> searchingNotes = new();
    DispatcherTimer? timer;
    int elapsedSeconds;

    string matchDuration;
    string searchingNote;
    string elapsedTime;

    public string MatchDuration
    {
        get => matchDuration;
        internal set
        {
            matchDuration = value;
            RaisePropertyChanged();
        }
    }

    public string SearchingNote
    {
        get => searchingNote;
        private set
        {
            searchingNote = value;
            RaisePropertyChanged();
        }
    }

    public string ElapsedTime
    {
        get => elapsedTime;
        private set
        {
            elapsedTime = value;
            RaisePropertyChanged();
        }
    }

    internal Action OnStopLookingForMatch { get; set; }
    public ICommand StopLookingForMatchCommand { get; }

    public LookingForMatchWindowViewModel() => 
        StopLookingForMatchCommand = new CommandExecuter<object?>(_ => StopLookingForMatch());

    internal void StartTimer()
    {
        DisposeTimer();

        elapsedSeconds = 0;
        ElapsedTime = "00:00";

        searchingNotes = new(AppResources.Get<List<string>>("SearchingNotes"));

        PickNewNote();

        timer = new DispatcherTimer
        {
            Interval = TimeSpan.FromSeconds(1)
        };

        timer.Tick += Tick;
        timer.Start();
    }

    void Tick(object? sender, EventArgs e)
    {
        elapsedSeconds++;

        int minutes = elapsedSeconds / 60;
        int seconds = elapsedSeconds % 60;

        ElapsedTime = $"{minutes:D2}:{seconds:D2}";

        if (elapsedSeconds % 5 is 0)
            PickNewNote();
    }

    void PickNewNote()
    {
        if (searchingNotes.Count is 0)
            searchingNotes = new(AppResources.Get<List<string>>("SearchingNotes"));

        int index = Random.Shared.Next(searchingNotes.Count);
        SearchingNote = searchingNotes[index];
        searchingNotes.RemoveAt(index);
    }

    void StopLookingForMatch()
    {
        DisposeTimer();

        OnStopLookingForMatch?.Invoke();
    }

    void DisposeTimer()
    {
        if (timer is not null)
        {
            timer.Tick -= Tick;
            timer.Stop();
            timer = null;
        }
    }

    public void Dispose() =>
        DisposeTimer();
}