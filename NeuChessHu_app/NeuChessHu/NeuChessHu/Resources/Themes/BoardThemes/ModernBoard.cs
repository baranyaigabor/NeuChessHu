using NeuChessHu.Converters;
using System.Windows;

namespace NeuChessHu.Resources.Themes.BoardThemes;

internal static class ModernBoard
{
    readonly static ResourceDictionary resources = Application.Current.Resources;

    internal static void Set()
    {
        resources["LightSquareBrush"] = ColorConverters.BrushFromString("#687385");
        resources["DarkSquareBrush"] = ColorConverters.BrushFromString("#292F3D");
        resources["SelectedLightSquareBrush"] = ColorConverters.BrushFromString("#607F99");
        resources["SelectedDarkSquareBrush"] = ColorConverters.BrushFromString("#607F99");
        resources["OddBoardIdentifierBrush"] = ColorConverters.BrushFromString("#292F3D");
        resources["EvenBoardIdentifierBrush"] = ColorConverters.BrushFromString("#687385");
        resources["BoardBorderBrush"] = ColorConverters.BrushFromString("#00FFFFFF");
    }
}