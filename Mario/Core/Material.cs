using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mario.Core
{
    class Material
    {
        public byte[] graphics;
        public short[] bitmapColor;
        public byte[,] bitmapColorR;
        public byte[,] bitmapColorG;
        public byte[,] bitmapColorB;
        public byte[,] bitmapTransparent;
        public int width;
        public int height;

        public Material(Bitmap img)
        {
            Bitmap sourceBmp = img;
            bitmapColorR = new byte[sourceBmp.Height, sourceBmp.Width];
            bitmapColorG = new byte[sourceBmp.Height, sourceBmp.Width];
            bitmapColorB = new byte[sourceBmp.Height, sourceBmp.Width];
            bitmapTransparent = new byte[sourceBmp.Height, sourceBmp.Width];
            width = sourceBmp.Width;
            height = sourceBmp.Height;

            for (int i = 0; i < sourceBmp.Height; i++)
                for (int j = 0; j < sourceBmp.Width; j++)
                {
                    Color c = sourceBmp.GetPixel(j, i);
                    bitmapTransparent[i, j] = c.A;
                    bitmapColorR[i, j] = c.R;
                    bitmapColorG[i, j] = c.G;
                    bitmapColorB[i, j] = c.B;
                }
            
            graphics = Sprite();
            bitmapColor = ConsoleEnum();
        }

        public byte[] Sprite()
        {
            byte[] sprite = new byte[width * height];

            for (int row = 0; row < height; row++)
            {
                for (int column = 0; column < width; column++)
                {
                    sprite[column + row * width] = 32;
                    if (bitmapTransparent[row, column] != 0)
                        sprite[column + row * width] = 219;
                }
            }

            return sprite;
        }

        public short[] ConsoleEnum()
        {
            short[] index = new short[width * height];

            ConsoleColor[] xx = Coloring();

            int count = 0;

            foreach (var item in xx)
            {
                switch (item)
                {
                    case ConsoleColor.Black:
                        index[count++] = 0;
                        break;

                    case ConsoleColor.Blue:
                        index[count++] = 9;
                        break;

                    case ConsoleColor.Cyan:
                        index[count++] = 11;
                        break;

                    case ConsoleColor.DarkBlue:
                        index[count++] = 1;
                        break;

                    case ConsoleColor.DarkCyan:
                        index[count++] = 3;
                        break;

                    case ConsoleColor.DarkGray:
                        index[count++] = 8;
                        break;

                    case ConsoleColor.DarkGreen:
                        index[count++] = 2;
                        break;

                    case ConsoleColor.DarkMagenta:
                        index[count++] = 5;
                        break;

                    case ConsoleColor.DarkRed:
                        index[count++] = 4;
                        break;

                    case ConsoleColor.DarkYellow:
                        index[count++] = 6;
                        break;

                    case ConsoleColor.Gray:
                        index[count++] = 7;
                        break;

                    case ConsoleColor.Green:
                        index[count++] = 10;
                        break;

                    case ConsoleColor.Magenta:
                        index[count++] = 13;
                        break;

                    case ConsoleColor.Red:
                        index[count++] = 12;
                        break;

                    case ConsoleColor.White:
                        index[count++] = 15;
                        break;

                    case ConsoleColor.Yellow:
                        index[count++] = 14;
                        break;
                }
            }

            return index;
        }

        public ConsoleColor ClosestConsoleColor(byte r, byte g, byte b)
        {
            ConsoleColor ret = 0;
            double rr = r, gg = g, bb = b, delta = double.MaxValue;

            foreach (ConsoleColor cc in Enum.GetValues(typeof(ConsoleColor)))
            {
                var n = Enum.GetName(typeof(ConsoleColor), cc);
                var c = System.Drawing.Color.FromName(n == "DarkYellow" ? "Orange" : n); // bug fix
                var t = Math.Pow(c.R - rr, 2.0) + Math.Pow(c.G - gg, 2.0) + Math.Pow(c.B - bb, 2.0);
                if (t == 0.0)
                    return cc;
                if (t < delta)
                {
                    delta = t;
                    ret = cc;
                }
            }
            return ret;
        }

        public ConsoleColor[] Coloring()
        {
            ConsoleColor[] colors = new ConsoleColor[width * height];

            for(int row = 0; row < height; row++)
            {
                for(int column = 0; column < width; column++)
                {
                    colors[row * width + column] = ClosestConsoleColor(bitmapColorB[row, column], bitmapColorG[row, column], bitmapColorR[row, column]);
                }
            }

            return colors;
        }
    }
}
