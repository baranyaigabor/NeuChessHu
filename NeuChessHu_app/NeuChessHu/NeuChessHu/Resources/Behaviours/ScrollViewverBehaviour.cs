using Microsoft.Xaml.Behaviors;
using NeuChessHu.Resources.Types;
using System.Windows;
using System.Windows.Controls;

namespace NeuChessHu.Resources.Behaviours;

internal class ScrollViewerBehaviours : Behavior<ScrollViewer>
{
    internal ScrollTo Direction
    {
        get => (ScrollTo)GetValue(DirectionProperty);
        private set => SetValue(DirectionProperty, value);
    }

    internal static readonly DependencyProperty DirectionProperty =
        DependencyProperty.Register(nameof(Direction), typeof(ScrollTo), typeof(ScrollViewerBehaviours),
            new PropertyMetadata(ScrollTo.Top, OnDirectionChanged));

    static void OnDirectionChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
    {
        ScrollViewerBehaviours behaviours = (ScrollViewerBehaviours)o;
        ScrollViewer scrollViewer = behaviours.AssociatedObject;

        if (scrollViewer is null)
            return;

        Action scrolling = (ScrollTo)e.NewValue switch
        {
            ScrollTo.Top => scrollViewer.ScrollToTop,
            ScrollTo.Bottom => scrollViewer.ScrollToBottom,
            _ => throw new NotImplementedException()
        };

        scrolling();
    }
}