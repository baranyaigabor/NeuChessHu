using ChessMechanics.Common;
using NeuChessHu.ViewModels.Overlays.MenuOverlays.MenuPopUps;
using NeuChessHu.ViewModels.Overlays.SettingsPopUp;

namespace NeuChessHu.Collections.Containers;

public class MenuPopUpsContainer(LoginPopUpViewModel loginPopUp, MenuPopUpViewModel menuPopUp,
    SettingsPopUpViewModel settingsPopUp) : ObservableBase
{
    LoginPopUpViewModel loginPopUp = loginPopUp;
    MenuPopUpViewModel menuPopUp = menuPopUp;
    SettingsPopUpViewModel settingsPopUp = settingsPopUp;

    public LoginPopUpViewModel LoginPopUp
    {
        get => loginPopUp;
        private set { loginPopUp = value; RaisePropertyChanged(); }
    }
    public MenuPopUpViewModel MenuPopUp
    {
        get => menuPopUp;
        private set { menuPopUp = value; RaisePropertyChanged(); }
    }
    public SettingsPopUpViewModel SettingsPopUp
    {
        get => settingsPopUp;
        private set { settingsPopUp = value; RaisePropertyChanged(); }
    }
}