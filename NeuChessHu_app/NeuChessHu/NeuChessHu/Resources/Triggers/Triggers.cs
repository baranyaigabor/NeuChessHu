using Newtonsoft.Json.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace NeuChessHu.Resources.Triggers;

internal static class Triggers
{
    readonly static ResourceDictionary resources = Application.Current.Resources;
    
    internal static void MergeTriggers()
    {
        ButtonBaseMenuHoverTriggerBuilder();
        ButtonOverlayHoverTriggerBuilder();

        CloseEllipseButtonHoverTriggerBuilder();

        SendMessageHoverTriggerBuilder();
    }
    
    static void ButtonBaseMenuHoverTriggerBuilder()
    {
        Trigger buttonBaseMenuHoverTrigger = new()
        {
            Property = UIElement.IsMouseOverProperty,
            Value = true,
            Setters =
            {
                new Setter(Border.BorderThicknessProperty, new Thickness(3)),
            }
        };

        resources.Add("ButtonTriggerBaseMenuHoverTrigger", buttonBaseMenuHoverTrigger);
    }

    static void ButtonOverlayHoverTriggerBuilder()
    {
        Trigger buttonTriggerOverlayHoverTrigger = new()
        {
            Property = UIElement.IsMouseOverProperty,
            Value = true,
            Setters =
            {
                new Setter(UIElement.RenderTransformProperty, new ScaleTransform(1.129, 1.129)),
                new Setter(UIElement.RenderTransformOriginProperty, new Point(0.5, 0.5)), 
            }            
        };

        resources.Add("ButtonTriggerOverlayHoverTrigger", buttonTriggerOverlayHoverTrigger);
    }

    static void CloseEllipseButtonHoverTriggerBuilder()
    {
        Trigger closeEllipseButtonHoverTriggerBuilder = new()
        {
            Property = UIElement.IsMouseOverProperty,
            Value = true,
            Setters =
            {
                new Setter(Ellipse.StrokeThicknessProperty, 2.3),
            }
        };

        resources.Add("CloseEllipseButtonHoverTrigger", closeEllipseButtonHoverTriggerBuilder);
    }

    static void SendMessageHoverTriggerBuilder()
    {
        Trigger sendMessageHoverTrigger = new()
        {
            Property = UIElement.IsMouseOverProperty,
            Value = true,
            Setters =
            {
                new Setter(TextBox.BorderThicknessProperty, new Thickness(0.8)),
            }
        };

        resources.Add("SendMessageHoverTrigger", sendMessageHoverTrigger);
    }
}