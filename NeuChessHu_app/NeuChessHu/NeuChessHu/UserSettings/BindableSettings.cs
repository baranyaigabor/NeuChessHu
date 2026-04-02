using ChessMechanics.ChessBoard.Definitions;
using ChessMechanics.Common;
using NeuChessHu.Resources.Types;
using NeuChessHu.Resources.Types.ThemeTypes;
using NeuChessHu.Services.SoundServices;
using NeuChessHu.UserSettings.SettingManagers;
using Properties;
using System.Text.RegularExpressions;

namespace NeuChessHu.UserSettings;

public partial class BindableSettings(Settings settings) : ObservableBase
{
    public AppTheme AppTheme
    {
        get => AppThemeManager.Decode();
        set
        {
            if (value == default || value.Value is null) return;

            string appTheme = value.Value;

            if (settings.AppTheme != appTheme)
            {
                settings.AppTheme = appTheme;
                settings.Save();

                AppThemeManager.ApplyTheme(value);
                RaisePropertyChanged();
            }
        }
    }
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

    public bool FocusMode
    {
        get => settings.FocusMode;
        set
        {
            if (settings.FocusMode != value)
            {
                settings.FocusMode = value;
                settings.Save();

                RaisePropertyChanged();
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

    public bool ConfirmEachMove
    {
        get => settings.ConfirmEachMove;
        set
        {
            if (settings.ConfirmEachMove != value)
            {
                settings.ConfirmEachMove = value;
                settings.Save();

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

    public string LastCustomDuration
    {
        get => settings.LastCustomDuration;
        set
        {
            if (settings.LastCustomDuration != value && Regex.IsMatch(@"^\d{1,2}(\s\|\s\d{1,2})?$", value))
            {
                settings.LastCustomDuration = value;
                settings.Save();

                RaisePropertyChanged();
            }
        }
    }

    public Side LastCustomSide
    {
        get => Enum.TryParse(settings.LastCustomSide, out Side side) ? side : Side.None;
        set
        {
            if (Enum.TryParse(settings.LastCustomSide, out Side side) && side != value)
            {
                settings.LastCustomSide = value.ToString();
                settings.Save();
                RaisePropertyChanged();
            }
        }
    }
}