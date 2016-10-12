using Mario_vNext.Core.SystemExt;
using Mario_vNext.Data.Scenes;

namespace Mario_vNext
{
    class Program
    {
        static void Main(string[] args)
        {
            WindowControl mainWindow = new WindowControl();
            mainWindow.WindowInit();

            ObjectDatabase.Init();

            //Test test = new Test();

            WorldEditor worldEdit = new WorldEditor();
            worldEdit.Start();
        }
    }
}
