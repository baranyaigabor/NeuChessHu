using NeuChessHu.Resources.Types;
using System.Windows;
using System.Windows.Media.Imaging;

namespace NeuChessHu.Resources.Images.Register.Images.Dynamics.Localized;

internal static class LocalizedImages
{
    readonly static string basePath = "pack://application:,,,/NeuChessHu;component/resources/Images/Dynamics/";

    readonly static Dictionary<string, Uri> hungarianImages = new()
    {
        { "SelectedLanguageFlagImage", new($"{basePath}Flags/HungarianFlag.svg") },
    };

    readonly static Dictionary<string, Uri> englishImages = new()
    {
        { "SelectedLanguageFlagImage", new($"{basePath}Flags/EnglishFlag.svg") },
    };

    internal static void SetLanguage(Language language)
    {
        Dictionary<string, Uri> dictionary = language.Value switch
        {
            "Hungarian" => hungarianImages,
            "English" => englishImages,
            _ => throw new NotSupportedException($"{language} is not supported!")
        };

        foreach (var item in dictionary)
        {
            Application.Current.Resources[item.Key] = item.Value.ToString().EndsWith(".svg")
                ? item.Value
                : new BitmapImage(item.Value);
        }
    }
}
