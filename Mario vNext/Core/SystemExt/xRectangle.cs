using Mario_vNext.Core.Interfaces;
using Mario_vNext.Data.Objects;
using System.Drawing;

namespace Mario_vNext.Core.SystemExt
{
    class xRectangle : ICore, I3Dimensional
    {
        private double _scaleX = 1;
        private double _scaleY = 1;
        private double _scaleZ = 1;

        xList<WorldObject> border = new xList<WorldObject>();

        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }

        public int width { get; set; }
        public int height { get; set; }
        public int depth { get; set; }

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
        
        public xRectangle(int x, int y, int z, int width, int height)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;

            this.width = width;
            this.height = height;

            this.border.Clear();

            for (int i = 0; i <= width + 8; i += 8)
            {
                border.Add(new WorldObject(ObjectDatabase.WorldObjects.Border, X + i, Y, this.Z));
                border.Add(new WorldObject(ObjectDatabase.WorldObjects.Border, X + i, Y + height + 8, this.Z));
            }

            for (int i = 8; i <= height; i += 8)
            {
                border.Add(new WorldObject(ObjectDatabase.WorldObjects.Border, X, Y + i, this.Z));
                border.Add(new WorldObject(ObjectDatabase.WorldObjects.Border, X + width + 8, Y + i, this.Z));
            }
        }

        public object DeepCopy()
        {
            return this.MemberwiseClone();
        }

        public void Render(int x, int y, byte[] bufferData, bool[] bufferKey)
        {
            border.Render(x, y, bufferData, bufferKey);
        }
    }
}
