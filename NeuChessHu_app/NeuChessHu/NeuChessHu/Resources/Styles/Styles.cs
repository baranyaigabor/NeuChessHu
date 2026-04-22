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
}