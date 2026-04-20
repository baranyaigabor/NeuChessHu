using NeuChessHu.UserSettings;
using ChessMechanics.ChessBoard.Definitions;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Media;
using NeuChessHu.UserSettings.SettingManagers;

namespace NeuChessHu.ViewModels.SideBars.MatchSideBar.Displays;

public class CapturedPiecesDisplay
{
    public Piece Piece { get; set; }
    required public ImageSource PieceImage { get; set; }
    public Thickness Margin { get; set; }

    internal static void Add(ObservableCollection<CapturedPiecesDisplay> uiList, ObservableCollection<Piece> backendList,
        Side currentSide, BindableSettings settings)
    {
        uiList.Clear();

        foreach (Piece piece in backendList)
            uiList.Add(ImageFactory(piece, currentSide, settings, uiList));

        LeftMarginsSetter(uiList);
    }

    static CapturedPiecesDisplay ImageFactory(Piece piece, Side playerSide, BindableSettings settings, ObservableCollection<CapturedPiecesDisplay> capturedPieces) => new()
    {
        Piece = piece,
        PieceImage = PieceThemeManager.ImageLoader(piece, playerSide, settings),
        Margin = new Thickness()
    };

    static void LeftMarginsSetter(ObservableCollection<CapturedPiecesDisplay> capturedPieces)
    {
        for (int i = 0; i < capturedPieces.Count; i++)
            capturedPieces[i].Margin = i switch
            {
                0 => new Thickness(0),
                _ => (capturedPieces[i - 1].Piece == capturedPieces[i].Piece) 
                    ? new Thickness(-15, 0, 0, 0) 
                    : new Thickness(-8, 0, 0, 0),
            };
    }
}