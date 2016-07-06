using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mario.Core
{
    class xList<T> : List<T>, ICore
    {

        public xList() { }

        public xList(IEnumerable<T> collection)
        {
            foreach (var item in collection)
            {
                this.Add(item);
            }
        }

        public object Copy()
        {
            return this.MemberwiseClone();
        }

        public void AddTo(List<object> destination)
        {
            destination.Add(this);
        }
    }
}
