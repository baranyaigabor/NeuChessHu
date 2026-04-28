using ChessMechanics.ChessBoard.ChessPieces;
using ChessMechanics.ChessBoard.Definitions;
using ChessMechanics.MatchData.MatchDatas.ComplexTypeJSONConverters;
using Newtonsoft.Json;
using NUnit.Framework;

namespace ChessMechanics.Test.MatchData.MatchDatas.ComplexTypeJSONConverters;

[TestFixture]
public class ChessPieceConverterTests
{
    static readonly JsonSerializerSettings settings = new()
    {
        Converters = { new ChessPieceConverter() }
    };

    [Test]
    public void ReadJsonWithPieceAndSideNameReturnsChessPiece()
    {
        ChessPiece? piece = JsonConvert.DeserializeObject<ChessPiece>("\"KnightBlack\"", settings);

        Assert.That(piece, Is.EqualTo(ChessPiece.Create(Piece.Knight, Side.Black)));
    }

    [Test]
    public void ReadJsonWithInvalidValueThrowsJsonSerializationException()
    {
        Assert.Throws<JsonSerializationException>(() =>
            JsonConvert.DeserializeObject<ChessPiece>("\"DragonWhite\"", settings));
    }

    [Test]
    public void WriteJsonWithPieceWritesPieceAndSideName()
    {
        string json = JsonConvert.SerializeObject(ChessPiece.Create(Piece.Pawn, Side.White), settings);

        Assert.That(json, Is.EqualTo("\"PawnWhite\""));
    }
}
