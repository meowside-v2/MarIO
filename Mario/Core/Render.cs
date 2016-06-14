using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Mario.Core
{
    class Render
    {
        
        private const int Render_WIDTH = 300;
        private const int RENDER_HEIGHT = 100;

        public Render()
        {
            Console.CursorVisible = true;
            Console.Title = "MarIO";
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;

            Thread Buff = new Thread(new ThreadStart(Buffering));

            Buff.Start();

        }

        

        private static void Buffering()
        {
            DoubleBuffer screen = new DoubleBuffer();
            byte[] buffer = new byte[RENDER_HEIGHT * Render_WIDTH];
            short[] render_colors = new short[RENDER_HEIGHT * Render_WIDTH];

            bool Sized = false;
            
            while (true)
            {

                if (Console.WindowHeight != RENDER_HEIGHT || Console.WindowWidth != Render_WIDTH)
                {
                    do
                    {
                        try
                        {
                            Console.SetWindowSize(Render_WIDTH, RENDER_HEIGHT);
                            Console.SetBufferSize(Render_WIDTH, RENDER_HEIGHT);
                            Sized = true;
                        }
                        catch (ArgumentOutOfRangeException)
                        {
                            Console.SetCursorPosition(0, 0);
                            Console.WriteLine("Decrease your font size and press enter");
                            Console.ReadLine();
                            Sized = false;
                        }
                    } while (!Sized);
                }

                buffer = Enumerable.Repeat(Convert.ToByte(32), buffer.Length).ToArray();
                render_colors = Enumerable.Repeat(Convert.ToInt16((short)ConsoleColor.White << 4), render_colors.Length).ToArray();

                for(int row = 0; row < Program.map.mesh.height; row++)
                {
                    for(int column = 0; column < Program.map.mesh.width; column++)
                    {
                        if(Program.map.mesh.bitmapTransparent[row, column] == 255)
                        {
                            buffer[row * Program.map.mesh.width + column] = Program.map.mesh.bitmapTransparent[row, column];
                            render_colors[row * Program.map.mesh.width + column] = Program.map.mesh.bitmapColor[row * Program.map.mesh.width + column];
                        }
                    }
                }

                foreach (var item in Program.Objects)
                {
                    if (item.X + Render_WIDTH - 1 >= 0 && item.X < Render_WIDTH && item.Y + RENDER_HEIGHT - 1 >= 0 && item.Y < RENDER_HEIGHT)
                    {
                        for (int row = 0; row < item.mesh.height; row++)
                        {
                            for (int column = 0; column < item.mesh.width; column++)
                            {
                                if (item.X + column >= 0 && item.X + column < Render_WIDTH && item.Y + row < RENDER_HEIGHT && item.Y + row >= 0)
                                {
                                    if (item.mesh.bitmapTransparent[row, column] == 255)
                                    {
                                        buffer[item.Y * Render_WIDTH + row * Render_WIDTH + item.X + column] = 219;
                                        render_colors[item.Y * Render_WIDTH + row * Render_WIDTH + item.X + column] = item.mesh.bitmapColor[row * item.mesh.width + column];
                                    }
                                }
                            }
                        }
                    }
                }

                screen.Scr_Buffer(Render_WIDTH, RENDER_HEIGHT, render_colors, buffer);
            }
        }
    }
}
