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
            WorldSpriteException
        }

        public static string[] sError =
        {
            "Mario sprite can't be loaded",
            "World sprite can't be loaded"
        };

        public static string ErrorHandle(eError type)
        {
            return sError[(int)type] + ", please reinstall game";
        }
    }
}
