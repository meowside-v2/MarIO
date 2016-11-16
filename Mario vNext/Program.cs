using DKBasicEngine_1_0;

using Mario_vNext.Core.SystemExt;
using Mario_vNext.Data;
using Mario_vNext.Data.Scenes;
using System.Collections;
using System.Drawing;
using System.Globalization;

namespace Mario_vNext
{
    class Program
    {
        static void Main(string[] args)
        {
            WindowControl mainWindow = new WindowControl();
            mainWindow.WindowInit();

            Database.InitDatabase();

            foreach (DictionaryEntry source in Sprites.ResourceManager.GetResourceSet(CultureInfo.CurrentUICulture, true, true))
            {
                Database.AddNewGameObject((string)source.Key, new Material((Bitmap)source.Value));
            }

            WorldEditor worldEdit = new WorldEditor();
            worldEdit.Start();
        }
    }
}
