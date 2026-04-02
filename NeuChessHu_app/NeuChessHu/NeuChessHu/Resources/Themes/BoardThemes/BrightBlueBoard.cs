using NeuChessHu.Converters;
using System.Windows;

namespace NeuChessHu.Resources.Themes.BoardThemes;

internal static class BrightBlueBoard
{
    readonly static ResourceDictionary resources = Application.Current.Resources;

    internal static void Set()
    {
        resources["LightSquareBrush"] = ColorConverters.BrushFromString("#F3F6FA");
        resources["DarkSquareBrush"] = ColorConverters.BrushFromString("#6594EB");
        resources["SelectedLightSquareBrush"] = ColorConverters.BrushFromString("#B0C435");
        resources["SelectedDarkSquareBrush"] = ColorConverters.BrushFromString("#6DB9E5");
        resources["OddBoardIdentifierBrush"] = ColorConverters.BrushFromString("#6594EB");
        resources["EvenBoardIdentifierBrush"] = ColorConverters.BrushFromString("#F3F6FA");
        resources["BoardBorderBrush"] = ColorConverters.BrushFromString("#00FFFFFF");
    }
}