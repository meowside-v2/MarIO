using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Mario.Core
{
    class Player : Core_objects
    {

        //private double Acceleration_X = 0;

        private bool Jumped = false;
        private bool WKeyIsHeld = false;
        
        enum PhysicState
        {
            Jump,
            Fall
        };

        public Player()
        {
            mesh = new Material((Bitmap)Image.FromFile(Environment.CurrentDirectory + "\\Data\\Sprites\\mario_12x16.png"));

            name = "Mario";

            X = 20;
            Y = 50;

            jumpheight = 30;
            jumplength = 15;

            Thread keychecker = new Thread(KeyPress);
            keychecker.Start();

            physics.Fall(this);
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
                    if (!collider.CollisionLeft(this)) X--;
                    if (!collider.CollisionBottom(this) && !Jumped)
                    {
                        Physics(PhysicState.Fall);
                    }
                }

                if (IsKeyPressed(ConsoleKey.D))
                {
                    if (!collider.CollisionRight(this)) X++;
                    if (!collider.CollisionBottom(this) && !Jumped)
                    {
                        Physics(PhysicState.Fall);
                    }
                }

                Thread.Sleep(10);
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
                    Thread FallEvent = new Thread(() => physics.Fall(this));
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
                    physics.Fall(this);
                    Jumped = false;
                    return;
                }
                else if (collider.CollisionTop(this))
                {
                    physics.Fall(this);
                    Jumped = false;
                    return;
                }
                else if (!WKeyIsHeld && StartPositon - Y > MinJump)
                {
                    physics.Fall(this);
                    Jumped = false;
                    return;
                }

                else Y -= 1;

                Thread.Sleep(jumplength);
            } while (true);
        }
    }
}
