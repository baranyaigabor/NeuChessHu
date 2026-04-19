using NeuChessHu.Converters;
using System.Windows;
using System.Windows.Controls;

namespace NeuChessHu.Resources.Components.ViewElements.Login;

internal static class LoginNotification
{
    internal static Border LoginNotificationBuilder() => new()
    {
        Height = 30,
        Width = 240,
        Margin = new Thickness(0, 7, 0, 0),
        BorderBrush = ColorConverters.BrushFromString("#4A0F0F"),
        BorderThickness = new Thickness(2),
        CornerRadius = new CornerRadius(10),
        Background = ColorConverters.BrushFromString("#F2B8B5"),
        Opacity = 0.85,
        Child = new Label
        {
            HorizontalAlignment = HorizontalAlignment.Center,
            Content = AppResources.Get<string>("LoginNotificationText"),
            Style = AppResources.Get<Style>("TextStyle"),
            Foreground = ColorConverters.BrushFromString("#4A0F0F"),
            FontSize = 13.4,
            FontWeight = FontWeights.Bold,
        }
    };
}