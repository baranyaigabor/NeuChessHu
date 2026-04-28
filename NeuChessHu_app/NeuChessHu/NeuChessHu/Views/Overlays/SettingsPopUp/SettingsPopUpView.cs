using NeuChessHu.CommandUtils;
using NeuChessHu.Resources;
using NeuChessHu.Resources.Components;
using NeuChessHu.Resources.Components.ViewElements.Settings;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace NeuChessHu.Views.Overlays.SettingsPopUp;

public partial class SettingsPopUpView : UserControl
{
    public SettingsPopUpView() =>
        Loaded += (s, e) =>
        {
            Content ??= SettingsBuilder();
        };

    static Border SettingsBuilder()
    {
        Style buttonStyle = AppResources.Get<Style>("ButtonStyleOverlayStyle");

        Style textStyle = AppResources.Get<Style>("TextStyle");

        Border settingsBorder = new()
        {
            Style = AppResources.Get<Style>("PopUpsBorderStyle"),
            BorderThickness = new Thickness(2),
            CornerRadius = new CornerRadius(10),
            Margin = new Thickness(180),
            Width = 660,
            Height = 240,
        };

        Grid settingsContainer = new()
        {
            Background = Brushes.Transparent
        };

        StackPanel settingsMenu = new()
        {
            Margin = new Thickness(0, 10, 0, 0)
        };

        Border settingsBackgroundEffect = UIElements.PopUpBackgroundFactory(settingsMenu);

        StackPanel settingsPanel = new()
        {
            Orientation = Orientation.Horizontal
        };

        StackPanel selectableSettingsPanel = new()
        {
            Width = 330
        };

        StackPanel toggleSettingsPanel = new()
        {
            Width = 290
        };

        Border goBackButton = new()
        {
            Width = 230,
            HorizontalAlignment = HorizontalAlignment.Center,
            Margin = new Thickness(0, 8, 0, 15),
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

        settingsBackgroundEffect.SetResourceReference(Border.BackgroundProperty, "PopUpBackground");

        (goBackButton.Child as Label)!.SetResourceReference(ForegroundProperty, "TextPopUpBrush");
        (goBackButton.Child as Label)!.SetResourceReference(ContentProperty, "GoBackText");

        foreach (UIElement item in new UIElement[] { SelectableSettings.Create("Board Theme"),
            UIElements.HorizontalBarFactory(settingsMenu), SelectableSettings.Create("Piece Theme"),
            UIElements.HorizontalBarFactory(settingsMenu), SelectableSettings.Create("Language") })
            selectableSettingsPanel.Children.Add(item);

        foreach (UIElement element in new UIElement[] { ToggleSettings.Create("Disable Sounds"),
            UIElements.HorizontalBarFactory(settingsMenu), ToggleSettings.Create("Auto-Queen"),
            UIElements.HorizontalBarFactory(settingsMenu), ToggleSettings.Create("Dark Mode") })
            toggleSettingsPanel.Children.Add(element);

        foreach (UIElement element in new UIElement[] { selectableSettingsPanel,
            UIElements.SettingsVerticalSeparatorFactory(), toggleSettingsPanel})
            settingsPanel.Children.Add(element);

        foreach (UIElement element in new UIElement[] { settingsPanel, goBackButton })
            settingsMenu.Children.Add(element);

        CommandAttachers.OnClickEvent(goBackButton, "GoBackCommand");

        Panel.SetZIndex(settingsBackgroundEffect, 0);
        Panel.SetZIndex(settingsMenu, 1);

        settingsContainer.Children.Add(settingsBackgroundEffect);
        settingsContainer.Children.Add(settingsMenu);
        settingsBorder.Child = settingsContainer;

        return settingsBorder;
    }
}