using Mario_vNext.Core.Interfaces;
using Mario_vNext.Core.SystemExt;
using Mario_vNext.Data.Objects;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mario_vNext.Core.Components
{
    abstract class Dialog : ICore, I3Dimensional
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }

        public int depth { get; }
        public int height { get; }
        public int width { get; }
        
        public double ScaleX { get; set; }
        public double ScaleY { get; set; }
        public double ScaleZ { get; set; }

        public string Caption { get; set; }

        protected xList<I3Dimensional> Content = new xList<I3Dimensional>();

        protected Material background;

        public object DeepCopy()
        {
            return MemberwiseClone();
        }

        public void Render(int X, int Y, byte[] bufferData, bool[] bufferKey)
        {
            foreach (ICore item in Content)
            {
                item.Render(X, Y, bufferData, bufferKey);
            }

            if (background != null) background.Render(X, Y, bufferData, bufferKey, 1, 1, Color.DarkCyan);
        }
    }

    class BlockDialog : Dialog
    {
        WorldObject toChange = new WorldObject();

        TextBlock coordX = new TextBlock()
        {
            HAlignment = TextBlock.HorizontalAlignment.Left,
            VAlignment = TextBlock.VerticalAlignment.Top,
            X = 2,
            Y = 20,
            Text = "X",
            HasShadow = true
        };

        TextBlock coordY = new TextBlock()
        {
            HAlignment = TextBlock.HorizontalAlignment.Left,
            VAlignment = TextBlock.VerticalAlignment.Top,
            X = 2,
            Y = 30,
            Text = "Y",
            HasShadow = true
        };
    }
}
