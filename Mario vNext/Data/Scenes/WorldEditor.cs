using Mario_vNext.Core;
using Mario_vNext.Core.Components;
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

        Keyboard keyboard;

        World map = new World();
        Block newBlock = new Block();

        Camera cam = new Camera();

        xList<xList<I3Dimensional>> undo = new xList<xList<I3Dimensional>>();

        TextBlock posX = new TextBlock(1, 1, "GUI");
        TextBlock posY = new TextBlock(1, 7, "GUI");
        TextBlock posZ = new TextBlock(1, 13, "GUI");

        BaseFrame core = new BaseFrame();

        private int Z = 1;
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
            Console.Write("Width (number of blocks):  ");
            int w = int.Parse(Console.ReadLine());

            Console.Write("Height (number of blocks):  ");
            int h = int.Parse(Console.ReadLine());

            Console.Write("World Name:  ");
            string n = Console.ReadLine();

            map = new World(w * 16, h * 16, n);
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

                int select = int.Parse(Console.ReadLine());

                //map = WorldLoader.Load(files[select]);
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
            keyboard = new Keyboard();

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

            newBlock = new Block((ObjectDatabase.Blocks)selected, 0, 0, Z);

            posX.text = string.Format("X {0}", newBlock.X);
            posY.text = string.Format("Y {0}", newBlock.Y);
            posZ.text = string.Format("Z {0}", newBlock.Z);

            core.GUI.Add(posX);
            core.GUI.Add(posY);
            core.GUI.Add(posZ);

            core.exclusiveReference.Add(newBlock);

            xRectangle border = new xRectangle(-8, -8, 32,map.Width, map.Height);

            core.borderReference = border;
            core.worldReference = map;

            cam.Init(-(Shared.RenderWidth / 2 - 8), -(Shared.RenderHeight / 2 - 8), core);
        }
        
        private I3Dimensional BlockFinder(xList<I3Dimensional> model, int x, int y, int z)
        {
            return model.Where(item => item.X == x && item.Y == y && item.Z == z).FirstOrDefault();
        }

        private void BlockMoveUp()
        {
            if (newBlock.Y > 0)
            {
                newBlock.Y -= 16;
                posY.text = string.Format("Y {0}", newBlock.Y);

                cam.Yoffset -= 16;
            }
        }

        private void BlockMoveDown()
        {
            if (newBlock.Y + newBlock.height < map.Height)
            {
                newBlock.Y += 16;
                posY.text = string.Format("Y {0}", newBlock.Y);

                cam.Yoffset += 16;
            }
        }

        private void BlockMoveLeft()
        {
            if (newBlock.X > 0)
            {
                newBlock.X -= 16;
                posX.text = string.Format("X {0}", newBlock.X);

                cam.Xoffset -= 16;
            }
        }

        private void BlockMoveRight()
        {
            if (newBlock.X + newBlock.width < map.Width)
            {
                newBlock.X += 16;
                posX.text = string.Format("X {0}", newBlock.X);

                cam.Xoffset += 16;
            }
        }

        private void BlockPlace()
        {
            undo.Add((xList<I3Dimensional>)map.model.DeepCopy());
            if (undo.Count > undoMaxCapacity) undo.Remove(undo[0]);

            Block temp = new Block();

            temp = (Block)newBlock.DeepCopy();

            map.model.Remove(BlockFinder(map.model, newBlock.X, newBlock.Y, newBlock.Z));
            map.model.Add(temp);

            newBlock.Type = (ObjectDatabase.Blocks)selected;
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
                selected--;
                newBlock.Type = (ObjectDatabase.Blocks)selected;
            }
        }

        private void BlockSwitchRight()
        {
            if (selected < 21)
            {
                selected++;
                newBlock.Type = (ObjectDatabase.Blocks)selected;
            }
        }

        private void LayerUp()
        {
            if (Z < 99)
            {
                Z++;
                posZ.text = string.Format("Z {0}", Z);
            }
        }

        private void LayerDown()
        {
            if (Z > 0)
            {
                Z--;
                posZ.text = string.Format("Z {0}", Z);
            }
        }

        private void Fill()
        {
            undo.Add((xList<I3Dimensional>)map.model.DeepCopy());
            if (undo.Count > undoMaxCapacity) undo.Remove(undo[0]);

            int Xoffset = 0;
            int Yoffset = 0;
            ObjectDatabase.Blocks type = newBlock.Type;

            xList<I3Dimensional> temp = new xList<I3Dimensional>();

            while (true)
            {
                temp.Add(new Block(type, Xoffset, Yoffset, Z));

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
            World temp = new World();
            temp = (World)map.DeepCopy();

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
                bw.Write(temp.Level);
                bw.Write(temp.Width);
                bw.Write(temp.Height);
                bw.Write(temp.Gravity);

                bw.Write(temp.PlayerSpawnX);
                bw.Write(temp.PlayerSpawnY);
                bw.Write(temp.PlayerSpawnZ);

                bw.Write(temp.model.Count);

                foreach (var item in temp.model)
                {
                    bw.Write((int)((item as Block).Type));
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
                map.model = (xList<I3Dimensional>) undo[undo.Count - 1].DeepCopy();

                undo.Remove(undo[undo.Count - 1]);
            }
        }
    }
}
