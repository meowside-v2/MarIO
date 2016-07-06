using Mario.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mario.Data.Objects
{
    class Enemy : Core_objects
    {
        
        public void Init(World world, List<Enemy> enemies)
        {
            Image img = null;

            try
            {
                img = Image.FromFile(Environment.CurrentDirectory + "\\Data\\Sprites\\mario_12x16.png");
            }
            catch
            {
                MessageBox.Show("Mario sprite can't be loaded, please reinstall game!");
                Environment.Exit(0);
            }

            mesh = new Material((Bitmap)img);

            name = "Turtle";
            X = 50;
            Y = 20;

            jumplength = 20;

            Thread movement = new Thread(MovementEnemy);
            movement.Start();

            //ll(this, world, enemies);
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
