using ChessMechanics.ChessBoard.Definitions;

namespace ChessMechanics.ChessBoard.ChessPieces;

public record ChessPiece(Piece Name, Side Color) 
{
    public static ChessPiece Create(Piece name, Side color) =>
        new(name, color);
}