using ChessMechanics.Common;
using NeuChessHu.CommandUtils;
using NeuChessHu.Resources;
using NeuChessHu.UserSettings;
using System.Text.RegularExpressions;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace NeuChessHu.ViewModels.Overlays.MenuOverlays.MenuWindows;

public class TimeSetterWindowViewModel : ObservableBase
{
    readonly BindableSettings settings;

    public ImageSource? BulletSource { get; private set; }
    public ImageSource? BlitzSource { get; private set; }
    public ImageSource? RapidSource { get; private set; }

    public Action? OnCloseOverlay { get; set; }

    public ICommand SelectTimeCommand { get; }
    public ICommand GoBackCommand { get; }

    public TimeSetterWindowViewModel(BindableSettings settings)
    {
        this.settings = settings;

        SelectTimeCommand = new CommandExecuter<string>(SelectTime!);
        GoBackCommand = new CommandExecuter<object?>(_ => OnCloseOverlay?.Invoke());

        BulletIconLoader();
        BlitzIconLoader();
        RapidIconLoader();
    }

    void SelectTime(string content)
    {
        settings.LastMatchDuration = Regex.Replace(content, @"[^0-9 |]", "");
        OnCloseOverlay?.Invoke();
    }

    void BulletIconLoader() =>
        BulletSource = AppResources.Get<BitmapImage>("BulletImage");
    void BlitzIconLoader() =>
        BlitzSource = AppResources.Get<BitmapImage>("BlitzImage");
    void RapidIconLoader() =>
        RapidSource = AppResources.Get<BitmapImage>("RapidImage");
}
