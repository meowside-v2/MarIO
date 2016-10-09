namespace Mario_vNext.Core
{
    class Shared
    {
        public static int RenderWidth = 640;
        public static int RenderHeight = 360;

        public static bool IsEscapeSequence(char letter)
        {
            if (letter == '\0')
                return true;

            if (letter == '\a')
                return true;

            if (letter == '\b')
                return true;

            if (letter == '\f')
                return true;

            if (letter == '\n')
                return true;

            if (letter == '\r')
                return true;

            if (letter == '\t')
                return true;

            if (letter == '\v')
                return true;

            if (letter == '\'')
                return true;

            if (letter == '\"')
                return true;

            return false;
        }
    }
}
