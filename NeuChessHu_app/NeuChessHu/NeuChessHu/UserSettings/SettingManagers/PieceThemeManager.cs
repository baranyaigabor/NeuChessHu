using ChessMechanics.ChessBoard.Definitions;
using NeuChessHu.Properties;
using NeuChessHu.Resources.Types.ThemeTypes;
using System.IO;
using System.Windows.Media.Imaging;

namespace NeuChessHu.UserSettings.SettingManagers;

internal static class PieceThemeManager
{
    internal static PieceTheme Decode()
    {
        if (PieceTheme.AllPieceThemes.TryGetValue(Settings.Default.PieceTheme, out PieceTheme pieceTheme))
            return pieceTheme;

        throw new NotSupportedException($"{Settings.Default.PieceTheme} is not supported!");
    }

    internal static BitmapImage ImageLoader(Piece? piece, Side color, BindableSettings settings)
    {
        if (piece is null)
            throw new ArgumentNullException(nameof(piece));

        string imagePath = Path.Combine(
            AppContext.BaseDirectory,
            "Resources",
            "Themes",
            "PieceThemes",
            settings.PieceTheme.ToString(),
            $"{color}Pieces",
            $"{piece}{color}.png");

        if (!File.Exists(imagePath))
            throw new FileNotFoundException($"Piece image was not found: {imagePath}", imagePath);

        BitmapImage image = new();
        image.BeginInit();
        image.CacheOption = BitmapCacheOption.OnLoad;
        image.UriSource = new Uri(imagePath, UriKind.Absolute);
        image.EndInit();
        image.Freeze();

        return image;
    }
}
