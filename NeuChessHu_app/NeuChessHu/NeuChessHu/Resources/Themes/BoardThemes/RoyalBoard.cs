using NeuChessHu.Converters;
using System.Windows;

namespace NeuChessHu.Resources.Themes.BoardThemes;

internal static class RoyalBoard
{
    readonly static ResourceDictionary resources = Application.Current.Resources;

    internal static void Set()
    {
        resources["LightSquareBrush"] = ColorConverters.BrushFromString("#B85555");
        resources["DarkSquareBrush"] = ColorConverters.BrushFromString("#333333");
        resources["SelectedLightSquareBrush"] = ColorConverters.BrushFromString("#D79C75");
        resources["SelectedDarkSquareBrush"] = ColorConverters.BrushFromString("#958B64");
        resources["OddBoardIdentifierBrush"] = ColorConverters.BrushFromString("#333333"); 
        resources["EvenBoardIdentifierBrush"] = ColorConverters.BrushFromString("#B85555");
        resources["BoardBorderBrush"] = ColorConverters.BrushFromString("#FAE18A");
    }
}