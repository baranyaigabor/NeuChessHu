using ChessMechanics.ChessBoard.Definitions;
using ChessMechanics.MatchData.MatchDatas.Models.DomainModels;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Media;

namespace NeuChessHu.ViewModels.SideBars.MatchSideBar.Displays;

public record ChatMessageDisplay(string Message, Brush BubbleBackground, HorizontalAlignment Alignment)
{
    internal static void Add(ObservableCollection<ChatMessageDisplay> chatMessageDisplays,
        ChatMessageRow message, int userID, Side playerSide) =>
        chatMessageDisplays.Add(Create(message, userID, playerSide));

    static ChatMessageDisplay Create(ChatMessageRow message, int userID, Side playerSide)
    {
        bool isOwnMessage = message.UserID == userID;

        Brush bubbleBackground = new SolidColorBrush(isOwnMessage
            ? Color.FromRgb(0, 120, 212)
            : Color.FromRgb(60, 60, 60));

        HorizontalAlignment alignment = isOwnMessage
            ? HorizontalAlignment.Right
            : HorizontalAlignment.Left;

        return new ChatMessageDisplay(message.Message, bubbleBackground, alignment);
    }
}