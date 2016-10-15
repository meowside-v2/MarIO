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
    class TextBox : TextBlock
    {
        public override string Text
        {
            get
            {
                return _text;
            }

            set
            {
                if (TextControl(value.ToArray()))
                {
                    _text = value;
                    Update();
                }
            }
        }

        public enum Type
        {
            All,
            AlphaNumerical,
            Alpha,
            Numerical
        };

        public Type AllowedChars { get; set; }

        public bool TextControl(params char[] key)
        {
            if (AllowedChars == Type.All)
                return true;

            foreach (char k in key)
            {
                if (AllowedChars == Type.Alpha)
                    if (!Char.IsLetter(k))
                        return false;

                else if (AllowedChars == Type.Numerical)
                    if (!Char.IsNumber(k))
                        return false;

                else if (AllowedChars == Type.AlphaNumerical)
                    if (!(Char.IsLetterOrDigit(k) || k == ' '))
                        return false;
            }

            return true;
        }
        
        public TextBox(int X,
                       int Y,
                       string Layer,
                       HAlignment HAlignment,
                       VAlignment VAlignment,
                       string Text,
                       FontFamily FontFamily,
                       float FontSize,
                       Color TextColor,
                       bool HasShadow,
                       Type AllowedChars)
            :base(X, Y, Layer, HAlignment, VAlignment, Text, FontFamily, FontSize, TextColor, HasShadow)
        {
            this.AllowedChars = AllowedChars;
        }

        public void RemoveLastLetter()
        {
            if (this.Text.Length > 0)
            {
                this.Text = Text.Remove(Text.Length - 1, 1);
            }
        }
    }
}
