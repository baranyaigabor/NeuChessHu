using ChessMechanics.ChessBoard.ChessPieces;
using ChessMechanics.ChessBoard.Definitions;
using Newtonsoft.Json;

namespace ChessMechanics.MatchData.MatchDatas.ComplexTypeJSONConverters;

public class ChessPieceMatrixConverter : JsonConverter<ChessPiece[,]>
{
    public override ChessPiece[,] ReadJson(JsonReader reader, Type objectType,
        ChessPiece[,]? existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        string?[][] jagged = serializer.Deserialize<string?[][]>(reader)!;

        int rows = jagged.Length;
        int cols = jagged[0].Length;
        ChessPiece[,] matrix = new ChessPiece[rows, cols];

        for (int r = 0; r < rows; r++)
            for (int c = 0; c < cols; c++)
            {
                if (string.IsNullOrEmpty(jagged[r][c]))
                {
                    matrix[r, c] = ChessPiece.Create(Piece.None, Side.None);
                    continue;
                }

                using StringReader stringReader = new($"\"{jagged[r][c]}\"");
                using JsonTextReader jsonReader = new(stringReader);
                jsonReader.Read();
                matrix[r, c] = new ChessPieceConverter().ReadJson(jsonReader, 
                    typeof(ChessPiece), null, false, serializer)!;
            }

        return matrix;
    }

    public override void WriteJson(JsonWriter writer, ChessPiece[,]? value, JsonSerializer serializer)
    {
        writer.WriteStartArray();

        for (int r = 0; r < 8; r++)
        {
            writer.WriteStartArray();

            for (int c = 0; c < 8; c++)
                serializer.Serialize(writer, value?[r, c]);

            writer.WriteEndArray();
        }

        writer.WriteEndArray();
    }
}