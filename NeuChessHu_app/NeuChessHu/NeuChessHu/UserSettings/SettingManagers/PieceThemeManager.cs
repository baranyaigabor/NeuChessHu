using ChessMechanics.ChessBoard.Definitions;
using NeuChessHu.Properties;
using NeuChessHu.Resources.Types.ThemeTypes;
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

    internal static BitmapImage ImageLoader(Piece? piece, Side color, BindableSettings settings) => 
        piece is null 
            ? throw new ArgumentNullException(nameof(piece))
            : new (new Uri($"/resources/Themes/PieceThemes/{settings.PieceTheme}/" +
                $"{color}Pieces/{piece}{color}.png", UriKind.Relative));
}