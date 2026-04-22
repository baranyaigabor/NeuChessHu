using ChessMechanics.ChessBoard.ChessPieces;
using ChessMechanics.ChessBoard.Definitions;
using ChessMechanics.MatchData.MatchDatas.ComplexTypeJSONConverters;
using Newtonsoft.Json;
using NUnit.Framework;

namespace ChessMechanics.Test.MatchData.MatchDatas.ComplexTypeJSONConverters;

[TestFixture]
public class ChessPieceMatrixConverterTests
{
    static readonly JsonSerializerSettings Settings = new()
    {
        Converters =
        {
            new ChessPieceMatrixConverter(),
            new ChessPieceConverter()
        }
    };

    [Test]
    public void ReadJsonWithPieceNamesAndEmptyCellsReturnsMatrix()
    {
        const string json = """
            [
              ["RookWhite", ""],
              [null, "KingBlack"]
            ]
            """;

        ChessPiece[,]? matrix = JsonConvert.DeserializeObject<ChessPiece[,]>(json, Settings);

        Assert.Multiple(() =>
        {
            Assert.That(matrix![0, 0], Is.EqualTo(ChessPiece.Create(Piece.Rook, Side.White)));
            Assert.That(matrix[0, 1], Is.EqualTo(ChessPiece.Create(Piece.None, Side.None)));
            Assert.That(matrix[1, 0], Is.EqualTo(ChessPiece.Create(Piece.None, Side.None)));
            Assert.That(matrix[1, 1], Is.EqualTo(ChessPiece.Create(Piece.King, Side.Black)));
        });
    }

    [Test]
    public void WriteJsonWithEightByEightMatrixWritesNestedPieceArrays()
    {
        ChessPiece[,] matrix = new ChessPiece[8, 8];

        for (int row = 0; row < 8; row++)
            for (int column = 0; column < 8; column++)
                matrix[row, column] = ChessPiece.Create(Piece.None, Side.None);

        matrix[0, 0] = ChessPiece.Create(Piece.Queen, Side.White);

        string json = JsonConvert.SerializeObject(matrix, Settings);

        Assert.Multiple(() =>
        {
            Assert.That(json, Does.StartWith("[[\"QueenWhite\",\"NoneNone\""));
            Assert.That(json, Does.EndWith("]]"));
        });
    }
}
