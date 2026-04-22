using System.Drawing.Printing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Media.Media3D;
using System.Windows.Shapes;

namespace NeuChessHu.Resources.Styles;

internal static class Styles
{
    readonly static ResourceDictionary resources = Application.Current.Resources;

    internal static void MergeStyles()
    {
        SetCursorOnButtons();
        SetCursorOnTextBoxes();

        WindowsHeaderStyle();
        PopUpsBorderStyle();

        ButtonStyleOverlayStyle();
        ButtonStyleBaseMenuStyle();
        TextStyle();

        MainWindowStyle();
        MainWindowOpenOverlayStyle();

        MainOverlayStyle();
        MainOpenOverlayStyle();

        OpenPromotionWindowStyle();
        ClosedPromotionWindowStyle();

        CloseEllipseButtonStyle();

        DefaultProfilePictureStyle();
    }

    static void SetCursorOnButtons()
    {
        Cursor cursorOnButtons = Cursors.Hand;
        resources.Add("CursorOnButtons", cursorOnButtons);
    }

    static void SetCursorOnTextBoxes()
    {
        Cursor cursorOnTextBoxes = Cursors.IBeam;
        resources.Add("CursorOnTextBoxes", cursorOnTextBoxes);
    }

    static void WindowsHeaderStyle()
    {
        Style windowsHeaderStyle = new()
        {
            Setters =
            {
                new Setter(Border.CornerRadiusProperty, new CornerRadius(15, 15, 0, 0)),
                new Setter(Border.BorderThicknessProperty, new Thickness(0, 0, 0, 2)),
                new Setter(Border.BorderBrushProperty, new DynamicResourceExtension("BorderBrush")),
                new Setter(Border.BackgroundProperty, new DynamicResourceExtension("NavBarBrush")),
                new Setter(Border.HorizontalAlignmentProperty, HorizontalAlignment.Stretch)
            }
        };

        resources.Add("WindowsHeaderStyle", windowsHeaderStyle);
    }

    static void PopUpsBorderStyle()
    {
        Style popUpsBorderStyle = new()
        {
            Setters =
            {
                new Setter(Border.BorderBrushProperty, new DynamicResourceExtension("BorderBrush")),
                new Setter(Border.BorderThicknessProperty, new Thickness(2)),
                new Setter(Border.BackgroundProperty, Brushes.Transparent),
                new Setter(Border.CornerRadiusProperty, new CornerRadius(10))
            }
        };

        resources.Add("PopUpsBorderStyle", popUpsBorderStyle);
    }

    static void ButtonStyleOverlayStyle()
    {
        Style buttonStyleOverlayStyle = new()
        {
            Setters =
            {
                new Setter(Border.BorderBrushProperty, new DynamicResourceExtension("BorderBrush")),
                new Setter(Border.BackgroundProperty, Brushes.Transparent),
                new Setter(Border.VerticalAlignmentProperty, VerticalAlignment.Center),
                new Setter(Border.HorizontalAlignmentProperty, HorizontalAlignment.Center),
                new Setter(Border.CursorProperty, AppResources.Get<Cursor>("CursorOnButtons")),
                new Setter(Border.ForceCursorProperty, true)
            },
            Triggers =
            {
                AppResources.Get<Trigger>("ButtonTriggerOverlayHoverTrigger")
            }
        };

        resources.Add("ButtonStyleOverlayStyle", buttonStyleOverlayStyle);
    }

    static void ButtonStyleBaseMenuStyle()
    {
        Style buttonStyleBaseMenu = new()
        {
            Setters =
            {
                new Setter(Border.HeightProperty, 65.0),
                new Setter(Border.WidthProperty, 230.0),
                new Setter(Border.CornerRadiusProperty, new CornerRadius(10)),
                new Setter(Border.BackgroundProperty, new DynamicResourceExtension("ButtonBrush")),
                new Setter(Border.BorderBrushProperty, new DynamicResourceExtension("BorderBrush")),
                new Setter(Border.BorderThicknessProperty, new Thickness(1)),
                new Setter(Border.VerticalAlignmentProperty, VerticalAlignment.Center),
                new Setter(Border.HorizontalAlignmentProperty, HorizontalAlignment.Center),
                new Setter(Border.CursorProperty, AppResources.Get<Cursor>("CursorOnButtons")),
                new Setter(Border.ForceCursorProperty, true)
            },
            Triggers =
            {
                AppResources.Get<Trigger>("ButtonTriggerBaseMenuHoverTrigger")
            }
        };

        resources.Add("ButtonStyleBaseMenuStyle", buttonStyleBaseMenu);
    }

