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
        public xList<TextBlock> UI = new xList<TextBlock>();

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
            retValue.UI = (xList<TextBlock>)this.UI.DeepCopy();

            return retValue;
        }

        public void Render(byte[] destination, short[] destinationColor, int frameWidth, int frameHeight, int ? layer = null, int? x = null, int? y = null)
        {
            background.Render(destination, destinationColor, frameWidth, frameHeight, 0);
            middleground.Render(destination, destinationColor, frameWidth, frameHeight, 1);
            foreground.Render(destination, destinationColor, frameWidth, frameHeight, 1);
            UI.Render(destination, destinationColor, frameWidth, frameHeight, 1);
        }
    }
}
