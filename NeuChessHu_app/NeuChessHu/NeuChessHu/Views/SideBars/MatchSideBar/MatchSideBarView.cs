using Microsoft.Xaml.Behaviors;
using NeuChessHu.CommandUtils;
using NeuChessHu.Resources;
using NeuChessHu.Resources.Behaviours;
using NeuChessHu.Resources.Components.ViewElements.MatchSideBar;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Shapes;

namespace NeuChessHu.Views.SideBars.MatchSideBar;

public partial class MatchSideBarView : UserControl
{
    public MatchSideBarView() =>
        Loaded += (s, e) =>
        {
            Content ??= MatchSideBarBuilder();
        };

    static Border MatchSideBarBuilder()
    {
        Style textStyle = AppResources.Get<Style>("TextStyle");

        Grid matchSideBar = new();

        int[] heights = { 60, 40, 1, 60, 40 };

        for (int i = 0; i < 5; i++)
        {
            if (i == 0)
                matchSideBar.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
            else if (i == 1)
                matchSideBar.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(70) });

            if (i == 2)
                matchSideBar.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Star) });
            else matchSideBar.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(heights[i]) });
        }

        Border opponentPictureAndName = MatchSideBarViewElements.PlayerInfoPanelsFactory();

        Border opponentTime = new()
        {
            BorderThickness = new Thickness(0.5),
            Height = 60,
            Width = 70,
            Child = new Label
            {
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                Style = textStyle,
                FontSize = 20
            }
        };

        Border opponentPieces = MatchSideBarViewElements.CapturedPiecesDisplayFactory("Opponent");

        Border chat = MatchSideBarViewElements.OptionAndChatButtonsFactory();

        (chat.Child as Grid)!.Children.Add(new Ellipse()
        {
            Height = 8,
            Width = 8,
            Margin = new Thickness(0, 0, -18, -16),
            Fill = Brushes.Red,
            Stroke = Brushes.DarkRed,
            StrokeThickness = 1.2,
        });

        Border scrollableContainer = new()
        {
            BorderThickness = new Thickness(0.5, 0, 0.5, 0.5),
            Padding = new Thickness(0, 15, 0, 15),
            Child = new Grid
            {
                Children =
                {
                    MatchSideBarViewElements.NotationRowFactory(),
                    MatchSideBarViewElements.ChatPanelDisplayFactory()
                }
            }
        };                            

        Border playerPictureAndName = MatchSideBarViewElements.PlayerInfoPanelsFactory();

        Border playerTime = new()
        {
            BorderThickness = new Thickness(0.5),
            Height = 60,
            Width = 70,
            Child = new Label
            {
                Style = textStyle,
                FontSize = 20,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
            }
        };

        Border playerPieces = MatchSideBarViewElements.CapturedPiecesDisplayFactory("Player");

        Border options = MatchSideBarViewElements.OptionAndChatButtonsFactory();

        if (((opponentPictureAndName.Child as DockPanel)!.Children[0] as Border)!.Child is Image opponentPicture)
            DockPanel.SetDock(opponentPicture, Dock.Left);

        if (((playerPictureAndName.Child as DockPanel)!.Children[0] as Border)!.Child is Image playerPicture)
            DockPanel.SetDock(playerPicture, Dock.Left);

        Grid.SetRow(opponentPictureAndName, 0);
        Grid.SetColumn(opponentPictureAndName, 0);

        Grid.SetRow(opponentTime, 0);
        Grid.SetColumn(opponentTime, 1);

        Grid.SetRow(opponentPieces, 1);
        Grid.SetColumn(opponentPieces, 0);

        Grid.SetRow(chat, 1);
        Grid.SetColumn(chat, 1);

        Grid.SetRow(scrollableContainer, 2);
        Grid.SetColumnSpan(scrollableContainer, 2);

        Grid.SetRow(playerPictureAndName, 3);
        Grid.SetColumn(playerPictureAndName, 0);

        Grid.SetRow(playerTime, 3);
        Grid.SetColumn(playerTime, 1);

        Grid.SetRow(playerPieces, 4);
        Grid.SetColumn(playerPieces, 0);

        Grid.SetRow(options, 4);
        Grid.SetColumn(options, 1);

        matchSideBar.SetResourceReference(BackgroundProperty, "SideBarBrush");
        opponentTime.SetResourceReference(BorderBrushProperty, "BorderBrush");
        scrollableContainer.SetResourceReference(BorderBrushProperty, "BorderBrush");
        playerTime.SetResourceReference(BorderBrushProperty, "BorderBrush");

        ((chat.Child as Grid)!.Children[0] as Image)!.SetResourceReference(Image.SourceProperty, "ChatImage");
        ((options.Child as Grid)!.Children[0] as Image)!.SetResourceReference(Image.SourceProperty, "OptionsImage");

        Binding playerProfilePictureBinding = new("PlayerProfilePicture") { Mode = BindingMode.OneWay };
        (((playerPictureAndName.Child as DockPanel)!.Children[0] as Border)!.Child as Image)!
            .SetBinding(Image.SourceProperty, playerProfilePictureBinding);

        Binding playerProfilePictureStyleBinding = 
            new("PlayerProfilePictureStyle") { Mode = BindingMode.OneWay };
        (((playerPictureAndName.Child as DockPanel)!.Children[0] as Border)!.Child as Image)!
            .SetBinding(StyleProperty, playerProfilePictureStyleBinding);

        Binding playerNicknameBinding = new("PlayerNickname") { Mode = BindingMode.OneWay };
        (((playerPictureAndName.Child as DockPanel)!.Children[1] as Border)!.Child as Label)!
            .SetBinding(ContentProperty, playerNicknameBinding);

        Binding opponentProfilePictureBinding = new("OpponentProfilePicture") { Mode = BindingMode.OneWay };
        (((opponentPictureAndName.Child as DockPanel)!.Children[0] as Border)!.Child as Image)!
            .SetBinding(Image.SourceProperty, opponentProfilePictureBinding);

        Binding opponentProfilePictureStyleBinding = 
            new("OpponentProfilePictureStyle") { Mode = BindingMode.OneWay };
        (((opponentPictureAndName.Child as DockPanel)!.Children[0] as Border)!.Child as Image)!
            .SetBinding(StyleProperty, opponentProfilePictureStyleBinding);

        Binding opponentNicknameBinding = new("OpponentNickname") { Mode = BindingMode.OneWay };
        (((opponentPictureAndName.Child as DockPanel)!.Children[1] as Border)!.Child as Label)!
            .SetBinding(ContentProperty, opponentNicknameBinding);

        Binding opponentClockBinding = new("OpponentClock") { Mode = BindingMode.OneWay };
        (opponentTime.Child as Label)!.SetBinding(ContentProperty, opponentClockBinding);

        Binding playerClockBinding = new("PlayerClock") { Mode = BindingMode.OneWay };
        (playerTime.Child as Label)!.SetBinding(ContentProperty, playerClockBinding);

        ScrollViewerBehaviours notationsBehaviour = new();
        Binding notationsBehaviourBinding = new("NotationsScrollDirection") { Mode = BindingMode.OneWay };
        BindingOperations.SetBinding(notationsBehaviour, ScrollViewerBehaviours.DirectionProperty, notationsBehaviourBinding);

        Interaction.GetBehaviors(((scrollableContainer.Child as Grid)!.Children[0] as DockPanel)!.Children[1]).Add(notationsBehaviour);

        ScrollViewerBehaviours chatBehaviour = new();
        Binding chatBehaviourBinding = new("ChatScrollDirection") { Mode = BindingMode.OneWay };
        BindingOperations.SetBinding(chatBehaviour, ScrollViewerBehaviours.DirectionProperty, chatBehaviourBinding);

        Interaction.GetBehaviors(((scrollableContainer.Child as Grid)!.Children[1] as DockPanel)!
            .Children[2]).Add(chatBehaviour);

        Binding chatImageVisibilityBinding = new("ChatImageVisibility") { Mode = BindingMode.OneWay };
        ((chat.Child as Grid)!.Children[0] as Image)!
            .SetBinding(VisibilityProperty, chatImageVisibilityBinding);

        Binding notationsVisibilityBinding = new("NotationsVisibility") { Mode = BindingMode.OneWay };
        (((scrollableContainer.Child as Grid)!.Children[0] as DockPanel)!.Children[1] as ScrollViewer)!
            .SetBinding(VisibilityProperty, notationsVisibilityBinding);

        Binding chatVisibilityBinding = new("ChatVisibility") { Mode = BindingMode.OneWay };
        ((scrollableContainer.Child as Grid)!.Children[1] as DockPanel)!.SetBinding(VisibilityProperty, chatVisibilityBinding);

        ((chat.Child as Grid)!.Children[1] as Ellipse)!
            .SetBinding(Ellipse.VisibilityProperty, "UnreadMessageNotificationVisibility");

        Binding chatButtonThicknessBinding = new("ChatButtonThickness") { Mode = BindingMode.OneWay };
        chat.SetBinding(Border.BorderThicknessProperty, chatButtonThicknessBinding);

        CommandAttachers.OnClickEvent(chat, "OpenCloseChatCommand");
        CommandAttachers.OnClickEvent(options, "OpenOptionsCommand");

        foreach (var element in new UIElement[] { opponentPictureAndName, opponentTime, opponentPieces, options, scrollableContainer, playerPictureAndName, playerTime, playerPieces, chat })
            matchSideBar.Children.Add(element);

        return new Border()
        {
            Height = 520,
            Width = 280,
            BorderBrush = AppResources.Get<SolidColorBrush>("BorderBrush"),
            BorderThickness = new Thickness(0.5, 0.5, 0.5, 0),
            Margin = new Thickness(10, 0, 10, 0),
            Child = matchSideBar
        };
    }
}