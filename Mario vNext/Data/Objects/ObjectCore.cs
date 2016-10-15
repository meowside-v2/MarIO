using Mario_vNext.Core.Components;
using Mario_vNext.Core.Interfaces;
using System.Drawing;

namespace Mario_vNext.Data.Objects
{
    class ObjectCore : ICore, I3Dimensional, IGraphics
    {

        public Collider collider;
        protected double _scaleX = 1;
        protected double _scaleY = 1;
        protected double _scaleZ = 1;

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

        public double ScaleX
        {
            get
            {
                return _scaleX;
            }
            set
            {
                if (value < 0.1f)
                    _scaleX = 0.1f;

                else
                    _scaleX = value;
            }
        }

        public double ScaleY
        {
            get
            {
                return _scaleY;
            }
            set
            {
                if (value < 0.1f)
                    _scaleY = 0.1f;

                else
                    _scaleY = value;
            }
        }

        public double ScaleZ
        {
            get
            {
                return _scaleZ;
            }
            set
            {
                if (value < 0.1f)
                    _scaleZ = 0.1f;

                else
                    _scaleZ = value;
            }
        }

        public int AnimationState { get; set; }

        public Material model { get; set; }
        
        public int jumpheight { get; set; }     // Number of Blocks
        public int jumplength { get; set; }     // In miliseconds

        public bool ForceJump { get; set; }
        public bool Jumped { get; set; }

        public object DeepCopy()
        {
            return this.MemberwiseClone();
        }

        public void Render(int x, int y, byte[] bufferData, bool[] bufferKey)
        {
            if (model != null) model.Render(X - x, Y - y, bufferData, bufferKey, ScaleX, ScaleY);
        }
    }
}
