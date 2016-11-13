using DKBasicEngine_1_0;
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

        public static void Init()
        {
            CreateBlockSpriteReferences();
        }
    }
}
