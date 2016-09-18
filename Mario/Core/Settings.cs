using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mario.Core
{
    class Settings
    {
        private static Rectangle maxResolution;
        private static Rectangle currentResolution;

        private static int[] posibleWidth =
        {
            800,
            1280,
            1366,
            1920
        };

        private static int[] posibleHeight =
        {
            640,
            720,
            768,
            1080
        };

        public static int availableMaxResolution { get; set; }

        public static void GetMaxScreenResolution()
        {
            maxResolution = Screen.PrimaryScreen.Bounds;

            availableMaxResolution = posibleWidth.ToList().FindLastIndex(x => x < maxResolution.Width) + 1;

            currentResolution = new Rectangle(0,
                                              0,
                                              posibleWidth[availableMaxResolution],
                                              posibleHeight[availableMaxResolution]);
        }

        public static Image SetNewResolution(int selection)
        {
            currentResolution = new Rectangle(0,
                                              0,
                                              posibleWidth[selection],
                                              posibleHeight[selection]);
            
            return new Bitmap(currentResolution.Width,
                                currentResolution.Height); ;
        }
    }
}
