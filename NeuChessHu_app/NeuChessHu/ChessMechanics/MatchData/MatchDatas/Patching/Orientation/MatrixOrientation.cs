using ChessMechanics.ChessBoard.ChessPieces;
using ChessMechanics.ChessBoard.Definitions;

namespace ChessMechanics.MatchData.MatchDatas.Patching.Orientation;

internal static class MatrixOrientation
{
    internal static ChessPiece[,] ClientMatrix(Side playingSide, ChessPiece[,] serverMatrix) =>
        playingSide is Side.White ? serverMatrix : MatrixFlipper(serverMatrix);

    static ChessPiece[,] MatrixFlipper(ChessPiece[,] serverMatrix)
    {
        ChessPiece[,] flipped = new ChessPiece[8, 8];

        for (int r = 0; r < 8; r++)
            for (int c = 0; c < 8; c++)
                flipped[7 - r, 7 - c] = serverMatrix[r, c];

        return flipped;
    }
}