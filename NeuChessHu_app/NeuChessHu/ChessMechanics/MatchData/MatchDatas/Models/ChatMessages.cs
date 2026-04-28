using ChessMechanics.MatchData.MatchDatas.Models.DomainModels;
using System.Collections.ObjectModel;

namespace ChessMechanics.MatchData.MatchDatas.Models;

public class ChatMessages
{
    public string? Status { get; set; }

    public ObservableCollection<ChatMessageRow> ChatMessageList { get; internal set; } = [];

    internal static ChatMessages CreateChatMessages() => new();
}
