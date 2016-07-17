using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mario.Core
{
    class Error
    {
        public enum eError
        {
            MarioSpriteException,
            GroundBlock1Exception,
            GroundBlock2Exception,
            GroundBlock3Exception,
            GroundBlock4Exception,
            GroundBlock5Exception,
            GroundBlock6Exception,
            GroundBlock7Exception,
            GroundBlock8Exception,
            GroundBlock9Exception,
            GroundBlock10Exception,
            GroundBlock11Exception,
            UndergroundBlock1Exception,
            UndergroundBlock2Exception,
            UndergroundBlock3Exception,
            UndergroundBlock4Exception,
            UndergroundBlock5Exception,
            UndergroundBlock6Exception,
            PipeBlock1Exception,
            PipeBlock2Exception,
            PipeBlock3Exception,
            PipeBlock4Exception,
            PipeBlock5Exception,
            BorderException
        }

        private static string[] sError =
        {
            "Ground\\Mario sprite can't be loaded",
            "Ground\\block_01 sprite can't be loaded",
            "Ground\\block_02 sprite can't be loaded",
            "Ground\\block_03 sprite can't be loaded",
            "Ground\\block_04 sprite can't be loaded",
            "Ground\\sky_01 sprite can't be loaded",
            "Ground\\sky_02 sprite can't be loaded",
            "Ground\\sky_03 sprite can't be loaded",
            "Ground\\sky_04 sprite can't be loaded",
            "Ground\\sky_05 sprite can't be loaded",
            "Ground\\sky_06 sprite can't be loaded",
            "Ground\\sky_07 sprite can't be loaded",
            "Underground\\block_01 sprite can't be loaded",
            "Underground\\block_02 sprite can't be loaded",
            "Underground\\block_03 sprite can't be loaded",
            "Underground\\block_04 sprite can't be loaded",
            "Underground\\background_01 sprite can't be loaded",
            "Underground\\background_02 sprite can't be loaded",
            "Pipes\\pipe_01 sprite can't be loaded",
            "Pipes\\pipe_02 sprite can't be loaded",
            "Pipes\\pipe_03 sprite can't be loaded",
            "Pipes\\pipe_04 sprite can't be loaded",
            "Pipes\\pipe_05 sprite can't be loaded",
            "Border sprite can't be loaded"
        };

        public static string ErrorHandle(int type)
        {
            return sError[type] + ", please reinstall game";
        }
    }
}
