using Mario_vNext.Core.Exceptions;
using Mario_vNext.Core.Interfaces;
using Mario_vNext.Core.SystemExt;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Mario_vNext.Data.Objects
{
    class World : ICore
    {
        public double Gravity { get; set; }
        public string Level { get; set; }

        public xList<I3Dimensional> model = new xList<I3Dimensional>();

        public Player player;

        public int PlayerSpawnX { get; set; }
        public int PlayerSpawnY { get; set; }
        public int PlayerSpawnZ { get; set; }

        public int Width { get; set; }
        public int Height { get; set; }

        public World() { }

        public enum Mode
        {
            Game,
            Edit
        }

        public void New(int heightInBlocks, int widthInBlocks, string LevelName)
        {
            if(heightInBlocks <= 0 || widthInBlocks <= 0)
                throw new WorldInitFailedException("One of sizes is less or equal 0");

            try
            {
                this.Height = heightInBlocks * 16;
                this.Width = widthInBlocks * 16;
                this.Level = LevelName;
            }
            catch (Exception ex)
            {
                throw new WorldInitFailedException("World creation failed", ex);
            }
        }

        public void Init(string path, Mode mode)
        {
            BinaryReader br;

            try
            {
                br = new BinaryReader(new FileStream(path, FileMode.Open));
            }
            catch (IOException e)
            {
                throw new WorldInitFailedException(path + "\nWorld wasn't found", e);
            }

            try
            {
                this.Level = br.ReadString();
                this.Width = br.ReadInt32();
                this.Height = br.ReadInt32();
                this.Gravity = br.ReadDouble();
                
                this.PlayerSpawnX = br.ReadInt32();
                this.PlayerSpawnY = br.ReadInt32();
                this.PlayerSpawnZ = br.ReadInt32();

                int temp_ModelCount = br.ReadInt32();

                this.model.Clear();

                for(int count = 0; count < temp_ModelCount; count++)
                {
                    model.Add(new Block((ObjectDatabase.Blocks)br.ReadInt32(),
                                         br.ReadInt32(),
                                         br.ReadInt32(),
                                         br.ReadInt32()));
                }

                switch (mode)
                {
                    case Mode.Game:
                        player = new Player();
                        player.Init(this, PlayerSpawnX, PlayerSpawnY, PlayerSpawnZ);
                        model.Add(player);
                        break;

                    case Mode.Edit:
                        break;

                    default:
                        break;
                }
            }
            catch (Exception e)
            {
                throw new WorldInitFailedException("World loading failed", e);
            }

            br.Close();
        }

        public object DeepCopy()
        {
            World temp = (World) MemberwiseClone();
            temp.model = new xList<I3Dimensional>();

            for (int i = 0; i < this.model.Count; i++)
            {
                Block temp_Block = (Block)this.model[i];

                temp.model.Add(new Block(temp_Block.Type, temp_Block.X, temp_Block.Y, temp_Block.Z));
            }

            return temp;
        }

        public void Render(int x, int y, byte[] imageBuffer, bool[] imageBufferKey)
        {
            model.Render(x, y, imageBuffer, imageBufferKey);
        }
    }
}
