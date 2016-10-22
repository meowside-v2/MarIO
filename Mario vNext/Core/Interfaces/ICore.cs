using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mario_vNext.Core.Interfaces
{
    interface ICore
    {
        void Render(int x, int y, byte[] bufferData, bool[] bufferKey);
    }
}
