using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mario.Core
{
    interface ICore
    {
        
        object Copy();
        object DeepCopy();
        void Render(byte[] destination, short[] destinationColor, int frameWidth, int frameHeight, int? layer = null, int? x = null, int? y = null);
    }
}
