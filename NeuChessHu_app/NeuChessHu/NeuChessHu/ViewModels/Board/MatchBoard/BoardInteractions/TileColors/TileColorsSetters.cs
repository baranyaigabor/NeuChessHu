using NeuChessHu.Converters;
using NeuChessHu.Resources;
using NeuChessHu.Services.SoundServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace NeuChessHu.ViewModels.Board.MatchBoard.BoardInteractions.TileColors;

internal static class TileColorsSetters
{
    internal static async Task PlayIllegalMoveAsync(Tuple<int, int> coordinates, Grid board)
    {
        Brush normalBrush = GetNormalBrush(coordinates);
        Brush illegalBrush = ColorConverters.BrushFromString("#99E8333C");

        Sounds.Play("IllegalMove");
        SetTileFill(coordinates, board, illegalBrush);
        await Task.Delay(20);
        SetTileFill(coordinates, board, normalBrush);
        await Task.Delay(70);
        SetTileFill(coordinates, board, illegalBrush);
        await Task.Delay(120);
        SetTileFill(coordinates, board, normalBrush);
        await Task.Delay(70);
        SetTileFill(coordinates, board, illegalBrush);
        await Task.Delay(120);
        SetTileFill(coordinates, board, normalBrush);
    }

    static void SetTileFill(Tuple<int, int> coordinates, Grid board, Brush brush)
    {
        foreach (UIElement child in board.Children)
            if (child is Border border && border.Child is Grid grid && 
                grid.Children[0] is Rectangle tile &&
                Grid.GetRow(border) == coordinates.Item1 &&
                Grid.GetColumn(border) == coordinates.Item2)
            {
                tile.Fill = brush;
                return;
            }
    }

    internal static void SelectTile(Tuple<int, int> coordinates, Grid board) =>
    SetTileFill(coordinates, board, GetSelectedBrush(coordinates));

    internal static void DeselectTile(Tuple<int, int> coordinates, Grid board) =>
        SetTileFill(coordinates, board, GetNormalBrush(coordinates));

    static Brush GetNormalBrush(Tuple<int, int> coordinates) =>
        (coordinates.Item1 + coordinates.Item2) % 2 == 0
            ? AppResources.Get<Brush>("LightSquareBrush")
            : AppResources.Get<Brush>("DarkSquareBrush");

    static Brush GetSelectedBrush(Tuple<int, int> coordinates) =>
        (coordinates.Item1 + coordinates.Item2) % 2 == 0
            ? AppResources.Get<Brush>("SelectedLightSquareBrush") 
            : AppResources.Get<Brush>("SelectedDarkSquareBrush"); 
}