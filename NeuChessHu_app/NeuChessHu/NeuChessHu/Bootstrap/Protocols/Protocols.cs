using Microsoft.Win32;

namespace NeuChessHu.Bootstrap.Protocols;

internal static class Protocols
{
    internal static void RegisterAppURL()
    {
        using (RegistryKey baseKey = Registry.CurrentUser.CreateSubKey(@"Software\Classes\neuchesshu"))
        {
            baseKey.SetValue(string.Empty, "URL:Neuchess Protocol");
            baseKey.SetValue("URL Protocol", string.Empty);
        }

        using RegistryKey commandKey = Registry.CurrentUser.CreateSubKey(@"Software\Classes\neuchesshu\shell\open\command");
        string exePath = Environment.ProcessPath!;
        commandKey.SetValue(string.Empty, $"\"{exePath}\" \"%1\"");
    }
}