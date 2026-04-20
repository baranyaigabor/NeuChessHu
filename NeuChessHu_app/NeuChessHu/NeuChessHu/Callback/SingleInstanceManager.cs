using NeuChessHu.Resources.Types;
using System.Windows;

namespace NeuChessHu.Callback;

internal static class SingleInstanceManager
{
    static Mutex? appInstance;

    internal static bool CanCreateNewInstance(this Application application, StartupEventArgs e)
    {
        appInstance = new Mutex(initiallyOwned: true, "NeuChessHuApp", out bool isNewInstance);

        if (!isNewInstance)
        {
            if (e.Args.Length > 0)
                CallbackDatas.SendMessageToRunningInstance(e.Args[0]);

            application.Shutdown();
            return false;
        }

        return true;
    }

    internal static void Dispose()
    {
        if (appInstance is not null)
        {
            appInstance.ReleaseMutex();
            appInstance.Dispose();
        }
    }
}