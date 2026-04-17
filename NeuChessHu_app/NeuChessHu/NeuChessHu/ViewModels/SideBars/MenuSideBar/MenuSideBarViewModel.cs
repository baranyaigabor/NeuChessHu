using ChessMechanics.Common;
using NeuChessHu.CommandUtils;
using NeuChessHu.Resources;
using NeuChessHu.Resources.Types;
using NeuChessHu.UserSettings;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace NeuChessHu.ViewModels.SideBars.MenuSideBar;

public class MenuSideBarViewModel : ObservableBase, IDisposable
{
    BindableSettings settings;
    string timeSetterButtonContent;
    ImageSource? handMovesPieceSource;
    ImageSource? moreIconSource;

    ScrollTo scrollDirection;
    Visibility playStockfishButtonVisibility = Visibility.Collapsed;

    public BindableSettings Settings
    {   
        get => settings;
        private set { settings = value; RaisePropertyChanged(); }
    }

    public string TimeSetterButtonContent
    {
        get => timeSetterButtonContent;
        set { timeSetterButtonContent = value; RaisePropertyChanged(); }
    }

    public ImageSource? HandMovesPieceSource
    {
        get => handMovesPieceSource;
        private set { handMovesPieceSource = value; RaisePropertyChanged(); }
    }

    public ImageSource? MoreIconSource
    {
        get => moreIconSource;
        private set { moreIconSource = value; RaisePropertyChanged(); }
    }

    public ScrollTo ScrollDirection
    {
        get => scrollDirection;
        private set { scrollDirection = value; RaisePropertyChanged(); }
    }

    public Visibility PlayStockfishButtonVisibility
    {
        get => playStockfishButtonVisibility;
        private set { playStockfishButtonVisibility = value; RaisePropertyChanged(); }
    }

    public Action? OnOpenTimeSetter { get; set; }
    public Action? OnOpenCustomGamePanel { get; set; }
    public Action? OnStartMatch { get; set; }

    public ICommand OpenTimeSetterCommand { get; }
    public ICommand StartMatchCommand { get; }
    public ICommand MoreIconToggleCommand { get; }
    public ICommand CustomGameCommand { get; }

    public MenuSideBarViewModel(BindableSettings settings)
    {
        this.settings = settings;

        Settings.PropertyChanged += OnSettingsChanged;

        TimeSetterButtonContentSetter();
        MoreIconImageLoader();

        OpenTimeSetterCommand = new CommandExecuter<object?>(_ => OnOpenTimeSetter?.Invoke());
        StartMatchCommand = new CommandExecuter<object?>(_ => OnStartMatch?.Invoke());
        MoreIconToggleCommand = new CommandExecuter<object?>(_ => MoreIconToggle());
        CustomGameCommand = new CommandExecuter<object?>(_ => OnOpenCustomGamePanel?.Invoke());
    }

    void OnSettingsChanged(object? s, PropertyChangedEventArgs e)
    {
        if (e.PropertyName is nameof(Settings.AppTheme))
            MoreIconSource = AppResources.Get<BitmapImage>(
                playStockfishButtonVisibility is Visibility.Visible
                    ? "MoreDownImage"
                    : "MoreUpImage"
            );

        else if (e.PropertyName is nameof(Settings.Language) || e.PropertyName is nameof(Settings.LastMatchDuration))
            TimeSetterButtonContentSetter();
    }

    void TimeSetterButtonContentSetter()
    {
        string minuteText = Settings.LastMatchDuration.Contains('|')
            ? string.Empty
            : AppResources.Get<string>("MinuteText");

        TimeSetterButtonContent = $"{Settings.LastMatchDuration}{minuteText}";
    }

    void MoreIconImageLoader() =>
        MoreIconSource = AppResources.Get<BitmapImage>("MoreUpImage");

    void MoreIconToggle()
    {
        if (playStockfishButtonVisibility is Visibility.Collapsed)
        {
            MoreIconSource = AppResources.Get<BitmapImage>("MoreDownImage");

            PlayStockfishButtonVisibility = Visibility.Visible;

            ScrollDirection = ScrollTo.Bottom;
        }
        else
        {
            MoreIconSource = AppResources.Get<BitmapImage>("MoreUpImage");

            PlayStockfishButtonVisibility = Visibility.Collapsed;

            ScrollDirection = ScrollTo.Top;
        }
    }

    public void Dispose() =>
        Settings.PropertyChanged -= OnSettingsChanged;
}