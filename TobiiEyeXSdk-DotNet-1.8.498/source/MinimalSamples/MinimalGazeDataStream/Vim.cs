using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MinimalGazeDataStream
{
    public class Vim
    {
        // Get a handle to an application window.
        [DllImport("USER32.DLL", CharSet = CharSet.Unicode)]
        public static extern IntPtr FindWindow(string lpClassName,
            string lpWindowName);

        // Activate an application window.
        [DllImport("USER32.DLL")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        // Send a series of key presses to the Calculator application.
        public void Go(double x, double y)
        {
            // Get a handle to the Calculator application. The window class
            // and window name were obtained using the Spy++ tool.
            IntPtr calculatorHandle = FindWindow(default(string), "MINGW64:/c/Users/hal");


            // Verify that Calculator is a running process.
            if (calculatorHandle == IntPtr.Zero)
            {
                Console.WriteLine("Calculator is not running.");
                return;
            }

            // Make Calculator the foreground application and send it 
            // a set of calculations.
            /*SendKeys.SendWait(":");
            SendKeys.SendWait("c");
            SendKeys.SendWait("a");
            SendKeys.SendWait("l");
            SendKeys.SendWait("l");
            SendKeys.SendWait(" ");
            SendKeys.SendWait("c");
            SendKeys.SendWait("u");
            SendKeys.SendWait("l");*/
            SendKeys.SendWait(":call cursor{(}"+ (int)((y / 1080) * 33 + 1) + ","+(int)((x / 1920) * 101) + "{)}{ENTER}");
            //SendKeys.Send(Keys.Enter.ToString());


        }

        public void Foreground()
        {
            // Get a handle to the Calculator application. The window class
            // and window name were obtained using the Spy++ tool.
            IntPtr calculatorHandle = FindWindow(default(string), "MINGW64:/c/Users/hal");


            // Verify that Calculator is a running process.
            if (calculatorHandle == IntPtr.Zero)
            {
                Console.WriteLine("Calculator is not running.");
                return;
            }

            // Make Calculator the foreground application and send it 
            // a set of calculations.
            SetForegroundWindow(calculatorHandle);
        }

    }
}
