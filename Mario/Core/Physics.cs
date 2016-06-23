using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Mario.Core
{
    class Physics
    {
        public void Fall(Core_objects obj)
        {


            do
            {
                obj.Y += 1;

                if (obj.collider.CollisionBottom(obj)) return;

                Thread.Sleep(obj.jumplength);
            } while (true);
        }


    }
}
