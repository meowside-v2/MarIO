using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mario.Core
{
    class World
    {
        public static double Gravity = 1f;
        
        public string Level { get; set; }

        public Material mesh;

        public World()
        {
            Image img = null;

            try
            {
                img = Image.FromFile(Environment.CurrentDirectory + "\\Data\\Sprites\\world_test.png");
            }
            catch
            {
                MessageBox.Show("World can't be loaded, please reinstall game!", "Error");
                Environment.Exit(0);
            }

            mesh = new Material((Bitmap)img);
        }
    }
}
