using Mario.Data.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mario.Core
{
    class Camera
    {
        public int Xoffset { get; set; }
        public int Yoffset { get; set; }

        Render screen = new Render();

        public void Init(List<object> objects)
        {
            Xoffset = 0;
            Yoffset = 0;

            objects.Add(this);

            screen.Init(objects);
        }
    }
}
