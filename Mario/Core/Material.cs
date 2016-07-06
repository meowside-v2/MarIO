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
                index[count] = (short)xx[count++];
            }

            return index;
        }

        public ConsoleColor ClosestConsoleColor(string hex)
        {
            int counter = 0;

            foreach (ConsoleColor cc in Enum.GetValues(typeof(ConsoleColor)))
            {
                if (ColorPalette.color[counter++] == hex)
                {
                    return cc;
                }
            }

            return 0;
        }

        private string GetHexClr(byte R, byte G, byte B)
        {
            return string.Format("{0:X2}{1:X2}{2:X2}", R, G, B);
        }

        public ConsoleColor[] Coloring()
        {
            ConsoleColor[] colors = new ConsoleColor[width * height];

            for (int row = 0; row < height; row++)
            {
                for(int column = 0; column < width; column++)
                {
                    colors[row * width + column] = ClosestConsoleColor(GetHexClr(bitmapColorR[row, column], bitmapColorG[row, column], bitmapColorB[row, column]));
                }
            }

            return colors;
        }
    }
}
