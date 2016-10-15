using Mario_vNext.Core;
using Mario_vNext.Core.Components;
using Mario_vNext.Core.Exceptions;
using Mario_vNext.Core.SystemExt;
using Mario_vNext.Data.Objects;
using System;
using System.Collections.Generic;
using System.Drawing;
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
                                          TextBlock.HAlignment.Center,
                                          TextBlock.VAlignment.Center,
                                          "Level name",
                                          Shared.pfc.Families[0],
                                          24f,
                                          Color.White,
                                          true);

        TextBlock widthTxt = new TextBlock(0,
                                           -10,
                                           "GUI",
                                           TextBlock.HAlignment.Center,
                                           TextBlock.VAlignment.Center,
                                           "Width",
                                           Shared.pfc.Families[0],
                                          24f,
                                          Color.White,
                                          true);

        TextBlock heightTxt = new TextBlock(0,
                                            20,
                                            "GUI",
                                            TextBlock.HAlignment.Center,
                                            TextBlock.VAlignment.Center,
                                            "Height",
                                            Shared.pfc.Families[0],
                                          24f,
                                          Color.White,
                                          true);

        TextBlock returnTxt = new TextBlock(0,
                                            50,
                                            "GUI",
                                            TextBlock.HAlignment.Center,
                                            TextBlock.VAlignment.Center,
                                            "OK",
                                            Shared.pfc.Families[0],
                                          24f,
                                          Color.White,
                                          true);

        TextBlock selectionTxt = new TextBlock(0,
                                               0,
                                               "GUI",
                                               TextBlock.HAlignment.Left,
                                               TextBlock.VAlignment.Top,
                                               ">",
                                               Shared.pfc.Families[0],
                                          24f,
                                          Color.White,
                                          true);
        TextBlock selectionTxt2 = new TextBlock(0,
                                               0,
                                               "GUI",
                                               TextBlock.HAlignment.Left,
                                               TextBlock.VAlignment.Top,
                                               "<",
                                               Shared.pfc.Families[0],
                                          24f,
                                          Color.White,
                                          true);

        TextBox _nameTxt = new TextBox(0,
                                           -30,
                                           "GUI",
                                           TextBox.HAlignment.Center,
                                           TextBox.VAlignment.Center,
                                           "",
                                           Shared.pfc.Families[0],
                                          18f,
                                          Color.White,
                                          true,
                                          TextBox.Type.AlphaNumerical);

        TextBox _widthTxt = new TextBox(0,
                                            0,
                                            "GUI",
                                            TextBox.HAlignment.Center,
                                            TextBox.VAlignment.Center,
                                            "",
                                            Shared.pfc.Families[0],
                                          18f,
                                          Color.White,
                                          true,
                                          TextBox.Type.Numerical);

        TextBox _heightTxt = new TextBox(0,
                                             30,
                                             "GUI",
                                             TextBox.HAlignment.Center,
                                             TextBox.VAlignment.Center,
                                             "",
                                             Shared.pfc.Families[0],
                                          18f,
                                          Color.White,
                                          true,
                                          TextBox.Type.Numerical);

        int textblockSelection = 0;
        bool returnToEdit = false;

        World referenceToWorld;

        public NewWorld() { }

        public void Init(World worldReference)
        {
            referenceToWorld = worldReference;

            keyboard.onUpArrowKey = this.MoveUp;
            keyboard.onDownArrowKey = this.MoveDown;
            keyboard.onEnterKey = this.EnterDoStuff;
            keyboard.onBackSpaceKey = this.RemoveLetter;

            keyboard.onAlphaNumericalKeys(GetLetter);

            keyboard.Start();

            background.Init(@"Data\Worlds\WorldEditor.WORLD", World.Mode.Game);

            cam.worldReference = background;
            cam.GUI.AddAll(nameTxt, widthTxt, heightTxt, selectionTxt, selectionTxt2, returnTxt, _nameTxt, _widthTxt, _heightTxt);

            textblockSelection = 0;
            NewSelectionPosition();

            cam.Init(0, 0);

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

            TextBlock reference = null;

            switch (textblockSelection)
            {
                case 0:
                    reference = _nameTxt;
                    break;

                case 1:
                    reference = _widthTxt;
                    break;

                case 2:
                    reference = _heightTxt;
                    break;

                case 3:
                    reference = returnTxt;
                    break;
            }

            if (reference != null)
            {
                selectionTxt.X = reference.X - selectionTxt.width - 5;
                selectionTxt.Y = reference.Y - reference.height / 2 + 2;

                selectionTxt2.X = reference.X + reference.width + 6;
                selectionTxt2.Y = reference.Y - reference.height / 2 + 2;
            }
        }

        private void GetLetter()
        {
            char key = Console.ReadKey(true).KeyChar;

            if (Console.KeyAvailable)
            {
                Console.ReadKey(true);
            }

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

            NewSelectionPosition();
        }
    }
}
