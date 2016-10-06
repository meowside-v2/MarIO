﻿using Mario_vNext.Core.SystemExt;
using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

namespace Mario_vNext.Core.Components
{
    class Camera
    {

        public int Xoffset { set; get; }
        public int Yoffset { set; get; }

        private const int MAX_FRAME_RATE = 60;    // Frames per Second
        private const short sampleSize = 100;
        private int lastTime = 0;
        private int numRenders = 0;
        private bool _Vsync = true;
        
        private TextBlock fpsMeter = new TextBlock();

        private byte[] _tempBuffer = new byte[4 * Shared.RenderHeight * Shared.RenderWidth]; 

        private BaseHiararchy referenceToWorld;
        private BaseHiararchy temp_objects;

        public void Init(int Xoffset, int Yoffset, BaseHiararchy objectsToRender)
        {
            referenceToWorld = objectsToRender;

            this.Xoffset = Xoffset;
            this.Yoffset = Yoffset;

            fpsMeter.X = 1;
            fpsMeter.Y = Shared.RenderHeight - 6;

            //fpsMeter.text = "";
            
            Thread Buff = new Thread(() => Buffering());
            Buff.Start();
            Thread Ren = new Thread(() => Rendering());
            Ren.Start();
        }

        private void Rendering()
        {
            while (true)
            {
                int beginRender = Environment.TickCount;

                Point location = new Point(0, 0);
                Size imageSize = new Size(Console.WindowWidth, Console.WindowHeight); // desired image size in characters

                unsafe
                {
                    fixed (byte *ptr = _tempBuffer)
                    {
                        using (Graphics g = Graphics.FromHwnd(GetConsoleWindow()))
                        {
                            using (Bitmap outFrame = new Bitmap(Shared.RenderWidth,
                                                                Shared.RenderHeight,
                                                                3 * Shared.RenderWidth,
                                                                System.Drawing.Imaging.PixelFormat.Format32bppArgb,
                                                                new IntPtr(ptr)))
                            {
                                Size fontSize = GetConsoleFontSize();

                                Rectangle imageRect = new Rectangle(location.X * fontSize.Width,
                                                                    location.Y * fontSize.Height,
                                                                    imageSize.Width * fontSize.Width,
                                                                    imageSize.Height * fontSize.Height);

                                g.DrawImage(outFrame, imageRect);
                            }
                        }
                    }
                }
                

                int endRender = Environment.TickCount - beginRender;

                Vsync(MAX_FRAME_RATE, endRender, false);
            }
        }

        private void Buffering()
        {
            byte[] _buffer = new byte[4 * Shared.RenderHeight * Shared.RenderWidth];
            
            while (true)
            {
                int beginRender = Environment.TickCount;
                
                temp_objects = (BaseHiararchy) referenceToWorld.DeepCopy();

                temp_objects.Render(Xoffset, Yoffset, _buffer);

                Buffer.BlockCopy(_buffer, 0, _tempBuffer, 0, _buffer.Count());

                int endRender = Environment.TickCount - beginRender;

                if (_Vsync) Vsync(MAX_FRAME_RATE, endRender, true);

                if (numRenders == 0)
                {
                    lastTime = Environment.TickCount;
                }

                numRenders++;
            }
        }

        private void Vsync(int TargetFrameRate, int imageRenderDelay, bool renderFPS)
        {
            int targetDelay = 1000 / TargetFrameRate;

            if (imageRenderDelay < targetDelay)
            {
                Thread.Sleep(targetDelay - imageRenderDelay);
            }

            if (renderFPS)
            {
                if (numRenders == sampleSize)
                {
                    int temp = Environment.TickCount - lastTime;

                    if (temp > 0)
                    {
                        fpsMeter.text = string.Format("{0}", sampleSize * 1000 / temp);
                        Debug.WriteLine(string.Format("Buff {0}", sampleSize * 1000 / temp));
                    }

                    numRenders = 0;
                }
            }
        }
        

        private static Size GetConsoleFontSize()
        {
            // getting the console out buffer handle
            IntPtr outHandle = CreateFile("CONOUT$",
                                           GENERIC_READ | GENERIC_WRITE,
                                           FILE_SHARE_READ | FILE_SHARE_WRITE,
                                           IntPtr.Zero,
                                           OPEN_EXISTING,
                                           0,
                                           IntPtr.Zero);

            int errorCode = Marshal.GetLastWin32Error();
            if (outHandle.ToInt32() == INVALID_HANDLE_VALUE)
            {
                throw new IOException("Unable to open CONOUT$", errorCode);
            }

            ConsoleFontInfo cfi = new ConsoleFontInfo();

            if (!GetCurrentConsoleFont(outHandle, false, cfi))
            {
                throw new InvalidOperationException("Unable to get font information.");
            }

            return new Size(cfi.dwFontSize.X, cfi.dwFontSize.Y);
        }

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern IntPtr GetConsoleWindow();

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern IntPtr CreateFile(
            string lpFileName,
            int dwDesiredAccess,
            int dwShareMode,
            IntPtr lpSecurityAttributes,
            int dwCreationDisposition,
            int dwFlagsAndAttributes,
            IntPtr hTemplateFile);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool GetCurrentConsoleFont(
            IntPtr hConsoleOutput,
            bool bMaximumWindow,
            [Out][MarshalAs(UnmanagedType.LPStruct)]ConsoleFontInfo lpConsoleCurrentFont);

        [StructLayout(LayoutKind.Sequential)]
        internal class ConsoleFontInfo
        {
            internal int nFont;
            internal Coord dwFontSize;
        }

        [StructLayout(LayoutKind.Explicit)]
        internal struct Coord
        {
            [FieldOffset(0)]
            internal short X;
            [FieldOffset(2)]
            internal short Y;
        }

        private const int GENERIC_READ = unchecked((int)0x80000000);
        private const int GENERIC_WRITE = 0x40000000;
        private const int FILE_SHARE_READ = 1;
        private const int FILE_SHARE_WRITE = 2;
        private const int INVALID_HANDLE_VALUE = -1;
        private const int OPEN_EXISTING = 3;
    }
}