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
        public static List<Enemy> enemies = new List<Enemy>();

        public static ColorPalette bufferColors = new ColorPalette();
        public static World map = new World();
        public static Player player = new Player();
        //public static Enemy enemy1 = new Enemy();

        static void Main(string[] args)
        {
            
            Render game = new Render();

        }
    }
}
