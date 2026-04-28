using NeuChessHu.CommandUtils;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Shapes;

namespace NeuChessHu.Views.Board;

public partial class BoardView : UserControl
{
    public BoardView() => 
        Loaded += (s, e) =>
        {
            Content ??= BoardBuilder();
        };

    static Border BoardBuilder()
    {
        Border boardBorder = new()
        {
            BorderThickness = new Thickness(1),
            Child = new Grid()
        };

        for (int i = 0; i < 8; i++)
        {
            (boardBorder.Child as Grid)!.RowDefinitions.Add(new RowDefinition 
                { Height = new GridLength(1, GridUnitType.Star) });
            (boardBorder.Child as Grid)!.ColumnDefinitions.Add(new ColumnDefinition 
                { Width = new GridLength(1, GridUnitType.Star) });
        }

        Binding borderBrushBinding = new("BorderBrush") { Mode = BindingMode.OneWay };
        boardBorder.SetBinding(Border.BorderBrushProperty, borderBrushBinding);

        for (int r = 0; r < 8; r++)
            for (int c = 0; c < 8; c++)
            {
                Border tileContainer = new()
                {
                    BorderThickness = new Thickness(1),
                    BorderBrush = Brushes.White,
                    Height = 60,
                    Width = 60,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                    Background = Brushes.Transparent,
                    Focusable = false,
                    ForceCursor = true,
                    Child = new Grid()
                    {
                        Background = Brushes.Transparent,
                    }
                };

                Rectangle tile = new()
                {
                    HorizontalAlignment = HorizontalAlignment.Stretch,
                    VerticalAlignment = VerticalAlignment.Stretch,
                };

                Image pieceImage = new()
                {
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                    Margin = new Thickness(7),
                };

                Label tileLetter = new();
                Label tileNumber = new();

                Ellipse legalMoveHighlighter = new()
                {
                    Fill = (r + c) % 2 == 0 ? Brushes.LightGray : Brushes.Gray,
                    Opacity = 0.4,
                    Visibility = Visibility.Collapsed,
                    Margin = new Thickness(15),
                };

                if (c is 0)
                {
                    tileNumber = new()
                    {
                        Background = Brushes.Transparent,
                        FontWeight = FontWeights.Bold,
                        VerticalAlignment = VerticalAlignment.Top,
                        HorizontalAlignment = HorizontalAlignment.Left,
                        Margin = new Thickness(3, 3, 0, 0)
                    };

                    Binding tileNumberColorBinding = IdentifiersThemeBinding(r, c);
                    tileNumber.SetBinding(Label.ForegroundProperty, tileNumberColorBinding);

                    Binding tileNumbersList = new($"TileListNumbers[{r}]") { Mode = BindingMode.OneWay };
                    tileNumber.SetBinding(ContentProperty, tileNumbersList);
                }

                if (r is 7)
                {
                    tileLetter = new()
                    {
                        Background = Brushes.Transparent,
                        FontWeight = FontWeights.Bold,
                        VerticalAlignment = VerticalAlignment.Bottom,
                        HorizontalAlignment = HorizontalAlignment.Right,
                        Margin = new Thickness(0, 0, 4, -2)
                    };


                    Binding tileLetterColorBinding = IdentifiersThemeBinding(r, c);
                    tileLetter.SetBinding(Label.ForegroundProperty, tileLetterColorBinding);

                    Binding tileLettersList = new($"TileListLetters[{c}]") { Mode = BindingMode.OneWay };
                    tileLetter.SetBinding(ContentProperty, tileLettersList);
                }

                Grid.SetRow(tileContainer, r);
                Grid.SetColumn(tileContainer, c);

                CommandAttachers.OnClickEvent(tileContainer, "InteractionWithPiecesCommand", 
                    args: ((boardBorder.Child as Grid)!, tileContainer));
                CommandAttachers.OnLoaded(pieceImage, "PieceImageSetterCommand", args: (r, c));

                Binding cursorOnInteractBinding = new("CursorOnInteract") { Mode = BindingMode.OneWay };
                tileContainer.SetBinding(CursorProperty, cursorOnInteractBinding);

                Binding borderThichnessBinding = new("BorderThickness") { Mode = BindingMode.OneWay };
                tileContainer.SetBinding(Border.BorderThicknessProperty, borderThichnessBinding);

                tileContainer.SetBinding(Border.BorderBrushProperty, borderBrushBinding);

                Binding tileColorBinding = TilesThemeBinding(r, c);
                tile.SetBinding(Rectangle.FillProperty, tileColorBinding);

                Binding pieceImageBinding = new($"PieceImages[{r * 8 + c}]") { Mode = BindingMode.OneWay };
                pieceImage.SetBinding(Image.SourceProperty, pieceImageBinding);

                foreach (UIElement element in new UIElement[] { tile, pieceImage, 
                    tileLetter, tileNumber, legalMoveHighlighter })
                {
                    element.IsHitTestVisible = false;
                    (tileContainer.Child as Grid)!.Children.Add(element);
                }

                (boardBorder.Child as Grid)!.Children.Add(tileContainer);
            }

        return boardBorder;
    }
        
    static Binding TilesThemeBinding(int r, int c) =>
        new ($"{((r + c) % 2 is 0
            ? "LightTileBrush"
            : "DarkTileBrush")}") 
            { Mode = BindingMode.OneWay };

    static Binding IdentifiersThemeBinding(int r, int c) =>
        new ($"{((r + c) % 2 is 1
            ? "EvenBoardIdentifierBrush" 
            : "OddBoardIdentifierBrush")}") 
            { Mode = BindingMode.OneWay };
}