using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mario.Core
{
    class ObjectDatabase
    {
        public enum Object
        {
            Mario,
            BlockGround1,
            BlockGround2,
            BlockGround3,
            BlockGround4,
            BlockUnderGround1,
            BlockUnderGround2,
            BlockUnderGround3,
            BlockUnderGround4,
            Sky1,
            Sky2,
            Sky3,
            Sky4,
            Sky5,
            Sky6,
            UnderGroundBackground1,
            UnderGroundBackground2
        };

        public static readonly string[] path =
        {
            "\\Data\\Sprites\\mario_12x16.png",
            "\\Data\\Sprites\\Ground\\block_01.png",
            "\\Data\\Sprites\\Ground\\block_02.png",
            "\\Data\\Sprites\\Ground\\block_03.png",
            "\\Data\\Sprites\\Ground\\block_04.png",
            "\\Data\\Sprites\\Underground\\block_01.png",
            "\\Data\\Sprites\\Underground\\block_02.png",
            "\\Data\\Sprites\\Underground\\block_03.png",
            "\\Data\\Sprites\\Underground\\block_04.png",
            "\\Data\\Sprites\\Ground\\sky_01.png",
            "\\Data\\Sprites\\Ground\\sky_02.png",
            "\\Data\\Sprites\\Ground\\sky_03.png",
            "\\Data\\Sprites\\Ground\\sky_04.png",
            "\\Data\\Sprites\\Ground\\sky_05.png",
            "\\Data\\Sprites\\Ground\\sky_06.png",
            "\\Data\\Sprites\\Underground\\background_01.png",
            "\\Data\\Sprites\\Underground\\background_02.png"
        };
    }
}
