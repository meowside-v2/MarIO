using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mario_vNext.Core.SystemExt
{
    static class xGraphics
    {
        private static Bitmap temp = new Bitmap(1, 1);
        private static Graphics g = Graphics.FromImage(temp);

        public static SizeF MeasureString(string text, Font font)
        {
            return g.MeasureString(text, font);
        }
    }
}
