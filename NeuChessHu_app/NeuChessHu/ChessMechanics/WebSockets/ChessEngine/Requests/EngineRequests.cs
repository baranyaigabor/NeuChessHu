using ChessMechanics.ChessBoard.ChessPieces;
using ChessMechanics.ChessBoard.Definitions;
using ChessMechanics.MatchData.MatchDatas.ComplexTypeJSONConverters;
using ChessMechanics.WebSockets.ChessEngine.Requests.Payloads;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Text.Json;

namespace ChessMechanics.WebSockets.ChessEngine.Requests;

public class EngineRequests(ChessEngineTasks tasks, ChessEngineClientService chessEngine)
{
    public async Task<JsonElement> SendRequestAsync(string type, object payload)
    {
        string requestID = Guid.NewGuid().ToString();
        tasks.PendingRequests[requestID] = new(TaskCreationOptions.RunContinuationsAsynchronously);

        JsonSerializerSettings settings = new()
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver(),
            Converters =
            {
                new ChessPieceConverter(),
                new ChessPieceMatrixConverter(),
                new TupleConverter()
            }
        };

        string json = JsonConvert.SerializeObject(new { type, requestID, payload }, settings);

        try
        {
            await chessEngine.SendAsync(json);
        }
        catch (Exception ex)
        {
            tasks.PendingRequests.TryRemove(requestID, out _);
            throw new Exception(ex.Message);
        }

        return await tasks.PendingRequests[requestID].Task;
    }

    public async Task<string> MovePieceRequest(string channel, Tuple<int, int> from,
      Tuple<int, int> to, Piece promotionChoice)
    {
        JsonElement response = await SendRequestAsync("request-move-piece",
            MovePiecePayload.CreateMovePiecePayload(channel, from, to, promotionChoice));

        string? soundName = response.Deserialize<string>()
            ?? throw new NullReferenceException();

        return DoesFileExist(soundName)
            ? soundName
            : throw new ArgumentException(soundName);
    }

    public async Task<bool> IsLegalMoveRequestAsync(string channel, Tuple<int, int> from, Tuple<int, int> to)
    {
        JsonElement response = await SendRequestAsync("request-is-legal-move", IsLegalMovePayload.CreateIsLegalMovePayload(channel, from, to));

        return response.Deserialize<bool>();
    }

    public async Task<bool[,]> LegalMovesWithSelectedPieceRequestAsync(string channel,
    Tuple<int, int> from, ChessPiece[,] pieceMatrix, Side playingSide)
    {
        JsonElement response = await SendRequestAsync("request-legal-moves",
            LegalMovesPayload.CreateLegalMovesPayload(channel, from, pieceMatrix, playingSide));

        bool[][] jaggedBoard = response.Deserialize<bool[][]>()!;

        return ConvertToMatrix(jaggedBoard);
    }

    public async Task<string> ChatMessageRequestAsync(string channel, string message, int userID)
    {
        JsonElement response = await SendRequestAsync("request-chat-message",
            ChatMessagePayload.Create(channel, userID, message));

        return response.GetProperty("Status").GetString()!;
    }

    public async Task<string> MatchPointRequestAsync(string channel, int userID, string matchPointReason) =>
        (await SendRequestAsync("request-match-point",
            MatchPointPayload.Create(channel, userID, matchPointReason))).Deserialize<string>()!;

    public async Task<string> DrawResponseRequestAsync(string channel, int userID, bool drawResponse) =>
        (await SendRequestAsync("request-draw-response",
            DrawResponsePayload.Create(channel, userID, drawResponse))).Deserialize<string>()!;


    internal static bool DoesFileExist(string soundName)
    {
        List<string> soundFileNames = Directory.GetFiles(Path.Combine(
            AppDomain.CurrentDomain.BaseDirectory, "Resources", "Sounds"), "*.wav").Select(Path.GetFileNameWithoutExtension).ToList()!;

        return soundFileNames.Contains(soundName);
    }

    static bool[,] ConvertToMatrix(bool[][] jaggedBoard)
    {
        bool[,] legalMovesWithSelectedPiece = new bool[8, 8];

        for (int r = 0; r < 8; r++)
            for (int c = 0; c < 8; c++)
                legalMovesWithSelectedPiece[r, c] = jaggedBoard[r][c];

        return legalMovesWithSelectedPiece;
    }
}