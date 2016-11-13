using DKBasicEngine_1_0;
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
        Keyboard keyboard = new Keyboard(40, 100, 40);
        Camera cam = new Camera();

        Scene background = new Scene();

        TextBlock nameTxt = new TextBlock()
        {
            X = 0,
            Y = -60,
            Z = (int)TextBlock.Layer.GUI,
            HAlignment = TextBlock.HorizontalAlignment.Center,
            VAlignment = TextBlock.VerticalAlignment.Center,
            ScaleX = 2,
            ScaleY = 2,
            Text = "Level",
            HasShadow = true
        };

        TextBlock returnTxt = new TextBlock()
        {
            X = 0,
            Y = 100,
            Z = (int)TextBlock.Layer.GUI,
            HAlignment = TextBlock.HorizontalAlignment.Center,
            VAlignment = TextBlock.VerticalAlignment.Center,
            ScaleX = 2,
            ScaleY = 2,
            Text = "OK",
            HasShadow = true
        };

        TextBlock selectionTxt = new TextBlock()
        {
            X = 0,
            Y = 0,
            Z = (int)TextBlock.Layer.GUI,
            HAlignment = TextBlock.HorizontalAlignment.Left,
            VAlignment = TextBlock.VerticalAlignment.Top,
            ScaleX = 1,
            ScaleY = 1,
            Text = "-",
            HasShadow = true
        };
        TextBlock selectionTxt2 = new TextBlock()
        {
            X = 0,
            Y = 0,
            Z = (int)TextBlock.Layer.GUI,
            HAlignment = TextBlock.HorizontalAlignment.Left,
            VAlignment = TextBlock.VerticalAlignment.Top,
            ScaleX = 1,
            ScaleY = 1,
            Text = "-",
            HasShadow = true
        };

        TextBox _nameTxt = new TextBox()
        {
            AllowedChars = TextBox.Type.AlphaNumerical,
            X = 0,
            Y = -45,
            Z = (int)TextBlock.Layer.GUI,
            HAlignment = TextBlock.HorizontalAlignment.Center,
            VAlignment = TextBlock.VerticalAlignment.Center,
            ScaleX = 1,
            ScaleY = 1,
            Text = "",
            HasShadow = true
        };

        int textblockSelection = 0;
        bool returnToEdit = false;

        Scene referenceToWorld;

        public NewWorld() { }

        public void Init(Scene worldReference)
        {
            referenceToWorld = worldReference;

            keyboard.onUpArrowKey = this.MoveUp;
            keyboard.onDownArrowKey = this.MoveDown;
            keyboard.onEnterKey = this.EnterDoStuff;
            keyboard.onBackSpaceKey = this.RemoveLetter;

            keyboard.onAlphaNumericalKeys(GetLetter);

            keyboard.Start();

            //background.Init(@"Data\Worlds\WorldEditor.WORLD", Scene.Mode.Game);

            cam.sceneReference = background;
            cam.GUI.AddAll(nameTxt, selectionTxt, selectionTxt2, returnTxt, _nameTxt);

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
            
            _nameTxt = null;
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
            if (textblockSelection < 1)
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
                    referenceToWorld.New(_nameTxt.Text);
                }
                catch (SceneInitFailedException ex)
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
            
            if (!Char.IsWhiteSpace(key) && !Shared.IsEscapeSequence(key) || key == ' ')
            {
                switch (textblockSelection)
                {
                    case 0:
                        _nameTxt.Text = _nameTxt.Text + key;
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
            }

            NewSelectionPosition();
        }
    }
}
