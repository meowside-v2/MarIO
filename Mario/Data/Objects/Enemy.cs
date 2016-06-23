using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Mario.Core
{
    class Enemy : Core_objects
    {
        
        public Enemy()
        {
            name = "Turtle";
            X = 50;
            Y = 20;

            jumplength = 20;

            Thread movement = new Thread(MovementEnemy);
            movement.Start();

            physics.Fall(this);
        }

        public void MovementEnemy()
        {
            Stopwatch deltatime = new Stopwatch();
            deltatime.Start();

            while (true)
            {
                X = X - int.Parse(deltatime.ElapsedMilliseconds.ToString()) / 1000;
                deltatime.Restart();
                Thread.Sleep(1000);
            }
        }
    }
}
