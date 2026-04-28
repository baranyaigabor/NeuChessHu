using ChessMechanics.Common;
using NeuChessHu.ViewModels.Overlays.MatchOverlays.MatchWindows;

namespace NeuChessHu.Collections.Containers;

public class MatchWindowsContainer(MatchEndWindowViewModel matchEndWindow, PromotionWindowViewModel promotionWindow) : ObservableBase
{
    MatchEndWindowViewModel matchEndWindow = matchEndWindow;
    PromotionWindowViewModel promotionWindow = promotionWindow;

    public MatchEndWindowViewModel MatchEndWindow 
    { 
        get => matchEndWindow; 
        private set { matchEndWindow = value; RaisePropertyChanged(); }
    }
    public PromotionWindowViewModel PromotionWindow 
    { 
        get => promotionWindow; 
        private set { promotionWindow = value; RaisePropertyChanged(); }
    }
}