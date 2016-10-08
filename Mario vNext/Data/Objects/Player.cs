using Mario_vNext.Core.Components;
using System.Threading;

namespace Mario_vNext.Data.Objects
{
    class Player : ObjectCore
    {
        Physics basicPhysics = new Physics();
        //Keyboard keyboard = new Keyboard();
        //Camera cam = new Camera();

        private bool Jumped = false;
        private bool WKeyIsHeld = false;

        public void Init(int X, int Y, int Z)
        {
            this.X = X;
            this.Y = Y;
            this.Z = Z;
        }



        private void KeyPress()
        {
            /*while (true)
            {

                if (keyboard.IsKeyPressed(ConsoleKey.W))
                {
                    DoPhysics(Physics.PhysicState.Jump);
                    WKeyIsHeld = true;
                }
                else
                {
                    WKeyIsHeld = false;
                }


                if (keyboard.IsKeyPressed(ConsoleKey.A))
                {
                    if (!collider.CollisionLeft(this, world, enemies)) X--;
                    if (!collider.CollisionBottom(this, world, enemies) && !Jumped)
                    {
                        DoPhysics(Physics.PhysicState.Fall);
                    }
                }

                if (keyboard.IsKeyPressed(ConsoleKey.D))
                {
                    if (!collider.CollisionRight(this, world, enemies)) X++;
                    if (!collider.CollisionBottom(this, world, enemies) && !Jumped)
                    {
                        DoPhysics(Physics.PhysicState.Fall);
                    }
                }

                Thread.Sleep(10);
            }*/
        }

        private void DoPhysics(Physics.PhysicState type)
        {

            switch (type)
            {
                case Physics.PhysicState.Jump:

                    if (!Jumped)
                    {
                        Jumped = true;
                        Thread JumpEvent = new Thread(() => basicPhysics.Jump(this));
                        JumpEvent.Start();
                    }

                    break;

                case Physics.PhysicState.Fall:

                    Jumped = true;
                    Thread FallEvent = new Thread(() => basicPhysics.Fall(this));
                    FallEvent.Start();

                    break;
            }

        }
    }
}
