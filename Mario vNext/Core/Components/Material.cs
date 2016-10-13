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

        public void Render(int x, int y, byte[] imageBuffer, bool[] imageBufferKey)
        {
            for(int row = 0; row < this.height; row++)
            {
                for(int column = 0; column < this.width; column++)
                {
                    if (IsOnScreen(x, y, row, column))
                    {
                        int offset = ((3 * (y + row)) * Shared.RenderWidth) + (3 * (x + column));
                        int keyOffset = (y + row) * Shared.RenderWidth + x + column;

                        if (!imageBufferKey[keyOffset])
                        {
                            Color temp = colorMap[column, row];

                            if(temp.A != 0)
                            {
                                imageBuffer[offset] = temp.B;
                                imageBuffer[offset + 1] = temp.G;
                                imageBuffer[offset + 2] = temp.R;

                                imageBufferKey[keyOffset] = true;
                            }
                        }
                    }
                }
            }
        }

        private bool IsOnScreen(int x, int y, int row, int column)
        {
            return x + column >= 0 && x + column < Shared.RenderWidth && y + row >= 0 && y + row < Shared.RenderHeight;
        }
    }
}
