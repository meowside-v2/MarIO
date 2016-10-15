using Mario_vNext.Core.Components;
using Mario_vNext.Core.Interfaces;
using Mario_vNext.Core.SystemExt;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Mario_vNext
{
    class Test
    {
        Bitmap test = new System.Drawing.Bitmap(1000, 100);

        Graphics graphics;
        
        public Test()
        {
            graphics = Graphics.FromImage(test);

            PrivateFontCollection pfc = new PrivateFontCollection();

            int fontLength = Properties.Resources.Pixel_Millennium.Length;

            // create a buffer to read in to
            byte[] fontdata = Properties.Resources.Pixel_Millennium;

            // create an unsafe memory block for the font data
            System.IntPtr data = Marshal.AllocCoTaskMem(fontLength);

            // copy the bytes to the unsafe memory block
            Marshal.Copy(fontdata, 0, data, fontLength);


            pfc.AddMemoryFont(data, fontLength);

            graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            graphics.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;

            Rectangle rectf = new Rectangle(-20, 0, 200, 50);
            StringFormat format = new StringFormat()
            {
                Alignment = StringAlignment.Near,
                LineAlignment = StringAlignment.Near
            };
            graphics.DrawString("AAA", new Font(pfc.Families[0], 48), Brushes.Coral, rectf, format);

            rectf = new Rectangle(20, 10, 200, 50);
            graphics.DrawString("BBB", new Font(pfc.Families[0], 48), Brushes.Aqua, rectf, format);

            graphics.Flush();

            test.Save("aha.jpg");

            Console.ReadLine();
        }
    }
}
