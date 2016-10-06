using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Mario.Core
{
    class WindowMaximize
    {
        [StructLayout(LayoutKind.Sequential)]
        private struct COORD
        {

            public short X;
            public short Y;
            public COORD(short x, short y)
            {
                this.X = x;
                this.Y = y;
            }

        }
        [DllImport("kernel32.dll")]
        private static extern IntPtr GetStdHandle(int handle);
        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool SetConsoleDisplayMode(
            IntPtr ConsoleOutput
            , uint Flags
            , out COORD NewScreenBufferDimensions
            );

        private static IntPtr hConsole = GetStdHandle(-11);
        private static COORD xy = new COORD(1000, 1000);

        private static System.Windows.Forms.Timer WindowMaximizer = new System.Windows.Forms.Timer();
        
        public static void WindowInit()
        {
            WindowMaximizer.Interval = 10;
            WindowMaximizer.Tick += WindowSizeChecker;
            WindowMaximizer.Start();
        }

        private static void WindowSizeChecker(object sender, EventArgs e)
        {
            if (Console.WindowHeight != Console.LargestWindowHeight || Console.WindowWidth != Console.LargestWindowWidth)
            {
                SetConsoleDisplayMode(hConsole, 1, out xy);
            }
        }

        /*[DllImport("user32.dll")]
        private static extern bool ShowWindow(IntPtr hWnd, int cmdShow);

        public static void Maximize()
        {
            Process p = Process.GetCurrentProcess();
            ShowWindow(p.MainWindowHandle, 3); //SW_MAXIMIZE = 3
        }*/
    }
}
