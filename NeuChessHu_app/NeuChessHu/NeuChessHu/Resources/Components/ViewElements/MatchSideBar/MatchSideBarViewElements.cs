using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace NeuChessHu.Resources.Components.ViewElements.MatchSideBar;

internal static class MatchSideBarViewElements
{
    internal static Border PlayerInfoPanelsFactory() => new()
    {
        Child = new DockPanel
        {
            Children =
            {
                new Border
                {
                    BorderBrush = AppResources.Get<SolidColorBrush>("BorderBrush"),
                    BorderThickness = new Thickness(0.5),
                    Width = 60,
                    Height = 60,
                    Child = new Image()
                },
                new Border
                {
                    BorderBrush = AppResources.Get<SolidColorBrush>("BorderBrush"),
                    BorderThickness = new Thickness(0.5),

                    Child = new Label
                    {
                        HorizontalAlignment = HorizontalAlignment.Left,
                        Style = AppResources.Get<Style>("TextStyle"),
                        FontSize = 14,
                    }
                }
            }
        }
    };

    internal static Border CapturedPiecesDisplayFactory(string player)
    {
        FrameworkElementFactory capturedPiecesPanelFactory = new(typeof(StackPanel));
        capturedPiecesPanelFactory.SetValue(StackPanel.OrientationProperty, Orientation.Horizontal);

        ItemsControl capturedPiecesItemsControl = new()
        {
            ItemsPanel = new ItemsPanelTemplate(capturedPiecesPanelFactory)
        };
        capturedPiecesItemsControl.SetBinding(ItemsControl.ItemsSourceProperty, new Binding(player + "Pieces"));

        FrameworkElementFactory capturedPieceImageFactory = new(typeof(Image));
        capturedPieceImageFactory.SetValue(Image.HeightProperty, 25.0);
        capturedPieceImageFactory.SetValue(Image.WidthProperty, 25.0);
        capturedPieceImageFactory.SetBinding(Image.SourceProperty, new Binding("PieceImage"));
        capturedPieceImageFactory.SetBinding(Image.MarginProperty, new Binding("Margin"));

        Label pointsLabel = new()
        {
            Style = AppResources.Get<Style>("TextStyle"),
            FontSize = 14
        };
        pointsLabel.SetBinding(Label.ContentProperty, new Binding(player + "Points"));

        capturedPiecesItemsControl.ItemTemplate = new DataTemplate
        {
            VisualTree = capturedPieceImageFactory
        };

        StackPanel capturedPiecesContainer = new()
        {
            Height = 30,
            Width = 210,
            HorizontalAlignment = HorizontalAlignment.Left,
            VerticalAlignment = VerticalAlignment.Center,
            Orientation = Orientation.Horizontal,
            Children =
            {
                capturedPiecesItemsControl,
                pointsLabel,
            }
        };

        return new Border
        {
            BorderBrush = AppResources.Get<SolidColorBrush>("BorderBrush"),
            BorderThickness = new Thickness(0.5, 0.5, 0.5, 1),
            Child = capturedPiecesContainer
        };
    }
}