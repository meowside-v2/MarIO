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

        public void AddTo(List<object> destination)
        {
            destination.Add(this);
        }

        public object DeepCopy()
        {
            World retValue = (World)this.MemberwiseClone();

            retValue.background = (xList<Block>)this.background.DeepCopy();
            retValue.middleground = (xList<Block>)this.middleground.DeepCopy();
            retValue.foreground = (xList<Block>)this.foreground.DeepCopy();

            return retValue;
        }

        public void Render(byte[] destination, short[] destinationColor, int frameWidth, int frameHeight, int? layer = null, int? x = null, int? y = null)
        {
            background.Render(destination, destinationColor, frameWidth, frameHeight, 0, x, y);
            middleground.Render(destination, destinationColor, frameWidth, frameHeight, 1, x, y);
            foreground.Render(destination, destinationColor, frameWidth, frameHeight, 1, x, y);
        }
    }
}
