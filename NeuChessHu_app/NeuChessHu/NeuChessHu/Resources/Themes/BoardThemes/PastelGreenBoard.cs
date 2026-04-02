using NeuChessHu.Converters;
using System.Windows;

namespace NeuChessHu.Resources.Themes.BoardThemes;

internal static class PastelGreenBoard
{
    readonly static ResourceDictionary resources = Application.Current.Resources;

    internal static void Set()
    {
        resources["LightSquareBrush"] = ColorConverters.BrushFromString("#E6E8C4");
        resources["DarkSquareBrush"] = ColorConverters.BrushFromString("#618541");
        resources["SelectedLightSquareBrush"] = ColorConverters.BrushFromString("#F3F677");
        resources["SelectedDarkSquareBrush"] = ColorConverters.BrushFromString("#B0C435");
        resources["OddBoardIdentifierBrush"] = ColorConverters.BrushFromString("#618541");
        resources["EvenBoardIdentifierBrush"] = ColorConverters.BrushFromString("#E6E8C4");
        resources["BoardBorderBrush"] = ColorConverters.BrushFromString("#00FFFFFF");
    }
}