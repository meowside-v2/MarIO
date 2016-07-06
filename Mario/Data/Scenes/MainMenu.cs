using Mario.Core;
using Mario.Data.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mario.Data.Scenes
{
    class MainMenu
    {
        string[] selections =
        {
            "Play",
            "Help",
            "Exit"
        };

        short selection = 0;

        List<object> objects = new List<object>();

        public void Init()
        {
            World map = new World();
            map.Init(0);
        }
    }
}
