using Mario.Data.Objects;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mario.Core
{

    class Camera
    {
        public int Xoffset { get; set; }
        public int Yoffset { get; set; }

        public static int RENDER_WIDTH = Console.LargestWindowWidth;
        public static int RENDER_HEIGHT = Console.LargestWindowHeight;

        private const int MAX_FRAME_RATE = 1000;    // Frames per Second
        private const short sampleSize = 100;
        private int lastTime = 0;
        private int numRenders = 0;
        private bool _Vsync = true;

        private short _sectors = 0;

        private short sectorsRendered
        {
            get
            {
                return _sectors;
            }

            set
            {
                this._sectors = value;
            }
        }

        private bool bufferingDone
        {
            get
            {
                if (sectorsRendered == 3)
                {
                    sectorsRendered = 0;
                    return true;
                }

                return false;
            }

            set
            {

            }
        }

        private xList<SectorPreferences> sectorsToRender = new xList<SectorPreferences>();

        private Task[] sectorRenderTask;

        private BaseHiararchy core = new BaseHiararchy();
        private BaseHiararchy world_objects;

        private TextBlock fpsMeter = new TextBlock();

        private CancellationTokenSource ct = new CancellationTokenSource();

        private Bitmap tempFrame;

        public void Init(BaseHiararchy world_objects, int Xoffset = 0, int Yoffset = 0)
        {
            tempFrame = (Bitmap)Settings.SetNewResolution(Settings.availableMaxResolution);

            this.Xoffset = Xoffset;
            this.Yoffset = Yoffset;

            this.world_objects = world_objects;
            
            fpsMeter.X = 1;
            fpsMeter.Y = RENDER_HEIGHT - 6;

            fpsMeter.Text("");

            world_objects.UI.Add(fpsMeter);

            Console.CursorVisible = false;
            Console.Title = "MarIO";
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;

            sectorsToRender = SplitScreenIntoSectors(RENDER_HEIGHT, RENDER_WIDTH);

            sectorRenderTask = new Task[sectorsToRender.Count];

            System.Windows.Forms.Timer WindowMaximizer = new System.Windows.Forms.Timer();
            WindowMaximizer.Interval = 10;
            WindowMaximizer.Tick += WindowSizeChecker;
            WindowMaximizer.Start();

            Task Buff = Task.Factory.StartNew(() => Buffering(world_objects));
            Task Rend = Task.Factory.StartNew(() => Rendering());
        }

        private xList<SectorPreferences> SplitScreenIntoSectors(int _rednderHeight, int _rednderWidth)
        {
            xList<SectorPreferences> retList = new xList<SectorPreferences>();

            //----------------------------------------------------------------//
            //----------------------------------------------------------------//
            //----------------------------------------------------------------//
            //----------------------------------------------------------------//
            //----------------------------------------------------------------//
            //----------------------------------------------------------------//
            //----------------------------------------------------------------//
            //----------------------------------------------------------------//

            return retList;
        }

        private void WindowSizeChecker(object sender, EventArgs e)
        {
            bool Sized = false;

            if (Console.WindowHeight != Console.LargestWindowHeight || Console.WindowWidth != Console.LargestWindowWidth)
            {
                WindowMaximize.Maximize();
            }
        }

        private void Buffering(BaseHiararchy world_objects)
        {
            Bitmap renderedFrame = (Bitmap)Settings.SetNewResolution(Settings.availableMaxResolution);

            while (true)
            {
                int beginRender = Environment.TickCount;

                core = (BaseHiararchy)world_objects.DeepCopy();

                int count = 0;
                
                foreach (SectorPreferences sector in sectorsToRender)
                {
                    sectorRenderTask[count++] = Task.Factory.StartNew(() => SectorRender(sector));
                }

                Task.WaitAll(sectorRenderTask);

                int endRender = Environment.TickCount - beginRender;

                if (_Vsync) Vsync(MAX_FRAME_RATE, endRender, true);

                if (numRenders == 0)
                {
                    lastTime = Environment.TickCount;
                }

                numRenders++;

                tempFrame = (Bitmap) renderedFrame.Clone();

                if (ct.IsCancellationRequested) return;
            }
        }

        private void SectorRender(SectorPreferences sec, Bitmap imageToRender)
        {
            for(int row = sec.startPointY; row <= sec.endPointY; row++)
            {
                for(int column = sec.startPointX; column <= sec.endPointX; column++)
                {
                    imageToRender.SetPixel(column, row, core.Render(column, row));
                }
            }
        }

        private void Rendering()
        {
            while (true)
            {
                int beginRender = Environment.TickCount;

                Point location = new Point(0, 0);
                Size imageSize = new Size(Console.WindowWidth, Console.WindowHeight); // desired image size in characters

                using (Graphics g = Graphics.FromHwnd(GetConsoleWindow()))
                {
                    using (Image outFrame = (Image)tempFrame.Clone())
                    {
                        Size fontSize = GetConsoleFontSize();

                        // translating the character positions to pixels
                        Rectangle imageRect = new Rectangle(
                            location.X * fontSize.Width,
                            location.Y * fontSize.Height,
                            imageSize.Width * fontSize.Width,
                            imageSize.Height * fontSize.Height);

                        g.DrawImage(outFrame, imageRect);
                    }
                }

                int endRender = Environment.TickCount - beginRender;

                Vsync(MAX_FRAME_RATE, endRender, false);

                if (ct.IsCancellationRequested) return;
            }
        }

        /*private void Rendering()
        {
            int beginRender = Environment.TickCount;
                
            DirectBuffer.Out(RENDER_WIDTH, RENDER_HEIGHT, RENDER_WIDTH * RENDER_HEIGHT, frame_colors, frame);

            int endRender = Environment.TickCount - beginRender;

            if (_Vsync) Vsync(MAX_FRAME_RATE, endRender, false);
        }*/

        private void Vsync(int TargetFrameRate, int imageRenderDelay, bool renderFPS)
        {
            int targetDelay = 1000 / TargetFrameRate;

            if (imageRenderDelay < targetDelay)
            {
                Thread.Sleep(targetDelay - imageRenderDelay);
            }

            if (renderFPS)
            {
                if (numRenders == sampleSize)
                {
                    int temp = Environment.TickCount - lastTime;

                    if (temp > 0)
                    {
                        fpsMeter.Text(string.Format("{0}", sampleSize * 1000 / temp));
                        Debug.WriteLine(string.Format("{0}", sampleSize * 1000 / temp));
                    }

                    numRenders = 0;
                }
            }
        }

        private static Size GetConsoleFontSize()
        {
            // getting the console out buffer handle
            IntPtr outHandle = CreateFile("CONOUT$",
                                           GENERIC_READ | GENERIC_WRITE,
                                           FILE_SHARE_READ | FILE_SHARE_WRITE,
                                           IntPtr.Zero,
                                           OPEN_EXISTING,
                                           0,
                                           IntPtr.Zero);

            int errorCode = Marshal.GetLastWin32Error();
            if (outHandle.ToInt32() == INVALID_HANDLE_VALUE)
            {
                throw new IOException("Unable to open CONOUT$", errorCode);
            }

            ConsoleFontInfo cfi = new ConsoleFontInfo();

            if (!GetCurrentConsoleFont(outHandle, false, cfi))
            {
                throw new InvalidOperationException("Unable to get font information.");
            }

            return new Size(cfi.dwFontSize.X, cfi.dwFontSize.Y);
        }

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern IntPtr GetConsoleWindow();

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern IntPtr CreateFile(
            string lpFileName,
            int dwDesiredAccess,
            int dwShareMode,
            IntPtr lpSecurityAttributes,
            int dwCreationDisposition,
            int dwFlagsAndAttributes,
            IntPtr hTemplateFile);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool GetCurrentConsoleFont(
            IntPtr hConsoleOutput,
            bool bMaximumWindow,
            [Out][MarshalAs(UnmanagedType.LPStruct)]ConsoleFontInfo lpConsoleCurrentFont);

        [StructLayout(LayoutKind.Sequential)]
        internal class ConsoleFontInfo
        {
            internal int nFont;
            internal Coord dwFontSize;
        }

        [StructLayout(LayoutKind.Explicit)]
        internal struct Coord
        {
            [FieldOffset(0)]
            internal short X;
            [FieldOffset(2)]
            internal short Y;
        }

        private const int GENERIC_READ = unchecked((int)0x80000000);
        private const int GENERIC_WRITE = 0x40000000;
        private const int FILE_SHARE_READ = 1;
        private const int FILE_SHARE_WRITE = 2;
        private const int INVALID_HANDLE_VALUE = -1;
        private const int OPEN_EXISTING = 3;
        

        /*private xList<RenderedSectorPreferences> SplitScreenIntoSectors(int dimensionOne, int dimensionTwo)
        {
            xList<RenderedSectorPreferences> returnList = new xList<RenderedSectorPreferences>();

            RenderedSectorPreferences secOne = new RenderedSectorPreferences();
            RenderedSectorPreferences secTwo = new RenderedSectorPreferences();
            RenderedSectorPreferences secThree = new RenderedSectorPreferences();
            RenderedSectorPreferences secFour = new RenderedSectorPreferences();

            secOne.startPointX = 0;
            secOne.startPointY = 0;

            secOne.endPointX = dimensionTwo / 2;
            secOne.endPointY = dimensionOne / 2;

            returnList.Add(secOne);

            secTwo.startPointX = 0;
            secTwo.startPointY = secOne.endPointY + 1;

            secTwo.endPointX = secOne.endPointX;
            secTwo.endPointY = dimensionOne - 1;

            returnList.Add(secTwo);

            secThree.startPointX = secOne.endPointX + 1;
            secThree.startPointY = 0;

            secThree.endPointX = dimensionTwo - 1;
            secThree.endPointY = secOne.endPointY;

            returnList.Add(secThree);

            secFour.startPointX = secThree.startPointX;
            secFour.startPointY = secTwo.startPointY;

            secFour.endPointX = dimensionTwo - 1;
            secFour.endPointY = dimensionOne - 1;

            returnList.Add(secFour);

            Debug.WriteLine(string.Format("Height {0}  Width {1}",
                                            dimensionOne,
                                            dimensionTwo));

            Debug.WriteLine(string.Format("X1 {0}  Y1 {1}  X2 {2}  Y2 {3}",
                                            secOne.startPointX,
                                            secOne.startPointY,
                                            secOne.endPointX,
                                            secOne.endPointY));

            Debug.WriteLine(string.Format("X1 {0}  Y1 {1}  X2 {2}  Y2 {3}",
                                            secTwo.startPointX,
                                            secTwo.startPointY,
                                            secTwo.endPointX,
                                            secTwo.endPointY));

            Debug.WriteLine(string.Format("X1 {0}  Y1 {1}  X2 {2}  Y2 {3}",
                                            secThree.startPointX,
                                            secThree.startPointY,
                                            secThree.endPointX,
                                            secThree.endPointY));

            Debug.WriteLine(string.Format("X1 {0}  Y1 {1}  X2 {2}  Y2 {3}",
                                            secFour.startPointX,
                                            secFour.startPointY,
                                            secFour.endPointX,
                                            secFour.endPointY));


            return returnList;
        }*/
    }
}

