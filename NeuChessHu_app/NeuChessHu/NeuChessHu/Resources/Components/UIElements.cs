using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace NeuChessHu.Resources.Components;

internal static class UIElements
{
    internal static Border PopUpBackgroundFactory(FrameworkElement panel) => new()
    {
        CornerRadius = new CornerRadius(10),
        Width = panel.Width,
        Height = panel.Height,
        Opacity = 0.7
    };

    internal static Rectangle HorizontalBarFactory(FrameworkElement panel) => new()
    {
        Width = panel.Width - 20,
        Height = 2,
        Fill = Brushes.Black,
        HorizontalAlignment = HorizontalAlignment.Center
    };

    internal static Rectangle SettingsVerticalSeparatorFactory() => new()
    {
        Height = 160,
        Width = 2,
        Fill = Brushes.Black,
        VerticalAlignment = VerticalAlignment.Center,
        HorizontalAlignment = HorizontalAlignment.Center,
        Margin = new Thickness(10, 0, 5, 0)
    };

    internal static Rectangle MatchEndVerticalSeparatorFactory() => new()
    {
        Height = 50,
        Width = 2,
        Fill = Brushes.Black,
        VerticalAlignment = VerticalAlignment.Center,
        HorizontalAlignment = HorizontalAlignment.Center
    };
}