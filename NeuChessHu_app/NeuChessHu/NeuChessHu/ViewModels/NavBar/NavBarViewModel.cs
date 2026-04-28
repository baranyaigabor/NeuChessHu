using ChessMechanics.Authentication.Session;
using ChessMechanics.Common;
using NeuChessHu.CommandUtils;
using NeuChessHu.Converters;
using NeuChessHu.Resources;
using NeuChessHu.Resources.Types;
using NeuChessHu.UserSettings;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace NeuChessHu.ViewModels.NavBar;

public class NavBarViewModel : ObservableBase, IDisposable
{
    readonly SessionDatas session;

    Visibility flagVisibility;
    Visibility profilePictureVisibility;

    ImageSource profilePicture;

    public ImageSource ProfilePicture
    {
        get => profilePicture;
        set
        {
            profilePicture = value;
            RaisePropertyChanged();
        }
    }

    public Visibility FlagVisibility
    {
        get => flagVisibility;
        set { flagVisibility = value; RaisePropertyChanged(); }
    }
    public Visibility ProfilePictureVisibility
    {
        get => profilePictureVisibility;
        set { profilePictureVisibility = value; RaisePropertyChanged(); }
    }

    public Action? OnShowMenuPopUpCommand { get; set; }

    public ICommand SwitchLanguageCommand { get; }
    public ICommand ShowMenuPopUpCommand { get; }

    public NavBarViewModel(BindableSettings settings, SessionDatas session)
    {
        this.session = session;

        session.PropertyChanged += OnSessionChanged;

        ProfilePictureLoader();

        SwitchLanguageCommand = new CommandExecuter<object?>( _ => SwitchLanguage(settings));
        ShowMenuPopUpCommand = new CommandExecuter<object?>( _ => OnShowMenuPopUpCommand?.Invoke());
    }

    void OnSessionChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName is nameof(SessionDatas.User))
            ProfilePictureLoader();
    }
    static void SwitchLanguage(BindableSettings settings) =>
        settings.Language = settings.Language == Language.English
                ? Language.Hungarian
                : Language.English;

    void ProfilePictureLoader() =>
        ProfilePicture = session.User is null || session.User!.ProfilePicture is "Unknown"
            ? AppResources.Get<ImageSource>("DefaultProfilePictureImage")
            : ImageConverters.LoadProfilePicture(session.User.ProfilePicture!)!;

    public void Dispose() =>
        session.PropertyChanged -= OnSessionChanged;
}