using Mario_vNext.Core;
using Mario_vNext.Core.Components;
using Mario_vNext.Core.Exceptions;
using Mario_vNext.Core.Interfaces;
using Mario_vNext.Core.SystemExt;
using Mario_vNext.Data.Objects;
using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace Mario_vNext.Data.Scenes
{
    class WorldEditor
    {
        CancellationTokenSource tokenSource = new CancellationTokenSource();

        Keyboard keyboard = new Keyboard(100, 100, 100);
        World map = new World();
        WorldObject newBlock = new WorldObject();
        Camera cam = new Camera();

        xList<xList<I3Dimensional>> undo = new xList<xList<I3Dimensional>>();

        TextBlock posX = new TextBlock(1,
                                        1,
                                        "GUI",
                                        TextBlock.HAlignment.Left,
                                        TextBlock.VAlignment.Top,
                                        "",
                                        Shared.pfc.Families[0],
                                        5f,
                                        System.Drawing.Color.White,
                                        true);
        TextBlock posY = new TextBlock(1,
                                        7,
                                        "GUI",
                                        TextBlock.HAlignment.Left,
                                        TextBlock.VAlignment.Top,
                                        "",
                                        Shared.pfc.Families[0],
                                        5f,
                                        System.Drawing.Color.White,
                                        true);
        TextBlock posZ = new TextBlock(1,
                                        13,
                                        "GUI",
                                        TextBlock.HAlignment.Left,
                                        TextBlock.VAlignment.Top,
                                        "",
                                        Shared.pfc.Families[0],
                                        5f,
                                        System.Drawing.Color.White,
                                        true);
        
        private int selected = 0;
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
                    map.Init(files[select], World.Mode.Edit);
                }
                catch (WorldInitFailedException e)
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
            keyboard.onWKey = this.BlockMoveUp;
            keyboard.onSKey = this.BlockMoveDown;
            keyboard.onAKey = this.BlockMoveLeft;
            keyboard.onDKey = this.BlockMoveRight;
            keyboard.onQKey = this.BlockSwitchLeft;
            keyboard.onEKey = this.BlockSwitchRight;
            keyboard.onFKey = this.Fill;
            keyboard.onZKey = this.Undo;
            keyboard.onDeleteKey = this.BlockDelete;
            keyboard.onBackSpaceKey = this.BlockDelete;
            keyboard.onPageUpKey = this.LayerUp;
            keyboard.onPageDownKey = this.LayerDown;
            keyboard.onEnterKey = this.BlockPlace;
            keyboard.onInsertKey = this.Save;

            keyboard.Start();

            newBlock = new WorldObject((ObjectDatabase.WorldObjects)selected, 0, 0, 0);

            posX.Text = string.Format("X {0}", newBlock.X);
            posY.Text = string.Format("Y {0}", newBlock.Y);
            posZ.Text = string.Format("Z {0}", newBlock.Z);

            cam.GUI.Add(posX);
            cam.GUI.Add(posY);
            cam.GUI.Add(posZ);

            cam.exclusiveReference.Add(newBlock);

            xRectangle border = new xRectangle(-8, -8, 32, map.Width, map.Height);

            cam.borderReference = border;
            cam.worldReference = map;

            cam.Init(-(Shared.RenderWidth / 2 - newBlock.width / 2), -(Shared.RenderHeight / 2 - newBlock.height / 2));
        }
        
        private I3Dimensional BlockFinder(xList<I3Dimensional> model, int x, int y, int z)
        {
            return model.Where(item => item.X == x && item.Y == y && item.Z == z).FirstOrDefault();
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
            if (newBlock.Y + newBlock.height < map.Height)
            {
                newBlock.Y += 8;
                posY.Text = string.Format("Y {0}", newBlock.Y);

                cam.Yoffset += 8;
            }
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
            if (newBlock.X + newBlock.width < map.Width)
            {
                newBlock.X += 8;
                posX.Text = string.Format("X {0}", newBlock.X);

                cam.Xoffset += 8;
            }
        }

        private void BlockPlace()
        {
            undo.Add((xList<I3Dimensional>)map.model.DeepCopy());
            if (undo.Count > undoMaxCapacity) undo.Remove(undo[0]);

            WorldObject temp = new WorldObject();

            temp = (WorldObject)newBlock.DeepCopy();

            map.model.Remove(BlockFinder(map.model, newBlock.X, newBlock.Y, newBlock.Z));
            map.model.Add(temp);

            newBlock.Type = (ObjectDatabase.WorldObjects)selected;
        }

        private void BlockDelete()
        {
            undo.Add((xList<I3Dimensional>)map.model.DeepCopy());
            if (undo.Count > undoMaxCapacity) undo.Remove(undo[0]);

            map.model.Remove(BlockFinder(map.model, newBlock.X, newBlock.Y, newBlock.Z));
        }

        private void BlockSwitchLeft()
        {
            if (selected > 0)
            {
                newBlock.X += newBlock.width / 2;
                newBlock.Y += newBlock.height / 2;

                selected--;
                newBlock.Type = (ObjectDatabase.WorldObjects)selected;

                newBlock.X -= newBlock.width / 2;
                newBlock.Y -= newBlock.height / 2;
            }
        }

        private void BlockSwitchRight()
        {
            if (selected < (int)ObjectDatabase.WorldObjects.Pipe5)
            {
                newBlock.X += newBlock.width / 2;
                newBlock.Y += newBlock.height / 2;

                selected++;
                newBlock.Type = (ObjectDatabase.WorldObjects)selected;

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
            undo.Add((xList<I3Dimensional>)map.model.DeepCopy());
            if (undo.Count > undoMaxCapacity) undo.Remove(undo[0]);

            int Xoffset = 0;
            int Yoffset = 0;
            ObjectDatabase.WorldObjects type = newBlock.Type;

            xList<I3Dimensional> temp = new xList<I3Dimensional>();

            while (true)
            {
                temp.Add(new WorldObject(type, Xoffset, Yoffset, newBlock.Z));

                Yoffset += newBlock.height;

                if (Xoffset + newBlock.width >= map.Width && Yoffset + newBlock.height > map.Height)
                {
                    map.model = temp;
                    return;
                }

                if (Yoffset + newBlock.height > map.Height)
                {
                    Yoffset = 0;
                    Xoffset += newBlock.width;
                }
            }
        }

        private void Save()
        {
            keyboard.Abort();

            BinaryWriter bw;

            try
            {
                bw = new BinaryWriter(new FileStream("Data\\Worlds\\" + map.Level + ".WORLD", FileMode.Create));
            }
            catch (IOException e)
            {
                MessageBox.Show(e.Message + "\n Cannot create file.");
                return;
            }

            try
            {

                bw.Write(map.Level);
                bw.Write(map.Width);
                bw.Write(map.Height);
                bw.Write(map.Gravity);

                bw.Write(map.PlayerSpawnX);
                bw.Write(map.PlayerSpawnY);
                bw.Write(map.PlayerSpawnZ);

                bw.Write(map.model.Count);

                foreach (WorldObject item in map.model)
                {
                    bw.Write((int)item.Type);
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

            keyboard.Start();
        }

        private void Undo()
        {
            if (undo.Count > 0)
            {
                map.model = (xList<I3Dimensional>) undo[undo.Count - 1].DeepCopy();

                undo.Remove(undo[undo.Count - 1]);
            }
        }
    }
}
