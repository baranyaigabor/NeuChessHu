using NeuChessHu.Converters;
using System.Windows;
using System.Windows.Media;

namespace NeuChessHu.Resources.Themes.AppThemes;

internal static class DarkTheme
{
    internal static void SetColors()
    {
        Application.Current.Resources["NavBarBrush"] = new SolidColorBrush(ColorConverters.ColorFromString("#21211F"));
        Application.Current.Resources["SideBarBrush"] = new SolidColorBrush(ColorConverters.ColorFromString("#2F2E2A"));
        Application.Current.Resources["WindowBrush"] = new SolidColorBrush(ColorConverters.ColorFromString("#5B5656"));
        Application.Current.Resources["ButtonBrush"] = new SolidColorBrush(ColorConverters.ColorFromString("#4C4A4A"));
        Application.Current.Resources["TextBrush"] = new SolidColorBrush(ColorConverters.ColorFromString("#FFFFFF"));
        Application.Current.Resources["TextPopUpBrush"] = new SolidColorBrush(ColorConverters.ColorFromString("#000000"));
        Application.Current.Resources["BorderBrush"] = new SolidColorBrush(ColorConverters.ColorFromString("#000000"));
        Application.Current.Resources["PopUpBackground"] = new SolidColorBrush(ColorConverters.ColorFromString("#C0C0C0"));
    }
}