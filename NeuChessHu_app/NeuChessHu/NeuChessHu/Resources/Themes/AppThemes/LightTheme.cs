using NeuChessHu.Converters;
using System.Windows;
using System.Windows.Media;

namespace NeuChessHu.Resources.Themes.AppThemes;

internal static class LightTheme
{
    internal static void SetColors()
    {
        Application.Current.Resources["NavBarBrush"] = new SolidColorBrush(ColorConverters.ColorFromString("#D0B399"));
        Application.Current.Resources["SideBarBrush"] = new SolidColorBrush(ColorConverters.ColorFromString("#DAB393"));
        Application.Current.Resources["WindowBrush"] = new SolidColorBrush(ColorConverters.ColorFromString("#FFE4C4"));
        Application.Current.Resources["ButtonBrush"] = new SolidColorBrush(ColorConverters.ColorFromString("#FFECC8"));
        Application.Current.Resources["TextBrush"] = new SolidColorBrush(ColorConverters.ColorFromString("#000000"));
        Application.Current.Resources["TextPopUpBrush"] = new SolidColorBrush(ColorConverters.ColorFromString("#000000"));
        Application.Current.Resources["BorderBrush"] = new SolidColorBrush(ColorConverters.ColorFromString("#000000"));
        Application.Current.Resources["PopUpBackground"] = new SolidColorBrush(ColorConverters.ColorFromString("#F5F5F5"));
    }
}