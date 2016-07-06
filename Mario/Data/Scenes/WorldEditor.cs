using Mario.Core;
using Mario.Data.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Mario.Data.Scenes
{
    class WorldEditor
    {
        World map;
        Block _newBlock = new Block();

        Camera cam = new Camera();
        
        private int Z = 1;
        private int selected = 0;

        bool EnterPressed = false;
        bool Changed = false;

        xList<object> core = new xList<object>();

        public void Start()
        {
            Console.Write("Width:  ");
            int w = int.Parse(Console.ReadLine());

            Console.Write("Height:  ");
            int h = int.Parse(Console.ReadLine());

            EnterPressed = true;

            map = new World(w, h);

            _newBlock.Init(selected);

            core = new xList<object>(Refill(Z));

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
                        Block temp = layer[j];
                        layer[j] = layer[j + 1];
                        layer[j + 1] = layer[j];
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
                        Block temp = layer[j];
                        layer[j] = layer[j + 1];
                        layer[j + 1] = layer[j];
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
            }
        }

        private List<object> Refill(int Z)
        {
            List<object> ret = new List<object>();

            switch (Z)
            {
                case 0:
                    
                    ret.Add(map.background);
                    ret.Add(_newBlock);
                    ret.Add(map.middleground);
                    ret.Add(map.foreground);

                    break;

                case 1:
                    
                    ret.Add(map.background);
                    ret.Add(map.middleground);
                    ret.Add(_newBlock);
                    ret.Add(map.foreground);

                    break;

                case 2:
                    
                    ret.Add(map.background);
                    ret.Add(map.middleground);
                    ret.Add(map.foreground);
                    ret.Add(_newBlock);

                    break;
            }

            return ret;
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
                Thread.Sleep(10);
                

                if (IsKeyPressed(ConsoleKey.W))
                {
                    _newBlock.Y--;
                }


                if (IsKeyPressed(ConsoleKey.A))
                {
                    _newBlock.X--;
                }

                if (IsKeyPressed(ConsoleKey.D))
                {
                    _newBlock.X++;
                }

                if (IsKeyPressed(ConsoleKey.S))
                {
                    _newBlock.Y++;
                }

                if (IsKeyPressed(ConsoleKey.Enter))
                {

                    if (!EnterPressed)
                    {
                        Block temp = new Block();

                        temp = (Block)_newBlock.Copy();

                        switch (Z)
                        {
                            case 0:
                                map.background.Add(temp);
                                LayerSort(map.background);
                                break;

                            case 1:
                                map.middleground.Add(temp);
                                LayerSort(map.middleground);
                                break;

                            case 2:
                                map.foreground.Add(temp);
                                LayerSort(map.foreground);
                                break;
                        }

                        EnterPressed = true;
                        
                        _newBlock.Init(selected);

                        core = new xList<object>(Refill(Z));
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
                            map.background.Add(_newBlock);
                            LayerSort(map.background);
                            break;

                        case 1:
                            map.middleground.Add(_newBlock);
                            LayerSort(map.middleground);
                            break;

                        case 2:
                            map.foreground.Add(_newBlock);
                            LayerSort(map.foreground);
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
                    if (selected < 15 && !Changed)
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
                        core = new xList<object>(Refill(Z));
                        Changed = true;
                    }
                }

                else if (IsKeyPressed(ConsoleKey.PageDown))
                {
                    if (Z > 0 && !Changed)
                    {
                        core = new xList<object>(Refill(Z));
                        Changed = true;
                    }
                }

                else Changed = false;
            }
        }
    }
}
