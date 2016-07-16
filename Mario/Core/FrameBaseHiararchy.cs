using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mario.Core
{
    class FrameBaseHiararchy : ICore
    {
        public xList<object> background    = new xList<object>();
        public xList<object> middleground  = new xList<object>();
        public xList<object> foreground    = new xList<object>();
        public xList<object> exclusive     = new xList<object>();
        public xList<object> UI            = new xList<object>();

        public object Copy()
        {
            return (FrameBaseHiararchy)this.MemberwiseClone();
        }

        public void AddTo(List<object> destination)
        {
            destination.Add(this);
        }

        public object DeepCopy()
        {
            FrameBaseHiararchy retValue = (FrameBaseHiararchy) this.MemberwiseClone();

            retValue.background = (xList<object>)this.background.DeepCopy();
            retValue.middleground = (xList<object>)this.middleground.DeepCopy();
            retValue.foreground = (xList<object>)this.foreground.DeepCopy();
            retValue.exclusive = (xList<object>)this.exclusive.DeepCopy();
            retValue.UI = (xList<object>)this.UI.DeepCopy();

            return retValue;
        }

        public void Render(byte[] destination, short[] destinationColor, int frameWidth, int frameHeight, int ? layer = null, int? x = null, int? y = null)
        {
            UI.Render(destination, destinationColor, frameWidth, frameHeight, 1, x, y);
            exclusive.Render(destination, destinationColor, frameWidth, frameHeight, 1, x, y);
            foreground.Render(destination, destinationColor, frameWidth, frameHeight, 1, x, y);
            middleground.Render(destination, destinationColor, frameWidth, frameHeight, 1, x, y);
            background.Render(destination, destinationColor, frameWidth, frameHeight, 0, x, y);
        }
    }
}
