using NeuChessHu.Converters;
using System.Windows;

namespace NeuChessHu.Resources.Themes.BoardThemes;

internal static class PinkWorldBoard
{
    readonly static ResourceDictionary resources = Application.Current.Resources;

    internal static void Set()
    {
        resources["LightSquareBrush"] = ColorConverters.BrushFromString("#F4F0F1");
        resources["DarkSquareBrush"] = ColorConverters.BrushFromString("#E098A4");
        resources["SelectedLightSquareBrush"] = ColorConverters.BrushFromString("#E6B298");
        resources["SelectedDarkSquareBrush"] = ColorConverters.BrushFromString("#DC8672");
        resources["OddBoardIdentifierBrush"] = ColorConverters.BrushFromString("#E098A4");
        resources["EvenBoardIdentifierBrush"] = ColorConverters.BrushFromString("#F4F0F1");
        resources["BoardBorderBrush"] = ColorConverters.BrushFromString("#00FFFFFF");
    }
}
