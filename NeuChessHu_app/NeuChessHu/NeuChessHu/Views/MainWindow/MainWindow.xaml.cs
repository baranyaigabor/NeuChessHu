using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace NeuChessHu;

public partial class MainWindow : Window
{
    Grid MainWindowBaseBuilder()
    {
        InitializeComponent();

        Grid rootContainer = new();

        Grid mainComponentsContainer = new();

        foreach (int height in new int[] { 3, 1, 24, 1 })
            mainComponentsContainer.RowDefinitions.Add(new RowDefinition { Height = new GridLength(height, GridUnitType.Star) });

        foreach (int width in new int[] { 1, 24, 1, 14, 1 })
            mainComponentsContainer.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(width, GridUnitType.Star) });

        ContentControl navBar = new();

        Viewbox boards = new()
        {
            Child = new Grid()
            {
                Children =
                {
                    new ContentControl(),
                    new ContentControl()
                }
            }
        };

        Viewbox sideBars = new()
        {
            Child = new ContentControl(),
        };

        Viewbox popUpPanelsLayer = new()
        {
            Child = new ContentControl()
        };

        mainComponentsContainer.SetBinding(StyleProperty, new Binding("MainWindowStyle"));

        navBar.SetBinding(ContentProperty, new Binding("NavBar"));

        ((boards.Child as Grid)!.Children[0] as ContentControl)!
            .SetBinding(ContentProperty, new Binding("CurrentBoard"));
        ((boards.Child as Grid)!.Children[1] as ContentControl)!.
            SetBinding(ContentProperty, new Binding("BoardOverlay"));
        ((boards.Child as Grid)!.Children[1] as ContentControl)!
            .SetBinding(StyleProperty, new Binding("BoardOverlayStyle"));

        (sideBars.Child as ContentControl)!
            .SetBinding(ContentProperty, new Binding("CurrentSidebar"));

        (popUpPanelsLayer.Child as ContentControl)!
            .SetBinding(ContentProperty, new Binding("MainOverlay"));
        (popUpPanelsLayer.Child as ContentControl)!
            .SetBinding(StyleProperty, new Binding("MainOverlayStyle"));

        Grid.SetRow(navBar, 0);
        Grid.SetColumn(navBar, 0);
        Grid.SetColumnSpan(navBar, 5);

        Grid.SetRow(boards, 2);
        Grid.SetColumn(boards, 1);

        Grid.SetRow(sideBars, 2);
        Grid.SetColumn(sideBars, 3);

        foreach (UIElement element in new UIElement[] { navBar, sideBars, boards })
            mainComponentsContainer.Children.Add(element);

        foreach (UIElement element in new UIElement[] { mainComponentsContainer, popUpPanelsLayer })
            rootContainer.Children.Add(element);

        return rootContainer;
    }
}