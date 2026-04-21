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

}