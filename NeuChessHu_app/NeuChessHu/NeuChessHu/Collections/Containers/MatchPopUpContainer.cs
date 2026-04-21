using ChessMechanics.Common;
using NeuChessHu.ViewModels.Overlays.MatchOverlays.MatchPopUps;
using NeuChessHu.ViewModels.Overlays.SettingsPopUp;

namespace NeuChessHu.Collections.Containers;

public class MatchPopUpsContainer(SettingsPopUpViewModel settingsPopUp, OptionsPopUpViewModel optionsPopUp) : ObservableBase
{
    SettingsPopUpViewModel settingsPopUp = settingsPopUp;
    OptionsPopUpViewModel optionsPopUp = optionsPopUp;

    public SettingsPopUpViewModel SettingsPopUp
    {
        get => settingsPopUp;
        set { settingsPopUp = value; RaisePropertyChanged(); }
    }
    public OptionsPopUpViewModel OptionsPopUp
    {
        get => optionsPopUp;
        set { optionsPopUp = value; RaisePropertyChanged(); }
    }
}