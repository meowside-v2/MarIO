using Mario_vNext.Core.Interfaces;
using System.Drawing;
using Mario_vNext.Core.Components;
using Mario_vNext.Data.Objects;

namespace Mario_vNext.Core.SystemExt
{
    class BaseHiararchy
    {
        public World worldReference;
        public xRectangle borderReference;
        public xList<TextBlock> GUI = new xList<TextBlock>();
        public xList<I3Dimensional> exclusiveReference = new xList<I3Dimensional>();

        public object DeepCopy()
        {
            BaseHiararchy retValue = (BaseHiararchy)this.MemberwiseClone();

            retValue.exclusiveReference = (xList<I3Dimensional>) this.exclusiveReference.DeepCopy();
            retValue.GUI = (xList<TextBlock>) this.GUI.DeepCopy();

            retValue.worldReference = this.worldReference;

            return retValue;
        }

        public void Render(int x, int y, byte[] imageBuffer, bool[] imageBufferKey)
        {
                GUI.Render(0, 0, imageBuffer, imageBufferKey);
                if(borderReference != null) borderReference.Render(x, y, imageBuffer, imageBufferKey);
                exclusiveReference.Render(x, y, imageBuffer, imageBufferKey);
                if(worldReference != null) worldReference.Render(x, y, imageBuffer, imageBufferKey);
        }
    }
}