    static void TextStyle()
    {
        Style textStyle = new()
        {
            Setters =
            {
                new Setter(Label.HorizontalAlignmentProperty, HorizontalAlignment.Center),
                new Setter(Label.VerticalAlignmentProperty, VerticalAlignment.Center),
                new Setter(Label.FontSizeProperty, 14.0),
                new Setter(Label.FontWeightProperty, FontWeights.Bold),
                new Setter(Label.FontFamilyProperty, new FontFamily("Aharoni")),
                new Setter(Label.ForegroundProperty, new DynamicResourceExtension("TextBrush"))
            }
        };

        resources.Add("TextStyle", textStyle);
    }

    static void MainWindowStyle()
    {
        Style mainWindowStyle = new()
        {
            Setters =
            {
                new Setter(Grid.BackgroundProperty, new DynamicResourceExtension("WindowBrush")),
                new Setter(Grid.EffectProperty, new BlurEffect { Radius = 0 }),
                new Setter(Grid.IsHitTestVisibleProperty, true)
            }
        };

        resources.Add("MainWindowStyle", mainWindowStyle);
    }

    static void MainWindowOpenOverlayStyle()
    {
        Style mainWindowOpenOverlayStyle = new()
        {
            Setters =
            {
                new Setter(Grid.BackgroundProperty, new DynamicResourceExtension("WindowBrush")),
                new Setter(Grid.EffectProperty, new BlurEffect { Radius = 15 }),
                new Setter(Grid.IsHitTestVisibleProperty, false)
            }
        };

        resources.Add("MainWindowOpenOverlayStyle", mainWindowOpenOverlayStyle);
    }

    static void MainOverlayStyle()
    {
        Style mainOverlayStyle = new()
        {
            Setters =
            {
                new Setter(Grid.IsHitTestVisibleProperty, false),
                new Setter(Panel.ZIndexProperty, 1),
            }
        };

        resources.Add("MainOverlayStyle", mainOverlayStyle);
    }

    static void MainOpenOverlayStyle()
    {
        Style mainOpenOverlayStyle = new()
        {
            Setters =
            {
                new Setter(Grid.IsHitTestVisibleProperty, true)
            }
        };

        resources.Add("MainOpenOverlayStyle", mainOpenOverlayStyle);
    }

    static void OpenPromotionWindowStyle()
    {
        Style openPromotionWindowStyle = new()
        {
            Setters =
            {
                new Setter(Grid.IsHitTestVisibleProperty, true),
                new Setter(Panel.ZIndexProperty, 1),
            }
        };

        resources.Add("OpenPromotionWindowStyle", openPromotionWindowStyle);
    }

    static void ClosedPromotionWindowStyle()
    {
        Style closedPromotionWindowStyle = new()
        {
            Setters =
            {
                new Setter(Grid.IsHitTestVisibleProperty, false),
            }
        };

        resources.Add("ClosedPromotionWindowStyle", closedPromotionWindowStyle);
    }

    static void CloseEllipseButtonStyle()
    {
        Style closeEllipseButtonStyle = new()
        {
            Setters =
            {
                new Setter(Ellipse.WidthProperty, 10.0),
                new Setter(Ellipse.HeightProperty, 10.0),
                new Setter(Ellipse.FillProperty, Brushes.Red),
                new Setter(Ellipse.StrokeProperty, Brushes.DarkRed),
                new Setter(Ellipse.StrokeThicknessProperty, 1.5),
                new Setter(Ellipse.HorizontalAlignmentProperty, HorizontalAlignment.Right),
                new Setter(Ellipse.VerticalAlignmentProperty, VerticalAlignment.Top),
                new Setter(Border.CursorProperty, AppResources.Get<Cursor>("CursorOnButtons")),
                new Setter(Border.ForceCursorProperty, true)
            },
            Triggers =
            {
                AppResources.Get<Trigger>("CloseEllipseButtonHoverTrigger")
            }
        };

        resources.Add("CloseEllipseButtonStyle", closeEllipseButtonStyle);
    }

    static void DefaultProfilePictureStyle()
    {
        Style defaultProfilePictureStyle = new()
        {
            Setters =
            {
                new Setter(FrameworkElement.MarginProperty, new Thickness(-12, -12, 0, 0)),
                new Setter(FrameworkElement.WidthProperty, 83.0),
                new Setter(FrameworkElement.HeightProperty, 83.0),
            }
        };

        resources.Add("DefaultProfilePictureStyle", defaultProfilePictureStyle);
    }
}