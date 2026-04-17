using Microsoft.Xaml.Behaviors;
using NeuChessHu.CommandUtils;
using NeuChessHu.Resources;
using SharpVectors.Converters;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;

namespace NeuChessHu.Views.SideBars.MenuSideBar;

public partial class MenuSideBarView : UserControl
{
    public MenuSideBarView() =>
        Loaded += (s, e) =>
        {
            Content ??= MenuSideBarBuilder();
        };

    static Border MenuSideBarBuilder()
    {
        Style buttonStyle = AppResources.Get<Style>("ButtonStyleBaseMenuStyle");

        Style textStyle = AppResources.Get<Style>("TextStyle");

        StackPanel menuSideBar = new();

        Border menuSideBarBorder = new()
        {
            Height = 520,
            Width = 280,
            BorderThickness = new Thickness(1),
            Margin = new Thickness(10, 0, 10, 0),
            Child = new ScrollViewer
            {
                Content = menuSideBar,
                VerticalScrollBarVisibility = ScrollBarVisibility.Visible
            }
        };

        SvgViewbox handMovesPiece = new()
        {
            Width = 120,
            Height = 170,
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Top,
            Stretch = Stretch.Uniform,
            Margin = new Thickness(16, 30, 0, -20)
        };

        Border timeSetterButton = new()
        {
            Style = buttonStyle,
            Margin = new Thickness(0, 0, 0, 20),
            Child = new Label()
            {
                Style = textStyle
            }
        };

        Border startButton = new()
        {
            Style = buttonStyle,
            Margin = new Thickness(0, 0, 0, 15),
            Child = new Label
            {
                Style = textStyle
            }
        };

        StackPanel moreButton = new()
        {
            Orientation = Orientation.Horizontal,
            HorizontalAlignment = HorizontalAlignment.Center,
            Margin = new Thickness(0, 5, 0, 20),
            Cursor = AppResources.Get<Cursor>("CursorOnButtons"),
            ForceCursor = true,
        };

        Label moreLabel = new()
        {
            Width = 70,
            FontSize = 18,
            FontWeight = FontWeights.Bold,
            Background = Brushes.Transparent,
            HorizontalAlignment = HorizontalAlignment.Center
        };

        Image moreIcon = new()
        {
            Width = 25,
            Height = 25,
            Margin = new Thickness(-18, 0, 0, 0),
        };

        Border joinTournamentButton = new()
        {
            Style = buttonStyle,
            Margin = new Thickness(0, 0, 0, 22),
            Child = new Label
            {
                Style = textStyle
            }
        };

        Border playStockfishButton = new()
        {
            Style = buttonStyle,
            Margin = new Thickness(0, 0, 0, 20),
            Child = new Label
            {
                Style = textStyle
            }
        };

        Border customGameButton = new()
        {
            Style = buttonStyle,
            Margin = new Thickness(0, 0, 0, 22),
            Child = new Label
            {
                Style = textStyle
            }
        };

        menuSideBar.SetResourceReference(BackgroundProperty, "SideBarBrush");
        menuSideBarBorder.SetResourceReference(BorderBrushProperty, "BorderBrush");
        moreLabel.SetResourceReference(ForegroundProperty, "TextBrush");
        handMovesPiece.SetResourceReference(SvgViewbox.SourceProperty, "HandMovesPieceImage");

        (startButton.Child as Label)!.SetResourceReference(ContentProperty, "StartGameText");
        moreLabel.SetResourceReference(ContentProperty, "MoreOptionsText");
        (joinTournamentButton.Child as Label)!.SetResourceReference(ContentProperty, "JoinTournamentText");
        (playStockfishButton.Child as Label)!.SetResourceReference(ContentProperty, "PlayStockfishText");
        (customGameButton.Child as Label)!.SetResourceReference(ContentProperty, "CustomGameText");

        Binding timeSetterButtonBinding = new("TimeSetterButtonContent") { Mode = BindingMode.OneWay };
        (timeSetterButton.Child as Label)!.SetBinding(ContentProperty, timeSetterButtonBinding);

        Binding moreIconImageBinding = new("MoreIconSource") { Mode = BindingMode.OneWay };
        moreIcon.SetBinding(Image.SourceProperty, moreIconImageBinding);

        Binding joinTournamentButtonBinding = new("JoinTournamentButtonVisibility") { Mode = BindingMode.OneWay };
        joinTournamentButton.SetBinding(VisibilityProperty, joinTournamentButtonBinding);

        Binding playStockfishButtonBinding = new("PlayStockfishButtonVisibility") { Mode = BindingMode.OneWay };
        playStockfishButton.SetBinding(VisibilityProperty, playStockfishButtonBinding);

        Binding customGameButtonBinding = new("CustomGameButtonVisibility") { Mode = BindingMode.OneWay };
        customGameButton.SetBinding(VisibilityProperty, customGameButtonBinding);

        CommandAttachers.OnClickEvent(timeSetterButton, "OpenTimeSetterCommand");
        CommandAttachers.OnClickEvent(startButton, "StartMatchCommand");
        CommandAttachers.OnClickEvent(moreButton, "MoreIconToggleCommand");
        CommandAttachers.OnClickEvent(customGameButton, "CustomGameCommand");

        foreach (UIElement element in new UIElement[] { moreLabel, moreIcon })
            moreButton.Children.Add(element);

        foreach (UIElement element in new UIElement[] { handMovesPiece, timeSetterButton, startButton, moreButton, joinTournamentButton, playStockfishButton, customGameButton })
            menuSideBar.Children.Add(element);

        return menuSideBarBorder;
    }
}
