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
        I3Dimensional Parent { get; set; }
        Letter BeforeLetter { get; set; }

        private int _x;
        private int _y;

        public int X
        {
            get
            {
                if (BeforeLetter != null)
                    return (int)((BeforeLetter.X + BeforeLetter.width - 1) + (this.X + (this.X / ScaleX)));

                else
                    return (int)(this.X + (this.X / ScaleX));
            }
            set
            {
                _x = value;
            }
        }

        public int Y
        {
            get
            {
                if (BeforeLetter != null)
                    return (int)((BeforeLetter.Y + BeforeLetter.height - 1) + (this.Y + (this.Y / ScaleY)));

                else
                    return (int)(this.Y + (this.Y / ScaleY));
            }
            set
            {
                _y = value;
            }
        }

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

        public double ScaleX
        {
            get
            {
                return Parent.ScaleX;
            }

            set { }
        }

        public double ScaleY
        {
            get
            {
                return Parent.ScaleY;
            }

            set { }
        }

        public double ScaleZ
        {
            get
            {
                return Parent.ScaleZ;
            }

            set { }
        }

        /*public int ScaledX
        {
            get
            {
                if (BeforeLetter != null)
                    return (int)((BeforeLetter.ScaledX + BeforeLetter.width - 1) + (this.X + (this.X / ScaleX)));

                else
                    return (int)(this.X + (this.X / ScaleX));
            }
        }

        public int ScaledY
        {
            get
            {
                return (int)(Y / ScaleY);
            }
        }

        public int ScaledZ
        {
            get
            {
                return (int)(Y / ScaleY);
            }
        }*/

        public int depth { get; }

        public Material model { get; set; }

        public Letter(I3Dimensional Parent, Letter BeforeLetter, int x, int y, int z, Material sourceModel)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;

            this.Parent = Parent;
            this.BeforeLetter = BeforeLetter;

            model = new Material(this, sourceModel);
        }

        public object DeepCopy()
        {
            return this.MemberwiseClone();
        }

        public void Render(int x, int y, byte[] imageBuffer, bool[] imageBufferKey)
        {
            model.Render(this.X + x, this.Y + y, imageBuffer, imageBufferKey);
        }
    }
}
