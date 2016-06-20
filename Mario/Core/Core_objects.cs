using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
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

        private int jumpheight { get; set; }     // Number of Blocks
        private int jumplenght { get; set; }     // In ms (miliseconds)
        
        private double Acceleration_X { get; set; }
        
        private bool Jumped = false;
        private bool WKeyIsHeld = false;

        public Material mesh;

        public Core_objects(string type)
        {
            switch (type)
            {
                case "player":

                    mesh = new Material((Bitmap)Image.FromFile(Environment.CurrentDirectory + "\\Data\\Sprites\\mario_12x16.png"));

                    name = type;

                    X = 20;
                    Y = 50;

                    jumpheight = 30;
                    jumplenght = 20;
                    
                    Thread keychecker = new Thread(KeyPress);
                    keychecker.Start();

                    break;

                case "turtle":
                    name = type;
                    X = 50;
                    Y = 20;

                    Thread movement = new Thread(MovementEnemy);
                    movement.Start();

                    break;
            }
        }

        enum PhysicState
        {
            Jump,
            Fall
        };

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

        private void Physics(PhysicState type)
        {

            switch (type)
            {
                case PhysicState.Jump:

                    if (!Jumped)
                    {
                        Jumped = true;
                        Thread JumpEvent = new Thread(Jump);
                        JumpEvent.Start();
                    }

                    break;

                case PhysicState.Fall:
                    
                    Jumped = true;
                    Thread FallEvent = new Thread(Fall);
                    FallEvent.Start();

                    break;
            }

        }

        

        private void Jump()
        {
            int StartPositon = Y;
            int MinJump = 10;

            do
            {
                if (StartPositon - Y == jumpheight / World.Gravity)
                {
                    Fall();
                    Jumped = false;
                    return;
                }
                else if (CollisionTop())
                {
                    Fall();
                    Jumped = false;
                    return;
                }
                else if (!WKeyIsHeld && StartPositon - Y > MinJump)
                {
                    Fall();
                    Jumped = false;
                    return;
                }

                else Y -= 1;
                
                Thread.Sleep(jumplenght);
            } while (true);
        }

        private void Fall()
        {
            do
            {
                Y += 1;

                if (CollisionBottom()) return;

                Thread.Sleep(jumplenght);
            } while (true);
        }

        private bool CollisionLeft()
        {
            for (int row = Y; row < Y + mesh.height - 1; row++)
            {
                if (X <= 0) return true;
                else if (Program.map.mesh.bitmapTransparent[row, X - 1] == 255 && X > 0) return true;
            }

            foreach (var item in Program.enemies)
            {

            }

            return false;
        }

        private bool CollisionRight()
        {
            for (int row = Y; row < Y + mesh.height - 1; row++)
            {
                if (X + mesh.width >= 300) return true;
                else if (Program.map.mesh.bitmapTransparent[row, X + mesh.width] == 255 && X + mesh.width - 1 < 300) return true;
            }

            foreach (var item in Program.enemies)
            {

            }

            return false;
        }

        public bool CollisionTop()
        {
                for (int column = X; column < X + mesh.width - 1; column++)
                {
                    if (Y <= 0) return true;
                    else if (Program.map.mesh.bitmapTransparent[Y - 1, column] == 255 && Y > 0) return true;
                }

            foreach (var item in Program.enemies)
            {

            }

            return false;
        }

        private bool CollisionBottom()
        {
            for (int column = X; column < X + mesh.width - 1; column++)
            {
                if (Y + mesh.height - 1 >= 100) return true;
                else if (Program.map.mesh.bitmapTransparent[Y + mesh.height, column] == 255 && Y + mesh.height - 1 < 100) return true;
            }

            foreach (var item in Program.enemies)
            {

            }

            return false;
        }

        [DllImport("user32.dll")]
        public static extern ushort GetKeyState(short nVirtKey);

        public const ushort keyDownBit = 0x80;

        public static bool IsKeyPressed(ConsoleKey key)
        {
            return ((GetKeyState((short)key) & keyDownBit) == keyDownBit);
        }

        private void KeyPress()
        {
            while (true)
            {

                if (IsKeyPressed(ConsoleKey.W))
                {
                    Physics((int)PhysicState.Jump);
                    WKeyIsHeld = true;
                }
                else
                {
                    WKeyIsHeld = false;
                }


                if (IsKeyPressed(ConsoleKey.A))
                {
                    if (!CollisionLeft()) X--;
                    if (!CollisionBottom() && !Jumped)
                    {
                        Physics(PhysicState.Fall);
                    }
                }

                if (IsKeyPressed(ConsoleKey.D))
                {
                    if (!CollisionRight()) X++;
                    if (!CollisionBottom() && !Jumped)
                    {
                        Physics(PhysicState.Fall);
                    }
                }

                Thread.Sleep(10);
            }
            
        }
    }
}
