﻿using Mario.Core;
using Mario.Data.Objects;
using Mario.Data.Scenes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mario
{
    class Program
    {
        static void Main(string[] args)
        {
            WorldEditor worldCreate = new WorldEditor();
            worldCreate.Start();
        }
    }
}
