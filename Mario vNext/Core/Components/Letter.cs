using Mario_vNext.Core.Components;
using Mario_vNext.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mario_vNext.Core.Componenets
{
    class Letter : ICore, I3Dimensional, IGraphics
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }

        public int width
        {
            get
            {
                return model.width;
            }
        }

        public int height
        {
            get
            {
                return model.height;
            }
        }

        public int depth { get; }

        public Material model { get; set; }

        public Letter(int x, int y, int z, Material sourceModel)
        {
            model = sourceModel;

            this.X = x;
            this.Y = y;
            this.Z = z;
        }

        public object DeepCopy()
        {
            return this.MemberwiseClone();
        }

        public void Render(int x, int y, byte[] imageBuffer, bool[] imageBufferKey)
        {
            model.Render(X + x, Y + y, imageBuffer, imageBufferKey);
        }
    }
}
