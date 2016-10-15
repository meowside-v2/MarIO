using Mario_vNext.Core.Components;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace Mario_vNext.Core.SystemExt
{
    static class ObjectDatabase
    {
        public enum Live
        {
            Mario
        };

        /*public static readonly string[] path_live =
        {
            "\\Data\\Sprites\\mario_12x16.png"
        };*/

        public enum WorldObjects
        {
            BlockGround1,
            BlockGround2,
            BlockGround3,
            BlockGround4,
            Bridge,
            Bush1,
            Bush2,
            Bush3,
            BushSmall,
            CastleBig,
            CastleSmall,
            Cloud1,
            Cloud2,
            Cloud3,
            Fence,
            FinishFlag,
            FlagPole,
            Mountain,
            Sky,
            Water1,
            Water2,
            UnderGroundBackground1,
            UnderGroundBackground2,
            BlockUnderGround1,
            BlockUnderGround2,
            BlockUnderGround3,
            BlockUnderGround4,
            Pipe1,
            Pipe2,
            Pipe3,
            Pipe4,
            Pipe5,
            Border
        };

        public static List<Material> worldObjectsMaterial = new List<Material>();

        private static void CreateBlockSpriteReferences()
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
                        worldObjectsMaterial.Add(new Material((Bitmap) Image.FromStream(ms)));
                    }
                }
            }
        }

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

        public static List<Material> letterMaterial = new List<Material>();

        private static void CreateLetterReferences()
        {
            using (BinaryReader br = new BinaryReader(new FileStream(Environment.CurrentDirectory + @"\MarioFontFile.MEX", FileMode.Open)))
            {
                int lenght = br.ReadInt32();

                for (int index = 0; index < lenght; index++)
                {
                    int temp = br.ReadInt32();
                    byte[] byteArray = br.ReadBytes(temp);

                    using (MemoryStream ms = new MemoryStream(byteArray))
                    {
                        letterMaterial.Add(new Material((Bitmap) Image.FromStream(ms)));
                    }
                }
            }
        }

        public static void Init()
        {
            CreateBlockSpriteReferences();
            CreateLetterReferences();
        }
    }
}
