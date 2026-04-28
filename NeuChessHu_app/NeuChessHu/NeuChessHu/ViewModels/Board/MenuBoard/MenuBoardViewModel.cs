using ChessMechanics.ChessBoard;
using ChessMechanics.ChessBoard.ChessPieces;
using ChessMechanics.ChessBoard.Definitions;
using ChessMechanics.Common;
using NeuChessHu.CommandUtils;
using NeuChessHu.Resources;
using NeuChessHu.UserSettings;
using NeuChessHu.UserSettings.SettingManagers;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace NeuChessHu.ViewModels.Board.MenuBoard;

public class MenuBoardViewModel : ObservableBase, IDisposable
{
    BindableSettings settings;
    readonly ChessPiece[,] board;

    Brush lightTileBrush;
    Brush darkTileBrush;
    Brush oddBoardIdentifierBrush;
    Brush evenBoardIdentifierBrush;

    Thickness borderThickness;
    Brush borderBrush;

    public BindableSettings Settings
    {
        get => settings;
        set { settings = value; RaisePropertyChanged(); }
    }
    public List<char> TileListLetters { get; } = ['a', 'b', 'c', 'd', 'e', 'f', 'g', 'h'];
    public List<char> TileListNumbers { get; } = ['8', '7', '6', '5', '4', '3', '2', '1'];
    public ObservableCollection<ImageSource> PieceImages { get; }

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
    public ICommand? InteractionWithPiecesCommand { get; }

    public MenuBoardViewModel(BindableSettings settings)
    {
        this.settings = settings;
        board = ChessBoardFactory.BoardFiller(playingSide: Side.White);

        LightTileBrush = AppResources.Get<Brush>("LightSquareBrush");
        DarkTileBrush = AppResources.Get<Brush>("DarkSquareBrush");
        OddBoardIdentifierBrush = AppResources.Get<Brush>("OddBoardIdentifierBrush");
        EvenBoardIdentifierBrush = AppResources.Get<Brush>("EvenBoardIdentifierBrush");

        BorderSetter();

        settings.PropertyChanged += OnSettingsChaged;

        PieceImages = new ObservableCollection<ImageSource>(Enumerable.Repeat<ImageSource>(null!, 64));
        PieceImageSetterCommand = new CommandExecuter<(int r, int c)?>(PieceImagesSetter);
        InteractionWithPiecesCommand = null;
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
        {
            for (int r = 0; r < 8; r++)
                for (int c = 0; c < 8; c++)
                    PieceImagesSetter((r, c));
        }
    }

    void PieceImagesSetter((int r, int c)? args)
    {
        (int r, int c) = args!.Value;

        ChessPiece piece = board[r, c];

        if (piece.Name is not Piece.None)
            PieceImages[r * 8 + c] = PieceThemeManager.ImageLoader(piece.Name, piece.Color, settings);
    }

    void BorderSetter()
    {
        Brush boardBorderBrush = AppResources.Get<Brush>("BoardBorderBrush");

        bool isBorderedBoardTheme = boardBorderBrush.ToString() is not "#00FFFFFF";

        BorderThickness = new Thickness(isBorderedBoardTheme ? 0.75 : 0);
        BorderBrush = isBorderedBoardTheme ? boardBorderBrush : AppResources.Get<Brush>("BorderBrush");
    }

    public void Dispose() =>
        settings.PropertyChanged -= OnSettingsChaged;
}