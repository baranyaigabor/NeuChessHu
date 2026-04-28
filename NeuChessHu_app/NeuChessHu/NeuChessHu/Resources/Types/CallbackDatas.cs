using System.Runtime.InteropServices;

namespace NeuChessHu.Resources.Types;

[StructLayout(LayoutKind.Sequential)]
internal struct CallbackDatas
{
    internal IntPtr MessageID;
    internal int DataSize;
    internal IntPtr DataPointer;

    internal static void SendMessageToRunningInstance(string url)
    {
        IntPtr hwnd = FindWindow(null, "NeuChess.hu");

        if (hwnd != IntPtr.Zero)
        {
            CallbackDatas data = new()
            {
                DataPointer = Marshal.StringToHGlobalUni(url),
                DataSize = (url.Length + 1) * 2
            };

            SendMessage(hwnd, 0x004A, IntPtr.Zero, ref data);

            SetForegroundWindow(hwnd);
            ShowWindow(hwnd, 9);
        }
    }

    [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
    static extern IntPtr FindWindow(string? lpClassName, string? lpWindowName);

    [DllImport("user32.dll")]
    static extern IntPtr SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, ref CallbackDatas lParam);

    [DllImport("user32.dll")]
    static extern bool SetForegroundWindow(IntPtr hWnd);

    [DllImport("user32.dll")]
    static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
}