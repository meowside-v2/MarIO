using Mario.Data.Objects;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mario.Core
{
    class BaseHiararchy : ICore
    {
        public World worldReference;
        public Player playerReference;

        public xList<object> exclusiveReference = new xList<object>();

        public xList<TextBlock> UI = new xList<TextBlock>();
        
        public object Copy()
        {
            return (BaseHiararchy)this.MemberwiseClone();
        }

        public void AddTo(List<object> destination)
        {
            destination.Add(this);
        }

        public object DeepCopy()
        {
            BaseHiararchy retValue = (BaseHiararchy) this.MemberwiseClone();

            retValue.exclusiveReference = (xList<object>)this.exclusiveReference.DeepCopy();
            retValue.UI = (xList<TextBlock>)this.UI.DeepCopy();

            retValue.worldReference = this.worldReference;
            retValue.playerReference = this.playerReference;

            return retValue;
        }

        public Color Render(int x, int y)
        {
            return Color.AliceBlue;
        }
    }
}
