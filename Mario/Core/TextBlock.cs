using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mario.Core
{
    class TextBlock : ICore
    {
        public xList<Material> text = new xList<Material>();
        public int X { get; set; }
        public int Y { get; set; }

        public TextBlock() { }

        public TextBlock(int X, int Y)
        {
            this.X = X;
            this.Y = Y;
        }

        public void Text(string text)
        {
            this.text.Clear();

            for (int c = 0; c < text.Length; c++)
            {
                this.text.Add(new Material(ImageLoader.Load(ObjectDatabase.font[text[c]], ObjectDatabase.font_path)));
            }
        }

        public object Copy()
        {
            return (TextBlock) this.MemberwiseClone();
        }

        public void AddTo(List<object> destination)
        {
            destination.Add(this);
        }

        public object DeepCopy()
        {
            TextBlock retValue = (TextBlock)this.MemberwiseClone();

            retValue.text = (xList<Material>)text.DeepCopy();

            return retValue;
        }

        public void Render(byte[] destination, short[] destinationColor, int frameWidth, int frameHeight, int? layer, int? x, int? y)
        {
            int Xoffset = 0;

            for(int index = 0; index < text.Count(); index++)
            {
                if (index >= text.Count()) break;

                text[index].Render(destination, destinationColor, frameWidth, frameHeight, layer, this.X + Xoffset, this.Y);
                Xoffset += text[index].width + 1;
            }
        }
    }
}
