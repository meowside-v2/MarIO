using Mario.Core;
using Mario.Data.Objects;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mario.Data.Scenes
{
    class WorldEditor
    {
        World map;
        Block _newBlock = new Block();

        TextBlock posX = new TextBlock(1, 1);
        TextBlock posY = new TextBlock(1, 7);
        TextBlock posZ = new TextBlock(1, 13);

        Camera cam = new Camera();
        
        private int Z = 1;
        private int selected = 0;

        bool EnterPressed = false;
        bool Changed = false;

        FrameBaseHiararchy core = new FrameBaseHiararchy();

        public void Start()
        {
            Console.WriteLine("1) NEW");
            Console.WriteLine("2) LOAD");

            switch (int.Parse(Console.ReadLine()))
            {
                case 1:
                    New();
                    break;

                case 2:
                    Load();
                    break;
            }
        }

        private void New()
        {
            Console.Write("Width (number of blocks):  ");
            int w = int.Parse(Console.ReadLine());

            Console.Write("Height (number of blocks):  ");
            int h = int.Parse(Console.ReadLine());

            Console.Write("World Name:  ");
            string n = Console.ReadLine();

            EnterPressed = true;

            map = new World(w * 16, h * 16, n);

            Init();
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

                map = WorldLoader.Load(files[select]);

                Init();
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
            _newBlock.Init(selected);

            posX.Text("X " + _newBlock.X.ToString());
            posY.Text("Y " + _newBlock.Y.ToString());
            posZ.Text("Z " + Z.ToString());

            core.background.Add(map.background);
            core.middleground.Add(map.middleground);
            core.foreground.Add(map.foreground);
            
            core.UI.Add(posX);
            core.UI.Add(posY);
            core.UI.Add(posZ);

            core.exclusive.Add(_newBlock);

            cam.Init(core);

            Thread keychecker = new Thread(() => KeyPress());
            keychecker.Start();
        }

        private void LayerSort(List<Block> layer)
        {
            for(int i = 0; i < layer.Count - 1; i++)
            {
                for(int j = 0; j < layer.Count - 1; j++)
                {
                    if(layer[j].X > layer[j + 1].X)
                    {

                        Block temp = (Block)layer[j].DeepCopy();
                        layer[j] = (Block)layer[j + 1].DeepCopy();
                        layer[j + 1] = (Block)temp.DeepCopy();
                    }
                }
            }

            for (int i = 0; i < layer.Count - 1; i++)
            {
                for (int j = 0; j < layer.Count - 1; j++)
                {
                    if(layer[j].X == layer[j + 1].X)
                        if (layer[j].Y > layer[j + 1].Y)
                        {
                            Block temp = (Block)layer[j].DeepCopy();
                            layer[j] = (Block)layer[j + 1].DeepCopy();
                            layer[j + 1] = (Block)temp.DeepCopy();
                        }
                }
            }
        }

        private void BlockFinder(List<Block> layer, int X, int Y)
        {
            for(int i = 0; i < layer.Count; i++)
            {
                if (layer[i].X == X && layer[i].Y == Y)
                {
                    layer.Remove(layer[i]);
                    return;
                }

                if (layer[i].X > X && layer[i].Y > Y) return;
            }
        }

        private void Fill(List<Block> layer)
        {
            int Xoffset = 0;
            int Yoffset = 0;

            while(true)
            {
                layer.Add(new Block(Xoffset, Yoffset, _newBlock.Type));
                Yoffset += _newBlock.mesh.height;

                if (Xoffset + _newBlock.mesh.width > map.width && Yoffset + _newBlock.mesh.height > map.height)
                {
                    return;
                }

                if (Yoffset + _newBlock.mesh.height > map.height)
                {
                    Yoffset = 0;
                    Xoffset += _newBlock.mesh.width;
                }

            }
        }

        private bool Save(World w)
        {
            BinaryWriter bw;

            try
            {
                bw = new BinaryWriter(new FileStream("Data\\Worlds\\" + map.Level + ".World", FileMode.Create));
            }
            catch (IOException e)
            {
                MessageBox.Show(e.Message + "\n Cannot create file.");
                return false;
            }

            try
            {
                bw.Write(w.width);
                bw.Write(w.height);

                bw.Write(w.PlayerSpawnX);
                bw.Write(w.PlayerSpawnY);

                bw.Write(w.foreground.Count);

                foreach (Block item in w.foreground)
                {
                    bw.Write((int)item.X);
                    bw.Write((int)item.Y);
                    bw.Write(item.Type);
                }

                bw.Write(w.middleground.Count);

                foreach (Block item in w.middleground)
                {
                    bw.Write((int)item.X);
                    bw.Write((int)item.Y);
                    bw.Write(item.Type);
                }

                bw.Write(w.background.Count);

                foreach (Block item in w.background)
                {
                    bw.Write((int)item.X);
                    bw.Write((int)item.Y);
                    bw.Write(item.Type);
                }
            }

            catch (IOException e)
            {
                MessageBox.Show(e.Message + "\n Cannot write to file.");
                return false;
            }

            bw.Close();

            return true;
        }

        //
        //  Controls
        //

        [DllImport("user32.dll")]
        public static extern ushort GetKeyState(short nVirtKey);

        public const ushort keyDownBit = 0x80;

        public bool IsKeyPressed(ConsoleKey key)
        {
            return ((GetKeyState((short)key) & keyDownBit) == keyDownBit);
        }
        
        private void KeyPress()
        {
            
            while (true)
            {
                Thread.Sleep(100);
                

                if (IsKeyPressed(ConsoleKey.W))
                {
                    _newBlock.Y -= 16;
                    posY.Text("Y " + _newBlock.Y.ToString());
                }


                if (IsKeyPressed(ConsoleKey.A))
                {
                    if(_newBlock.X > 0)
                    {
                        _newBlock.X -= 16;
                        posX.Text("X " + _newBlock.X.ToString());

                        if (_newBlock.X < cam.Xoffset && _newBlock.X >= 0)
                        {
                            cam.Xoffset -= _newBlock.mesh.width;
                        }
                    }
                }

                if (IsKeyPressed(ConsoleKey.D))
                {
                    if(_newBlock.X < map.width)
                    {
                        _newBlock.X += 16;
                        posX.Text("X " + _newBlock.X.ToString());

                        if (_newBlock.X + _newBlock.mesh.width - cam.Xoffset > cam.RENDER_WIDTH && _newBlock.X <= map.width)
                        {
                            cam.Xoffset += _newBlock.mesh.width;
                        }
                    }
                }

                if (IsKeyPressed(ConsoleKey.S))
                {
                    _newBlock.Y += 16;
                    posY.Text("Y " + _newBlock.Y.ToString());
                }

                if (IsKeyPressed(ConsoleKey.Enter))
                {

                    if (!EnterPressed)
                    {
                        Block temp = new Block();

                        temp = (Block)_newBlock.DeepCopy();

                        switch (Z)
                        {
                            case 0:
                                BlockFinder(map.background, (int)_newBlock.X, (int)_newBlock.Y);
                                map.background.Add(temp);
                                LayerSort(map.background);
                                break;

                            case 1:
                                BlockFinder(map.middleground, (int)_newBlock.X, (int)_newBlock.Y);
                                map.middleground.Add(temp);
                                LayerSort(map.middleground);
                                break;

                            case 2:
                                BlockFinder(map.foreground, (int)_newBlock.X, (int)_newBlock.Y);
                                map.foreground.Add(temp);
                                LayerSort(map.foreground);
                                break;
                        }

                        EnterPressed = true;
                        
                        _newBlock.Init(selected);
                    }
                }

                else
                {
                    EnterPressed = false;
                }

                if (IsKeyPressed(ConsoleKey.Delete) || IsKeyPressed(ConsoleKey.Backspace))
                {
                    switch (Z)
                    {
                        case 0:
                            BlockFinder(map.background, (int)_newBlock.X, (int)_newBlock.Y);
                            break;

                        case 1:
                            BlockFinder(map.middleground, (int)_newBlock.X, (int)_newBlock.Y);
                            break;

                        case 2:
                            BlockFinder(map.foreground, (int)_newBlock.X, (int)_newBlock.Y);
                            break;
                    }
                }

                if (IsKeyPressed(ConsoleKey.Q))
                {
                    if (selected > 0 && !Changed)
                    {
                        selected--;
                        _newBlock.Init(selected);
                        Changed = true;
                    }
                }

                else if (IsKeyPressed(ConsoleKey.E))
                {
                    if (selected < 21 && !Changed)
                    {
                        selected++;
                        _newBlock.Init(selected);
                        Changed = true;
                    }
                }

                else if (IsKeyPressed(ConsoleKey.PageUp))
                {
                    if (Z < 2 && !Changed)
                    {
                        Z++;
                        posZ.Text("Z " + Z.ToString());
                        Changed = true;
                    }
                }

                else if (IsKeyPressed(ConsoleKey.PageDown))
                {
                    if (Z > 0 && !Changed)
                    {
                        Z--;
                        posZ.Text("Z " + Z.ToString());
                        Changed = true;
                    }
                }

                else if (IsKeyPressed(ConsoleKey.F))
                {
                    switch (Z)
                    {
                        case 0:
                            Fill(map.background);
                            break;

                        case 1:
                            Fill(map.middleground);
                            break;

                        case 2:
                            Fill(map.foreground);
                            break;
                    }
                }

                else if (IsKeyPressed(ConsoleKey.End))
                {
                    World temp = new World();
                    temp = (World)map.DeepCopy();

                    Save(temp);
                }

                else Changed = false;
            }
        }
    }
}
