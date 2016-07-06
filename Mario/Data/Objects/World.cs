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
    class World : ICore
    {
        public double Gravity { get; set; }
        public string Level { get; set; }

        public xList<Block> foreground    = new xList<Block>();
        public xList<Block> middleground  = new xList<Block>();
        public xList<Block> background    = new xList<Block>();

        public int width { get; set; }
        public int height { get; set; }

        public World() { }

        public World(int width, int height)
        {
            this.width = width;
            this.height = height;
        }

        public void Init(int lvl)
        {

        }

        public object Copy()
        {
            return this.MemberwiseClone();
        }

        public void AddTo(List<object> destination)
        {
            destination.Add(this);
        }
    }
}
