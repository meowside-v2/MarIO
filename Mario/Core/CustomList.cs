using Mario.Data.Objects;
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

        public object DeepCopy()
        {
            xList<T> retValue = new xList<T>();
            
            for(int index = 0; index < this.Count; index++)
            {
                if (index >= this.Count()) break;
                
                if (this[index] != null)
                {
                    retValue.Add((T)(this[index] as ICore).DeepCopy());
                }
            }

            return (xList<T>) retValue;
        }

        public void Render(byte[] destination, short[] destinationColor, int frameWidth, int frameHeight, int? layer = null, int? x = null, int? y = null)
        {
            for(int index = 0; index < this.Count; index++)
            {
                if (!(index >= this.Count))
                    (this[index] as ICore).Render(destination, destinationColor, frameWidth, frameHeight, layer, x, y);
                    
                else break;
            }
        }
    }
}
