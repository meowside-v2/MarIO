using Mario_vNext.Core.Componenets;
using Mario_vNext.Core.Interfaces;
using Mario_vNext.Core.SystemExt;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace Mario_vNext.Core.Components
{
    class TextBlock : ICore, I3Dimensional
    {
        
        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }

        private int vertOffset { get; set; }
        private int horiOffset { get; set; }

        public enum HorizontalAlignment
        {
            Left,
            Center,
            Right
        };

        public enum VerticalAlignment
        {
            Top,
            Center,
            Bottom
        };

        private HorizontalAlignment _HA;
        private VerticalAlignment _VA;

        public HorizontalAlignment HAlignment
        {
            set
            {
                _HA = value;

                switch (value)
                {
                    case HorizontalAlignment.Left:
                        horiOffset = 0;
                        break;

                    case HorizontalAlignment.Center:
                        horiOffset = (Shared.RenderWidth - this.width) / 2;
                        break;

                    case HorizontalAlignment.Right:
                        horiOffset = Shared.RenderWidth - this.width;
                        break;

                    default:
                        break;
                }
            }
        }

        public VerticalAlignment VAlignment
        {
            set
            {
                _VA = value;

                switch (value)
                {
                    case VerticalAlignment.Top:
                        vertOffset = 0;
                        break;

                    case VerticalAlignment.Center:
                        vertOffset = (Shared.RenderHeight - this.height) / 2;
                        break;

                    case VerticalAlignment.Bottom:
                        vertOffset = Shared.RenderHeight - this.height;
                        break;

                    default:
                        break;
                }
            }
        }

        public TextBlock() { }

        public TextBlock(int x, int y, int z)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }

        public TextBlock(int x, int y, string layer)
        {
            this.X = x;
            this.Y = y;

            switch (layer)
            {
                case "GUI":
                    this.Z = 100;
                    break;
                    
                case "Background":
                    this.Z = 0;
                    break;
            }
        }

        private string _stringText;
        private xList<Letter> _text = new xList<Letter>();

        public string text
        {
            set
            {
                _stringText = value;

                Task.Factory.StartNew(() => Text(value));

                VAlignment = _VA;
                HAlignment = _HA;
            }

            get
            {
                return _stringText;
            }
        }

        public int width
        {
            get
            {
                return (_text.Count > 0 ? _text[_text.Count - 1].X + _text[_text.Count - 1].width - 1 : 0);
            }
        }

        public int height
        {
            get
            {
                return (_text.Count > 0 ? _text[0].height : 0);
            }
        }
        public int depth
        {
            get
            {
                return 0;
            }
        }
        

        private void Text(string txt)
        {
            xList<Letter> retValue = new xList<Letter>();

            int Xoffset = 0;

            foreach (char letter in txt)
            {
                if (letter == ' ')
                {
                    Xoffset += 3;
                }

                else
                {
                    retValue.Add(new Letter(Xoffset,
                                        this.Y,
                                        this.Z,
                                        ObjectDatabase.letterMesh[(int)ObjectDatabase.font[Char.ToUpper(letter)]]));

                    Xoffset += ObjectDatabase.letterMesh[(int)ObjectDatabase.font[Char.ToUpper(letter)]].width + 1;
                }
            }

            _text = retValue;
        }

        public object DeepCopy()
        {
            TextBlock retVal = (TextBlock) this.MemberwiseClone();

            retVal.text = "";

            foreach(Letter letter in _text.ToList())
            {
                retVal._text.Add((Letter) letter.DeepCopy());
            }

            return retVal;
        }

        public void Render(int x, int y, byte[] imageBuffer, bool[] imageBufferKey)
        {
            foreach(Letter item in _text.FindAll(obj => Finder(obj as I3Dimensional, x, y)))
            {
                item.Render(this.X - x + horiOffset, y + vertOffset, imageBuffer, imageBufferKey);
            }
        }

        private bool Finder(I3Dimensional obj, int x, int y)
        {
            return obj.X + obj.width >= x && obj.X < x + Shared.RenderWidth && obj.Y + obj.height >= y && obj.Y < y + Shared.RenderHeight;
        }
    }
}
