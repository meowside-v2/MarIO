﻿using Mario.Data.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Mario.Core
{
    class xRectangle : ICore, ICoordinated, I2Dimensional
    {
        xList<Block> border = new xList<Block>();

        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }
        public int width { get; set; }
        public int height { get; set; }
        
        public xRectangle(int x, int y, int width, int height)
        {
            this.X = x;
            this.Y = y;

            this.width = width;
            this.height = height;

            this.border.Clear();

            for(int i = 0; i <= width + 8; i += 8)
            {
                border.Add(new Block(X + i, Y, (int)ObjectDatabase.Blocks.Border));
                border.Add(new Block(X + i, Y + height + 8, (int)ObjectDatabase.Blocks.Border));
            }

            for(int i = 8; i <= height; i+= 8)
            {
                border.Add(new Block(X, Y + i, (int)ObjectDatabase.Blocks.Border));
                border.Add(new Block(X + width + 8, Y + i, (int)ObjectDatabase.Blocks.Border));
            }
        }

        public object Copy()
        {
            return this.MemberwiseClone();
        }

        public object DeepCopy()
        {
            xRectangle retValue = (xRectangle)this.MemberwiseClone();

            retValue.border = (xList<Block>)this.border.DeepCopy();

            return retValue;
        }

        public void Render(int x, int y)
        {
            //this.border.Render(destination, destinationColor, frameWidth, frameHeight, x, y);
        }

        Color ICore.Render(int x, int y)
        {
            throw new NotImplementedException();
        }
    }
}
