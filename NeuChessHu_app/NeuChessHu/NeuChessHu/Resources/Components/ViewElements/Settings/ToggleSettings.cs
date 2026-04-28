using NeuChessHu.UserSettings;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Shapes;

namespace NeuChessHu.Resources.Components.ViewElements.Settings;

internal static class ToggleSettings
{
    internal static DockPanel Create(string settingName)
    {
        string propertyName = settingName.Replace("-", "").Replace(" ", "");

        PropertyInfo? settingProperty = typeof(BindableSettings).GetProperties()
            .FirstOrDefault(x => x.Name == propertyName);

        if (settingProperty is null || settingProperty.PropertyType != typeof(bool))
            throw new ArgumentException($"Property '{propertyName}' has not been found");

        ToggleButton toggleButton = ToggleButtonFactory();

        DockPanel toggleSettingPanel = new()
        {
            Margin = new Thickness(10, 10, 0, 10),
            Children =
            {
                new Label
                {
                    Content = settingName,
                    Style = AppResources.Get<Style>("TextStyle"),
                    FontSize = 18,
                },

                toggleButton,
            }
        };

        DockPanel.SetDock(toggleSettingPanel.Children[0], Dock.Left);
        DockPanel.SetDock(toggleSettingPanel.Children[1], Dock.Right);

        (toggleSettingPanel.Children[0] as Label)!.SetResourceReference(Label.ContentProperty,
            propertyName + "Text");
        (toggleSettingPanel.Children[0] as Label)!.SetResourceReference(Label.ForegroundProperty,
            "TextPopUpBrush");

        Binding binding = new($"Settings.{propertyName}") { Mode = BindingMode.TwoWay };
        toggleButton.SetBinding(ToggleButton.IsCheckedProperty, binding);

        return toggleSettingPanel;
    }

    static ToggleButton ToggleButtonFactory()
    {
        ToggleButton toggleButton = new()
        {
            HorizontalAlignment = HorizontalAlignment.Right,
            Width = 46,
            Height = 25,
            Margin = new Thickness(20, 6, 10, 6),
            Focusable = false,
        };

        Ellipse toggleKnob = new()
        {
            Width = 16,
            Height = 16,
            Fill = Brushes.White,
            Margin = new Thickness(2, 2, 22, 2)
        };

        toggleButton.Content = new Border()
        {
            CornerRadius = new CornerRadius(15),
            Background = Brushes.LightGray,
            Child = toggleKnob
        };

        toggleButton.Checked += (s, e) =>
        {
            (toggleButton.Content as Border)!.Background = Brushes.MediumSeaGreen;
            toggleKnob.Margin = new Thickness(22, 2, 2, 2);
        };

        toggleButton.Unchecked += (s, e) =>
        {
            (toggleButton.Content as Border)!.Background = Brushes.LightGray;
            toggleKnob.Margin = new Thickness(2, 2, 22, 2);
        };

        if (toggleButton.IsChecked is true)
        {
            (toggleButton.Content as Border)!.Background = Brushes.MediumSeaGreen;
            toggleKnob.Margin = new Thickness(22, 2, 2, 2);
        }
        else
        {
            (toggleButton.Content as Border)!.Background = Brushes.LightGray;
            toggleKnob.Margin = new Thickness(2, 2, 22, 2);
        }

        FrameworkElementFactory templateFactory = new(typeof(Grid));
        templateFactory.AppendChild(new(typeof(Border)));

        toggleButton.Template = new ControlTemplate(typeof(ToggleButton))
        {
            VisualTree = new FrameworkElementFactory(typeof(ContentPresenter))
        };

        return toggleButton;
    }
}