//
//  Buffer backup
//

/*
            for(int row = 0; row < map.mesh.height; row++)
            {
                for(int column = 0; column < map.mesh.width; column++)
                {
                    if(map.mesh.bitmapTransparent[row, column] == 255)
                    {
                        buffer[row * map.mesh.width + column] = map.mesh.bitmapTransparent[row, column];
                        render_colors[row * map.mesh.width + column] = map.mesh.bitmapColor[row * map.mesh.width + column];
                    }
                }
            }

            foreach (var item in nearby)
            {
                if (item.X + Render_WIDTH - 1 >= 0 && item.X < Render_WIDTH && item.Y + RENDER_HEIGHT - 1 >= 0 && item.Y < RENDER_HEIGHT)
                {
                    for (int row = 0; row < item.mesh.height; row++)
                    {
                        for (int column = 0; column < item.mesh.width; column++)
                        {
                            if (item.X + column >= 0 && item.X + column < Render_WIDTH && item.Y + row < RENDER_HEIGHT && item.Y + row >= 0)
                            {
                                if (item.mesh.bitmapTransparent[row, column] == 255)
                                {
                                    buffer[item.Y * Render_WIDTH + row * Render_WIDTH + item.X + column] = 219;
                                    render_colors[item.Y * Render_WIDTH + row * Render_WIDTH + item.X + column] = item.mesh.bitmapColor[row * item.mesh.width + column];
                                }
                            }
                        }
                    }
                }
            }

            for(int row = 0; row < player.mesh.height; row++)
            {
                for( int column = 0; column < player.mesh.width; column++)
                {
                    if (player.X + column >= 0 && player.X + column < Render_WIDTH && player.Y + row < RENDER_HEIGHT && player.Y + row >= 0)
                    {
                        if (player.mesh.bitmapTransparent[row, column] == 255)
                        {
                            buffer[player.Y * Render_WIDTH + row * Render_WIDTH + player.X + column] = 219;
                            render_colors[player.Y * Render_WIDTH + row * Render_WIDTH + player.X + column] = player.mesh.bitmapColor[row * player.mesh.width + column];

                        }
                    }
                }
            }
*/


