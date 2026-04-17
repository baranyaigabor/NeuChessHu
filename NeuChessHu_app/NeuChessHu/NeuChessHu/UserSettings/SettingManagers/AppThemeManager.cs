using NeuChessHu.Properties;
using NeuChessHu.Resources.Images.Register.Images.Dynamics.Themed;
using NeuChessHu.Resources.Themes.AppThemes;
using NeuChessHu.Resources.Types.ThemeTypes;

internal static class AppThemeManager
{
    static void SetLightTheme()
    {
        LightTheme.SetColors();
        ThemedImages.SetTheme(AppTheme.Light);
    }

    static void SetDarkTheme()
    {
        DarkTheme.SetColors();
        ThemedImages.SetTheme(AppTheme.Dark);
    }

    internal static bool Decode() =>
        Settings.Default.DarkMode;

    internal static void ApplyTheme(bool isDark)
    {
        if (isDark)
            SetDarkTheme();

        else SetLightTheme();
    }
}