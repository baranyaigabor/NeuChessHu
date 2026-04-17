using NeuChessHu.Resources.Types.ThemeTypes;
using System.Windows;
using System.Windows.Media.Imaging;

namespace NeuChessHu.Resources.Images.Register.Images.Dynamics.Themed;

internal static class ThemedImages
{
    readonly static string basePath = "pack://application:,,,/NeuChessHu;component/resources/Images/Dynamics/";

    readonly static Dictionary<string, Uri> themedImages = new()
    {
        { "LogoImage", new($"{basePath}Logos/NeuChess_Hu_wide_light.svg") },
        { "DefaultProfilePictureImage", new($"{basePath}DefaultProfilePictures/ProfilePic_light.png") },
        { "MoreDownImage", new($"{basePath}MoreArrows/MoreUp_light.png") },
        { "MoreUpImage", new($"{basePath}MoreArrows/MoreDown_light.png") },
        { "ChatImage", new($"{basePath}Chat/ChatPic_light.png") },
        { "SendMessageImage", new($"{basePath}SendMessage/SendMessage_light.png") },
        { "OptionsImage", new($"{basePath}Options/OptionsPic_light.png") },
        { "TickImage", new($"{basePath}Tick/Tick_light.png") },
        { "CrossImage", new($"{basePath}Cross/Cross_light.png") },
    };

    internal static void SetTheme(AppTheme appTheme)
    {
        foreach (var item in themedImages)
        {
            Uri uri = appTheme == AppTheme.Dark 
                ? new(item.Value.ToString().Replace("light", "dark"))
                : item.Value;

            Application.Current.Resources[item.Key] = item.Value.ToString().EndsWith(".svg")
                ? uri
                : new BitmapImage(uri);
        }
    }
}
