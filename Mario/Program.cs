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
        public static List<Core_objects> Objects = new List<Core_objects>();

        public static Core_objects player = new Core_objects("player");
        public static Core_objects enemy1 = new Core_objects("turtle");

        static void Main(string[] args)
        {
            Objects.Add(player);
            //Objects.Add(enemy1);

            Render game = new Render();

        }
    }
}
