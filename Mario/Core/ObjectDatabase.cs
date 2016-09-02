using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mario.Core
{
    class ObjectDatabase
    {
        public enum Live
        {
            Mario
        };

        /*public static readonly string[] path_live =
        {
            "\\Data\\Sprites\\mario_12x16.png"
        };*/

        public enum Blocks
        {
            BlockGround1,
            BlockGround2,
            BlockGround3,
            BlockGround4,
            Sky1,
            Sky2,
            Sky3,
            Sky4,
            Sky5,
            Sky6,
            Sky7,
            BlockUnderGround1,
            BlockUnderGround2,
            BlockUnderGround3,
            BlockUnderGround4,
            UnderGroundBackground1,
            UnderGroundBackground2,
            Pipe1,
            Pipe2,
            Pipe3,
            Pipe4,
            Pipe5,
            Border
        };

        public static List<Material> blockMesh = new List<Material>();

        public static void CreateBlockSpriteReferences()
        {
            using (BinaryReader br = new BinaryReader(new FileStream(Environment.CurrentDirectory + @"\MarioBlockSpriteFile.MEX", FileMode.Open)))
            {
                int lenght = br.ReadInt32();

                for (int index = 0; index < lenght; index++)
                {
                    int temp = br.ReadInt32();
                    byte[] byteArray = br.ReadBytes(temp);

                    using (MemoryStream ms = new MemoryStream(byteArray))
                    {
                        blockMesh.Add(new Material(Image.FromStream(ms)));
                    }
                }
            }
        }

        /*public static readonly string[] path_block =
        {
            "\\Data\\Sprites\\Ground\\block_01.png",
            "\\Data\\Sprites\\Ground\\block_02.png",
            "\\Data\\Sprites\\Ground\\block_03.png",
            "\\Data\\Sprites\\Ground\\block_04.png",
            "\\Data\\Sprites\\Ground\\sky_01.png",
            "\\Data\\Sprites\\Ground\\sky_02.png",
            "\\Data\\Sprites\\Ground\\sky_03.png",
            "\\Data\\Sprites\\Ground\\sky_04.png",
            "\\Data\\Sprites\\Ground\\sky_05.png",
            "\\Data\\Sprites\\Ground\\sky_06.png",
            "\\Data\\Sprites\\Ground\\sky_07.png",
            "\\Data\\Sprites\\Underground\\block_01.png",
            "\\Data\\Sprites\\Underground\\block_02.png",
            "\\Data\\Sprites\\Underground\\block_03.png",
            "\\Data\\Sprites\\Underground\\block_04.png",
            "\\Data\\Sprites\\Underground\\background_01.png",
            "\\Data\\Sprites\\Underground\\background_02.png",
            "\\Data\\Sprites\\Pipes\\pipe_01.png",
            "\\Data\\Sprites\\Pipes\\pipe_02.png",
            "\\Data\\Sprites\\Pipes\\pipe_03.png",
            "\\Data\\Sprites\\Pipes\\pipe_04.png",
            "\\Data\\Sprites\\Pipes\\pipe_05.png",
            "\\Data\\Sprites\\border.png"
        };*/

        public enum Font
        {
            Num0,
            Num1,
            Num2,
            Num3,
            Num4,
            Num5,
            Num6,
            Num7,
            Num8,
            Num9,
            A,
            B,
            C,
            D,
            E,
            F,
            G,
            H,
            I,
            J,
            K,
            L,
            M,
            N,
            O,
            P,
            Q,
            R,
            S,
            T,
            U,
            V,
            W,
            X,
            Y,
            Z,
            minus,
            space,
            NumberOfTypes
        };

        /*public static readonly string[] font_path =
        {
            "\\Data\\Font\\A.png",
            "\\Data\\Font\\B.png",
            "\\Data\\Font\\C.png",
            "\\Data\\Font\\D.png",
            "\\Data\\Font\\E.png",
            "\\Data\\Font\\F.png",
            "\\Data\\Font\\G.png",
            "\\Data\\Font\\H.png",
            "\\Data\\Font\\I.png",
            "\\Data\\Font\\J.png",
            "\\Data\\Font\\K.png",
            "\\Data\\Font\\L.png",
            "\\Data\\Font\\M.png",
            "\\Data\\Font\\N.png",
            "\\Data\\Font\\O.png",
            "\\Data\\Font\\P.png",
            "\\Data\\Font\\Q.png",
            "\\Data\\Font\\R.png",
            "\\Data\\Font\\S.png",
            "\\Data\\Font\\T.png",
            "\\Data\\Font\\U.png",
            "\\Data\\Font\\V.png",
            "\\Data\\Font\\W.png",
            "\\Data\\Font\\X.png",
            "\\Data\\Font\\Y.png",
            "\\Data\\Font\\Z.png",
            "\\Data\\Font\\0.png",
            "\\Data\\Font\\1.png",
            "\\Data\\Font\\2.png",
            "\\Data\\Font\\3.png",
            "\\Data\\Font\\4.png",
            "\\Data\\Font\\5.png",
            "\\Data\\Font\\6.png",
            "\\Data\\Font\\7.png",
            "\\Data\\Font\\8.png",
            "\\Data\\Font\\9.png",
            "\\Data\\Font\\space.png",
            "\\Data\\Font\\minus.png"
        };*/

        public static Dictionary<char, Font> font = new Dictionary<char, Font>()
        {
            { '0' , Font.Num0 },
            { '1' , Font.Num1 },
            { '2' , Font.Num2 },
            { '3' , Font.Num3 },
            { '4' , Font.Num4 },
            { '5' , Font.Num5 },
            { '6' , Font.Num6 },
            { '7' , Font.Num7 },
            { '8' , Font.Num8 },
            { '9' , Font.Num9 },
            { 'A' , Font.A },
            { 'B' , Font.B },
            { 'C' , Font.C },
            { 'D' , Font.D },
            { 'E' , Font.E },
            { 'F' , Font.F },
            { 'G' , Font.G },
            { 'H' , Font.H },
            { 'I' , Font.I },
            { 'J' , Font.J },
            { 'K' , Font.K },
            { 'L' , Font.L },
            { 'M' , Font.M },
            { 'N' , Font.N },
            { 'O' , Font.O },
            { 'P' , Font.P },
            { 'Q' , Font.Q },
            { 'R' , Font.R },
            { 'S' , Font.S },
            { 'T' , Font.T },
            { 'U' , Font.U },
            { 'V' , Font.V },
            { 'W' , Font.W },
            { 'X' , Font.X },
            { 'Y' , Font.Y },
            { 'Z' , Font.Z },
            { '-' , Font.minus },
            { ' ' , Font.space }
        };

        public static List<Material> letterMesh = new List<Material>();

        public static void CreateLetterReferences()
        {
            using (BinaryReader br = new BinaryReader(new FileStream(Environment.CurrentDirectory + @"\MarioFontFile.MEX", FileMode.Open)))
            {
                int lenght = br.ReadInt32();

                for(int index = 0; index < lenght; index++)
                {
                    int temp = br.ReadInt32();
                    byte[] byteArray = br.ReadBytes(temp);

                    using (MemoryStream ms = new MemoryStream(byteArray))
                    {
                        letterMesh.Add(new Material(Image.FromStream(ms)));
                    }
                }
            }
        }
    }
}
