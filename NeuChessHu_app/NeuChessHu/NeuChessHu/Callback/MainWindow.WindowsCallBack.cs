using ChessMechanics.Authentication;
using NeuChessHu.Resources.Types;
using NeuChessHu.ViewModels.MainWindow;
using System.Runtime.InteropServices;
using System.Windows.Interop;
using System.Windows.Threading;

namespace NeuChessHu;

public partial class MainWindow
{
    readonly SessionManager sessionManager;

    public MainWindow(SessionManager sessionManager)
    {
        this.sessionManager = sessionManager;

        InitializeWndProcHook();

        Loaded += (s, e) =>
        {
            Content ??= MainWindowBaseBuilder();
        };
    }

    void InitializeWndProcHook()
    {
        SourceInitialized += (s, e) =>
        {
            nint hwnd = new WindowInteropHelper(this).Handle;
            HwndSource source = HwndSource.FromHwnd(hwnd);
            source.AddHook(WndProc);
        };
    }

    IntPtr WndProc(IntPtr hwnd, int message, IntPtr wParam, IntPtr lParam, ref bool handled)
    {
        if (message == 0x004A)
        {
            CallbackDatas data = Marshal.PtrToStructure<CallbackDatas>(lParam)!;
            string callbackUrl = Marshal.PtrToStringUni(data.DataPointer)!;

            if (callbackUrl.StartsWith("neuchesshu://auth/callback"))
            {
                Dispatcher.InvokeAsync(async () =>
                {
                    await sessionManager.OnAuthenticated(callbackUrl);
                    (DataContext as MainWindowViewModel)?.CloseMainOverlay();
                });

                Activate();
            }

            handled = true;
        }

        return IntPtr.Zero;
    }
}