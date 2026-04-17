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
}