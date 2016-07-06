using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Mario.Core;

namespace Mario.Data.Objects
{
    class World
    {
        public double Gravity { get; set; }
        public string Level { get; set; }

        public List<Block> foreground    = new List<Block>();
        public List<Block> middleground  = new List<Block>();
        public List<Block> background    = new List<Block>();

        public int width { get; set; }
        public int height { get; set; }

        public World() { }

        public World(int w, int h)
        {
            width = w;
            height = h;
        }

        public void Init(int lvl)
        {

        }
    }
}
