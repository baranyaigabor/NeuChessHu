using ChessMechanics.ChessBoard.Definitions;
using Newtonsoft.Json;

namespace ChessMechanics.MatchData.MatchDatas.ComplexTypeJSONConverters;

public class PieceConverter : JsonConverter<Piece>
{
    public override Piece ReadJson(JsonReader reader, Type objectType,
        Piece existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        string? value = reader.Value?.ToString();
        return string.IsNullOrEmpty(value) ? Piece.None : Enum.Parse<Piece>(value);
    }

    public override void WriteJson(JsonWriter writer, Piece value, JsonSerializer serializer)
        => writer.WriteValue(value.ToString());
}