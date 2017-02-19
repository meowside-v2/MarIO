using DKBasicEngine_1_0;
using DKBasicEngine_1_0.Core;
using DKBasicEngine_1_0.Core.Components;
using Mario_vNext.Data;
using System;
using System.Collections;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Resources;

namespace Mario_vNext
{
    class Program
    {
        static void Main(string[] args)
        {
            Engine.Init();
            Database.LoadResources(Sprites.ResourceManager.GetResourceSet(CultureInfo.CurrentCulture, true, true));


        }
    }
}
