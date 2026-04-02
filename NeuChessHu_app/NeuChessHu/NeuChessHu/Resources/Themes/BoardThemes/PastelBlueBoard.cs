using NeuChessHu.Converters;
using System.Windows;

namespace NeuChessHu.Resources.Themes.BoardThemes;

internal static class PastelBlueBoard
{
    readonly static ResourceDictionary resources = Application.Current.Resources;

    internal static void Set()
    {
        resources["LightSquareBrush"] = ColorConverters.BrushFromString("#A4B8CC");
        resources["DarkSquareBrush"] = ColorConverters.BrushFromString("#8396AA");
        resources["SelectedLightSquareBrush"] = ColorConverters.BrushFromString("#86A3BD");
        resources["SelectedDarkSquareBrush"] = ColorConverters.BrushFromString("#7492AC");
        resources["OddBoardIdentifierBrush"] = ColorConverters.BrushFromString("#FFFFFF");
        resources["EvenBoardIdentifierBrush"] = ColorConverters.BrushFromString("#FFFFFF");
        resources["BoardBorderBrush"] = ColorConverters.BrushFromString("#00FFFFFF");
    }
}
