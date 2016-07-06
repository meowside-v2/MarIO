using Mario.Core;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mario.Data.Objects
{
    class Player : Core_objects
    {
        Camera cam = new Camera();

        private bool Jumped = false;
        private bool WKeyIsHeld = false;
        
        enum PhysicState
        {
            Jump,
            Fall
        };
        
        public void Init(World world, List<Enemy> enemies)
        {
            
            mesh = new Material(ImageLoader.Load(ObjectDatabase.Object.Mario, ObjectDatabase.path));

            name = "Mario";

            X = 20;
            Y = 50;

            jumpheight = 30;
            jumplength = 15;

            //cam.Init(this, world, enemies);

            Thread keychecker = new Thread(() => KeyPress(world, enemies));
            keychecker.Start();

            Fall(world, enemies);
        }

        [DllImport("user32.dll")]
        public static extern ushort GetKeyState(short nVirtKey);

        public const ushort keyDownBit = 0x80;

        public static bool IsKeyPressed(ConsoleKey key)
        {
            return ((GetKeyState((short)key) & keyDownBit) == keyDownBit);
        }

        private void KeyPress(World world, List<Enemy> enemies)
        {
            while (true)
            {

                if (IsKeyPressed(ConsoleKey.W))
                {
                    Physics((int)PhysicState.Jump, world, enemies);
                    WKeyIsHeld = true;
                }
                else
                {
                    WKeyIsHeld = false;
                }


                if (IsKeyPressed(ConsoleKey.A))
                {
                    if (!collider.CollisionLeft(this, world, enemies)) X--;
                    if (!collider.CollisionBottom(this, world, enemies) && !Jumped)
                    {
                        Physics(PhysicState.Fall, world, enemies);
                    }
                }

                if (IsKeyPressed(ConsoleKey.D))
                {
                    if (!collider.CollisionRight(this, world, enemies)) X++;
                    if (!collider.CollisionBottom(this, world, enemies) && !Jumped)
                    {
                        Physics(PhysicState.Fall, world, enemies);
                    }
                }

                Thread.Sleep(10);
            }

        }

        private void Physics(PhysicState type, World world, List<Enemy> enemies)
        {

            switch (type)
            {
                case PhysicState.Jump:

                    if (!Jumped)
                    {
                        Jumped = true;
                        Thread JumpEvent = new Thread(() => Jump(world, enemies));
                        JumpEvent.Start();
                    }

                    break;

                case PhysicState.Fall:

                    Jumped = true;
                    Thread FallEvent = new Thread(() => Fall(world, enemies));
                    FallEvent.Start();

                    break;
            }

        }

        private void Jump(World world, List<Enemy> enemies)
        {
            int StartPositon = Y;
            int MinJump = 10;

            do
            {
                if (StartPositon - Y == jumpheight / world.Gravity)
                {
                    Fall(world, enemies);
                    Jumped = false;
                    return;
                }
                else if (collider.CollisionTop(this, world, enemies))
                {
                    Fall(world, enemies);
                    Jumped = false;
                    return;
                }
                else if (!WKeyIsHeld && StartPositon - Y > MinJump)
                {
                    Fall(world, enemies);
                    Jumped = false;
                    return;
                }

                else Y -= 1;

                Thread.Sleep(jumplength);
            } while (true);
        }

        private void Fall(World world, List<Enemy> enemies)
        {
            do
            {
                Y += 1;
                
                Thread.Sleep(jumplength);
            } while (!collider.CollisionBottom(this, world, enemies));

            Jumped = false;
        }
    }
}
