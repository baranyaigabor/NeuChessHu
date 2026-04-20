using NeuChessHu.CommandUtils;
using NeuChessHu.Resources;
using NeuChessHu.Resources.Components;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace NeuChessHu.Views.Overlays.MatchOverlays.MatchWindows;

public partial class MatchEndWindowView : UserControl
{
    public MatchEndWindowView() =>
        Loaded += (s, e) =>
        {
            Content ??= MatchEndWindowBuilder();
        };

    static Border MatchEndWindowBuilder()
    {
        Style textStyle = AppResources.Get<Style>("TextStyle");

        Style buttonStyle = AppResources.Get<Style>("ButtonStyleBaseMenuStyle");

        Border matchEndBorder = new()
        {
            CornerRadius = new CornerRadius(15),
            BorderBrush = Brushes.Black,
            BorderThickness = new Thickness(2),
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Center,
            Background = AppResources.Get<SolidColorBrush>("SideBarBrush"),
            Margin = new Thickness(190),
        };

        DockPanel matchEndContainer = new()
        {
            Width = 280,
            Height = 280,
        };

        Border matchEndHeader = new()
        {
            Width = matchEndBorder.Width,
            Height = 60,
            CornerRadius = new CornerRadius(15, 15, 0, 0),
            Style = AppResources.Get<Style>("WindowsHeaderStyle"),
            BorderThickness = new Thickness(0, 0, 0, 2),
            Background = AppResources.Get<SolidColorBrush>("NavBarBrush"),
            HorizontalAlignment = HorizontalAlignment.Stretch,
            Child = new Grid
            {
                Children =
                {
                    new Ellipse
                    {
                        Style = AppResources.Get<Style>("CloseEllipseButtonStyle"),
                        Margin = new Thickness(0, 8, 8, 0),
                    },
                    new StackPanel
                    {
                        Children =
                        {
                            new Label
                            {
                                Style = textStyle,
                                FontSize = 20,
                                Margin = new Thickness(0, 5, 0, 0)
                            },
                            new Label
                            {
                                Style = textStyle,
                                FontSize = 13,
                                Margin = new Thickness(0, -8, 0, 0)
                            }
                        }
                    }
                }
            }
        };

        Border matchEndBody = MatchEndBodyBuilder(textStyle, matchEndBorder);

        Border playAgainButton = new()
        {
            Width = 160,
            Height = 35,
            Margin = new Thickness(0, 5, 0, 5),
            CornerRadius = new CornerRadius(10),
            Style = buttonStyle,
            Child = new Label
            {
                Style = textStyle,
                FontSize = 14,
                Background = Brushes.Transparent,
                BorderBrush = Brushes.Transparent
            }
        };

        Border returnToMenuButton = new()
        {
            Width = 160,
            Height = 35,
            Margin = new Thickness(0, -10, 0, 0),
            CornerRadius = new CornerRadius(10),
            Style = buttonStyle,
            Child = new Label
            {
                Style = textStyle,
                FontSize = 14,
                Background = Brushes.Transparent,
                BorderBrush = Brushes.Transparent
            }
        };

        (playAgainButton.Child as Label)!.SetResourceReference(ContentProperty, "PlayAgainButtonText");
        (returnToMenuButton.Child as Label)!.SetResourceReference(ContentProperty, "QuitToMenuButtonText");

        Binding matchResultBinding = new("MatchResult") { Mode = BindingMode.OneWay };
        (((matchEndHeader.Child as Grid)!.Children[1] as StackPanel)!.Children[0] as Label)!
            .SetBinding(ContentProperty, matchResultBinding);

        Binding matchEndReasonBinding = new("MatchEndReason") { Mode = BindingMode.OneWay };
        (((matchEndHeader.Child as Grid)!.Children[1] as StackPanel)!.Children[1] as Label)!
            .SetBinding(ContentProperty, matchEndReasonBinding);

        Ellipse closeButton = ((matchEndHeader.Child as Grid)!.Children[0] as Ellipse)!;
        CommandAttachers.OnClickEvent(closeButton, "CloseOverlayCommand");

        CommandAttachers.OnClickEvent(playAgainButton, "PlayAgainCommand");
        CommandAttachers.OnClickEvent(returnToMenuButton, "SwitchMenuCommand");

        DockPanel.SetDock(matchEndHeader, Dock.Top);
        DockPanel.SetDock(matchEndBody, Dock.Top);
        DockPanel.SetDock(playAgainButton, Dock.Top);
        DockPanel.SetDock(returnToMenuButton, Dock.Top);

        foreach (UIElement element in new UIElement[] { matchEndHeader, matchEndBody,
            playAgainButton, returnToMenuButton })
            matchEndContainer.Children.Add(element);

        matchEndBorder.Child = matchEndContainer;

        return matchEndBorder;
    }

