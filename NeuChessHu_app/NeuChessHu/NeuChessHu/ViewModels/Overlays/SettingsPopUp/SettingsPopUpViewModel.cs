using ChessMechanics.Common;
using NeuChessHu.CommandUtils;
using NeuChessHu.Resources;
using NeuChessHu.Resources.Components.ViewElements.Settings.Options;
using NeuChessHu.Resources.Types;
using NeuChessHu.Resources.Types.ThemeTypes;
using NeuChessHu.UserSettings;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Reflection;
using System.Windows.Input;
namespace NeuChessHu.ViewModels.Overlays.SettingsPopUp;

public class SettingsPopUpViewModel : ObservableBase, IDisposable
{
    BindableSettings settings;

    readonly ObservableCollection<SettingOption<BoardTheme>> boardThemeOptions = [];
    readonly ObservableCollection<SettingOption<PieceTheme>> pieceThemeOptions = [];
    readonly ObservableCollection<SettingOption<Language>> languageOptions = [];

    public BindableSettings Settings
    {
        get => settings;
        private set { settings = value; RaisePropertyChanged(); }
    }

    public ObservableCollection<SettingOption<BoardTheme>> BoardThemeOptions => boardThemeOptions;
    public ObservableCollection<SettingOption<PieceTheme>> PieceThemeOptions => pieceThemeOptions;
    public ObservableCollection<SettingOption<Language>> LanguageOptions => languageOptions;

    public SettingOption<BoardTheme>? SelectedBoardTheme
    {
        get => boardThemeOptions.FirstOrDefault(x => x.Value == Settings.BoardTheme);
        set
        {
            if (value is not null)
                Settings.BoardTheme = value.Value;
        }
    }

    public SettingOption<PieceTheme>? SelectedPieceTheme
    {
        get => pieceThemeOptions.FirstOrDefault(x => x.Value == Settings.PieceTheme);
        set
        {
            if (value is not null)
                Settings.PieceTheme = value.Value;
        }
    }

    public SettingOption<Language>? SelectedLanguage
    {
        get => languageOptions.FirstOrDefault(x => x.Value == Settings.Language);
        set
        {
            if (value is not null)
                Settings.Language = value.Value;
        }
    }

    public Action? OnCloseOverlay { get; internal set; }

    public ICommand GoBackCommand { get; }

    public SettingsPopUpViewModel(BindableSettings settings)
    {
        this.settings = settings;

        LoadSettingOptions();

        Settings.PropertyChanged += OnSettingsChanged;

        GoBackCommand = new CommandExecuter<object?>(_ => OnCloseOverlay?.Invoke());
    }

    void OnSettingsChanged(object? s, PropertyChangedEventArgs e)
    {
        if (e.PropertyName is nameof(BindableSettings.Language))
            LoadSettingOptions();
    }

    void LoadSettingOptions()
    {
        UpdateOrBuild(boardThemeOptions, typeof(BoardTheme), x => (BoardTheme)x.GetValue(null)!,
            x => x == BoardTheme.PastelGreen ? 0 : 1, x => GetResourceValue(x.Value, "BoardTheme"));

        UpdateOrBuild(pieceThemeOptions, typeof(PieceTheme), x => (PieceTheme)x.GetValue(null)!,
            x => x == PieceTheme.Default ? 0 : 1, GetPieceThemeDirectoryName);

        UpdateOrBuild(languageOptions, typeof(Language), x => (Language)x.GetValue(null)!,
            x => x == Language.System ? 0 : 1, x => GetResourceValue(x.Value, "Language"));
    }

    static void UpdateOrBuild<T>(ObservableCollection<SettingOption<T>> settingOptions, Type type,
        Func<PropertyInfo, T> selector, Func<T, int> orderBy, Func<T, string> getLabel)
    {
        List<T> values = type.GetProperties(BindingFlags.Public | BindingFlags.Static)
            .Where(x => x.PropertyType == typeof(T))
            .Select(selector)
            .OrderBy(orderBy)
            .ToList();

        if (settingOptions.Count is 0)
            foreach (T item in values)
                settingOptions.Add(new SettingOption<T>(item, getLabel(item)));

        else foreach (var item in settingOptions)
            item.Label = getLabel(item.Value);
    }

    static string GetResourceValue(string value, string resourceType) =>
        AppResources.Get<string>($"{value.Replace(" ", "")}{resourceType}");

    static string GetPieceThemeDirectoryName(PieceTheme pieceTheme) =>
        Directory.GetDirectories("Resources/Themes/PieceThemes").Select(Path.GetFileName)
            .FirstOrDefault(x => string.Equals(x, pieceTheme.Value, StringComparison.OrdinalIgnoreCase))
            ?? pieceTheme.Value;

    public void Dispose() =>
        settings.PropertyChanged -= OnSettingsChanged;
}