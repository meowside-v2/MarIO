using Mario_vNext.Core;
using Mario_vNext.Core.Exceptions;
using Mario_vNext.Core.Interfaces;
using Mario_vNext.Core.SystemExt;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;

namespace Mario_vNext.Data.Objects
{
    class World : ICore
    {
        public double Gravity { get; set; }
        public string Level { get; set; }

        public List<I3Dimensional> model = new List<I3Dimensional>();

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
                    model.Add(new WorldObject((ObjectDatabase.WorldObjects)br.ReadInt32(),
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
            temp.model = model.ToList();

            return temp;
        }

        public void Render(int x, int y, byte[] bufferData, bool[] bufferKey)
        {
            List<I3Dimensional> temp;

            lock (model)
            {
                temp = model.Where(item => Finder(item, x, y)).ToList();
            }
            
            while (temp.Count > 0 || !BufferIsFull(bufferKey))
            {
                int tempHeight = temp.Max(item => (item).Z);
                List<I3Dimensional> toRender = temp.Where(item => (item).Z == tempHeight).ToList();

                foreach (ICore item in toRender)
                {
                    item.Render(x, y, bufferData, bufferKey);
                }

                temp.RemoveAll(item => toRender.FirstOrDefault(item2 => ReferenceEquals(item, item2)) != null);
            }
            //model.Render(x, y, bufferData, bufferKey);
        }

        private bool Finder(I3Dimensional obj, int x, int y)
        {
            return (obj.X + obj.width >= x && obj.X < x + Shared.RenderWidth && obj.Y + obj.height >= y && obj.Y < y + Shared.RenderHeight);
        }

        private bool FindBiggerZ(I3Dimensional item1, I3Dimensional item2)
        {
            return (item1.Z > item2.Z);
        }

        private bool BufferIsFull(bool[] key)
        {
            return key.FirstOrDefault(ret => ret == false);
        }
    }
}
