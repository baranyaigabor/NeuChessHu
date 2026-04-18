using NeuChessHu.CommandUtils;
using NeuChessHu.Converters;
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

    internal static DockPanel ChatPanelDisplayFactory()
    {
        FrameworkElementFactory bubbleBorderFactory = new(typeof(Border));
        bubbleBorderFactory.SetValue(Border.CornerRadiusProperty, new CornerRadius(10));
        bubbleBorderFactory.SetValue(Border.MarginProperty, new Thickness(8, 3, 8, 3));
        bubbleBorderFactory.SetValue(Border.PaddingProperty, new Thickness(10, 6, 10, 6));
        bubbleBorderFactory.SetValue(Border.MaxWidthProperty, 160.0);

        FrameworkElementFactory messageTextFactory = new(typeof(TextBlock));
        messageTextFactory.SetBinding(TextBlock.TextProperty, new Binding("Message"));
        messageTextFactory.SetValue(TextBlock.TextWrappingProperty, TextWrapping.Wrap);
        messageTextFactory.SetValue(TextBlock.ForegroundProperty, Brushes.White);
        messageTextFactory.SetValue(TextBlock.FontSizeProperty, 14.0);

        bubbleBorderFactory.AppendChild(messageTextFactory);

        FrameworkElementFactory wrapperFactory = new(typeof(Grid));
        wrapperFactory.AppendChild(bubbleBorderFactory);

        FrameworkElementFactory messagesPanelFactory = new(typeof(StackPanel));
        messagesPanelFactory.SetValue(StackPanel.OrientationProperty, Orientation.Vertical);

        ItemsControl messagesItemsControl = new()
        {
            ItemsPanel = new ItemsPanelTemplate(messagesPanelFactory),
            Margin = new Thickness(0, 5, 0, 5),
            ItemTemplate = new DataTemplate { VisualTree = wrapperFactory }
        };

        ScrollViewer scrollableContainer = new()
        {
            Padding = new Thickness(5, 0, 5, 0),
            VerticalScrollBarVisibility = ScrollBarVisibility.Auto,
            HorizontalScrollBarVisibility = ScrollBarVisibility.Disabled,
            Content = messagesItemsControl
        };

        Border sendButton = new()
        {
            Style = AppResources.Get<Style>("SendButtonStyle"),
            Child = new Image
            {
                Style = AppResources.Get<Style>("SendButtonImageStyle"),
            }
        };

        DockPanel inputRow = new()
        {
            Height = 44,
            VerticalAlignment = VerticalAlignment.Bottom,
            Margin = new Thickness(5, 5, 5, -5),
            Children =
            {
                sendButton,
                InputFieldFactory()
            }
        };

        Border violationNotification = ViolationNotificationFactory();

        (sendButton.Child as Image)?.SetResourceReference(Image.SourceProperty, "SendMessageImage");
        (inputRow.Children[1] as Border)!.SetResourceReference(Border.BackgroundProperty, "ButtonBrush");
        (violationNotification.Child as Label)?.SetResourceReference(Label.ContentProperty, "ViolationNotificationText");

        Grid inputContainer = ((inputRow.Children[1] as Border)!.Child as Grid)!;

        Binding messageInputBinding = new("MessageInput")
        {
            Mode = BindingMode.TwoWay,
            UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
        };
        (inputContainer.Children[0] as TextBox)!.SetBinding(TextBox.TextProperty, messageInputBinding);

        Binding chatMessagePlaceholderTextVisibilityBinding = new("ChatMessagePlaceholderTextVisibility")
        { Mode = BindingMode.OneWay };
        (inputContainer.Children[1] as TextBlock)!.SetBinding(TextBlock.VisibilityProperty,
            chatMessagePlaceholderTextVisibilityBinding);

        inputRow.SetBinding(DockPanel.VisibilityProperty, new Binding("InputRowVisibility"));
        bubbleBorderFactory.SetBinding(Border.HorizontalAlignmentProperty, new Binding("Alignment"));
        bubbleBorderFactory.SetBinding(Border.BackgroundProperty, new Binding("BubbleBackground"));
        messagesItemsControl.SetBinding(ItemsControl.ItemsSourceProperty, new Binding("ChatMessageDisplays"));

        violationNotification.SetBinding(Border.VisibilityProperty, new Binding("ViolationNotificationVisibility"));

        DockPanel.SetDock(sendButton, Dock.Right);
        DockPanel.SetDock(inputRow, Dock.Bottom);
        DockPanel.SetDock(violationNotification, Dock.Bottom);

        CommandAttachers.OnClickEvent(sendButton, "SendChatMessageCommand");

        return new DockPanel
        {
            Margin = new Thickness(7, 5, 5, 5),
            Children =
            {
                inputRow,
                violationNotification,
                scrollableContainer
            }
        };
    }

    static Border InputFieldFactory() => new()
    {
        Style = AppResources.Get<Style>("SendMessageBorderStyle"),
        Child = new Grid
        {
            Children =
            {
                new TextBox()
                {
                    Style = AppResources.Get<Style>("SendMessageTextBoxStyle")
                },
                new TextBlock()
                {
                    Text = AppResources.Get<string>("ChatInputPlaceHolderText"),
                    Foreground = Brushes.Gray,
                    Opacity = 0.8,
                    Margin = new Thickness(12, 0, 0, 0),
                    IsHitTestVisible = false,
                    VerticalAlignment = VerticalAlignment.Center
                }
            }
        }
    };

    static Border ViolationNotificationFactory() => new()
    {
        Height = 30,
        Width = 247,
        Margin = new Thickness(10, 10, 0, 0),
        HorizontalAlignment = HorizontalAlignment.Left,
        BorderBrush = ColorConverters.BrushFromString("#4A0F0F"),
        BorderThickness = new Thickness(2),
        CornerRadius = new CornerRadius(10),
        Background = ColorConverters.BrushFromString("#F2B8B5"),
        Opacity = 0.85,
        Child = new Label
        {
            HorizontalAlignment = HorizontalAlignment.Center,
            Style = AppResources.Get<Style>("TextStyle"),
            Foreground = ColorConverters.BrushFromString("#4A0F0F"),
            FontSize = 13.4,
            FontWeight = FontWeights.Bold,
        }
    };
}