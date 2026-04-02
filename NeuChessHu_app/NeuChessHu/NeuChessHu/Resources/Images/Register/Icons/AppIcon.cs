using System.Windows;
using System.Windows.Media.Imaging;

namespace NeuChessHu.Resources.Images.Register.Icons;

internal static class AppIcon
{
    internal static void Register() =>
        Application.Current.Resources["AppIcon"] = new BitmapImage(new Uri
            ("pack://application:,,,/NeuChessHu;component/resources/Images/Statics/Icons/NeuChess_Hu.ico"));
}