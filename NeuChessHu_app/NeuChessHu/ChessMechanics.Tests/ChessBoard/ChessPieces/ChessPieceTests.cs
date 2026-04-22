using ChessMechanics.ChessBoard.ChessPieces;
using ChessMechanics.ChessBoard.Definitions;
using NUnit.Framework;

namespace ChessMechanics.Tests.ChessBoard.ChessPieces;

[TestFixture]
public class ChessPieceTests
{
    [Test]
    public void CreateWithRealPieceReturnsPieceAndSide()
    {
        ChessPiece piece = ChessPiece.Create(Piece.Queen, Side.White);

        Assert.Multiple(() =>
        {
            Assert.That(piece.Name, Is.EqualTo(Piece.Queen));
            Assert.That(piece.Color, Is.EqualTo(Side.White));
        });
    }

    [Test]
    public void CreateWithNonePieceReturnsEmptySquare()
    {
        ChessPiece piece = ChessPiece.Create(Piece.None, Side.None);

        Assert.That(piece, Is.EqualTo(new ChessPiece(Piece.None, Side.None)));
    }
}