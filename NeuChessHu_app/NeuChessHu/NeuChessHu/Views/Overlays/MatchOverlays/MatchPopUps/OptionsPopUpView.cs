using NeuChessHu.CommandUtils;
using NeuChessHu.Resources;
using NeuChessHu.Resources.Components;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Shapes;

namespace NeuChessHu.Views.Overlays.MatchOverlays.MatchPopUps;

public partial class OptionsPopUpView : UserControl
{
    public OptionsPopUpView() =>
        Loaded += (s, e) =>
        {
            Content ??= OptionsPanelBuilder();
        };

    static Border OptionsPanelBuilder()
    {
        Style buttonStyle = AppResources.Get<Style>("ButtonStyleOverlayStyle");

        Style textStyle = AppResources.Get<Style>("TextStyle");

        Border optionsBorder = new()
        {
            Style = AppResources.Get<Style>("PopUpsBorderStyle"),
            BorderThickness = new Thickness(2),
            CornerRadius = new CornerRadius(10),
            Margin = new Thickness(251),
        };

        Grid optionsContainer = new()
        {
            Background = Brushes.Transparent
        };

        StackPanel optionsMenu = new()
        {
            Width = 250,
            Orientation = Orientation.Vertical
        };

        Border optionsBackgroundEffect = UIElements.PopUpBackgroundFactory(optionsMenu);

        Border abortResignQuitButton = new()
        {
            Width = optionsMenu.Width - 20,
            Margin = new Thickness(3, 5, 3, 3),
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

        Rectangle offerDrawSeparatorBar = UIElements.HorizontalBarFactory(optionsMenu);

        Border offerDrawButton = new()
        {
            Width = optionsMenu.Width - 20,
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

        Border settingsButton = new()
        {
            Width = optionsMenu.Width - 20,
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
            Width = optionsMenu.Width - 20,
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

        foreach (UIElement element in new UIElement[] { abortResignQuitButton, 
            offerDrawSeparatorBar, offerDrawButton,
            UIElements.HorizontalBarFactory(optionsMenu), settingsButton, 
            UIElements.HorizontalBarFactory(optionsMenu), goBackButton })
            optionsMenu.Children.Add(element);

        optionsBackgroundEffect.SetResourceReference(Border.BackgroundProperty, "PopUpBackground");

        (offerDrawButton.Child as Label)!.SetResourceReference(ContentProperty, "DrawText");
        (settingsButton.Child as Label)!.SetResourceReference(ContentProperty, "SettingsText");
        (goBackButton.Child as Label)!.SetResourceReference(ContentProperty, "GoBackText");

        foreach (Border element in new Border[] { abortResignQuitButton, offerDrawButton,
            settingsButton, goBackButton })
            (element.Child as Label)!.SetResourceReference(ForegroundProperty, "TextPopUpBrush");

        Binding optionsMenuHeightBinding = new("OptionsMenuHeight") { Mode = BindingMode.OneWay };
        optionsMenu.SetBinding(HeightProperty, optionsMenuHeightBinding);

        Binding abortResignQuitButtonBinding = new("AbortResignQuitButtonContent") { Mode = BindingMode.OneWay };
        (abortResignQuitButton.Child as Label)!.SetBinding(ContentProperty, abortResignQuitButtonBinding);

        Binding offerDrawButtonVisibilityBinding = new("OfferDrawButtonVisibility") { Mode = BindingMode.OneWay };
        offerDrawSeparatorBar.SetBinding(VisibilityProperty, offerDrawButtonVisibilityBinding);
        offerDrawButton.SetBinding(VisibilityProperty, offerDrawButtonVisibilityBinding);

        CommandAttachers.OnClickEvent(abortResignQuitButton, "AbortResignQuitCommand");
        CommandAttachers.OnClickEvent(offerDrawButton, "OffersDrawCommand");
        CommandAttachers.OnClickEvent(settingsButton, "OpenInGameSettignsCommand");
        CommandAttachers.OnClickEvent(goBackButton, "GoBackCommand");

        Panel.SetZIndex(optionsBackgroundEffect, 0);
        Panel.SetZIndex(optionsMenu, 1);

        foreach (UIElement element in new UIElement[] { optionsBackgroundEffect, optionsMenu })
            optionsContainer.Children.Add(element);

        optionsBorder.Child = optionsContainer;

        return optionsBorder;
    }
}