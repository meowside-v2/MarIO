using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Mario.Core
{
    class DoubleBuffer
    {
        const int STD_OUTPUT_HANDLE = -11;

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern IntPtr GetStdHandle(int nStdHandle);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool WriteConsoleOutput(
          IntPtr hConsoleOutput,
          CharInfo[] lpBuffer,
          Coord dwBufferSize,
          Coord dwBufferCoord,
          ref SmallRect lpWriteRegion);

        [StructLayout(LayoutKind.Sequential)]
        public struct Coord
        {
            public short X;
            public short Y;

            public Coord(short X, short Y)
            {
                this.X = X;
                this.Y = Y;
            }
        };

        [StructLayout(LayoutKind.Explicit)]
        public struct CharUnion
        {
            [FieldOffset(0)]
            public char UnicodeChar;
            [FieldOffset(0)]
            public byte AsciiChar;
        }

        [StructLayout(LayoutKind.Explicit)]
        public struct CharInfo
        {
            [FieldOffset(0)]
            public CharUnion Char;
            [FieldOffset(2)]
            public short Attributes;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct SmallRect
        {
            public short Left;
            public short Top;
            public short Right;
            public short Bottom;
        }

        public void Scr_Buffer(int x, int y, int size, short[] clr, byte[] chr)
        {
            CharInfo[] buf = new CharInfo[size];
            
            for (int index = 0; index < size; index++)
            {
                
                buf[index].Char.AsciiChar = chr[index];
                buf[index].Attributes = clr[index];
                
            }

            SmallRect rect = new SmallRect() { Left = 0, Top = 0, Right = (short)x, Bottom = (short)y };
            bool b = WriteConsoleOutput(
                        GetStdHandle(STD_OUTPUT_HANDLE), buf,
                        new Coord() { X = (short)x, Y = (short)y },
                        new Coord() { X = 0, Y = 0 },
                        ref rect);
        }
    }
}
