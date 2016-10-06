using Mario_vNext.Data.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Mario_vNext.Core.Components
{
    class Physics
    {
        
        public enum PhysicState
        {
            Jump,
            Fall
        };

        private bool Jumped = false;


        public void Jump(ObjectCore reference)
        {
            int StartPositon = reference.Y;
            int MinJump = 10;

            /*do
            {
                if (StartPositon - reference.Y == reference.jumpheight)
                {
                    //Fall(world, enemies);
                    Jumped = false;
                    return;
                }
                else if (collider.CollisionTop(this, world, enemies))
                {
                    Fall(reference);
                    Jumped = false;
                    return;
                }
                else if (!WKeyIsHeld && StartPositon - reference.Y > MinJump)
                {
                    Fall(reference);
                    Jumped = false;
                    return;
                }

                else reference.Y -= 1;

                Thread.Sleep(reference.jumplength);
            } while (true);*/
        }

        public void Fall(ObjectCore reference)
        {
            /*do
            {
                reference.Y += 1;

                Thread.Sleep(reference.jumplength);
            } while (!collider.CollisionBottom());*/

            Jumped = false;
        }
    }
}
