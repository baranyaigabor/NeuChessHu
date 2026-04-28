using ChessMechanics.APIs;
using ChessMechanics.Authentication;
using ChessMechanics.Authentication.Session;
using ChessMechanics.MatchData.Clock;
using ChessMechanics.MatchData.MatchDatas;
using ChessMechanics.MatchData.MatchDatas.DataTransferObjects;
using ChessMechanics.MatchData.MatchDatas.Models;
using ChessMechanics.WebSockets.ChessEngine;
using ChessMechanics.WebSockets.ChessEngine.Requests;
using ChessMechanics.WebSockets.Pusher;
using ChessMechanics.WebSocketss.Pusher.Config;
using Microsoft.Extensions.DependencyInjection;
using NeuChessHu.Collections.Containers;
using NeuChessHu.Collections.Contexts;
using NeuChessHu.Controllers;
using NeuChessHu.Services.MatchServices;
using NeuChessHu.ViewModels.Board.MatchBoard;
using NeuChessHu.ViewModels.Board.MatchBoard.BoardInteractions;
using NeuChessHu.ViewModels.Overlays.MatchOverlays.MatchPopUps;
using NeuChessHu.ViewModels.Overlays.MatchOverlays.MatchWindows;
using NeuChessHu.ViewModels.SideBars.MatchSideBar;

namespace NeuChessHu.Bootstrap.Dependencies.AppServiceRegistrations;

internal static class MatchServices
{
    internal static IServiceCollection AddMatch(this IServiceCollection services) =>
        services.AddSingleton<LookingForMatchService>()
                .AddScoped<MatchContext>()
                .AddScoped<MatchSideBarViewModel>()
                .AddScoped<MatchBoardViewModel>()
                .AddScoped<BoardInteractionHandler>()
                .AddMatchOverlays()
                .AddMatchDatas()
                .AddServerHandlingLayer();

    static IServiceCollection AddMatchOverlays(this IServiceCollection services) =>
        services.AddScoped<MatchPopUpsContainer>()
                .AddScoped<OptionsPopUpViewModel>()

                .AddScoped<MatchWindowsContainer>()
                .AddScoped<MatchEndWindowViewModel>()
                .AddScoped<PromotionWindowViewModel>();

    static IServiceCollection AddMatchDatas(this IServiceCollection services) =>
        services.AddScoped<MatchDataStore>()
                .AddScoped<MatchPoints>()
                .AddScoped<MatchState>()

                .AddScoped<MatchPointsDTO>()
                .AddScoped<MatchStateDTO>()
                .AddScoped<PlayerDatasDTO>()

                .AddScoped<ClockHandler>();

    static IServiceCollection AddServerHandlingLayer(this IServiceCollection services) =>
        services.AddSingleton<SessionDatas>()
                .AddSingleton<SessionManager>()
                .AddSingleton<APIHandlers>();

    internal static IServiceCollection AddWebSocketLayer(this IServiceCollection services) =>
        services.AddScoped<MatchController>()
                .AddSingleton(PusherConfig.Create())
                .AddSingleton<PusherClientService>()
                .AddSingleton<ChessEngineClientService>()
                .AddSingleton<EngineRequests>()
                .AddSingleton<ChessEngineTasks>();
}