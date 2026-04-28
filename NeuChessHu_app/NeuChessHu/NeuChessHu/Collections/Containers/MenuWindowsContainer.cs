using ChessMechanics.Common;
using NeuChessHu.ViewModels.Overlays.MenuOverlays.MenuWindows;

namespace NeuChessHu.Collections.Containers;

public class MenuWindowsContainer(TimeSetterWindowViewModel timeSetterWindow,
    LookingForMatchWindowViewModel lookingForMatchWindow) : ObservableBase
{
    TimeSetterWindowViewModel timeSetterWindow = timeSetterWindow;
    LookingForMatchWindowViewModel lookingForMatchWindow = lookingForMatchWindow;

    public TimeSetterWindowViewModel TimeSetterWindow
    {
        get => timeSetterWindow;
        private set { timeSetterWindow = value; RaisePropertyChanged(); }
    }

    public LookingForMatchWindowViewModel LookingForMatchWindow
    {
        get => lookingForMatchWindow;
        private set { lookingForMatchWindow = value; RaisePropertyChanged(); }
    }
}