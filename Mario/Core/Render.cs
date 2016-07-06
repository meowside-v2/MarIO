using Mario.Data.Objects;
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

        public const int FRAME_RATE = 30;    // Frames per Second

        private byte[] temp_buffer = new byte[RENDER_HEIGHT * Render_WIDTH];
        private short[] temp_render_colors = new short[RENDER_HEIGHT * Render_WIDTH];

        private int X = 0;
        private int Y = 0;
        private int Xindex = 0;

        bool RenderBlocked = false;

        Thread Buff;
        Thread Ren;

        ManualResetEvent mrse = new ManualResetEvent(false);

        public void Init(List<object> world_objects)
        {
            Console.CursorVisible = true;
            Console.Title = "MarIO";
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;

            Buff = new Thread(() => Buffering(world_objects));
            Ren = new Thread(Rendering);
            Buff.Start();
            Ren.Start();
        }

        private void Buffering(List<object> world_objects)
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
                            Sized = true;
                            RenderBlocked = true;

                            Console.SetWindowSize(Render_WIDTH, RENDER_HEIGHT);
                            Console.SetBufferSize(Render_WIDTH, RENDER_HEIGHT);
                            
                        }
                        catch (ArgumentOutOfRangeException)
                        {
                            Sized = false;

                            Console.SetCursorPosition(0, 0);
                            Console.BackgroundColor = (ConsoleColor)ColorPalette.eColors.Black;
                            Console.ForegroundColor = (ConsoleColor)ColorPalette.eColors.White;
                            Console.WriteLine("Decrease your font size, set font to Raster font and press enter");
                            Console.ReadLine();
                            
                        }
                    } while (!Sized);

                    RenderBlocked = false;
                    mrse.Set();
                }

                buffer = Enumerable.Repeat(Convert.ToByte(32), buffer.Length).ToArray();
                render_colors = Enumerable.Repeat(Convert.ToInt16((short)ColorPalette.eColors.Black << 4), render_colors.Length).ToArray();

                List<object> temp = new List<object>(world_objects);

                foreach (var item in temp)
                {
                    if(item == null)
                    {
                        break;
                    }

                    if(item is Camera)
                    {
                        Camera cam = item as Camera;

                        X = cam.Xoffset;
                        Y = cam.Yoffset;
                    }

                    else if(item is List<Block>)
                    {
                        List<Block> layer = item as List<Block>;
                        bool newIndex = false;
                        int index = 0;

                        foreach (var block in layer.Skip(Xindex))
                        {
                            if (block.mesh == null) break;

                            if(!newIndex)
                                if (block.X + block.mesh.width > X && block.X < X + Render_WIDTH)
                                {
                                    Xindex = index;
                                    newIndex = true;
                                }

                            for (int row = 0; row < block.mesh.height; row++)
                            {
                                for (int column = 0; column < block.mesh.width; column++)
                                {
                                    if (block.X + column >= 0 && block.X + column < Render_WIDTH && block.Y + row < RENDER_HEIGHT && block.Y + row >= 0)
                                    {
                                        if (block.mesh.bitmapTransparent[row, column] == 255)
                                        {
                                            buffer[block.Y * Render_WIDTH + row * Render_WIDTH + block.X + column] = 219;
                                            render_colors[block.Y * Render_WIDTH + row * Render_WIDTH + block.X + column] = block.mesh.bitmapColor[row * block.mesh.width + column];

                                        }
                                    }
                                }
                            }
                        }
                    }

                    else if(item is Player)
                    {
                        Player player = item as Player;

                        for (int row = 0; row < player.mesh.height; row++)
                        {
                            for (int column = 0; column < player.mesh.width; column++)
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
                    }

                    else if(item is List<Enemy>)
                    {
                        List<Enemy> nearby = item as List<Enemy>;

                        foreach (var item2 in nearby)
                        {
                            if (item2.X + Render_WIDTH - 1 >= 0 && item2.X < Render_WIDTH && item2.Y + RENDER_HEIGHT - 1 >= 0 && item2.Y < RENDER_HEIGHT)
                            {
                                for (int row = 0; row < item2.mesh.height; row++)
                                {
                                    for (int column = 0; column < item2.mesh.width; column++)
                                    {
                                        if (item2.X + column >= 0 && item2.X + column < Render_WIDTH && item2.Y + row < RENDER_HEIGHT && item2.Y + row >= 0)
                                        {
                                            if (item2.mesh.bitmapTransparent[row, column] == 255)
                                            {
                                                buffer[item2.Y * Render_WIDTH + row * Render_WIDTH + item2.X + column] = 219;
                                                render_colors[item2.Y * Render_WIDTH + row * Render_WIDTH + item2.X + column] = item2.mesh.bitmapColor[row * item2.mesh.width + column];
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }

                    else if (item is Block)
                    {
                        Block block = item as Block;
                        
                        for (int row = 0; row < block.mesh.height; row++)
                        {
                            for (int column = 0; column < block.mesh.width; column++)
                            {
                                if (block.X + column >= 0 && block.X + column < Render_WIDTH && block.Y + row < RENDER_HEIGHT && block.Y + row >= 0)
                                {
                                    if (block.mesh.bitmapTransparent[row, column] == 255)
                                    {
                                        buffer[block.Y * Render_WIDTH + row * Render_WIDTH + block.X + column] = 219;
                                        render_colors[block.Y * Render_WIDTH + row * Render_WIDTH + block.X + column] = block.mesh.bitmapColor[row * block.mesh.width + column];

                                    }
                                }
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
                if (RenderBlocked)
                {
                    mrse.WaitOne();
                    mrse.Reset();
                }

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

        public void Resume()
        {
            mrse.Set();
        }

        public void Pause()
        {
            mrse.Reset();
        }
    }


    //
    //  Buffer save
    //

    /*
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
    */
}
