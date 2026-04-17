using ChessMechanics.ChessBoard.ChessPieces;
using ChessMechanics.ChessBoard.Definitions;
using ChessMechanics.Common;
using ChessMechanics.MatchData.MatchDatas;
using ChessMechanics.MatchData.MatchDatas.Models;
using ChessMechanics.WebSockets.ChessEngine.Requests;
using NeuChessHu.Services.SoundServices;
using NeuChessHu.UserSettings;
using NeuChessHu.ViewModels.Board.MatchBoard.BoardInteractions.TileColors;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace NeuChessHu.ViewModels.Board.MatchBoard.BoardInteractions;

public class BoardInteractionHandler : ObservableBase, IDisposable
{
    readonly BindableSettings settings;
    readonly MatchDataStore matchDataStore;
    readonly EngineRequests requests;
    ChessPiece[,] pieceMatrix;

    bool[,]? currentLegalMoves;

    Tuple<int, int>? from;
    Tuple<int, int>? to;
    Tuple<int, int>? selectedTileCoordinates;

    public Action? OnOpenPromotionWindow { get; set; }

    public BoardInteractionHandler(BindableSettings settings, MatchDataStore matchDataStore,
        EngineRequests requests)
    {
        this.settings = settings;
        this.matchDataStore = matchDataStore;
        this.requests = requests;

        matchDataStore.MatchState.PropertyChanged += OnMatchStateChanged;

        pieceMatrix = matchDataStore.MatchState.PieceMatrix;
    }
    void OnMatchStateChanged(object? s, PropertyChangedEventArgs e)
    {
        if (e.PropertyName is nameof(MatchState.PieceMatrix))
            pieceMatrix = matchDataStore.MatchState.PieceMatrix;
    }

    internal async Task InteractionWithPieces((Grid board, Border tile) args)
    {
        if (matchDataStore.MatchState.CurrentSide is Side.None ||
            matchDataStore.MatchState.CurrentSide != matchDataStore.PlayingSide)
            return;

        (Grid boardUI, Border tile) = args;

        int row = Grid.GetRow(tile);
        int col = Grid.GetColumn(tile);

        if (pieceMatrix[row, col].Name is not Piece.None &&
            pieceMatrix[row, col].Color == matchDataStore.MatchState.CurrentSide)
        {
            Tuple<int, int>? previousCoordinates = selectedTileCoordinates;
            from = new(row, col);
            selectedTileCoordinates = from;

            currentLegalMoves = await requests.LegalMovesWithSelectedPieceRequestAsync(
                matchDataStore.MatchChannel!, CoordinatesToServer(from),
                PieceMatrixToServer(pieceMatrix), Side.White);

            currentLegalMoves = matchDataStore.PlayingSide == Side.White
                ? currentLegalMoves
                : FlipLegalMovesMatrixFromServer(currentLegalMoves);

            await AppearenceHighlighter(boardUI, currentLegalMoves, from, previousCoordinates);
            return;
        }

        if (from is not null)
        {
            to = new(row, col);

            if (!from.Equals(to))
            {
                if (await requests.IsLegalMoveRequestAsync(matchDataStore.MatchChannel!,
                    CoordinatesToServer(from), CoordinatesToServer(to)))
                {
                    Piece promotionChoice = await PromotionPieceDefiner(from, to);

                    string sound = await requests.MovePieceRequest(matchDataStore.MatchChannel!,
                        CoordinatesToServer(from), CoordinatesToServer(to), promotionChoice);

                    Sounds.Play(sound);
                }

                else await Application.Current.Dispatcher.InvokeAsync(async () =>
                        await TileColorsSetters.PlayIllegalMoveAsync(from, boardUI));
            }

            await AppearenceHighlighter(boardUI, null, from);

            from = null;
            to = null;
            selectedTileCoordinates = null;
            currentLegalMoves = null;
        }
    }

    static async Task AppearenceHighlighter(Grid boardUI, bool[,]? currentLegalMoves,
        Tuple<int, int> fromCoordinates, Tuple<int, int>? previousCoordinates = null) =>
        await Application.Current.Dispatcher.InvokeAsync(() =>
        {
            if (currentLegalMoves is not null)
            {
                if (previousCoordinates is not null && !previousCoordinates.Equals(fromCoordinates))
                {
                    TileColorsSetters.DeselectTile(previousCoordinates, boardUI);

                    for (int r = 0; r < 8; r++)
                        for (int c = 0; c < 8; c++)
                            IsRemovingHighlighter(boardUI, r, c, isRemoving: true);
                }

                TileColorsSetters.SelectTile(fromCoordinates, boardUI);

                for (int r = 0; r < 8; r++)
                    for (int c = 0; c < 8; c++)
                        IsRemovingHighlighter(boardUI, r, c, isRemoving: !currentLegalMoves[r, c]);
            }
            else
            {
                TileColorsSetters.DeselectTile(fromCoordinates, boardUI);

                for (int r = 0; r < 8; r++)
                    for (int c = 0; c < 8; c++)
                        IsRemovingHighlighter(boardUI, r, c, isRemoving: true);
            }
        });

    static void IsRemovingHighlighter(Grid boardUI, int r, int c, bool isRemoving)
    {
        Border? tileContainer = boardUI.Children.OfType<Border>()
            .FirstOrDefault(x => Grid.GetRow(x) == r && Grid.GetColumn(x) == c);

        Ellipse? legalMoveCircle = tileContainer?.Child is Grid container
            ? container.Children.OfType<Ellipse>().FirstOrDefault() : null;

        if (legalMoveCircle is not null)
            legalMoveCircle.Visibility = isRemoving ? Visibility.Collapsed : Visibility.Visible;
    }

    ChessPiece[,] PieceMatrixToServer(ChessPiece[,] clientMatrix)
    {
        if (matchDataStore.PlayingSide == Side.White)
            return clientMatrix;

        ChessPiece[,] flipped = new ChessPiece[8, 8];
        for (int r = 0; r < 8; r++)
            for (int c = 0; c < 8; c++)
                flipped[7 - r, 7 - c] = clientMatrix[r, c];

        return flipped;
    }

    static bool[,] FlipLegalMovesMatrixFromServer(bool[,] matrix)
    {
        bool[,] flipped = new bool[8, 8];
        for (int r = 0; r < 8; r++)
            for (int c = 0; c < 8; c++)
                flipped[7 - r, 7 - c] = matrix[r, c];
        return flipped;
    }

    Tuple<int, int> CoordinatesToServer(Tuple<int, int> coordinates) =>
        matchDataStore.PlayingSide == Side.Black
            ? Tuple.Create(7 - coordinates.Item1, 7 - coordinates.Item2)
            : coordinates;

    async Task<Piece> PromotionPieceDefiner(Tuple<int, int> from, Tuple<int, int> to)
    {
        ChessPiece fromPiece = pieceMatrix[from.Item1, from.Item2];

        if (fromPiece.Name is Piece.Pawn && (to.Item1 == 0 || to.Item1 == 7))
        {
            if (!settings.AutoQueen)
            {
                await Application.Current.Dispatcher.InvokeAsync(() => OnOpenPromotionWindow!.Invoke());
            }
            else return Piece.Queen;
        }

        return Piece.None;
    }

    public void Dispose() =>
        matchDataStore.MatchState.PropertyChanged -= OnMatchStateChanged;
}