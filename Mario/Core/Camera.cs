using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mario.Core
{
    class Camera
    {
        public int Xoffset { get; set; }
        public int Yoffset { get; set; }

        Render screen = new Render();

        public void Init(Player core, World world, List<Enemy> enemies)
        {
            screen.Init(core, world, enemies);
        }
    }
}
