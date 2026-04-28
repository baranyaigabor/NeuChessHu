using NeuChessHu.CommandUtils;
using NeuChessHu.UserSettings;
using ChessMechanics.ChessBoard.Definitions;
using ChessMechanics.Common;
using ChessMechanics.MatchData.MatchDatas;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows.Media;
using NeuChessHu.UserSettings.SettingManagers;
using System.Windows;

namespace NeuChessHu.ViewModels.Overlays.MatchOverlays.MatchWindows;

public class PromotionWindowViewModel : ObservableBase
{
    readonly BindableSettings settings;
    readonly MatchDataStore matchDataStore;
    readonly List<Piece> Options = [Piece.Queen, Piece.Rook, Piece.Bishop, Piece.Knight];

    TaskCompletionSource<Piece> taskCompletionSource;

    public Piece PromotionChoice { get; private set; }

    public ObservableCollection<ImageSource> PieceImages { get; private set; }
    public Action OnClosePromotionWindow { get; set; }
    public ICommand SelectCommand { get; }
    public ICommand PieceImageSetterCommand { get; }

    public PromotionWindowViewModel(BindableSettings settings, MatchDataStore matchDataStore)
    {
        this.settings = settings;
        this.matchDataStore = matchDataStore;

        PieceImages = new ObservableCollection<ImageSource>(Enumerable.Repeat<ImageSource>(null!, 4));
        PieceImageSetterCommand = new CommandExecuter<int>(PieceImagesSetter);

        SelectCommand = new CommandExecuter<int>(PromotionChosen);
    }

    void PieceImagesSetter(int index) =>
        PieceImages[index] = PieceThemeManager.ImageLoader(Options[index], matchDataStore.MatchState.CurrentSide, settings);

    public Task<Piece> WaitForChooseAsync()
    {
        taskCompletionSource = new TaskCompletionSource<Piece>();
        return taskCompletionSource.Task;
    }

    void PromotionChosen(int index)
    {
        Application.Current.Dispatcher.Invoke(() =>
        {
            PromotionChoice = Options[index];
            taskCompletionSource?.TrySetResult(PromotionChoice);

            OnClosePromotionWindow.Invoke();
        });
    }
}