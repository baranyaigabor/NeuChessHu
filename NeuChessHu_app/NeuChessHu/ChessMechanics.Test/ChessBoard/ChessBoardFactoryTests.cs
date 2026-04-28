using ChessMechanics.ChessBoard;
using ChessMechanics.ChessBoard.ChessPieces;
using ChessMechanics.ChessBoard.Definitions;
using NUnit.Framework;

namespace ChessMechanics.Test.ChessBoard;

[TestFixture]
public class ChessBoardFactoryTests
{
    [Test]
    public void BoardFillerWhenPlayingWhiteCreatesStandardWhitePerspectiveBoard()
    {
        ChessPiece[,]? board = ChessBoardFactory.BoardFiller(Side.White);

        Assert.Multiple(() =>
        {
            Assert.That(board.GetLength(0), Is.EqualTo(8));
            Assert.That(board.GetLength(1), Is.EqualTo(8));
            Assert.That(board[7, 0].Name, Is.EqualTo(Piece.Rook));
            Assert.That(board[7, 4].Name, Is.EqualTo(Piece.King));
            Assert.That(board[6, 3].Name, Is.EqualTo(Piece.Pawn));
            Assert.That(board[0, 3].Name, Is.EqualTo(Piece.Queen));
            Assert.That(board[1, 6].Color, Is.EqualTo(Side.Black));
            Assert.That(board[3, 3].Name, Is.EqualTo(Piece.None));
            Assert.That(board[3, 3].Color, Is.EqualTo(Side.None));
        });
    }

    [Test]
    public void BoardFillerWhenPlayingBlackFlipsBackRankAndPawnRows()
    {
        ChessPiece[,]? board = ChessBoardFactory.BoardFiller(Side.Black);

        Assert.Multiple(() =>
        {
            Assert.That(board[0, 0].Name, Is.EqualTo(Piece.Rook));
            Assert.That(board[0, 3].Name, Is.EqualTo(Piece.King));
            Assert.That(board[1, 4].Name, Is.EqualTo(Piece.Pawn));
            Assert.That(board[7, 4].Name, Is.EqualTo(Piece.Queen));
            Assert.That(board[6, 2].Color, Is.EqualTo(Side.Black));
            Assert.That(board[4, 5].Name, Is.EqualTo(Piece.None));
        });
    }
}
