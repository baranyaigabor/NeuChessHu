using System.IO;
using System.Windows.Media.Imaging;

namespace NeuChessHu.Converters;

internal class ImageConverters
{
    internal static BitmapImage? LoadProfilePicture(string profilePicture)
    {
        if (string.IsNullOrWhiteSpace(profilePicture))
            return null;

        try
        {
            int base64Index = profilePicture.LastIndexOf("base64,");
            if (base64Index < 0)
                return null;

            string base64Data = profilePicture.Substring(base64Index + 7); 

            byte[] bytes = Convert.FromBase64String(base64Data);

            using MemoryStream stream = new(bytes);
            BitmapImage image = new();
            image.BeginInit();
            image.CacheOption = BitmapCacheOption.OnLoad;
            image.StreamSource = stream;
            image.EndInit();
            image.Freeze();
            return image;
        }
        catch
        {
            return null;
        }
    }
}