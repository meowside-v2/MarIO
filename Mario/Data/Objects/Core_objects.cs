using Mario.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Mario.Data.Objects
{
    class Core_objects
    {
        public string name { get; set; }
        public int X { get; set; }
        public int Y { get; set; }

        public int AnimationState { get; set; }

        public int jumpheight = 0;     // Number of Blocks
        public int jumplength = 0;     // In ms (miliseconds)

        public Material mesh;
        public Collider collider = new Collider();
    }
}
