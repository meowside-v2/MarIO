using Mario_vNext.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mario_vNext.Core.SystemExt
{
    class GameCore
    {

        Task updateEveryFrame;

        xList<ICore> objectsInGame = new xList<ICore>();

        public void Run()
        {
            Shared.referenceToGameObjects = objectsInGame;
            Shared.Time.Start();
            updateEveryFrame = Task.Factory.StartNew(() => Update());
        }

        /// <summary>
        /// Updates game every frame
        /// </summary>

        public void Update()
        {
            while (true)
            {
                int start = Environment.TickCount;

                Shared.Time.Restart();

                int count = 0;

                lock(objectsInGame)
                {
                    foreach (ICore item in objectsInGame.ToList())
                    {
                        item.Update();
                        count++;
                    }
                }
                

                if(Shared.CamRefrence != null)
                {
                    Shared.CamRefrence.Buffering();
                    Shared.CamRefrence.Rendering();
                    Shared.CamRefrence.Vsync(60, Environment.TickCount - start, true);
                }
            }
        }
    }
}
