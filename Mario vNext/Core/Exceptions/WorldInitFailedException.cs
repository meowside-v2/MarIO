using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mario_vNext.Core.Exceptions
{
    class WorldInitFailedException : Exception
    {
        public WorldInitFailedException()
            : base()
        { }

        public WorldInitFailedException(string msg)
            : base(msg)
        {

        }

        public WorldInitFailedException(string msg, Exception ex)
            : base(msg, ex)
        {

        }
    }
}
