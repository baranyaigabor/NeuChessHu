using ChessMechanics.MatchData.MatchDatas.Models.DomainModels;

namespace ChessMechanics.MatchData.MatchDatas.DataTransferObjects;

public class ChatMessagesDTO
{
    public string? Status { get; set; }
    public ChatMessageRow? NewMessage { get; set; }
}