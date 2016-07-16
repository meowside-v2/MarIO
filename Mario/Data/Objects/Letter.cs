using Mario.Core;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mario.Data.Objects
{
    class Letter : Core_objects
    {

        public Letter(Image img)
        {
            this.mesh = new Material(img);
        }
    }
}
