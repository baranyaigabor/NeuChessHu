using NeuChessHu.Resources.Themes.BoardThemes;
using NeuChessHu.Resources.Types.ThemeTypes;
using NeuChessHu.Properties;

namespace NeuChessHu.UserSettings.SettingManagers;

internal static class BoardThemeManager
{
    static readonly Dictionary<BoardTheme, Action> ThemeActions = new()
    {
        {
            BoardTheme.PastelGreen,
            PastelGreenBoard.Set
        },
        {
            BoardTheme.BrightBlue,
            BrightBlueBoard.Set
        },
        {
            BoardTheme.PastelBlue,
            PastelBlueBoard.Set
        },
        {
            BoardTheme.PinkWorld,
            PinkWorldBoard.Set
        },
        {
            BoardTheme.Modern,
            ModernBoard.Set
        },
        {
            BoardTheme.Wooden,
            WoodenBoard.Set
        },
        {
            BoardTheme.Fradi,
            FradiBoard.Set
        },
        {
            BoardTheme.Royal,
            RoyalBoard.Set
        },
        {
            BoardTheme.Death,
            DeathBoard.Set
        },
    };

    internal static BoardTheme Decode()
    {
        if (BoardTheme.AllBoardThemes.TryGetValue(Settings.Default.BoardTheme, out BoardTheme boardTheme))
            return boardTheme;

        throw new NotSupportedException($"{Settings.Default.BoardTheme} is not supported!");
    }

    internal static void ApplyTheme(BoardTheme theme) =>
        ThemeActions[theme].Invoke();
}