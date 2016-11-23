

namespace Mario_vNext.Core.Components
{
    /*abstract class Dialog : ICore, I3Dimensional
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }

        public int depth { get; }
        public int height { get; }
        public int width { get; }
        
        public double ScaleX { get; set; }
        public double ScaleY { get; set; }
        public double ScaleZ { get; set; }

        public string Caption { get; set; }

        protected List<I3Dimensional> Content = new List<I3Dimensional>();

        protected Material background;

        protected Keyboard Control = new Keyboard(null, 100, null);

        public void Render(int X, int Y, byte[] bufferData, bool[] bufferKey)
        {
            foreach (ICore item in Content)
            {
                item.Render(X, Y, bufferData, bufferKey);
            }

            if (background != null) background.Render(X, Y, bufferData, bufferKey, 1, 1, Color.DarkCyan);
        }
    }

    class BlockDialog : Dialog
    {
        private int selection = 0;

        WorldObject toChange = new WorldObject();

        TextBlock coordX = new TextBlock()
        {
            HAlignment = TextBlock.HorizontalAlignment.Left,
            VAlignment = TextBlock.VerticalAlignment.Top,
            X = 2,
            Y = 20,
            Text = "X",
            HasShadow = true
        };

        TextBlock coordY = new TextBlock()
        {
            HAlignment = TextBlock.HorizontalAlignment.Left,
            VAlignment = TextBlock.VerticalAlignment.Top,
            X = 2,
            Y = 30,
            Text = "Y",
            HasShadow = true
        };

        TextBlock objectType = new TextBlock()
        {
            HAlignment = TextBlock.HorizontalAlignment.Left,
            VAlignment = TextBlock.VerticalAlignment.Top,
            X = 2,
            Y = 60,
            Text = "Type",
            HasShadow = true
        };

        TextBlock OK = new TextBlock()
        {
            HAlignment = TextBlock.HorizontalAlignment.Left,
            VAlignment = TextBlock.VerticalAlignment.Top,
            X = 2,
            Y = 100,
            Text = "Type",
            HasShadow = true
        };

        TextBlock Cancel = new TextBlock()
        {
            HAlignment = TextBlock.HorizontalAlignment.Left,
            VAlignment = TextBlock.VerticalAlignment.Top,
            X = 40,
            Y = 100,
            Text = "Type",
            HasShadow = true
        };

        TextBlock SelLeft = new TextBlock()
        {
            HAlignment = TextBlock.HorizontalAlignment.Left,
            VAlignment = TextBlock.VerticalAlignment.Top,
            X = 0,
            Y = 0,
            Text = "-",
            HasShadow = true
        };

        TextBlock SelRight = new TextBlock()
        {
            HAlignment = TextBlock.HorizontalAlignment.Left,
            VAlignment = TextBlock.VerticalAlignment.Top,
            X = 0,
            Y = 0,
            Text = "-",
            HasShadow = true
        };

        public BlockDialog(WorldObject reference)
        {
            this.Content.AddAll(coordX, coordY, objectType, OK, Cancel);


        }

        public void ChangeCursorPosition()
        {
            switch (selection)
            {
                case 0:
                    break;

                case 1:
                    break;

                case 2:
                    break;

                case 3:
                    break;

                case 4:
                    break;
            }
        }

        private void MoveCursorUp()
        {
            if(selection > 0)
            {
                selection--;
                ChangeCursorPosition();
            }
        }

        private void MoveCursorDown()
        {
            if(selection < 4)
            {
                selection++;
                ChangeCursorPosition();
            }
        }
    }*/
}
