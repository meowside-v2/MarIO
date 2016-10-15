using Mario_vNext.Core.Interfaces;
using Mario_vNext.Core.SystemExt;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mario_vNext.Core.Components
{
    class TextBlock : ICore, I3Dimensional
    {
        public int X
        {
            get
            {
                return _x + HOffset;
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
                return _y + VOffset;
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
                return (HasShadow ? _width + 1 : _width);
            }
        }
        public int height
        {
            get
            {
                return Font.Height;
            }
        }
        public int depth { get; set; }
        
        public virtual string Text
        {
            get
            {
                return _text;
            }
            set
            {
                _text = value;
            }
        }

        public bool HasShadow { get; set; }

        protected Material rastered;

        protected SolidBrush _shadowBrush = new SolidBrush(Color.FromArgb(0x55, 0x00, 0x00, 0x00));

        protected Font Font;

        protected string _text;
        protected double _fontSize = 12f;
        protected FontFamily _fontFamily = FontFamily.Families[0];
        protected int HOffset = 0;
        protected int VOffset = 0;
        protected HAlignment _HA;
        protected VAlignment _VA;

        protected SolidBrush _outputBrush = new SolidBrush(Color.White);

        protected int _width;
        protected int _x;
        protected int _y;

        public enum HAlignment
        {
            Left,
            Center,
            Right
        }

        public enum VAlignment
        {
            Top,
            Center,
            Bottom
        }

        public HAlignment HorizontalAlignment
        {
            get
            {
                return _HA;
            }

            set
            {
                _HA = value;
                
                switch (_HA)
                {
                    case HAlignment.Left:
                        HOffset = 0;
                        break;
                    case HAlignment.Center:
                        HOffset = (Shared.RenderWidth - width) / 2; 
                        break;
                    case HAlignment.Right:
                        HOffset = Shared.RenderWidth - width;
                        break;
                    default:
                        break;
                }
            }
        }

        public VAlignment VerticalAlignment
        {
            get
            {
                return _VA;
            }

            set
            {
                _VA = value;

                switch (_VA)
                {
                    case VAlignment.Top:
                        VOffset = 0;
                        break;
                    case VAlignment.Center:
                        VOffset = (Shared.RenderHeight - Font.Height) / 2;
                        break;
                    case VAlignment.Bottom:
                        VOffset = Shared.RenderWidth - Font.Height;
                        break;
                    default:
                        break;
                }
            }
        }

        public double FontSize
        {
            get
            {
                return _fontSize;
            }
            set
            {
                if (value > 0)
                {
                    _fontSize = value;
                    Font = new Font(_fontFamily, (float)value);
                }


                else
                {
                    _fontSize = 0.1f;
                    Font = new Font(_fontFamily, (float)value);
                }
            }
        }

        
        public FontFamily FontFamily
        {
            get
            {
                return _fontFamily;
            }
            set
            {
                _fontFamily = value;
                Font = new Font(value, (float)_fontSize);
            }
        }

        

        public Color Color
        {
            get
            {
                return _outputBrush.Color;
            }
            set
            {
                _outputBrush = new SolidBrush(value);
            }
        }

        public TextBlock(int X,
                         int Y,
                         string Layer,
                         HAlignment HAlignment,
                         VAlignment VAlignment,
                         string Text,
                         FontFamily FontFamily,
                         float FontSize,
                         Color TextColor,
                         bool HasShadow)
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

                default:
                    try
                    {
                        this.Z = int.Parse(Layer);
                    }

                    catch
                    {
                        this.Z = 0;
                    }
                    break;
            }
            
            this.FontFamily = FontFamily;
            this.FontSize = FontSize;

            this.HasShadow = HasShadow;

            this.Color = TextColor;

            this.HorizontalAlignment = HAlignment;
            this.VerticalAlignment = VAlignment;
            
            this.Text = Text;

            Update();
        }

        private void Rasterize()
        {
            Bitmap temp = new Bitmap(width, height, System.Drawing.Imaging.PixelFormat.Format32bppRgb);

            temp.MakeTransparent();

            lock (Text)
            {
                using (Graphics g = Graphics.FromImage(temp))
                {
                    if (this.HasShadow) g.DrawString(Text, Font, _shadowBrush, 1f, 1f);
                    g.DrawString(Text, Font, _outputBrush, 0f, 0f);
                }
            }
            
            rastered = new Material(temp);
        }

        protected void Update()
        {
            _width = (int)xGraphics.MeasureString(Text, Font).Width;

            HorizontalAlignment = _HA;
            VerticalAlignment = _VA;

            Rasterize();
        }

        public void Render(int x, int y, byte[] bufferData, bool[] bufferKey)
        {
            if (rastered != null) rastered.Render(X - x, Y - y, bufferData, bufferKey);
        }

        public object DeepCopy()
        {
            return this.MemberwiseClone();
        }
    }
}
