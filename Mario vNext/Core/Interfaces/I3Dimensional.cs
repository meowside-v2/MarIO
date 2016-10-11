using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mario_vNext.Core.Interfaces
{
    interface I3Dimensional
    {
        int width { get; }
        int height { get; }
        int depth { get; }

        int X { get; set; }
        int Y { get; set; }
        int Z { get; set; }

        double ScaleX { get; set; }
        double ScaleY { get; set; }
        double ScaleZ { get; set; }

        /*int ScaledX { get; }
        int ScaledY { get; }
        int ScaledZ { get; }*/

    }
}
