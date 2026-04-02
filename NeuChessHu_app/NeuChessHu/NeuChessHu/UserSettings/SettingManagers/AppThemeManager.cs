using Microsoft.Win32;
using NeuChessHu.Properties;
using NeuChessHu.Resources.Themes.AppThemes;
using NeuChessHu.Resources.Types.ThemeTypes;

namespace NeuChessHu.UserSettings.SettingManagers;

internal static class AppThemeManager
{
    static readonly Dictionary<AppTheme, Action> ThemeActions = new()
    {
        {
            AppTheme.Light,
            SetLightTheme
        },
        {
            AppTheme.Dark,
            SetDarkTheme
        }
    };

    static void SetLightTheme()
    {
        LightTheme.SetColors();
    }

    static void SetDarkTheme()
    {
        DarkTheme.SetColors();
    }

    internal static AppTheme Decode()
    {
        if (AppTheme.AllAppThemes.TryGetValue(Settings.Default.AppTheme, out AppTheme appTheme))
            return appTheme;

        throw new NotSupportedException($"{Settings.Default.AppTheme} is not supported!");
    }

    internal static void ApplyTheme(AppTheme theme)
    {
        if (theme == AppTheme.System)
            theme = IsWindowsDark() ? AppTheme.Dark : AppTheme.Light;

        ThemeActions[theme].Invoke();
    }

    static bool IsWindowsDark() =>
        Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Themes\Personalize")?
            .GetValue("AppsUseLightTheme") is 0;
}