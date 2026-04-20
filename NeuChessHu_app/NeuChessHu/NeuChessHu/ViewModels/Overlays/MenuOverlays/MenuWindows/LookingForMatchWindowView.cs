using NeuChessHu.CommandUtils;
using NeuChessHu.Resources;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Shapes;

namespace NeuChessHu.Views.Overlays.MenuOverlays.MenuWindows;

public class LookingForMatchWindowView : UserControl
{
    public LookingForMatchWindowView() =>
        Loaded += (s, e) =>
        {
            Content ??= LookingForMatchWindowBuilder();
        };

    static Border LookingForMatchWindowBuilder()
    {
        Style textStyle = AppResources.Get<Style>("TextStyle");

        Border lookingForMatchBorder = new()
        {
            CornerRadius = new CornerRadius(15),
            BorderBrush = Brushes.Black,
            BorderThickness = new Thickness(2),
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Center,
            Margin = new Thickness(230)
        };

        DockPanel lookingForMatchContainer = new()
        {
            Width = 310,
            Height = 200,
        };

        Border lookingForMatchHeader = new()
        {
            Style = AppResources.Get<Style>("WindowsHeaderStyle"),
            Height = 70,
            Child = new Grid
            {
                Children =
                {
                    new Ellipse
                    {
                        Style = AppResources.Get<Style>("CloseEllipseButtonStyle"),
                        Margin = new Thickness(0, 8, 8, 0)
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
                                Margin = new Thickness(0, -3, 0, 0)
                            }
                        }
                    }
                }
            }
        };

        Label searchingNoteLabel = new()
        {
            Style = textStyle,
            FontSize = 15,
            HorizontalAlignment = HorizontalAlignment.Center,
            Margin = new Thickness(5, 15, 0, 0)
        };

        Label timer = new()
        {
            Style = textStyle,
            FontSize = 20,
            Margin = new Thickness(0, 0, 0, 0)
        };

        lookingForMatchBorder.SetResourceReference(BackgroundProperty, "SideBarBrush");

        (((lookingForMatchHeader.Child as Grid)!.Children[1] as StackPanel)!.Children[0] as Label)!
            .SetResourceReference(ContentProperty, "LookingForMatchText");

        Binding matchDurationBinding = new("MatchDuration") { Mode = BindingMode.OneWay };
        (((lookingForMatchHeader.Child as Grid)!.Children[1] as StackPanel)!.Children[1] as Label)!
            .SetBinding(ContentProperty, matchDurationBinding);

        Binding searchingNoteBinding = new("SearchingNote") { Mode = BindingMode.OneWay };
        searchingNoteLabel.SetBinding(ContentProperty, searchingNoteBinding);

        Binding timerBinding = new("ElapsedTime") { Mode = BindingMode.OneWay };
        timer.SetBinding(ContentProperty, timerBinding);

        Grid.SetRow(searchingNoteLabel, 0);
        Grid.SetColumnSpan(searchingNoteLabel, 2);

        Grid.SetRow(timer, 1);
        Grid.SetColumn(timer, 0);

        DockPanel.SetDock(lookingForMatchHeader, Dock.Top);
        DockPanel.SetDock(searchingNoteLabel, Dock.Top);
        DockPanel.SetDock(timer, Dock.Top);

        CommandAttachers.OnClickEvent(((lookingForMatchHeader.Child as Grid)!.Children[0] as Ellipse)!, "StopLookingForMatchCommand");

        foreach (UIElement element in new UIElement[] { lookingForMatchHeader, searchingNoteLabel, timer })
            lookingForMatchContainer.Children.Add(element);

        lookingForMatchBorder.Child = lookingForMatchContainer;

        return lookingForMatchBorder;
    }
}