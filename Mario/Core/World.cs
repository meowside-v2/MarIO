using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mario.Core
{
    class World
    {
        public static double Gravity = 1f;
        
        public string Level { get; set; }

        public Material mesh;

        public World()
        {
            mesh = new Material((Bitmap)Image.FromFile(Environment.CurrentDirectory + "\\Data\\Sprites\\world_test.png"));
        }
    }
}
