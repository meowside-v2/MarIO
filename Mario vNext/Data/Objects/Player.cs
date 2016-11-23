using DKBasicEngine_1_0;
using System.Threading.Tasks;


namespace Mario_vNext.Data.Objects
{
    class Player : GameObject
    {
        Physics basicPhysics;

        Scene Parent; 
            
        private bool Jumped = false;
        
        public void Init(Scene Parent, int X, int Y, int Z)
        {
            this.X = X;
            this.Y = Y;
            this.Z = Z;

            this.Parent = Parent;

            collider = new Collider(this, Parent.Model);
            basicPhysics = new Physics(this, collider);
        }

        public override void Update()
        {
            if (Engine.Input.IsKeyPressed(System.ConsoleKey.W))
            {
                if (!Jumped)
                {
                    DoPhysics(Physics.PhysicState.Jump);
                }
            }

            if (Engine.Input.IsKeyPressed(System.ConsoleKey.A))
            {
                if (!collider.Collision(Collider.Direction.Left))
                {
                    X--;

                    if (collider.Collision(Collider.Direction.Down))
                    {
                        this.DoPhysics(Physics.PhysicState.Fall);
                    }
                }
            }

            if (Engine.Input.IsKeyPressed(System.ConsoleKey.D))
            {
                if (!collider.Collision(Collider.Direction.Right))
                {
                    X++;

                    if (collider.Collision(Collider.Direction.Down))
                    {
                        this.DoPhysics(Physics.PhysicState.Fall);
                    }
                }
            }
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
    }
}
