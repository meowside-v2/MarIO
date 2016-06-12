using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Mario.Core
{
    class Core_objects
    {
        public string name { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int jumpheight { get; set; }
        public int jumplenght { get; set; }

        public Material mesh;

        public Core_objects(string type)
        {
            switch (type)
            {
                case "player":

                    mesh = new Material((Bitmap)Image.FromFile(Environment.CurrentDirectory + "\\Data\\Sprites\\mario_12x16.png"));

                    name = type;

                    X = 2;
                    Y = 20;

                    jumpheight = 5;
                    jumplenght = 2;

                    Thread keychecker = new Thread(KeyPress);
                    keychecker.Start();

                    break;

                case "turtle":
                    name = type;
                    X = 50;
                    Y = 20;
                    /*graphics = new byte[]{
                        ' ', '^', ' ',
                        '<', '_', '>',
                    };*/

                    Thread movement = new Thread(MovementEnemy);
                    movement.Start();

                    break;
            }
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

            //deltatime.Stop();
        }

        public void Physics(string type)
        {

            switch (type)
            {
                case "jump":

                    Thread JumpEvent = new Thread(Jump);
                    JumpEvent.Start();

                    break;
            }

        }

        private void Jump()
        {
            int start_position = Y;
            double step = Convert.ToDouble(jumpheight);

            Stopwatch deltatime = new Stopwatch();
            deltatime.Start();

            while (Y > start_position - jumpheight)
            {

                Y -= Convert.ToInt32(0.5 * World.Gravity * Math.Pow(deltatime.ElapsedMilliseconds / 1000, 2));

                Thread.Sleep(100);
                /*Y -= (int)Math.Sqrt(int.Parse((deltatime.ElapsedMilliseconds / 2000).ToString()));
                Thread.Sleep(100);*/
            }

            deltatime.Restart();

            while (Y < start_position)
            {
                Y += (int)Math.Pow(int.Parse((deltatime.ElapsedMilliseconds / 1000).ToString()), 1.5);
                Thread.Sleep(100);
            }

            /*while (Y < Console.WindowHeight)
            {
                Y = Y - (int)Math.Sin(double.Parse((deltatime.ElapsedMilliseconds / 1000 * 180).ToString()));
                Thread.Sleep(1000);
            }*/

            deltatime.Stop();
        }

        private void KeyPress()
        {
            ConsoleKeyInfo K;

            while (true)
            {
                K = Console.ReadKey(true);

                switch (K.Key)
                {
                    case ConsoleKey.A:
                        X--;
                        break;

                    case ConsoleKey.D:
                        X++;
                        break;

                    case ConsoleKey.W:
                        Physics("jump");
                        break;
                }
            }
        }
    }
}
