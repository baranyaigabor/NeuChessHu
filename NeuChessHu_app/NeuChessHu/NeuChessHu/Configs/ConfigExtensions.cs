using System.Windows;

namespace NeuChessHu.Configs;

internal static class ConfigExtensions
{
    internal static T WindowConfig<T>(this T window, Action<T> configure) where T : Window
    {
        configure(window);
        return window;
    }
}

