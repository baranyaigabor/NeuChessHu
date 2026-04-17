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

    internal static bool DoesFileExist(string soundName)
    {
        List<string> soundFileNames = Directory.GetFiles(Path.Combine(
            AppDomain.CurrentDomain.BaseDirectory, "Resources", "Sounds"), "*.wav").Select(Path.GetFileNameWithoutExtension).ToList()!;

        return soundFileNames.Contains(soundName);
    }
}