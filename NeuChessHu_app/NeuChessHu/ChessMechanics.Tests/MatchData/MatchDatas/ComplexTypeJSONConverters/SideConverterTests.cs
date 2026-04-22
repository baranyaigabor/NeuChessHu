using ChessMechanics.ChessBoard.Definitions;
using ChessMechanics.MatchData.MatchDatas.ComplexTypeJSONConverters;
using Newtonsoft.Json;
using NUnit.Framework;

namespace ChessMechanics.Tests.MatchData.MatchDatas.ComplexTypeJSONConverters;

[TestFixture]
public class SideConverterTests
{
    static readonly JsonSerializerSettings Settings = new()
    {
        Converters = { new SideConverter() }
    };

    [Test]
    public void ReadJsonWithEmptyStringReturnsNull()
    {
        Side? side = JsonConvert.DeserializeObject<Side?>("\"\"", Settings);

        Assert.That(side, Is.Null);
    }

    [Test]
    public void WriteJsonWithSideWritesSideName()
    {
        string json = JsonConvert.SerializeObject(Side.Black, Settings);

        Assert.That(json, Is.EqualTo("\"Black\""));
    }
}