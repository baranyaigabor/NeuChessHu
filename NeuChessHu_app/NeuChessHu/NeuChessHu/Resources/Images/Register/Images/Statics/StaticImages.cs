using System.Windows;
using System.Windows.Media.Imaging;

namespace NeuChessHu.Resources.Images.Register.Images.Statics;

internal static class StaticImages
{
    readonly static string basePath = "pack://application:,,,/NeuChessHu;component/Resources/Images/Statics/";

    readonly static Dictionary<string, Uri> staticImages = new()
    {
        { "HandMovesPieceImage", new($"{basePath}Misc/HandMovesPiece.svg") },
        { "GoldenMedalImage", new($"{basePath}Misc/GoldenMedal.png") },
        { "BulletImage", new($"{basePath}MatchDurations/BulletIcon.png") },
        { "BlitzImage", new($"{basePath}MatchDurations/BlitzIcon.png") },
        { "RapidImage", new($"{basePath}MatchDurations/RapidIcon.png") }
    };

    internal static void Register()
    {
        foreach (var item in staticImages)
            Application.Current.Resources[item.Key] = item.Value.ToString().EndsWith(".svg")
                ? item.Value
                : new BitmapImage(item.Value);
    }
}