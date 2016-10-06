using Mario_vNext.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mario_vNext.Core.Components
{
    class Material : ICore
    {

        /*private byte[,] _R;
        private byte[,] _G;
        private byte[,] _B;*/

        public int width;
        public int height;

        public Color[,] colorMap;


        public Material(Bitmap source)
        {
            colorMap = new Color[source.Width, source.Height];

            width = source.Width;
            height = source.Height;

            for(int row = 0; row < source.Height; row++)
            {
                for(int column = 0; column < source.Width; column++)
                {
                    colorMap[column, row] = source.GetPixel(column, row);
                }
            }
        }

        public string PixelToString(int x, int y)
        {
            return string.Format("#{0:X2}{1:X2}{2:X2}{3:X2}", this.colorMap[x, y].A,
                                                              this.colorMap[x, y].R,
                                                              this.colorMap[x, y].G,
                                                              this.colorMap[x, y].B);
        }

        public object DeepCopy()
        {
            return this.MemberwiseClone();
        }

        public void Render(int x, int y, byte[] imageBuffer)
        {
            for(int row = 0; row < this.height; row++)
            {
                for(int column = 0; column < this.width; column++)
                {
                    int offset = ((4 * (y + row)) * Shared.RenderWidth) + (4 * (x + column));

                    imageBuffer[offset] = colorMap[column, width].B;
                    imageBuffer[offset + 1] = colorMap[column, width].G;
                    imageBuffer[offset + 2] = colorMap[column, width].R;
                    imageBuffer[offset + 3] = colorMap[column, width].A;
                }
            }
        }
    }
}
