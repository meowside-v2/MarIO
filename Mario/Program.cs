using Mario.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mario
{
    class Program
    {
        static void Main(string[] args)
        {
            World map = new World();
            List<Enemy> enemies = new List<Enemy>();
            ColorPalette bufferColors = new ColorPalette();

            Player player = new Player();

            player.Init(map, enemies);
        }
    }
}
