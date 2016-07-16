using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mario.Core
{

    class Material : ICore
    {
        public short[] bitmapColor;
        public byte[,] bitmapColorR;
        public byte[,] bitmapColorG;
        public byte[,] bitmapColorB;
        public byte[,] bitmapTransparent;
        public int width;
        public int height;
        
        public Material(Image img)
        {
            Bitmap sourceBmp = (Bitmap)img;
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
            
            bitmapColor = ConsoleEnum();
        }
        
        private short[] ConsoleEnum()
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

        private ConsoleColor ClosestConsoleColor(string hex)
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

        private ConsoleColor[] Coloring()
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

        public object Copy()
        {
            return this.MemberwiseClone();
        }

        public void AddTo(List<object> destination)
        {
            destination.Add(this);
        }

        public object DeepCopy()
        {
            return (Material) this.MemberwiseClone();
        }

        public void Render(byte[] destination, short[] destinationColor, int frameWidth, int frameHeight, int? layer = null, int? x = null, int? y = null)
        {
            if (x + width < 0 || x > frameWidth) return;

            for (int row = 0; row < height; row++)
            {
                for (int column = 0; column < width; column++)
                {
                    if(x + column >= 0 && x + column < frameWidth && y + row < frameHeight && y + row >= 0)
                    {
                        if(bitmapTransparent[row, column] != 0)
                        {
                            if(destination[((int)y + row) * frameWidth + (int)x + column] != 219)
                            {
                                if(layer == 1)destination[((int)y + row) * frameWidth + (int)x + column] = 219;
                                destinationColor[((int)y + row) * frameWidth + (int)x + column] = ((int)layer == 0 ? (short)(bitmapColor[row * width + column] << 4) : bitmapColor[row * width + column]);
                            }
                        }
                    }
                }
            }
        }
    }
}
