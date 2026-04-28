using ChessMechanics.ChessBoard.Definitions;
using Newtonsoft.Json;

namespace ChessMechanics.MatchData.MatchDatas.ComplexTypeJSONConverters;

public class SideConverter : JsonConverter<Side?>
{
    public override Side? ReadJson(JsonReader reader, Type objectType,
        Side? existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        string? value = reader.Value?.ToString();
        return string.IsNullOrEmpty(value) ? null : Enum.Parse<Side>(value);
    }

    public override void WriteJson(JsonWriter writer, Side? value, JsonSerializer serializer)
        => writer.WriteValue(value?.ToString());
}