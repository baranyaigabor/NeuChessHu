using NeuChessHu.ViewModels.SideBars.MatchSideBar;
using NeuChessHu.ViewModels.Board.MatchBoard;
using NeuChessHu.Collections.Containers;

namespace NeuChessHu.Collections.Contexts;

public record MatchContext(MatchBoardViewModel Board, MatchSideBarViewModel MatchSideBar, 
    MatchPopUpsContainer MatchPopUpPanels, MatchWindowsContainer MatchWindows) { }