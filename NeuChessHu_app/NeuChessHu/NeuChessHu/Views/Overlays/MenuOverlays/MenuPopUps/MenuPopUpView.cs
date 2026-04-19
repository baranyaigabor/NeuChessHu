using NeuChessHu.CommandUtils;
using NeuChessHu.Resources;
using NeuChessHu.Resources.Components;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace NeuChessHu.Views.Overlays.MenuOverlays.MenuPopUps;

public partial class MenuPopUpView : UserControl
{
    public MenuPopUpView() =>
        Loaded += (s, e) =>
        {
            Content ??= MenuPanelBuilder();
        };

    static Border MenuPanelBuilder()
    {
        Style buttonStyle = AppResources.Get<Style>("ButtonStyleOverlayStyle");

        Style textStyle = AppResources.Get<Style>("TextStyle");

        Border menuBorder = new()
        {
            Style = AppResources.Get<Style>("PopUpsBorderStyle"),
            BorderThickness = new Thickness(2),
            CornerRadius = new CornerRadius(10),
            Margin = new Thickness(250),
        };

        Grid menuContainer = new() 
        { 
            Background = Brushes.Transparent 
        };

        StackPanel menu = new()
        {
            Height = 200,
            Width = 250,
            Orientation = Orientation.Vertical,
        };

        Border menuBackgrounfEffect = UIElements.PopUpBackgroundFactory(menu);

        Border profileSettingsButton = new()
        {
            Width = menu.Width - 20,
            Margin = new Thickness(3, 6.5, 3, 3),
            CornerRadius = new CornerRadius(10),
            Style = buttonStyle,
            Child = new Label
            {
                Style = textStyle,
                FontSize = 18,
                Background = Brushes.Transparent,
                BorderBrush = Brushes.Transparent
            }
        };

        Border preferencesButton = new()
        {
            Width = menu.Width - 20,
            Margin = new Thickness(3),
            CornerRadius = new CornerRadius(10),
            Style = buttonStyle,
            Child = new Label
            {
                Style = textStyle,
                FontSize = 18,
                Background = Brushes.Transparent,
                BorderBrush = Brushes.Transparent
            }
        };

        Border logoutButton = new()
        {
            Width = menu.Width - 20,
            Margin = new Thickness(3),
            CornerRadius = new CornerRadius(10),
            Style = buttonStyle,
            Child = new Label
            {
                Style = textStyle,
                FontSize = 18,
                Background = Brushes.Transparent,
                BorderBrush = Brushes.Transparent
            }
        };

        Border quitButton = new()
        {
            Width = menu.Width - 20,
            Margin = new Thickness(3),
            CornerRadius = new CornerRadius(10),
            Style = buttonStyle,
            Child = new Label
            {
                Style = textStyle,
                FontSize = 18,
                Background = Brushes.Transparent,
                BorderBrush = Brushes.Transparent
            }
        };

        Border goBackButton = new()
        {
            Width = menu.Width - 20,
            Margin = new Thickness(3),
            CornerRadius = new CornerRadius(10),
            Style = buttonStyle,
            Child = new Label
            {
                Style = textStyle,
                FontSize = 18,
                Background = Brushes.Transparent,
                BorderBrush = Brushes.Transparent,
            }
        };

        menuBackgrounfEffect.SetResourceReference(Border.BackgroundProperty, "PopUpBackground");

        (profileSettingsButton.Child as Label)!.SetResourceReference(ContentProperty, "ProfileSettingsText");
        (preferencesButton.Child as Label)!.SetResourceReference(ContentProperty, "SettingsText");
        (logoutButton.Child as Label)!.SetResourceReference(ContentProperty, "LogoutText");
        (quitButton.Child as Label)!.SetResourceReference(ContentProperty, "QuitApplicationText");
        (goBackButton.Child as Label)!.SetResourceReference(ContentProperty, "GoBackText");

        (profileSettingsButton.Child as Label)!.SetResourceReference(ForegroundProperty, "TextPopUpBrush");
        (preferencesButton.Child as Label)!.SetResourceReference(ForegroundProperty, "TextPopUpBrush");
        (logoutButton.Child as Label)!.SetResourceReference(ForegroundProperty, "TextPopUpBrush");
        (quitButton.Child as Label)!.SetResourceReference(ForegroundProperty, "TextPopUpBrush");
        (goBackButton.Child as Label)!.SetResourceReference(ForegroundProperty, "TextPopUpBrush");

        CommandAttachers.OnClickEvent(profileSettingsButton, "OpenProfileSettingsCommand");
        CommandAttachers.OnClickEvent(preferencesButton, "OpenPreferencesCommand");
        CommandAttachers.OnClickEvent(logoutButton, "LogoutCommand");
        CommandAttachers.OnClickEvent(quitButton, "QuitCommand");
        CommandAttachers.OnClickEvent(goBackButton, "GoBackCommand");

        foreach (UIElement element in new UIElement[] { profileSettingsButton, 
            UIElements.HorizontalBarFactory(menu), preferencesButton, 
            UIElements.HorizontalBarFactory(menu), logoutButton, 
            UIElements.HorizontalBarFactory(menu), quitButton, 
            UIElements.HorizontalBarFactory(menu), goBackButton })
            menu.Children.Add(element);

        Panel.SetZIndex(menuBackgrounfEffect, 0);
        Panel.SetZIndex(menu, 1);

        menuContainer.Children.Add(menuBackgrounfEffect);
        menuContainer.Children.Add(menu);

        menuBorder.Child = menuContainer;

        return menuBorder;
    }
}