using ChessMechanics.MatchData.MatchDatas.ComplexTypeJSONConverters;
using Newtonsoft.Json;
using NUnit.Framework;

namespace ChessMechanics.Test.MatchData.MatchDatas.ComplexTypeJSONConverters;

[TestFixture]
public class TupleConverterTests
{
    static readonly JsonSerializerSettings Settings = new()
    {
        Converters = { new TupleConverter() }
    };

    [Test]
    public void ReadJsonWithTwoItemArrayReturnsTuple()
    {
        Tuple<int, int>? tuple = JsonConvert.DeserializeObject<Tuple<int, int>>("[2,5]", Settings);

        Assert.That(tuple, Is.EqualTo(Tuple.Create(2, 5)));
    }

    [Test]
    public void ReadJsonWithNullReturnsNull()
    {
        Tuple<int, int>? tuple = JsonConvert.DeserializeObject<Tuple<int, int>?>("null", Settings);

        Assert.That(tuple, Is.Null);
    }

    [Test]
    public void WriteJsonWithTupleWritesTwoItemArray()
    {
        string json = JsonConvert.SerializeObject(Tuple.Create(4, 6), Settings);

        Assert.That(json, Is.EqualTo("[4,6]"));
    }
}
