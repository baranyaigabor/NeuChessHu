using NeuChessHu.CommandUtils;
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

    internal static Border ResignDrawConfirmationPanelFactory()
    {
        Border resignDrawConfirmationBorder = new()
        {
            BorderThickness = new Thickness(0.5),
            Margin = new Thickness(10, 15, 10, 0),
            Child = new Grid()
        };

        for (int i = 0; i < 2; i++)
        {
            (resignDrawConfirmationBorder.Child as Grid)!.ColumnDefinitions.Add(new ColumnDefinition()
            { Width = new GridLength(123.5) });

            (resignDrawConfirmationBorder.Child as Grid)!.RowDefinitions.Add(new RowDefinition()
            { Height = new GridLength(30) });
        }

        Label resignDrawLabel = new()
        {
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Top,
            Style = AppResources.Get<Style>("TextStyle"),
            FontSize = 17,
            FontWeight = FontWeights.Bold,
            Margin = new Thickness(0, -2, 0, 0),
        };

        Border tickImage = TickCrossImageFactory();
        Border crossImage = TickCrossImageFactory();

        Grid.SetRow(resignDrawLabel, 0);
        Grid.SetColumnSpan(resignDrawLabel, 2);

        Grid.SetRow(tickImage, 1);
        Grid.SetColumn(tickImage, 0);

        Grid.SetRow(crossImage, 1);
        Grid.SetColumn(crossImage, 1);

        resignDrawConfirmationBorder.SetResourceReference(Border.BackgroundProperty, "NavBarBrush");
        resignDrawLabel.SetResourceReference(Label.ForegroundProperty, "TextBrush");

        (tickImage.Child as Image)!.SetResourceReference(Image.SourceProperty, "TickImage");
        (crossImage.Child as Image)!.SetResourceReference(Image.SourceProperty, "CrossImage");

        resignDrawConfirmationBorder.SetResourceReference(Border.BorderBrushProperty, "BorderBrush");

        resignDrawLabel.SetBinding(Label.ContentProperty, new Binding("ResignDrawConfirmationText"));
        resignDrawConfirmationBorder.SetBinding(Border.VisibilityProperty, new Binding("ResignDrawConfirmationPanelVisibility"));

        CommandAttachers.OnClickEvent(tickImage, "ConfirmOrCancelCommand", args: true);
        CommandAttachers.OnClickEvent(crossImage, "ConfirmOrCancelCommand", args: false);

        foreach (UIElement element in new UIElement[] { resignDrawLabel, tickImage, crossImage })
            (resignDrawConfirmationBorder.Child as Grid)!.Children.Add(element);

        return resignDrawConfirmationBorder;
    }

    static Border TickCrossImageFactory() => new()
    {
        Style = AppResources.Get<Style>("ConfirmCancelButtonsStyle"),
        Child = new Image()
        {
            Height = 15,
            Width = 15,
            VerticalAlignment = VerticalAlignment.Center,
            HorizontalAlignment = HorizontalAlignment.Center,
        }
    };
}