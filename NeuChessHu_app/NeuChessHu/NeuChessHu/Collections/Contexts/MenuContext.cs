using NeuChessHu.Collections.Containers;
using NeuChessHu.ViewModels.Board.MenuBoard;
using NeuChessHu.ViewModels.SideBars.MenuSideBar;

namespace NeuChessHu.Collections.Contexts;

public record MenuContext(MenuSideBarViewModel MenuSideBar, MenuBoardViewModel Board,
    MenuPopUpsContainer MenuPopUpPanels, MenuWindowsContainer MenuWindows);