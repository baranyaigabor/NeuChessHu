using NeuChessHu.CommandUtils;
using NeuChessHu.Resources;
using NeuChessHu.Resources.Components;
using NeuChessHu.Resources.Components.ViewElements.Login;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace NeuChessHu.Views.Overlays.MenuOverlays.MenuPopUps;

public partial class LoginPopUpView : UserControl
{
    public LoginPopUpView() =>
        Loaded += (s, e) =>
        {
            Content ??= LoginPanelBuilder();
        };

    static Border LoginPanelBuilder()
    {
        Style buttonStyle = AppResources.Get<Style>("ButtonStyleOverlayStyle");

        Style textStyle = AppResources.Get<Style>("TextStyle");

        Border loginNotification = LoginNotification.LoginNotificationBuilder();

        Border loginBorder = new()
        {
            Style = AppResources.Get<Style>("PopUpsBorderStyle"),
            Margin = new Thickness(289),
        };

        Grid loginContainer = new()
        {
            Background = Brushes.Transparent
        };

        StackPanel login = new()
        {
            Width = 250,
            Orientation = Orientation.Vertical,
        };

        Border loginBackgrounfEffect = UIElements.PopUpBackgroundFactory(login);

        Border loginButton = new()
        {
            Width = login.Width - 20,
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

        Border goBackButton = new()
        {
            Width = login.Width - 20,
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

        loginBackgrounfEffect.SetResourceReference(Border.BackgroundProperty, "PopUpBackground");

        (loginButton.Child as Label)!.SetResourceReference(ContentProperty, "LoginText");
        (goBackButton.Child as Label)!.SetResourceReference(ContentProperty, "GoBackText");

        (loginButton.Child as Label)!.SetResourceReference(ForegroundProperty, "TextPopUpBrush");
        (goBackButton.Child as Label)!.SetResourceReference(ForegroundProperty, "TextPopUpBrush");

        Binding loginNotificationBinding = new("LoginNotificationVisibility") { Mode = BindingMode.OneWay };
        loginNotification.SetBinding(VisibilityProperty, loginNotificationBinding);

        Binding panelsHeightBinding = new("PanelHeight") { Mode = BindingMode.OneWay };
        login.SetBinding(HeightProperty, panelsHeightBinding);

        CommandAttachers.OnClickEvent(loginButton, "OpenLoginPanelCommand");
        CommandAttachers.OnClickEvent(goBackButton, "GoBackCommand");

        foreach (UIElement element in new UIElement[] { loginNotification, loginButton,
            UIElements.HorizontalBarFactory(login), goBackButton })
            login.Children.Add(element);

        Panel.SetZIndex(loginBackgrounfEffect, 0);
        Panel.SetZIndex(login, 1);

        loginContainer.Children.Add(loginBackgrounfEffect);
        loginContainer.Children.Add(login);
        loginBorder.Child = loginContainer;
        return loginBorder;
    }
}