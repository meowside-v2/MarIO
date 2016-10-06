using Mario_vNext.Core.Interfaces;
using Mario_vNext.Core.SystemExt;
using System.Drawing;

namespace Mario_vNext.Data.Objects
{
    class World : ICore
    {
        public double Gravity { get; set; }
        public string Level { get; set; }

        public xList<I3Dimensional> model = new xList<I3Dimensional>();

        public Player player = new Player();

        public int PlayerSpawnX { get; set; }
        public int PlayerSpawnY { get; set; }
        public int PlayerSpawnZ { get; set; }

        public int Width { get; set; }
        public int Height { get; set; }

        public World() { }

        public World(int width, int height, string name)
        {
            this.Width = width;
            this.Height = height;
            this.Level = name;
        }

        public void Init()
        {
            /*----- LOAD WORLD -----*/

            /*----------------------*/

            model.Add(player);
        }

        public object DeepCopy()
        {
            return MemberwiseClone();
        }

        public void Render(int x, int y, byte[] imageBuffer, bool[] imageBufferKey)
        {
            model.Render(x, y, imageBuffer, imageBufferKey);
        }
    }
}
