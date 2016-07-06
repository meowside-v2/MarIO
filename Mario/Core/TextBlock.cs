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
            return this.MemberwiseClone();
        }

        public void AddTo(List<object> destination)
        {
            destination.Add(this);
        }
    }
}
