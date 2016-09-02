using Mario.Data.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mario.Core
{
    class BaseHiararchy : ICore
    {
        public xList<object> background = new xList<object>();
        public xList<object> middleground = new xList<object>();
        public xList<object> foreground = new xList<object>();
        public xList<object> exclusive = new xList<object>();
        public xList<object> UI = new xList<object>();

        public Player player;

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

            if (this.background != null) retValue.background = (xList<object>)this.background.DeepCopy();
            if (this.middleground != null) retValue.middleground = (xList<object>)this.middleground.DeepCopy();
            if (this.foreground != null) retValue.foreground = (xList<object>)this.foreground.DeepCopy();
            if (this.UI != null) retValue.UI = (xList<object>)this.UI.DeepCopy();
            if (this.exclusive != null) retValue.exclusive = (xList<object>)this.exclusive.DeepCopy();
            if (this.player != null) retValue.player = (Player)this.player.DeepCopy();


            return retValue;
        }

        public void Render(byte[] destination, short[] destinationColor, int frameWidth, int frameHeight, int? x = null, int? y = null)
        {
            xList<object> exclusiveNear = new xList<object>();
            xList<object> foregroundNear = new xList<object>();
            xList<object> middlegroundNear = new xList<object>();
            xList<object> backgroundNear = new xList<object>();

            if (exclusive.Count != 0) exclusiveNear = new xList<object>(exclusive.Where(temp => (temp as ICoordinated).X + (temp as I2Dimensional).width > x && (temp as ICoordinated).X < x + frameWidth && (temp as ICoordinated).Y + (temp as I2Dimensional).height > y && (temp as ICoordinated).Y < y + frameHeight).Select(temp => temp));
            if (foreground.Count != 0) foregroundNear = new xList<object>(foreground.Where(temp => (temp as ICoordinated).X + (temp as I2Dimensional).width > x && (temp as ICoordinated).X < x + frameWidth && (temp as ICoordinated).Y + (temp as I2Dimensional).height > y && (temp as ICoordinated).Y < y + frameHeight).Select(temp => temp));
            if (middleground.Count != 0) middlegroundNear = new xList<object>(middleground.Where(temp => (temp as ICoordinated).X + (temp as I2Dimensional).width > x && (temp as ICoordinated).X < x + frameWidth && (temp as ICoordinated).Y + (temp as I2Dimensional).height > y && (temp as ICoordinated).Y < y + frameHeight).Select(temp => temp));
            if (background.Count != 0) backgroundNear = new xList<object>(background.Where(temp => (temp as ICoordinated).X + (temp as I2Dimensional).width > x && (temp as ICoordinated).X < x + frameWidth && (temp as ICoordinated).Y + (temp as I2Dimensional).height > y && (temp as ICoordinated).Y < y + frameHeight).Select(temp => temp));

            if (UI != null) UI.Render(destination, destinationColor, frameWidth, frameHeight, x, y);
            if (player != null) player.Render(destination, destinationColor, frameWidth, frameHeight, x, y);
            if (exclusiveNear != null) exclusiveNear.Render(destination, destinationColor, frameWidth, frameHeight, x, y);
            if (foregroundNear != null) foregroundNear.Render(destination, destinationColor, frameWidth, frameHeight, x, y);
            if (middlegroundNear != null) middlegroundNear.Render(destination, destinationColor, frameWidth, frameHeight, x, y);
            if (backgroundNear != null) backgroundNear.Render(destination, destinationColor, frameWidth, frameHeight, x, y);
        }
    }
}