    static Border MatchEndBodyBuilder(Style textStyle, Border matchEndBorder)
    {
        Grid matchEndBodyContainer = new();

        int[] widths = [65, 70, 10, 70, 65];
        for (int i = 0; i < 5; i++)
            matchEndBodyContainer.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(widths[i], GridUnitType.Pixel) });

        Border player = PlayerImageDisplay(isItLeft: true);

        Rectangle separator = UIElements.MatchEndVerticalSeparatorFactory();

        Border opponent = PlayerImageDisplay(isItLeft: false);

        Grid.SetColumn(player, 1);
        Grid.SetColumn(separator, 2);
        Grid.SetColumn(opponent, 3);

        Binding playerBorderBrushBinding = new("PlayerMatchResultBrush") { Mode = BindingMode.OneWay };
        player.SetBinding(Border.BorderBrushProperty, playerBorderBrushBinding);

        Binding opponentBorderBrushBinding = new("OpponentMatchResultBrush") { Mode = BindingMode.OneWay };
        opponent.SetBinding(Border.BorderBrushProperty, opponentBorderBrushBinding);

        Binding playerProfilePictureBinding = new("PlayerProfilePicture") { Mode = BindingMode.OneWay };
        ((player.Child as Grid)!.Children[0] as Image)!.SetBinding(Image.SourceProperty, playerProfilePictureBinding);

        Binding opponentProfilePictureBinding = new("OpponentProfilePicture") { Mode = BindingMode.OneWay };
        ((opponent.Child as Grid)!.Children[0] as Image)!.SetBinding(Image.SourceProperty, opponentProfilePictureBinding);

        Binding playerProfilePictureStyleBinding = new("PlayerProfilePictureStyle") { Mode = BindingMode.OneWay };
        ((player.Child as Grid)!.Children[0] as Image)!.SetBinding(StyleProperty, playerProfilePictureStyleBinding);

        Binding opponentProfilePictureStyleBinding = new("OpponentProfilePictureStyle") { Mode = BindingMode.OneWay };
        ((opponent.Child as Grid)!.Children[0] as Image)!.SetBinding(StyleProperty, opponentProfilePictureStyleBinding);

        Binding playerMedalVisibilityBinding = new("PlayerMedalVisibility") { Mode = BindingMode.OneWay };
        ((player.Child as Grid)!.Children[1] as Image)!.SetBinding(VisibilityProperty, playerMedalVisibilityBinding);

        Binding opponentMedalVisibilityBinding = new("OpponentMedalVisibility") { Mode = BindingMode.OneWay };
        ((opponent.Child as Grid)!.Children[1] as Image)!.SetBinding(VisibilityProperty, opponentMedalVisibilityBinding);

        CommandAttachers.OnClickEvent(player, "OpenPlayerProfileCommand");
        CommandAttachers.OnClickEvent(opponent, "OpenOpponentProfileCommand");

        foreach (UIElement element in new UIElement[] { player, separator, opponent })
            matchEndBodyContainer.Children.Add(element);

        return new()
        {
            Width = matchEndBorder.Width,
            VerticalAlignment = VerticalAlignment.Center,
            Height = 120,
            CornerRadius = new CornerRadius(0, 0, 15, 0),
            Child = matchEndBodyContainer
        };
    }

    static Border PlayerImageDisplay(bool isItLeft) => new()
    {
        Margin = isItLeft ? new Thickness(-20, 0, 10, 0) : new Thickness(10, 0, -20, 0),
        HorizontalAlignment = HorizontalAlignment.Center,
        BorderThickness = new Thickness(4),
        Width = 60,
        Height = 60,
        Child = new Grid()
        {
            Children =
            {
                new Image()
                {
                    Cursor = AppResources.Get<Cursor>("CursorOnButtons"),
                    ForceCursor = true
                },
                new Image()
                {
                    Margin = new Thickness(0, 0, -42, -47),
                    Width = 45,
                    Height = 45,
                    Source = AppResources.Get<ImageSource>("GoldenMedalImage")
                }
            }
        }
    };
}
