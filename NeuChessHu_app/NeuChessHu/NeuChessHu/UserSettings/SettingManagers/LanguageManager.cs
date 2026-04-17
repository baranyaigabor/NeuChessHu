using NeuChessHu.Properties;
using NeuChessHu.Resources.Images.Register.Images.Dynamics.Localized;
using NeuChessHu.Resources.Languages;
using NeuChessHu.Resources.Types;
using System.Globalization;

namespace NeuChessHu.UserSettings.SettingManagers;

internal static class LanguageManager
{
    static readonly Dictionary<Language, Action> LanguageActions = new()
    {
        {
            Language.Hungarian,
            SetHungarian
        },
        {
            Language.English,
            SetEnglish
        }
    };

    static void SetHungarian()
    {
        HungarianDictionary.SetLanguage();
        LocalizedImages.SetLanguage(Language.Hungarian);
    }

    static void SetEnglish()
    {
        EnglishDictionary.SetLanguage();
        LocalizedImages.SetLanguage(Language.English);
    }

    internal static Language Decode()
    {
        if (Language.AllLanguages.TryGetValue(Settings.Default.Language, out Language language))
            return language;

        throw new NotSupportedException($"{Settings.Default.Language} is not supported!");
    }

    internal static void ApplyLanguage(Language language)
    {
        if (language == Language.System)
            language = IsWindowsHungarian() ? Language.Hungarian : Language.English;

        LanguageActions[language].Invoke();
    }

    static bool IsWindowsHungarian() =>
        CultureInfo.InstalledUICulture.TwoLetterISOLanguageName.Equals("hu", StringComparison.OrdinalIgnoreCase);
}
