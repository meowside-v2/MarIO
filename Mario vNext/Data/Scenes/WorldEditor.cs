using DKBasicEngine_1_0;

using Mario_vNext.Data.Objects;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace Mario_vNext.Data.Scenes
{
    class WorldEditor : ICore
    {
        CancellationTokenSource tokenSource = new CancellationTokenSource();

        Scene map = new Scene();
        WorldObject newBlock = new WorldObject();
        Camera cam = new Camera();

        List<List<I3Dimensional>> undo = new List<List<I3Dimensional>>();

        TextBlock posX = new TextBlock(1, 1, "GUI");
        TextBlock posY = new TextBlock(1, 7, "GUI");
        TextBlock posZ = new TextBlock(1, 13, "GUI");

        private int selected = Database.GetMaterialDatabasePosition("block_01");
        private int undoMaxCapacity = 50;

        public void Start()
        {
            bool _done = false;

            do
            {
                Console.Clear();
                Console.WriteLine("1) NEW");
                Console.WriteLine("2) LOAD");

                switch (Console.ReadLine())
                {
                    case "1":
                        New();
                        _done = !_done;
                        break;

                    case "2":
                        Load();
                        _done = !_done;
                        break;

                    default:
                        continue;
                }
            } while (!_done);
            
            Init();
        }

        private void New()
        {
            NewWorld worldInfoGet = new NewWorld();
            worldInfoGet.Init(map);
            worldInfoGet.Destroy();
        }

        private void Load()
        {
            string[] files = Directory.GetFiles(Environment.CurrentDirectory + @"\Data\Worlds");

            Console.WriteLine(string.Format("\n\n"));

            if (files.Length != 0)
            {
                int count = 0;

                foreach (string s in files)
                {
                    Console.WriteLine(string.Format("{0}) {1}", count++, s));
                }

                try
                {
                    int select = int.Parse(Console.ReadLine());
                    map.Init(files[select], Scene.Mode.Edit);
                }
                catch (SceneInitFailedException e)
                {
                    MessageBox.Show(e.Message);
                    Environment.Exit(-1);
                }
            }

            else
            {
                Console.WriteLine("NO WORLDS FOUND, PRESS ENTER TO CONTINUE");
                Console.ReadLine();

                New();
            }
        }

        private void Init()
        {
            

            newBlock = new WorldObject(Database.GetMaterialDatabaseKey(selected), 0, 0, 0);

            posX.Text = string.Format("X {0}", newBlock.X);
            posY.Text = string.Format("Y {0}", newBlock.Y);
            posZ.Text = string.Format("Z {0}", newBlock.Z);

            cam.GUI.Add(posX);
            cam.GUI.Add(posY);
            cam.GUI.Add(posZ);

            cam.exclusiveReference.Add(newBlock);

            //xRectangle border = new xRectangle(-8, -8, 32, map.Width, map.Height);

            //cam.borderReference = border;
            cam.sceneReference = map;

            cam.Init(-(int)(Engine.Render.RenderWidth / 2 - newBlock.width / 2), -(int)(Engine.Render.RenderHeight / 2 - newBlock.height / 2));
        }
        
        private I3Dimensional BlockFinder(List<I3Dimensional> Model, double x, double y, double z)
        {
            return Model.Where(item => item.X == x && item.Y == y && item.Z == z).FirstOrDefault();
        }

        private void BlockMoveUp()
        {
            if (newBlock.Y > 0)
            {
                newBlock.Y -= 8;
                posY.Text = string.Format("Y {0}", newBlock.Y);

                cam.Yoffset -= 8;
            }
        }

        private void BlockMoveDown()
        {
            newBlock.Y += 8;
            posY.Text = string.Format("Y {0}", newBlock.Y);

            cam.Yoffset += 8;
        }

        private void BlockMoveLeft()
        {
            if (newBlock.X > 0)
            {
                newBlock.X -= 8;
                posX.Text = string.Format("X {0}", newBlock.X);

                cam.Xoffset -= 8;
            }
        }

        private void BlockMoveRight()
        {
            newBlock.X += 8;
            posX.Text = string.Format("X {0}", newBlock.X);

            cam.Xoffset += 8;
        }

        private void BlockPlace()
        {
            undo.Add(map.Model.ToList());
            if (undo.Count > undoMaxCapacity) undo.Remove(undo[0]);

            WorldObject temp = new WorldObject();

            temp = (WorldObject)newBlock.DeepCopy();

            map.Model.Remove(BlockFinder(map.Model, newBlock.X, newBlock.Y, newBlock.Z));
            map.Model.Add(temp);

            newBlock.Type = Database.GetMaterialDatabaseKey(selected);
        }

        private void BlockDelete()
        {
            undo.Add(map.Model.ToList());
            if (undo.Count > undoMaxCapacity) undo.Remove(undo[0]);

            map.Model.Remove(BlockFinder(map.Model, newBlock.X, newBlock.Y, newBlock.Z));
        }

        private void BlockSwitchLeft()
        {
            if (selected > Database.GetMaterialDatabasePosition("block_01"))
            {
                newBlock.X += newBlock.width / 2;
                newBlock.Y += newBlock.height / 2;

                selected--;
                newBlock.Type = Database.GetMaterialDatabaseKey(selected);

                newBlock.X -= newBlock.width / 2;
                newBlock.Y -= newBlock.height / 2;
            }
        }

        private void BlockSwitchRight()
        {
            if (selected < Database.GetMaterialDatabasePosition("water_02"))
            {
                newBlock.X += newBlock.width / 2;
                newBlock.Y += newBlock.height / 2;

                selected++;
                newBlock.Type = Database.GetMaterialDatabaseKey(selected);

                newBlock.X -= newBlock.width / 2;
                newBlock.Y -= newBlock.height / 2;
            }
        }

        private void LayerUp()
        {
            if (newBlock.Z < 99)
            {
                newBlock.Z++;
                posZ.Text = string.Format("Z {0}", newBlock.Z);
            }
        }

        private void LayerDown()
        {
            if (newBlock.Z > 0)
            {
                newBlock.Z--;
                posZ.Text = string.Format("Z {0}", newBlock.Z);
            }
        }

        private void Fill()
        {
            /*undo.Add(map.Model.ToList());
            if (undo.Count > undoMaxCapacity) undo.Remove(undo[0]);

            int Xoffset = 0;
            int Yoffset = 0;
            int Zoffset = newBlock.Z;
            ObjectDatabase.WorldObjects type = newBlock.Type;

            List<I3Dimensional> temp_List = new List<I3Dimensional>(map.Model);

            while (true)
            {
                I3Dimensional temp = BlockFinder(temp_List, Xoffset, Yoffset, Zoffset);

                if (temp != null)
                    temp_List.Remove(temp);

                temp_List.Add(new WorldObject(type, Xoffset, Yoffset, Zoffset));

                Yoffset += newBlock.height;

                if (Xoffset + newBlock.width >= map.Width && Yoffset + newBlock.height > map.Height)
                {
                    map.Model = temp_List;
                    return;
                }

                if (Yoffset + newBlock.height > map.Height)
                {
                    Yoffset = 0;
                    Xoffset += newBlock.width;
                }
            }*/
        }

        private void Save()
        {
            BinaryWriter bw;

            try
            {
                bw = new BinaryWriter(new FileStream("Data\\Worlds\\" + map.Name + ".WORLD", FileMode.Create));
            }
            catch (IOException e)
            {
                MessageBox.Show(e.Message + "\n Cannot create file.");
                return;
            }

            try
            {

                bw.Write(map.Name);

                bw.Write(map.PlayerSpawnX);
                bw.Write(map.PlayerSpawnY);
                bw.Write(map.PlayerSpawnZ);

                bw.Write(map.Model.Count);

                foreach (WorldObject item in map.Model)
                {
                    bw.Write(item.Type);
                    bw.Write(item.X);
                    bw.Write(item.Y);
                    bw.Write(item.Z);
                }
            }
            catch (IOException e)
            {
                MessageBox.Show(e.Message + "\n Cannot write to file.");
                return;
            }

            bw.Close();
        }

        private void Undo()
        {
            if (undo.Count > 0)
            {
                map.Model = undo[undo.Count - 1].ToList();

                undo.Remove(undo[undo.Count - 1]);
            }
        }

        public void Update()
        {
            if (Engine.Input.IsKeyPressed(ConsoleKey.W))
            {
                this.BlockMoveUp();
            }

            if (Engine.Input.IsKeyPressed(ConsoleKey.S))
            {
                this.BlockMoveDown();
            }

            if (Engine.Input.IsKeyPressed(ConsoleKey.A))
            {
                this.BlockMoveLeft();
            }

            if (Engine.Input.IsKeyPressed(ConsoleKey.D))
            {
                this.BlockMoveRight();
            }

            if (Engine.Input.IsKeyPressed(ConsoleKey.Q))
            {
                this.BlockSwitchLeft();
            }

            if (Engine.Input.IsKeyPressed(ConsoleKey.E))
            {
                this.BlockSwitchRight();
            }

            if (Engine.Input.IsKeyPressed(ConsoleKey.Z))
            {
                this.Undo();
            }

            if (Engine.Input.IsKeyPressed(ConsoleKey.Delete) || Engine.Input.IsKeyPressed(ConsoleKey.Backspace))
            {
                this.BlockDelete();
            }

            if (Engine.Input.IsKeyPressed(ConsoleKey.PageUp))
            {
                this.LayerUp();
            }

            if (Engine.Input.IsKeyPressed(ConsoleKey.PageDown))
            {
                this.LayerDown();
            }

            if (Engine.Input.IsKeyPressed(ConsoleKey.Enter))
            {
                this.BlockPlace();
            }

            if (Engine.Input.IsKeyPressed(ConsoleKey.Insert))
            {
                this.Save();
            }
        }

        public void Render(int x, int y, byte[] bufferData, bool[] bufferKey)
        {
            throw new NotImplementedException();
        }
    }
}
