using NeuChessHu.Converters;
using System.Windows;

namespace NeuChessHu.Resources.Themes.BoardThemes;

internal static class WoodenBoard
{
    readonly static ResourceDictionary resources = Application.Current.Resources;

    internal static void Set()
    {
        resources["LightSquareBrush"] = ColorConverters.BrushFromString("#BCA06B");
        resources["DarkSquareBrush"] = ColorConverters.BrushFromString("#7C5A3A");
        resources["SelectedLightSquareBrush"] = ColorConverters.BrushFromString("#BF9A54");
        resources["SelectedDarkSquareBrush"] = ColorConverters.BrushFromString("#A0773B");
        resources["OddBoardIdentifierBrush"] = ColorConverters.BrushFromString("#7C5A3A");
        resources["EvenBoardIdentifierBrush"] = ColorConverters.BrushFromString("#BCA06B");
        resources["BoardBorderBrush"] = ColorConverters.BrushFromString("#00FFFFFF");
    }
}
