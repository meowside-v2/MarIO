using DKBasicEngine_1_0;

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
            Engine.Init();
            
            foreach (DictionaryEntry source in Sprites.ResourceManager.GetResourceSet(CultureInfo.CurrentUICulture, true, true))
            {
                Database.AddNewGameObjectMaterial((string)source.Key, new Material((Bitmap)source.Value));
            }

            WorldEditor worldEdit = new WorldEditor();
            worldEdit.Start();
        }
    }
}
