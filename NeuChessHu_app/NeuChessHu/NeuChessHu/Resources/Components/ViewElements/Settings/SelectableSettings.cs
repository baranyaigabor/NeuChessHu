using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace NeuChessHu.Resources.Components.ViewElements.Settings;

internal static class SelectableSettings
{
    internal static DockPanel Create(string settingName)
    {
        string propertyName = settingName.Replace("-", "").Replace(" ", "");

        DockPanel settingSection = new()
        {
            Margin = new Thickness(20, 10, 14, 10),
            Children =
            {
                new Label
                {
                    Content = settingName,
                    Style = AppResources.Get<Style>("TextStyle"),
                    FontSize = 18,
                },

                new ComboBox
                {
                    FontSize = 15,
                    FontFamily = new FontFamily("Aharoni"),
                    Width = 153,
                    HorizontalAlignment = HorizontalAlignment.Right,
                }
            }
        };

        (settingSection.Children[0] as Label)!.SetResourceReference(Label.ContentProperty, propertyName + "Text");
        (settingSection.Children[0] as Label)!.SetResourceReference(Label.ForegroundProperty, "TextPopUpBrush");

        CreateSettingsBox(settingSection, propertyName);

        DockPanel.SetDock(settingSection.Children[0], Dock.Left);
        DockPanel.SetDock(settingSection.Children[1], Dock.Right);

        return settingSection;
    }

    static void CreateSettingsBox(DockPanel settingSection, string propertyName)
    {
        ComboBox settingBox = settingSection.Children.OfType<ComboBox>().First();

        Binding settingSourceBinding = new($"{propertyName}Options") { Mode = BindingMode.OneWay };
        settingBox.SetBinding(ComboBox.ItemsSourceProperty, settingSourceBinding);

        Binding selectedItemBinding = new($"Selected{propertyName}") { Mode = BindingMode.TwoWay };
        settingBox.SetBinding(ComboBox.SelectedItemProperty, selectedItemBinding);

        settingBox.MaxDropDownHeight = 170;
        settingBox.ItemTemplate = new DataTemplate
        {
            VisualTree = CreateSettingTextFactory()
        };
    }

    static FrameworkElementFactory CreateSettingTextFactory()
    {
        FrameworkElementFactory settingTextFactory = new(typeof(TextBlock));
        settingTextFactory.SetBinding(TextBlock.TextProperty, new Binding("Label"));

        return settingTextFactory;
    }
}