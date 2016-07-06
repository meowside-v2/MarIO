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
        void AddTo(List<object> destination);
    }
}
