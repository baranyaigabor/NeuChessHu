using ChessMechanics.ChessBoard.ChessPieces;
using ChessMechanics.ChessBoard.Definitions;
using System.Diagnostics;

namespace ChessMechanics.ChessBoard;

public static class ChessBoardFactory
{
    static readonly Piece[] backRank =
    [
        Piece.Rook,
        Piece.Knight,
        Piece.Bishop,
        Piece.Queen,
        Piece.King,
        Piece.Bishop,
        Piece.Knight,
        Piece.Rook
    ];

    public static ChessPiece[,] BoardFiller(Side playingSide)
    {
        ChessPiece[,] board = new ChessPiece[8, 8];

        int whiteBackRank = playingSide == Side.White ? 7 : 0;
        int whitePawns = playingSide == Side.White ? 6 : 1;

        int blackBackRank = playingSide == Side.White ? 0 : 7;
        int blackPawns = playingSide == Side.White ? 1 : 6;

        Piece[] backRanks = playingSide is Side.White ? backRank : backRank.Reverse().ToArray();

        for (int c = 0; c < 8; c++)
            board[whiteBackRank, c] = ChessPiece.Create(backRanks[c], Side.White);

        for (int c = 0; c < 8; c++)
            board[whitePawns, c] = ChessPiece.Create(Piece.Pawn, Side.White);

        for (int c = 0; c < 8; c++)
            board[blackBackRank, c] = ChessPiece.Create(backRanks[c], Side.Black);

        for (int c = 0; c < 8; c++)
            board[blackPawns, c] = ChessPiece.Create(Piece.Pawn, Side.Black);

        for (int r = 0; r < 8; r++)
            for (int c = 0; c < 8; c++)
                if (board[r, c] is null)
                    board[r, c] = ChessPiece.Create(Piece.None, Side.None);

        return board;
    }
}