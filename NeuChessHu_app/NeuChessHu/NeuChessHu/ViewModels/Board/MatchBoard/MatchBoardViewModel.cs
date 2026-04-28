using ChessMechanics.ChessBoard.ChessPieces;
using ChessMechanics.ChessBoard.Definitions;
using ChessMechanics.Common;
using ChessMechanics.MatchData.MatchDatas;
using ChessMechanics.MatchData.MatchDatas.Models;
using NeuChessHu.CommandUtils;
using NeuChessHu.Resources;
using NeuChessHu.Services.SoundServices;
using NeuChessHu.UserSettings;
using NeuChessHu.UserSettings.SettingManagers;
using NeuChessHu.ViewModels.Board.MatchBoard.BoardInteractions;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace NeuChessHu.ViewModels.Board.MatchBoard;

public class MatchBoardViewModel : ObservableBase, IDisposable
{
    readonly BindableSettings settings;
    readonly MatchDataStore matchDataStore;
    ChessPiece[,] pieceMatrix;

    Brush lightTileBrush;
    Brush darkTileBrush;
    Brush oddBoardIdentifierBrush;
    Brush evenBoardIdentifierBrush;

    Thickness borderThickness;
    Brush borderBrush;
    Cursor cursorOnInteract;

    public BoardInteractionHandler InteractionHandler { get; }
    public List<char> TileListLetters { get; private set; }
    public List<char> TileListNumbers { get; private set; }
    public ObservableCollection<ImageSource> PieceImages { get; }

    public Cursor CursorOnInteract
    {
        get => cursorOnInteract;
        private set { cursorOnInteract = value; RaisePropertyChanged(); }
    }

    public Brush LightTileBrush
    {
        get => lightTileBrush;
        private set { lightTileBrush = value; RaisePropertyChanged(); }
    }
    public Brush DarkTileBrush
    {
        get => darkTileBrush;
        private set { darkTileBrush = value; RaisePropertyChanged(); }
    }
    public Brush OddBoardIdentifierBrush
    {
        get => oddBoardIdentifierBrush;
        private set { oddBoardIdentifierBrush = value; RaisePropertyChanged(); }
    }
    public Brush EvenBoardIdentifierBrush
    {
        get => evenBoardIdentifierBrush;
        private set { evenBoardIdentifierBrush = value; RaisePropertyChanged(); }
    }
    public Thickness BorderThickness
    {
        get => borderThickness;
        private set { borderThickness = value; RaisePropertyChanged(); }
    }
    public Brush BorderBrush
    {
        get => borderBrush;
        private set { borderBrush = value; RaisePropertyChanged(); }
    }

    public ICommand PieceImageSetterCommand { get; }
    public ICommand InteractionWithPiecesCommand { get; }

    public MatchBoardViewModel(BindableSettings settings, MatchDataStore matchDataStore, BoardInteractionHandler interactionHandler)
    {
        this.settings = settings;
        this.matchDataStore = matchDataStore;
        InteractionHandler = interactionHandler;

        CursorOnInteract = AppResources.Get<Cursor>("CursorOnButtons");

        TileListLetters = ['a', 'b', 'c', 'd', 'e', 'f', 'g', 'h'];
        TileListNumbers = ['8', '7', '6', '5', '4', '3', '2', '1'];

        LightTileBrush = AppResources.Get<Brush>("LightSquareBrush");
        DarkTileBrush = AppResources.Get<Brush>("DarkSquareBrush");
        OddBoardIdentifierBrush = AppResources.Get<Brush>("OddBoardIdentifierBrush");
        EvenBoardIdentifierBrush = AppResources.Get<Brush>("EvenBoardIdentifierBrush");

        BorderSetter();

        Task.Run(async () =>
        {
            await matchDataStore.Initialize;
            Application.Current.Dispatcher.Invoke(SquareIdentifiersSetter);
        });

        settings.PropertyChanged += OnSettingsChaged;
        matchDataStore.MatchState.PropertyChanged += OnMatchStateChanged;

        InteractionWithPiecesCommand = new CommandExecuter<(Grid board, Border tile)>
            (async args => await interactionHandler.InteractionWithPieces(args));

        PieceImages = new ObservableCollection<ImageSource>(Enumerable.Repeat<ImageSource>(null!, 64));
        PieceImageSetterCommand = new CommandExecuter<(int r, int c)?>(PieceImagesSetter);

        Sounds.Play("MatchStart");
    }

    void OnSettingsChaged(object? s, PropertyChangedEventArgs e)
    {
        if (e.PropertyName is nameof(BindableSettings.BoardTheme))
        {
            LightTileBrush = AppResources.Get<Brush>("LightSquareBrush");
            DarkTileBrush = AppResources.Get<Brush>("DarkSquareBrush");
            OddBoardIdentifierBrush = AppResources.Get<Brush>("OddBoardIdentifierBrush");
            EvenBoardIdentifierBrush = AppResources.Get<Brush>("EvenBoardIdentifierBrush");

            BorderSetter();
        }

        else if (e.PropertyName is nameof(BindableSettings.PieceTheme))
            PieceImagesRefresher();
    }

    void OnMatchStateChanged(object? s, PropertyChangedEventArgs e)
    {
        if (e.PropertyName is nameof(MatchState.PieceMatrix) &&
            matchDataStore.MatchState.PieceMatrix is not null)
        {
            pieceMatrix = matchDataStore.MatchState.PieceMatrix;
            Application.Current.Dispatcher.Invoke(PieceImagesRefresher);
        }
    }

    void PieceImagesRefresher()
    {
        for (int r = 0; r < 8; r++)
            for (int c = 0; c < 8; c++)
                PieceImagesSetter((r, c));
    }

    void PieceImagesSetter((int r, int c)? args)
    {
        if (pieceMatrix is null)
            return;

        (int r, int c) = args!.Value;

        Application.Current.Dispatcher.Invoke(() =>
        {
            ChessPiece? piece = pieceMatrix[r, c];

            if (piece is null || piece.Name is Piece.None)
                PieceImages[r * 8 + c] = null!;

            else PieceImages[r * 8 + c] = PieceThemeManager.ImageLoader
                (pieceMatrix[r, c].Name!, pieceMatrix[r, c].Color!, settings);
        });
    }

    void BorderSetter()
    {
        Brush boardBorderBrush = AppResources.Get<Brush>("BoardBorderBrush");

        bool isBorderedBoardTheme = boardBorderBrush.ToString() is not "#00FFFFFF";

        BorderThickness = new Thickness(isBorderedBoardTheme ? 0.75 : 0);
        BorderBrush = isBorderedBoardTheme ? boardBorderBrush : AppResources.Get<Brush>("BorderBrush");
    }

    void SquareIdentifiersSetter()
    {
        if (matchDataStore.PlayingSide is Side.White && TileListLetters[0] is 'h')
        {
            TileListLetters.Reverse();
            TileListNumbers.Reverse();
        }
        else if (matchDataStore.PlayingSide is Side.Black && TileListLetters[0] is 'a')
        {
            TileListLetters.Reverse();
            TileListNumbers.Reverse();
        }
    }

    public void Dispose()
    {
        settings.PropertyChanged -= OnSettingsChaged;
        matchDataStore.MatchState.PropertyChanged -= OnMatchStateChanged;
    }
}