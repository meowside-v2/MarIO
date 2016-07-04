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

        public const int Render_WIDTH = 300;
        public const int RENDER_HEIGHT = 100;

        public const int FRAME_RATE = 120;    // Frames per Second

        private byte[] temp_buffer = new byte[RENDER_HEIGHT * Render_WIDTH];
        private short[] temp_render_colors = new short[RENDER_HEIGHT * Render_WIDTH];
        
        public void Init(Player player, World world, List<Enemy> nearby)
        {
            Console.CursorVisible = true;
            Console.Title = "MarIO";
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;

            Thread Buff = new Thread(() => Buffering(world, player, nearby));
            Thread Ren = new Thread(Rendering);
            Buff.Start();
            Ren.Start();
        }

        private void Buffering(World map, Player player, List<Enemy> nearby)
        {
            
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
                render_colors = Enumerable.Repeat(Convert.ToInt16((short)ColorPalette.eColors.Black << 4), render_colors.Length).ToArray();

                for(int row = 0; row < map.mesh.height; row++)
                {
                    for(int column = 0; column < map.mesh.width; column++)
                    {
                        if(map.mesh.bitmapTransparent[row, column] == 255)
                        {
                            buffer[row * map.mesh.width + column] = map.mesh.bitmapTransparent[row, column];
                            render_colors[row * map.mesh.width + column] = map.mesh.bitmapColor[row * map.mesh.width + column];
                        }
                    }
                }

                foreach (var item in nearby)
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

                for(int row = 0; row < player.mesh.height; row++)
                {
                    for( int column = 0; column < player.mesh.width; column++)
                    {
                        if (player.X + column >= 0 && player.X + column < Render_WIDTH && player.Y + row < RENDER_HEIGHT && player.Y + row >= 0)
                        {
                            if (player.mesh.bitmapTransparent[row, column] == 255)
                            {
                                buffer[player.Y * Render_WIDTH + row * Render_WIDTH + player.X + column] = 219;
                                render_colors[player.Y * Render_WIDTH + row * Render_WIDTH + player.X + column] = player.mesh.bitmapColor[row * player.mesh.width + column];
                                
                            }
                        }
                    }
                }
                
                Array.Copy(buffer, temp_buffer, buffer.Length);
                Array.Copy(render_colors, temp_render_colors, render_colors.Length);

            }
        }

        private void Rendering()
        {
            Stopwatch delay = new Stopwatch();
            DoubleBuffer screen = new DoubleBuffer();
            delay.Start();

            byte[] buffer = new byte[RENDER_HEIGHT * Render_WIDTH];
            short[] render_colors = new short[RENDER_HEIGHT * Render_WIDTH];

            while (true)
            {
                Array.Copy(temp_buffer, buffer, buffer.Length);
                Array.Copy(temp_render_colors, render_colors, render_colors.Length);

                screen.Scr_Buffer(Render_WIDTH, RENDER_HEIGHT, render_colors, buffer);
                Vsync(FRAME_RATE, (int)delay.ElapsedMilliseconds);

                delay.Restart();
            }
        }

        private void Vsync(int TargetFrameRate, int ImageRenderDelay)
        {
            int targetDelay = 1000 / TargetFrameRate;

            if (ImageRenderDelay < targetDelay)
            {
                Thread.Sleep(targetDelay - ImageRenderDelay);
            }
        }
    }

}
