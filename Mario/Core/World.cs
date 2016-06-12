using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mario.Core
{
    class World
    {
        public static double Gravity = 1f;

        public byte[,] Design { get; set; }
        public string Level { get; set; }

        public World()
        {

        }
    }
}
