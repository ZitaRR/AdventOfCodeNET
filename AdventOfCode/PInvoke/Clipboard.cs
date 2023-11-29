using System.Runtime.InteropServices;

namespace AdventOfCodeNet.PInvoke;

public static partial class Clipboard
{
    [LibraryImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static partial bool OpenClipboard(IntPtr hWndNewOwner);

    [LibraryImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static partial bool CloseClipboard();

    [LibraryImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static partial bool SetClipboardData(uint uFormat, IntPtr data);

    public static bool SetText(string text)
    {
        try
        {
            OpenClipboard(IntPtr.Zero);
            IntPtr pointer = Marshal.StringToHGlobalUni(text);
            SetClipboardData(13, pointer);
            CloseClipboard();
            Marshal.FreeHGlobal(pointer);
            return true;
        }
        catch
        {
            throw new ExternalException("Failed to set clipboard due to unknown reason.");
        }
    }
}
