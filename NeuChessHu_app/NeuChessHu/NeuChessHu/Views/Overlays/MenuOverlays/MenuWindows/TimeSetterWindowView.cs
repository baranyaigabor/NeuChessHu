using NeuChessHu.CommandUtils;
using NeuChessHu.Resources;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Shapes;

namespace NeuChessHu.Views.Overlays.MenuOverlays.MenuWindows;

public partial class TimeSetterWindowView : UserControl
{
    /// <summary>
    ///     A BUTTONOK STYLEJAT LEHETNE MEGJOBBAN EGYSZERUSITENI, ES CSAK A CONTENTET KELL MODOSITANI AKKOR
    /// </summary>
    public TimeSetterWindowView() =>
        Loaded += (s, e) =>
        {
            Content ??= TimeSetterPanelBuilder();
        };

    static Border TimeSetterPanelBuilder()
    {
        Style buttonsStyle = AppResources.Get<Style>("ButtonStyleBaseMenuStyle");

        Style textStyle = AppResources.Get<Style>("TextStyle");

        Border timeSetterBorder = new()
        {
            CornerRadius = new CornerRadius(15),
            BorderBrush = Brushes.Black,
            BorderThickness = new Thickness(2),
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Center,
            Background = AppResources.Get<SolidColorBrush>("SideBarBrush"),
            Margin = new Thickness(150)
        };

        Grid timeSetterContainer = new()
        {
            Width = 310,
            Height = 360,
        };

        for (int i = 0; i < 7; i++)
        {
            timeSetterContainer.RowDefinitions.Add(new RowDefinition());
            if (i < 3) timeSetterContainer.ColumnDefinitions.Add(new ColumnDefinition());
        }

        Border timeSetterHeader = new()
        {
            Style = AppResources.Get<Style>("WindowsHeaderStyle"),
            Child = new Grid
            {
                Children =
                {
                    new Label
                    {
                        Style = textStyle,
                        FontSize = 20,
                    },
                    new Ellipse
                    {
                        Style = AppResources.Get<Style>("CloseEllipseButtonStyle"),
                        Margin = new Thickness(0, 8, 8, 0)
                    },
                }
            }
        };

        StackPanel bulletsLabel = new()
        {
            Orientation = Orientation.Horizontal,
            HorizontalAlignment = HorizontalAlignment.Left,
            Margin = new Thickness(16, 0, 0, -20),
            Children =
            {
                new Image
                {
                    Width = 22,
                    Height = 22,
                    Margin = new Thickness(0, 0, 0, 3),
                    RenderTransform = new RotateTransform(-5)
                },
                new Label
                {
                    Content = "Bullet",
                    Style = textStyle,
                    FontSize = 18,
                }
            }
        };

        Border bulletOneMin = new()
        {
            Width = 90,
            Height = 30,
            Style = buttonsStyle,
            Margin = new Thickness(10, 0, 0, 0),
            Child = new Label
            {
                Content = "1 ",
                Style = textStyle
            }
        };

        Border bulletOneMinOne = new()
        {
            Width = 90,
            Height = 30,
            Style = buttonsStyle,
            Child = new Label
            {
                Content = "1 | 1",
                Style = textStyle
            }
        };

        Border bulletTwoMinOne = new()
        {
            Width = 90,
            Height = 30,
            Style = buttonsStyle,
            Margin = new Thickness(0, 0, 10, 0),
            Child = new Label
            {
                Content = "2 | 1",
                Style = textStyle
            }
        };

        StackPanel blitzslabel = new()
        {
            Orientation = Orientation.Horizontal,
            HorizontalAlignment = HorizontalAlignment.Left,
            Margin = new Thickness(16, 0, 0, -20),
            Children =
            {
                new Image
                {
                    Width = 25,
                    Height = 25,
                    Margin = new Thickness(0, 0, -3, 3),
                },
                new Label
                {
                    Content = "Blitz",
                    Style = textStyle,
                    FontSize = 18,
                }
            }
        };

        Border blitzThreeMin = new()
        {
            Width = 90,
            Height = 30,
            Style = buttonsStyle,
            Margin = new Thickness(10, 0, 0, 0),
            Child = new Label
            {
                Content = "3 min",
                Style = textStyle
            }
        };

        Border blitzThreeMinTwo = new()
        {
            Width = 90,
            Height = 30,
            Style = buttonsStyle,
            Child = new Label
            {
                Content = "3 | 2",
                Style = textStyle
            }
        };

        Border blitzFiveMin = new()
        {
            Width = 90,
            Height = 30,
            Style = buttonsStyle,
            Margin = new Thickness(0, 0, 10, 0),
            Child = new Label
            {
                Content = "5 min",
                Style = textStyle
            }
        };

        StackPanel rapidsLabel = new()
        {
            Orientation = Orientation.Horizontal,
            HorizontalAlignment = HorizontalAlignment.Left,
            Margin = new Thickness(16, 0, 0, -10),
            Children =
            {
                new Image
                {
                    Width = 28,
                    Height = 28,
                    Margin = new Thickness(0, 0, -4, 3),
                },
                new Label
                {
                    Content = "Rapid",
                    Style = textStyle,
                    FontSize = 18,
                }
            }
        };

        Border rapidTenMin = new()
        {
            Width = 90,
            Height = 30,
            Style = buttonsStyle,
            Margin = new Thickness(10, 0, 0, 10),
            Child = new Label
            {
                Content = "10 min",
                Style = textStyle
            }
        };

        Border rapidFifteenMinTen = new()
        {
            Width = 90,
            Height = 30,
            Style = buttonsStyle,
            Margin = new Thickness(0, 0, 0, 10),
            Child = new Label
            {
                Content = "15 | 10",
                Style = textStyle
            }
        };

        Border rapidThirtyMin = new()
        {
            Width = 90,
            Height = 30,
            Style = buttonsStyle,
            Margin = new Thickness(0, 0, 10, 10),
            Child = new Label
            {
                Content = "30 min",
                Style = textStyle
            }
        };

        ((timeSetterHeader.Child as Grid)!.Children[0] as Label)!.SetResourceReference(ContentProperty, "ChooseTimeText");

        (bulletsLabel.Children[1] as Label)!.SetResourceReference(ContentProperty, "BulletText");
        (blitzslabel.Children[1] as Label)!.SetResourceReference(ContentProperty, "BlitzText");
        (rapidsLabel.Children[1] as Label)!.SetResourceReference(ContentProperty, "RapidText");

        (bulletOneMin.Child as Label)!.SetResourceReference(ContentProperty, "BulletOneMinText");
        (blitzThreeMin.Child as Label)!.SetResourceReference(ContentProperty, "BlitzThreeMinText");
        (blitzFiveMin.Child as Label)!.SetResourceReference(ContentProperty, "BlitzFiveMinText");
        (rapidTenMin.Child as Label)!.SetResourceReference(ContentProperty, "RapidTenMinText");
        (rapidThirtyMin.Child as Label)!.SetResourceReference(ContentProperty, "RapidThirtyMinText");

        Binding bulletImageBinding = new("BulletSource") { Mode = BindingMode.OneWay };
        (bulletsLabel.Children[0] as Image)!.SetBinding(Image.SourceProperty, bulletImageBinding);

        Binding blitzImageBinding = new("BlitzSource") { Mode = BindingMode.OneWay };
        (blitzslabel.Children[0] as Image)!.SetBinding(Image.SourceProperty, blitzImageBinding);

        Binding rapidImageBinding = new("RapidSource") { Mode = BindingMode.OneWay };
        (rapidsLabel.Children[0] as Image)!.SetBinding(Image.SourceProperty, rapidImageBinding);

        CommandAttachers.OnClickEvent(((timeSetterHeader.Child as Grid)!.Children[1] as Ellipse)!, "GoBackCommand");

        foreach (Border button in new Border[] { bulletOneMin, bulletOneMinOne, bulletTwoMinOne,
                                                 blitzThreeMin, blitzThreeMinTwo, blitzFiveMin,
                                                 rapidTenMin, rapidFifteenMinTen, rapidThirtyMin })
            CommandAttachers.OnClickEvent(button, "SelectTimeCommand", args: (button.Child as Label)!.Content);

        Grid.SetRow(timeSetterHeader, 0);
        Grid.SetColumnSpan(timeSetterHeader, 3);

        Grid.SetRow(bulletsLabel, 1);
        Grid.SetColumnSpan(bulletsLabel, 3);

        Grid.SetRow(bulletOneMin, 2);
        Grid.SetColumn(bulletOneMin, 0);

        Grid.SetRow(bulletOneMinOne, 2);
        Grid.SetColumn(bulletOneMinOne, 1);

        Grid.SetRow(bulletTwoMinOne, 2);
        Grid.SetColumn(bulletTwoMinOne, 2);

        Grid.SetRow(blitzslabel, 3);
        Grid.SetColumnSpan(blitzslabel, 3);

        Grid.SetRow(blitzThreeMin, 4);
        Grid.SetColumn(blitzThreeMin, 0);

        Grid.SetRow(blitzThreeMinTwo, 4);
        Grid.SetColumn(blitzThreeMinTwo, 1);

        Grid.SetRow(blitzFiveMin, 4);
        Grid.SetColumn(blitzFiveMin, 2);

        Grid.SetRow(rapidsLabel, 5);
        Grid.SetColumnSpan(rapidsLabel, 3);

        Grid.SetRow(rapidTenMin, 6);
        Grid.SetColumn(rapidTenMin, 0);

        Grid.SetRow(rapidFifteenMinTen, 6);
        Grid.SetColumn(rapidFifteenMinTen, 1);

        Grid.SetRow(rapidThirtyMin, 6);
        Grid.SetColumn(rapidThirtyMin, 2);

        foreach (UIElement element in new UIElement[] { timeSetterHeader,
                    bulletsLabel, bulletOneMin, bulletOneMinOne, bulletTwoMinOne,
                    blitzslabel, blitzThreeMin, blitzThreeMinTwo, blitzFiveMin,
                    rapidsLabel, rapidTenMin, rapidFifteenMinTen, rapidThirtyMin})
            timeSetterContainer.Children.Add(element);

        timeSetterBorder.Child = timeSetterContainer;

        return timeSetterBorder;
    }
}