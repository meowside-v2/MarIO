using Mario.Data.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

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

        public void Render(int x, int y)
        {
            for(int index = 0; index < this.Count; index++)
            {
                if (index >= this.Count)
                {
                    return;
                }

                else
                {
                    //(this[index] as ICore).Render(destination, destinationColor, frameWidth, frameHeight, x, y);
                }
            }
        }

        Color ICore.Render(int x, int y)
        {
            throw new NotImplementedException();
        }
    }
}
