using NeuChessHu.Converters;
using System.Windows;

namespace NeuChessHu.Resources.Themes.BoardThemes;

internal static class FradiBoard
{
    readonly static ResourceDictionary resources = Application.Current.Resources;

    internal static void Set()
    {
        resources["LightSquareBrush"] = ColorConverters.BrushFromString("#E9E8E5");
        resources["DarkSquareBrush"] = ColorConverters.BrushFromString("#3E634A");
        resources["SelectedLightSquareBrush"] = ColorConverters.BrushFromString("#F7F7A6");
        resources["SelectedDarkSquareBrush"] = ColorConverters.BrushFromString("#A0B357");
        resources["OddBoardIdentifierBrush"] = ColorConverters.BrushFromString("#3E634A");
        resources["EvenBoardIdentifierBrush"] = ColorConverters.BrushFromString("#E9E8E5");
        resources["BoardBorderBrush"] = ColorConverters.BrushFromString("#00FFFFFF");
    }
}
