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
        private int vertOffset;
        private int horiOffset;

        private int _x = 0;
        private int _y = 0;

        public int X
        {
            get
            {
                if (this.HAlignment == HorizontalAlignment.Left)
                    return _x;

                if (this.HAlignment == HorizontalAlignment.Center)
                    return (int)((_x + horiOffset) - ((_x + horiOffset) / ScaleX));

                if (this.HAlignment == HorizontalAlignment.Right)
                    return (int)(horiOffset - (width * (ScaleX - 1)));

                return 0;
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
                if (this.VAlignment == VerticalAlignment.Top)
                    return _y;

                if (this.VAlignment == VerticalAlignment.Center)
                    return (int)((_y + vertOffset) - ((_y + vertOffset) / ScaleY));

                if (this.VAlignment == VerticalAlignment.Bottom)
                    return (int)(vertOffset - (height * (ScaleY - 1)));

                return 0;
            }
            set
            {
                _y = value;
            }
        }

        public int Z { get; set; }

        private double _scaleX = 1f;
        private double _scaleY = 1f;
        private double _scaleZ = 1f;

        public double ScaleX
        {
            get
            {
                return _scaleX;
            }
            set
            {
                if (value > 0)
                    _scaleX = (float)value;

                else
                    _scaleX = 0.1f;
            }
        }
        public double ScaleY
        {
            get
            {
                return _scaleY;
            }
            set
            {
                if (value > 0)
                    _scaleY = (float)value;

                else
                    _scaleY = 0.1f;
            }
        }
        public double ScaleZ
        {
            get
            {
                return _scaleZ;
            }
            set
            {
                if (value > 0)
                    _scaleZ = (float)value;

                else
                    _scaleZ = 0.1f;
            }
        }

        /*public int ScaledX
        {
            get
            {
                return (int)((X + horiOffset) - ((X + horiOffset) / ScaleX));
            }
        }

        public int ScaledY
        {
            get
            {
                return (int)((Y + vertOffset) - ((Y + vertOffset) / ScaleY));
            }
        }

        public int ScaledZ
        {
            get
            {
                return (int)(Z - (Z / ScaleZ));
            }
        }*/

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
            
            get
            {
                return _HA;
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
            
            get
            {
                return _VA;
            }
        }

        public TextBlock() { }
        public TextBlock(int X, int Y, int Z)
        {
            this.X = X;
            this.Y = Y;
            this.Z = Z;
        }
        public TextBlock(int X, int Y, int Z, HorizontalAlignment HAlignment, VerticalAlignment VAlignment, string Text)
        {
            this.X = X;
            this.Y = Y;
            this.Z = Z;

            _HA = HAlignment;
            _VA = VAlignment;

            this.Text = Text;
        }
        public TextBlock(int X, int Y, string Layer)
        {
            this.X = X;
            this.Y = Y;

            switch (Layer)
            {
                case "GUI":
                    this.Z = 100;
                    break;

                case "Background":
                    this.Z = 0;
                    break;
            }
        }
        public TextBlock(int X, int Y, string Layer, HorizontalAlignment HAlignment, VerticalAlignment VAlignment, string Text)
        {
            this.X = X;
            this.Y = Y;

            switch (Layer)
            {
                case "GUI":
                    this.Z = 100;
                    break;

                case "Background":
                    this.Z = 0;
                    break;
            }

            _HA = HAlignment;
            _VA = VAlignment;

            this.Text = Text;
        }

        private string _stringText;
        private xList<Letter> _text = new xList<Letter>();

        public string Text
        {
            set
            {
                Task.Factory.StartNew(() => TextRasterize(value));
                //TextRasterize(value);
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
        

        private void TextRasterize(string txt)
        {
            _stringText = txt;

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
                    retValue.Add(new Letter(this,
                                            (_text.Count > 0 ? _text[_text.Count - 1] : null),
                                            Xoffset,
                                            0,
                                            0,
                                            ObjectDatabase.letterMesh[(int)ObjectDatabase.font[Char.ToUpper(letter)]]));

                    Xoffset += ObjectDatabase.letterMesh[(int)ObjectDatabase.font[Char.ToUpper(letter)]].width + 1;
                }
            }

            _text = retValue;

            VAlignment = _VA;
            HAlignment = _HA;
        }

        public object DeepCopy()
        {
            TextBlock retVal = (TextBlock) this.MemberwiseClone();

            retVal.Text = "";

            foreach(Letter letter in _text.ToList())
            {
                retVal._text.Add((Letter) letter.DeepCopy());
            }

            return retVal;
        }

        public void Render(int x, int y, byte[] imageBuffer, bool[] imageBufferKey)
        {
            foreach(Letter item in _text.FindAll(obj => Finder(obj, x, y)))
            {
                item.Render(this.X + x, this.Y + y, imageBuffer, imageBufferKey);
            }
        }

        private bool Finder(I3Dimensional obj, int x, int y)
        {
            return obj.X + obj.width >= x && obj.X < x + Shared.RenderWidth && obj.Y + obj.height >= y && obj.Y < y + Shared.RenderHeight;
        }
    }
}
