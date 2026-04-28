using Newtonsoft.Json;

namespace ChessMechanics.MatchData.MatchDatas.ComplexTypeJSONConverters;

public class TupleConverter : JsonConverter<Tuple<int, int>?>
{
    public override Tuple<int, int>? ReadJson(JsonReader reader, Type objectType,
        Tuple<int, int>? existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        if (reader.TokenType is JsonToken.Null)
            return null;

        int[] array = serializer.Deserialize<int[]>(reader)!;
        return Tuple.Create(array[0], array[1]);
    }

    public override void WriteJson(JsonWriter writer, Tuple<int, int>? value, JsonSerializer serializer)
    {
        writer.WriteStartArray();
        writer.WriteValue(value!.Item1);
        writer.WriteValue(value.Item2);
        writer.WriteEndArray();
    }
}