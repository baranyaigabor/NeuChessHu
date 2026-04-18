using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace NeuChessHu.Resources.Components.ViewElements.MatchSideBar;

internal static class MatchSideBarViewElements
{
    internal static Border PlayerInfoPanelsFactory() => new()
    {
        Child = new DockPanel
        {
            Children =
            {
                new Border
                {
                    BorderBrush = AppResources.Get<SolidColorBrush>("BorderBrush"),
                    BorderThickness = new Thickness(0.5),
                    Width = 60,
                    Height = 60,
                    Child = new Image()
                },
                new Border
                {
                    BorderBrush = AppResources.Get<SolidColorBrush>("BorderBrush"),
                    BorderThickness = new Thickness(0.5),

                    Child = new Label
                    {
                        HorizontalAlignment = HorizontalAlignment.Left,
                        Style = AppResources.Get<Style>("TextStyle"),
                        FontSize = 14,
                    }
                }
            }
        }
    };

}