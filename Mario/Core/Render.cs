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

        private static byte[] buffer = new byte[RENDER_HEIGHT * Render_WIDTH];

        private static short[] render_colors = new short[RENDER_HEIGHT * Render_WIDTH];

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
            ReTry:
                try
                {
                    Console.SetWindowSize(Render_WIDTH, RENDER_HEIGHT);
                    Console.SetBufferSize(Render_WIDTH, RENDER_HEIGHT);
                }
                catch (ArgumentOutOfRangeException)
                {
                    Console.SetCursorPosition(0, 0);
                    Console.WriteLine("Decrease your font size and press enter");
                    Console.ReadLine();
                    goto ReTry;
                }
            

            while (true)
            {

                if (Console.WindowHeight != RENDER_HEIGHT || Console.WindowWidth != Render_WIDTH)
                {
                    Console.SetWindowSize(Render_WIDTH, RENDER_HEIGHT);
                    Console.SetBufferSize(Render_WIDTH, RENDER_HEIGHT);
                }

                for (int row = 0; row < RENDER_HEIGHT; row++)
                {
                    for (int column = 0; column < Render_WIDTH; column++)
                    {
                        //buffer[row  * Render_WIDTH + column] = 32;
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

                DoubleBuffer img = new DoubleBuffer(Render_WIDTH, RENDER_HEIGHT, render_colors, buffer);
            }
        }
    }
}
