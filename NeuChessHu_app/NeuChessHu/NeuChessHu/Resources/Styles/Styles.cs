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
}