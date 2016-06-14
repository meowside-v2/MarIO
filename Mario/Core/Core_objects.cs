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

        public double Acceleration_Y { get; set; }
        public double Acceleration_X { get; set; }
        public double MaxAcceleration { get; set; }

        public Material mesh;

        public Core_objects(string type)
        {
            switch (type)
            {
                case "player":

                    mesh = new Material((Bitmap)Image.FromFile(Environment.CurrentDirectory + "\\Data\\Sprites\\mario_12x16.png"));

                    name = type;

                    X = 2;
                    Y = 50;

                    jumpheight = 5;
                    jumplenght = 2;

                    MaxAcceleration = 1;

                    
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
            double start_position = Y;
            double pos_y = Y;

            bool IsFalling = false;

            Acceleration_Y = MaxAcceleration;

            Stopwatch deltatime = new Stopwatch();
            deltatime.Start();

            do
            {
                if (Acceleration_Y < 0 && !IsFalling)
                {
                    Acceleration_Y = 0;
                    IsFalling = true;
                    deltatime.Restart();
                }

                else
                {
                    Acceleration_Y -= deltatime.ElapsedMilliseconds / 500;
                }
                
                Y -= (int)(jumpheight * (Acceleration_Y / World.Gravity));
                Thread.Sleep(100);
            } while (Y < start_position);

            deltatime.Stop();
        }

        private void KeyPress()
        {   
            ConsoleKeyInfo K;

            while (true)
            {

                if (Console.KeyAvailable)
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
}
