using Microsoft.Extensions.DependencyInjection;
using NeuChessHu.Collections.Containers;
using NeuChessHu.Collections.Contexts;
using NeuChessHu.ViewModels.Board.MenuBoard;
using NeuChessHu.ViewModels.NavBar;
using NeuChessHu.ViewModels.Overlays.MenuOverlays.MenuPopUps;
using NeuChessHu.ViewModels.Overlays.MenuOverlays.MenuWindows;
using NeuChessHu.ViewModels.SideBars.MenuSideBar;

namespace NeuChessHu.Bootstrap.Dependencies.AppServiceRegistrations;

internal static class MenuServices
{
    internal static IServiceCollection AddMenu(this IServiceCollection services) =>
        services.AddSingleton<NavBarViewModel>()
                .AddTransient<MenuContext>()
                .AddTransient<MenuSideBarViewModel>()
                .AddSingleton<MenuBoardViewModel>()

                .AddMenuOverlays();

    static IServiceCollection AddMenuOverlays(this IServiceCollection services) =>
        services.AddTransient<MenuPopUpsContainer>()
                .AddTransient<LoginPopUpViewModel>()
                .AddTransient<MenuPopUpViewModel>()

                .AddTransient<MenuWindowsContainer>()
                .AddTransient<TimeSetterWindowViewModel>()
                .AddTransient<LookingForMatchWindowViewModel>();
}