/*foreach (var item in core)
                {
                    if (item == null)
                    {
                        break;
                    }

                    else if (item is TextBlock)
                    {
                        TextBlock ui_text = item as TextBlock;
                        int currentPosX = 0;

                        foreach (var letter in ui_text.text)
                        {
                            if (letter == null) break;

                            for (int row = 0; row < letter.height; row++)
                            {
                                for (int column = 0; column < letter.width; column++)
                                {
                                    if (ui_text.X + currentPosX + column >= 0 && ui_text.X + currentPosX + column < RENDER_WIDTH && ui_text.Y + row < RENDER_HEIGHT && ui_text.Y + row >= 0)
                                    {
                                        if (letter.bitmapTransparent[row, column] == 255)
                                        {
                                            buffer[(ui_text.Y + row) * RENDER_WIDTH + ui_text.X + currentPosX + column] = 219;
                                            render_colors[(ui_text.Y + row) * RENDER_WIDTH + ui_text.X + currentPosX + column] = letter.bitmapColor[row * letter.width + column];

                                        }
                                    }
                                }
                            }

                            currentPosX += letter.width + 1;
                        }
                    }

                    else if (item is xList<Block>)
                    {
                        xList<Block> layer = item as xList<Block>;
                        bool newIndex = false;
                        int index = 0;

                        foreach (var block in layer.Skip(Xindex))
                        {
                            if (block.mesh == null) break;

                            if (!newIndex)
                                if (block.X + block.mesh.width > Xoffset && block.X < Xoffset + RENDER_WIDTH)
                                {
                                    Xindex = index;
                                    newIndex = true;
                                }

                            for (int row = 0; row < block.mesh.height; row++)
                            {
                                for (int column = 0; column < block.mesh.width; column++)
                                {
                                    if (block.X + column >= 0 && block.X + column < RENDER_WIDTH && block.Y + row < RENDER_HEIGHT && block.Y + row >= 0)
                                    {
                                        if (block.mesh.bitmapTransparent[row, column] == 255)
                                        {
                                            buffer[block.Y * RENDER_WIDTH + row * RENDER_WIDTH + block.X + column] = 219;
                                            render_colors[block.Y * RENDER_WIDTH + row * RENDER_WIDTH + block.X + column] = block.mesh.bitmapColor[row * block.mesh.width + column];

                                        }
                                    }
                                }
                            }
                        }
                    }

                    else if (item is Player)
                    {
                        Player player = item as Player;

                        for (int row = 0; row < player.mesh.height; row++)
                        {
                            for (int column = 0; column < player.mesh.width; column++)
                            {
                                if (player.X + column >= 0 && player.X + column < RENDER_WIDTH && player.Y + row < RENDER_HEIGHT && player.Y + row >= 0)
                                {
                                    if (player.mesh.bitmapTransparent[row, column] == 255)
                                    {
                                        buffer[player.Y * RENDER_WIDTH + row * RENDER_WIDTH + player.X + column] = 219;
                                        render_colors[player.Y * RENDER_WIDTH + row * RENDER_WIDTH + player.X + column] = player.mesh.bitmapColor[row * player.mesh.width + column];

                                    }
                                }
                            }
                        }
                    }

                    else if (item is xList<Enemy>)
                    {
                        xList<Enemy> nearby = item as xList<Enemy>;

                        foreach (var item2 in nearby.ToList())
                        {
                            if (item2.X + RENDER_WIDTH - 1 >= 0 && item2.X < RENDER_WIDTH && item2.Y + RENDER_HEIGHT - 1 >= 0 && item2.Y < RENDER_HEIGHT)
                            {
                                for (int row = 0; row < item2.mesh.height; row++)
                                {
                                    for (int column = 0; column < item2.mesh.width; column++)
                                    {
                                        if (item2.X + column >= 0 && item2.X + column < RENDER_WIDTH && item2.Y + row < RENDER_HEIGHT && item2.Y + row >= 0)
                                        {
                                            if (item2.mesh.bitmapTransparent[row, column] == 255)
                                            {
                                                buffer[item2.Y * RENDER_WIDTH + row * RENDER_WIDTH + item2.X + column] = 219;
                                                render_colors[item2.Y * RENDER_WIDTH + row * RENDER_WIDTH + item2.X + column] = item2.mesh.bitmapColor[row * item2.mesh.width + column];
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }

                    else if (item is Block)
                    {
                        Block block = item as Block;

                        for (int row = 0; row < block.mesh.height; row++)
                        {
                            for (int column = 0; column < block.mesh.width; column++)
                            {
                                if (block.X + column >= 0 && block.X + column < RENDER_WIDTH && block.Y + row < RENDER_HEIGHT && block.Y + row >= 0)
                                {
                                    if (block.mesh.bitmapTransparent[row, column] == 255)
                                    {
                                        buffer[block.Y * RENDER_WIDTH + row * RENDER_WIDTH + block.X + column] = 219;
                                        render_colors[block.Y * RENDER_WIDTH + row * RENDER_WIDTH + block.X + column] = block.mesh.bitmapColor[row * block.mesh.width + column];

                                    }
                                }
                            }
                        }
                    }
                }*/
