using Mario_vNext.Core.Components;
using Mario_vNext.Core.Interfaces;
using System.Drawing;

namespace Mario_vNext.Data.Objects
{
    class ObjectCore : ICore, I3Dimensional, IGraphics
    {

        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }

        public int width
        {
            get
            {
                return (model == null ? 0 : model.width);
            }
        }
        public int height
        {
            get
            {
                return (model == null ? 0 : model.height);
            }
        }
        public int depth
        {
            get
            {
                return 0;
            }
        }

        public int AnimationState { get; set; }

        public Material model { get; set; }

        public int jumpheight { get; set; }     // Number of Blocks
        public int jumplength { get; set; }     // In miliseconds

        public object DeepCopy()
        {
            return this.MemberwiseClone();
        }

        public void Render(int x, int y, byte[] imageBuffer, bool[] imageBufferKey)
        {
            if(model != null) model.Render(X - x, Y - y, imageBuffer, imageBufferKey);
        }
    }
}
