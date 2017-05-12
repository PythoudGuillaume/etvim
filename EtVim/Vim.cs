using System;
using System.Runtime.InteropServices;

namespace EtVim
{
    public class Vim
    {
        private const int MOUSEEVENTF_LEFTDOWN = 0x02;
        private const int MOUSEEVENTF_LEFTUP = 0x04;

        [DllImport("USER32.DLL", CharSet = CharSet.Unicode)]
        private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("USER32.DLL")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("USER32.dll")]
        private static extern bool SetCursorPos(int x, int y);

        [DllImport("USER32.dll")]
        private static extern void mouse_event(int dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);

        public void Foreground()
        {
            // Get a handle to the mintty application
            var minttyHandle = FindWindow("mintty", default(string));

            // Verify that mintty is a running process.
            if (minttyHandle == IntPtr.Zero)
            {
                Console.WriteLine("Vim is not running.");
                return;
            }

            // Make mintty the foreground application
            SetForegroundWindow(minttyHandle);
        }


        //This simulates a left mouse click
        public void LeftMouseClick(int xpos, int ypos)
        {
            SetCursorPos(xpos, ypos);
            mouse_event(MOUSEEVENTF_LEFTDOWN, xpos, ypos, 0, 0);
            mouse_event(MOUSEEVENTF_LEFTUP, xpos, ypos, 0, 0);
        }

    }
}
