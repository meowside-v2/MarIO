using Mario.Core;
using Mario.Data.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mario.Data.Scenes
{
    class Game
    {
        World map = new World();
        List<Enemy> enemies = new List<Enemy>();
        Player player = new Player();

        public void Init(int world_level)
        {
            map.Init(world_level);

            

            player.Init(map, enemies);
        }
        
    }
}
