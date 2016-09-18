using Mario.Core;
using Mario.Data.Objects;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mario.Core
{
    class TextBlock : ICore, ICoordinated
    {
        public xList<Letter> text = new xList<Letter>();
        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }
        public TextBlock() { }

        public TextBlock(int X, int Y)
        {
            this.X = X;
            this.Y = Y;
        }

        public async void Text(string text)
        {
            this.text = await Task.Run(() =>
                        {

                            xList<Letter> textNew = new xList<Letter>();

                            for (int c = 0; c < text.Length; c++)
                            {
                                textNew.Add(new Letter(ObjectDatabase.letterMesh[(int)ObjectDatabase.font[Char.ToUpper(text[c])]]));
                            }

                            return textNew;

                        }
                        );
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

            retValue.text = (xList<Letter>)text.DeepCopy();

            return retValue;
        }

        public void Render(int x, int y)
        {
            int Xoffset = 0;

            for(int index = 0; index < text.Count(); index++)
            {
                if (index >= text.Count()) break;

                text[index].X = this.X + Xoffset;
                text[index].Y = this.Y;

                //text[index].Render(destination, destinationColor, frameWidth, frameHeight, 0, 0);
                Xoffset += text[index].mesh.width + 1;
            }
        }
    }
}
