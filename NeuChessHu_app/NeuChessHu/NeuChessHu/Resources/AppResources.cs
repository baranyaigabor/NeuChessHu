using System.Windows;

namespace NeuChessHu.Resources;

internal static class AppResources
{
    internal static T Get<T>(string key)
    {
        if (!Application.Current.Resources.Contains(key))
            throw new ResourceReferenceKeyNotFoundException(key, Application.Current);

        if (Application.Current.Resources[key] is not T value)
            throw new InvalidCastException($"Resource '{key}' and its type '{typeof(T).Name}' are not matching");

        return value;
    }
}