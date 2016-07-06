using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mario.Core
{
    class TextBlock : ICore
    {
        Material[] text { get; set; }
        int X { get; set; }
        int Y { get; set; }

        public TextBlock() { }

        public void Text(string text)
        {
            for(int c = 0; c < text.Count(); c++)
            {
                //this.text[c] = ImageLoader.Load();
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
