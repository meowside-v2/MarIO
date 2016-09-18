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

        public xList<Block> model = new xList<Block>();

        public int PlayerSpawnX { get; set; }
        public int PlayerSpawnY { get; set; }

        public int width { get; set; }
        public int height { get; set; }

        public World() { }

        public World(int width, int height, string name)
        {
            this.width = width;
            this.height = height;
            this.Level = name;
        }

        public void Init(int lvl)
        {

        }

        public object Copy()
        {
            return this.MemberwiseClone();
        }

        public object DeepCopy()
        {
            World retValue = (World)this.MemberwiseClone();

            retValue.model = (xList<Block>)this.model.DeepCopy();

            return retValue;
        }

        public void Render(int x, int y)
        {
            //foreground.Render(destination, destinationColor, frameWidth, frameHeight, x, y);
            //middleground.Render(destination, destinationColor, frameWidth, frameHeight, x, y);
            //background.Render(destination, destinationColor, frameWidth, frameHeight, x, y);
        }
    }
}
