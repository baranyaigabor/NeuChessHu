using NeuChessHu.Converters;
using System.Windows;

namespace NeuChessHu.Resources.Themes.BoardThemes;

internal static class DeathBoard
{
    readonly static ResourceDictionary resources = Application.Current.Resources;

    internal static void Set()
    {
        resources["DarkSquareBrush"] = ColorConverters.BrushFromString("#999486");
        resources["LightSquareBrush"] = ColorConverters.BrushFromString("#EAEAEA");
        resources["SelectedDarkSquareBrush"] = ColorConverters.BrushFromString("#AFC974");
        resources["SelectedLightSquareBrush"] = ColorConverters.BrushFromString("#D7F4A6");
        resources["OddBoardIdentifierBrush"] = ColorConverters.BrushFromString("#EAEAEA");
        resources["EvenBoardIdentifierBrush"] = ColorConverters.BrushFromString("#999486");
        resources["BoardBorderBrush"] = ColorConverters.BrushFromString("#00FFFFFF");
    }
}