using ChessMechanics.ChessBoard.Definitions;
using ChessMechanics.MatchData.MatchDatas.ComplexTypeJSONConverters;
using Newtonsoft.Json;
using NUnit.Framework;

namespace ChessMechanics.Test.MatchData.MatchDatas.ComplexTypeJSONConverters;

[TestFixture]
public class PieceConverterTests
{
    static readonly JsonSerializerSettings Settings = new()
    {
        Converters = { new PieceConverter() }
    };

    [Test]
    public void ReadJsonWithEmptyStringReturnsNone()
    {
        Piece piece = JsonConvert.DeserializeObject<Piece>("\"\"", Settings);

        Assert.That(piece, Is.EqualTo(Piece.None));
    }

    [Test]
    public void WriteJsonWithPieceWritesPieceName()
    {
        string json = JsonConvert.SerializeObject(Piece.Bishop, Settings);

        Assert.That(json, Is.EqualTo("\"Bishop\""));
    }
}