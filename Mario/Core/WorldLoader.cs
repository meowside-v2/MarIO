using Mario.Data.Objects;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mario.Core
{
    class WorldLoader
    {
        public static World Load(string path)
        {
            World world = new World();

            BinaryReader br;

            try
            {
                br = new BinaryReader(new FileStream(path, FileMode.Open));
            }
            catch (IOException e)
            {
                MessageBox.Show(e.Message + "selected world can't be loaded");
                return null;
            }

            try
            {
                world.Level = br.ReadString();
                world.width = br.ReadInt32();
                world.height = br.ReadInt32();

                world.PlayerSpawnX = br.ReadInt32();
                world.PlayerSpawnY = br.ReadInt32();

                int counter = br.ReadInt32();

                for(int i = 0; i < counter; i++)
                {
                    Block temp = new Block(br.ReadInt32(), br.ReadInt32(), br.ReadInt32());
                    world.foreground.Add(temp);
                }

                counter = br.ReadInt32();

                for (int i = 0; i < counter; i++)
                {
                    Block temp = new Block(br.ReadInt32(), br.ReadInt32(), br.ReadInt32());
                    world.middleground.Add(temp);
                }

                counter = br.ReadInt32();

                for (int i = 0; i < counter; i++)
                {
                    Block temp = new Block(br.ReadInt32(), br.ReadInt32(), br.ReadInt32());
                    world.background.Add(temp);
                }
            }
            catch (IOException e)
            {
                MessageBox.Show(e.Message + "selected world can't be loaded");
                return null;
            }

            br.Close();

            return world;
        }
    }
}
