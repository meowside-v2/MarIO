using Mario_vNext.Core.Components;
using Mario_vNext.Core.SystemExt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mario_vNext.Data.Scenes
{
    class NewWorld
    {
        Keyboard keyboard = new Keyboard();
        Camera cam = new Camera();

        TextBlock nameTxt = new TextBlock();
        TextBlock widthTxt = new TextBlock();
        TextBlock heightTxt = new TextBlock();

        TextBlock _nameTxt = new TextBlock();
        TextBlock _widthTxt = new TextBlock();
        TextBlock _heightTxt = new TextBlock();

        int textblockSelection = 0;

        public void Init()
        {
            //keyboard
        }

        private void MoveUp()
        {
            if (textblockSelection < 2)
                textblockSelection++;
        }

        private void MoveDown()
        {
            if (textblockSelection > 0)
                textblockSelection--;
        }
    }
}
