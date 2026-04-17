using ChessMechanics.ChessBoard.ChessPieces;
using ChessMechanics.ChessBoard.Definitions;
using Newtonsoft.Json;

namespace ChessMechanics.MatchData.MatchDatas.ComplexTypeJSONConverters;

public class ChessPieceConverter : JsonConverter<ChessPiece>
{
    public override ChessPiece ReadJson(JsonReader reader, Type objectType,
        ChessPiece? existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        string? value = reader.Value?.ToString();

        if (string.IsNullOrEmpty(value) || value == "None")
            return ChessPiece.Create(Piece.None, Side.None);

        foreach (Side side in Enum.GetValues<Side>())
        {
            string sideName = side.ToString();

            if (value.EndsWith(sideName))
            {
                string pieceName = value.Substring(0, value.Length - sideName.Length);

                if (Enum.TryParse<Piece>(pieceName, out var piece))
                    return ChessPiece.Create(piece, side);
            }

        }

        throw new JsonSerializationException($"Cannot convert '{value}' to ChessPiece");
    }

    public override void WriteJson(JsonWriter writer, ChessPiece? value, JsonSerializer serializer) =>
        writer.WriteValue(value is null ? "None" : $"{value.Name}{value.Color}");
}
