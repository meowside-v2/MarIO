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

        public enum Font
        {
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
            space
        };

        public static readonly string[] font_path =
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
            "\\Data\\Font\\space.png"
        };
    }
}
