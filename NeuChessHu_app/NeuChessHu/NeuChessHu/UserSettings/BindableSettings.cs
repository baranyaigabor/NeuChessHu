using ChessMechanics.Common;
using NeuChessHu.Properties;
using NeuChessHu.Resources.Types;
using NeuChessHu.Resources.Types.ThemeTypes;
using NeuChessHu.Services.SoundServices;
using NeuChessHu.UserSettings.SettingManagers;

namespace NeuChessHu.UserSettings;

public partial class BindableSettings(Settings settings) : ObservableBase
{
    public BoardTheme BoardTheme
    {
        get => BoardThemeManager.Decode();
        set
        {
            if (value == default || value.Value is null) return;

            string boardTheme = value.Value;

            if (settings.BoardTheme != boardTheme)
            {
                settings.BoardTheme = boardTheme;
                settings.Save();

                BoardThemeManager.ApplyTheme(value);
                RaisePropertyChanged();
            }
        }
    }

    public PieceTheme PieceTheme
    {
        get => PieceThemeManager.Decode();
        set
        {
            if (value == default || value.Value is null) return;

            string pieceTheme = value.Value;

            if (settings.PieceTheme != pieceTheme)
            {
                settings.PieceTheme = pieceTheme;
                settings.Save();

                RaisePropertyChanged();
            }
        }
    }

    public Language Language
    {
        get => LanguageManager.Decode();
        set
        {
            if (value == default || value.Value is null) return;

            string language = value.Value;

            if (settings.Language != language)
            {
                settings.Language = language;
                settings.Save();

                LanguageManager.ApplyLanguage(value);
                RaisePropertyChanged();
            }
        }
    }


    public bool DisableSounds
    {
        get => settings.DisableSounds;
        set
        {
            if (settings.DisableSounds != value)
            {
                settings.DisableSounds = value;
                settings.Save();

                Sounds.IsMuted = value;
            }
        }
    }

    public bool AutoQueen
    {
        get => settings.AutoQueen;
        set
        {
            if (settings.AutoQueen != value)
            {
                settings.AutoQueen = value;
                settings.Save();

                RaisePropertyChanged();
            }
        }
    }

    public bool DarkMode
    {
        get => AppThemeManager.Decode();
        set
        {
            if (settings.DarkMode != value)
            {
                settings.DarkMode = value;
                settings.Save();

                AppThemeManager.ApplyTheme(value);
                RaisePropertyChanged();
            }
        }
    }

    public string LastMatchDuration
    {
        get => settings.LastMatchDuration;
        set
        {
            if (settings.LastMatchDuration != value)
            {
                settings.LastMatchDuration = value;
                settings.Save();

                RaisePropertyChanged();
            }
        }
    }

    public bool LastMatchStockfish
    {
        get => settings.LastMatchStockfish;
        set
        {
            if (settings.LastMatchStockfish != value)
            {
                settings.LastMatchStockfish = value;
                settings.Save();

                RaisePropertyChanged();
            }
        }
    }
}