using Mario_vNext.Core.Components;
using Mario_vNext.Core.Interfaces;
using Mario_vNext.Core.SystemExt;
using System.Threading;
using System.Threading.Tasks;

namespace Mario_vNext.Data.Objects
{
    class Player : ObjectCore
    {
        Physics basicPhysics;
        Keyboard keyboard;

        World Parent; 
            
        private bool Jumped = false;
        
        public void Init(World Parent, int X, int Y, int Z)
        {
            this.X = X;
            this.Y = Y;
            this.Z = Z;

            this.Parent = Parent;

            collider = new Collider(this, Parent.model);
            basicPhysics = new Physics(this, collider);
            keyboard = new Keyboard(100, 100, 100);

            keyboard.onWKey = Jump;
        }

        private void DoPhysics(Physics.PhysicState type)
        {
            switch (type)
            {
                case Physics.PhysicState.Jump:

                    if (!Jumped)
                    {
                        Jumped = true;
                        Task JumpEvent = Task.Factory.StartNew(() => basicPhysics.Jump());
                    }

                    break;

                case Physics.PhysicState.Fall:

                    Jumped = true;
                    Task FallEvent = Task.Factory.StartNew(() => basicPhysics.Fall());

                    break;
            }
        }

        private void Jump()
        {
            if (!Jumped)
            {
                DoPhysics(Physics.PhysicState.Jump);
            }
        }

        private void MoveLeft()
        {
            if (this.X > 0 && !collider.Collision(Collider.Direction.Left))
            {
                X--;

                if (collider.Collision(Collider.Direction.Down))
                {
                    this.DoPhysics(Physics.PhysicState.Fall);
                }
            }
        }

        private void MoveRight()
        {
            if (this.X < 1000 && !collider.Collision(Collider.Direction.Right))
            {
                X++;

                if (collider.Collision(Collider.Direction.Down))
                {
                    this.DoPhysics(Physics.PhysicState.Fall);
                }
            }
        }
    }
}
