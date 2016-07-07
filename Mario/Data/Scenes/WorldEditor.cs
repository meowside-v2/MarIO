﻿using Mario.Core;
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
            Console.Write("Width:  ");
            int w = int.Parse(Console.ReadLine());

            Console.Write("Height:  ");
            int h = int.Parse(Console.ReadLine());

            EnterPressed = true;

            map = new World(w, h);

            _newBlock.Init(selected);
            
            posX.Text("X " + _newBlock.X.ToString());
            posY.Text("Y " + _newBlock.Y.ToString());
            posZ.Text("Z " + Z.ToString());

            core.background.Add(map.background);
            core.middleground.Add(map.middleground);
            core.foreground.Add(map.foreground);

            core.middleground.Add(_newBlock);

            core.UI.Add(posX);
            core.UI.Add(posY);
            core.UI.Add(posZ);

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
                Thread.Sleep(50);
                

                if (IsKeyPressed(ConsoleKey.W))
                {
                    _newBlock.Y--;
                    posY.Text("Y " + _newBlock.Y.ToString());
                }


                if (IsKeyPressed(ConsoleKey.A))
                {
                    _newBlock.X--;
                    posX.Text("X " + _newBlock.X.ToString());
                }

                if (IsKeyPressed(ConsoleKey.D))
                {
                    _newBlock.X++;
                    posX.Text("X " + _newBlock.X.ToString());
                }

                if (IsKeyPressed(ConsoleKey.S))
                {
                    _newBlock.Y++;
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
                        posZ.Text("Z " + Z.ToString());
                        Changed = true;
                    }
                }

                else if (IsKeyPressed(ConsoleKey.PageDown))
                {
                    if (Z > 0 && !Changed)
                    {
                        posZ.Text("Z " + Z.ToString());
                        Changed = true;
                    }
                }

                else Changed = false;
            }
        }
    }
}
