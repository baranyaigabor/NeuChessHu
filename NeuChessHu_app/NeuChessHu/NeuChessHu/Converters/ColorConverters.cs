using System.Windows.Media;

namespace NeuChessHu.Converters;

internal static class ColorConverters
{
    internal static Color ColorFromString(string hexa) =>
        (Color)ColorConverter.ConvertFromString(hexa);

    internal static Brush BrushFromString(string hexa) =>
        (Brush)new BrushConverter().ConvertFromString(hexa)!;
}