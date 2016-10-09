using Mario_vNext.Core;
using Mario_vNext.Core.Components;
using Mario_vNext.Core.Exceptions;
using Mario_vNext.Core.SystemExt;
using Mario_vNext.Data.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Mario_vNext.Data.Scenes
{
    class NewWorld
    {
        Keyboard keyboard = new Keyboard(40, 40, 40);
        Camera cam = new Camera();

        World background = new World();

        TextBlock nameTxt = new TextBlock(0,
                                          -40,
                                          "GUI",
                                          TextBlock.HorizontalAlignment.Center,
                                          TextBlock.VerticalAlignment.Center,
                                          "Level name");

        TextBlock widthTxt = new TextBlock(0,
                                           -10,
                                           "GUI",
                                           TextBlock.HorizontalAlignment.Center,
                                           TextBlock.VerticalAlignment.Center,
                                           "Width");

        TextBlock heightTxt = new TextBlock(0,
                                            20,
                                            "GUI",
                                            TextBlock.HorizontalAlignment.Center,
                                            TextBlock.VerticalAlignment.Center,
                                            "Height");

        TextBlock returnTxt = new TextBlock(0,
                                            50,
                                            "GUI",
                                            TextBlock.HorizontalAlignment.Center,
                                            TextBlock.VerticalAlignment.Center,
                                            "OK");

        TextBlock selectionTxt = new TextBlock(0,
                                               0,
                                               "GUI",
                                               TextBlock.HorizontalAlignment.Left,
                                               TextBlock.VerticalAlignment.Top,
                                               "-");
        TextBlock selectionTxt2 = new TextBlock(0,
                                               0,
                                               "GUI",
                                               TextBlock.HorizontalAlignment.Left,
                                               TextBlock.VerticalAlignment.Top,
                                               "-");

        TextBox _nameTxt = new TextBox(0,
                                           -30,
                                           "GUI",
                                           TextBox.HorizontalAlignment.Center,
                                           TextBox.VerticalAlignment.Center,
                                           "");

        TextBox _widthTxt = new TextBox(0,
                                            0,
                                            "GUI",
                                            TextBox.HorizontalAlignment.Center,
                                            TextBox.VerticalAlignment.Center,
                                            "");

        TextBox _heightTxt = new TextBox(0,
                                             30,
                                             "GUI",
                                             TextBox.HorizontalAlignment.Center,
                                             TextBox.VerticalAlignment.Center,
                                             "");

        int textblockSelection = 0;
        bool returnToEdit = false;

        World referenceToWorld;

        public NewWorld() { }

        public void Init(World worldReference)
        {
            referenceToWorld = worldReference;

            _heightTxt.AllowedChars = TextBox.Type.Numerical;
            _widthTxt.AllowedChars = TextBox.Type.Numerical;
            _nameTxt.AllowedChars = TextBox.Type.AlphaNumerical;

            keyboard.onUpArrowKey = this.MoveUp;
            keyboard.onDownArrowKey = this.MoveDown;
            keyboard.onEnterKey = this.EnterDoStuff;
            keyboard.onBackSpaceKey = this.RemoveLetter;

            keyboard.onAlphaNumericalKeys(GetLetter);

            keyboard.Start();

            background.Init(@"Data/Worlds/WorldEditMenu.WORLD", World.Mode.Game);

            cam.worldReference = background;
            cam.GUI.AddAll(nameTxt, widthTxt, heightTxt, selectionTxt, selectionTxt2, returnTxt, _nameTxt, _widthTxt, _heightTxt);

            textblockSelection = 0;
            NewSelectionPosition();

            cam.Init(0, 128);

            SpinWait.SpinUntil(() => { return returnToEdit; });
        }

        public void Destroy()
        {
            keyboard.Abort();
            cam.Abort();

            referenceToWorld = null;
            background = null;

            _heightTxt = null;
            _nameTxt = null;
            _widthTxt = null;

            heightTxt = null;
            widthTxt = null;
            nameTxt = null;
            returnTxt = null;
        }
        
        private void MoveUp()
        {
            if (textblockSelection > 0)
            {
                textblockSelection--;
                NewSelectionPosition();
            }
        }

        private void MoveDown()
        {
            if (textblockSelection < 3)
            {
                textblockSelection++;
                NewSelectionPosition();
            }
        }

        private void EnterDoStuff()
        {
            if (textblockSelection < 3)
                MoveDown();

            else
            {
                try
                {
                    referenceToWorld.New(int.Parse(_heightTxt.Text),
                                         int.Parse(_widthTxt.Text),
                                         _nameTxt.Text);
                }
                catch (WorldInitFailedException ex)
                {
                    System.Windows.Forms.MessageBox.Show(ex.Message);
                    return;
                }
                catch (Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show(ex.Message);
                    return;
                }

                returnToEdit = true;
            }
        }

        private void NewSelectionPosition()
        {
            switch (textblockSelection)
            {
                case 0:
                    selectionTxt.X = _nameTxt.X - selectionTxt.width - 5;
                    selectionTxt.Y = _nameTxt.Y;

                    selectionTxt2.X = _nameTxt.X +_nameTxt.width + 6;
                    selectionTxt2.Y = _nameTxt.Y;
                    break;

                case 1:
                    selectionTxt.X = _widthTxt.X - selectionTxt.width - 5;
                    selectionTxt.Y = _widthTxt.Y;

                    selectionTxt2.X = _widthTxt.X + _widthTxt.width + 6;
                    selectionTxt2.Y = _widthTxt.Y;
                    break;

                case 2:
                    selectionTxt.X = _heightTxt.X - selectionTxt.width - 5;
                    selectionTxt.Y = _heightTxt.Y;

                    selectionTxt2.X = _heightTxt.X + _heightTxt.width + 6;
                    selectionTxt2.Y = _heightTxt.Y;
                    break;

                case 3:
                    selectionTxt.X = returnTxt.X - selectionTxt.width - 5;
                    selectionTxt.Y = returnTxt.Y;

                    selectionTxt2.X = returnTxt.X + returnTxt.width + 6;
                    selectionTxt2.Y = returnTxt.Y;
                    break;
            }
        }

        private void GetLetter()
        {
            if (Console.KeyAvailable)
            {
                char key = Console.ReadKey(true).KeyChar;

                if (!Char.IsWhiteSpace(key) && !Shared.IsEscapeSequence(key) || key == ' ')
                {
                    switch (textblockSelection)
                    {
                        case 0:
                            _nameTxt.Text = _nameTxt.Text + key;
                            break;

                        case 1:
                            _widthTxt.Text = _widthTxt.Text + key;
                            break;

                        case 2:
                            _heightTxt.Text = _heightTxt.Text + key;
                            break;
                    }
                }

                NewSelectionPosition();
            }
        }

        private void RemoveLetter()
        {
            switch (textblockSelection)
            {
                case 0:
                    _nameTxt.RemoveLastLetter();
                    break;

                case 1:
                    _widthTxt.RemoveLastLetter();
                    break;

                case 2:
                    _heightTxt.RemoveLastLetter();
                    break;
            }
        }
    }
}
