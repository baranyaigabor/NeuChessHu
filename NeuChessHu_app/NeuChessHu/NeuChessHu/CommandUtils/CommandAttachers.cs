using Microsoft.Xaml.Behaviors;
using System.Windows;
using System.Windows.Data;
using EventTrigger = Microsoft.Xaml.Behaviors.EventTrigger;

namespace NeuChessHu.CommandUtils;

internal static class CommandAttachers
{
    internal static void OnClickEvent(UIElement element, string commandPath, object? args = null, BindingBase? parameterBinding = null)
    {
        EventTrigger trigger = new("MouseLeftButtonDown");
        InvokeCommandAction action = new();

        BindingOperations.SetBinding(action, InvokeCommandAction.CommandProperty, new Binding(commandPath));

        if (parameterBinding is not null)
            BindingOperations.SetBinding(action, InvokeCommandAction.CommandParameterProperty, parameterBinding);

        else if (args is not null)
            action.CommandParameter = args;

        trigger.Actions.Add(action);
        Interaction.GetTriggers(element).Add(trigger);
    }

    internal static void OnLoaded(UIElement element, string commandPath, object? args = null, BindingBase? parameterBinding = null)
    {
        EventTrigger trigger = new("Loaded");
        InvokeCommandAction action = new();

        BindingOperations.SetBinding(action, InvokeCommandAction.CommandProperty, new Binding(commandPath));

        if (parameterBinding is not null)
            BindingOperations.SetBinding(action, InvokeCommandAction.CommandParameterProperty, parameterBinding);

        else if (args is not null)
            action.CommandParameter = args;

        trigger.Actions.Add(action);
        Interaction.GetTriggers(element).Add(trigger);
    }
}