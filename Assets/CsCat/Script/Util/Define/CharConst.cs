using System.Runtime.CompilerServices;

namespace CsCat
{
    public static class CharConst
    {
        public const char Char_Plus = '+';
        public const char Char_Mutiply = 'x';
        public const char Char_Space = ' ';
        public const char Char_Minus = '-';
        public const char Char_Underline = '_';
        public const char Char_Div = '/';
        public const char Char_Slash = '/';
        public const char Char_SlashN = '\n';
        public const char Char_SlashR = '\r';
        public const char Char_Colon = ':';
        public const char Char_Comma = ',';
        public const char Char_Dot = '.';
        public const char Char_DoubleQuotes = '\"';
        public const char Char_Quotes = '\'';
        public const char Char_LeftRoundBrackets = '(';
        public const char Char_RightRoundBrackets = ')';
        public const char Char_LeftSquareBrackets = '[';
        public const char Char_RightSquareBrackets = ']';
        public const char Char_LeftCurlyBrackets = '{';
        public const char Char_RightCurlyBrackets = '}';
        public const char Char_LeftAngleBrackets = '<';
        public const char Char_RightAngleBrackets = '>';
        public const char Char_Vertical = '|';
        public const char Char_Tilde = '~';
        public const char Char_Tab = '\t';
        public const char Char_NumberSign = '#';
        public const char Char_BackSlash = '\\';

        public const char Char_0 = '0';
        public const char Char_1 = '1';
        public const char Char_2 = '2';
        public const char Char_3 = '3';
        public const char Char_4 = '4';
        public const char Char_5 = '5';
        public const char Char_6 = '6';
        public const char Char_7 = '7';
        public const char Char_8 = '8';
        public const char Char_9 = '9';

        public const char Char_A = 'A';
        public const char Char_X = 'X';
        public const char Char_Z = 'Z';

        public const char Char_a = 'a';
        public const char Char_c = 'c';
        public const char Char_x = 'x';
        public const char Char_y = 'y';
        public const char Char_z = 'z';

        public static readonly char[] Digits =
        {
            '0', '1', '2', '3', '4', '5', '6', '7', '8', '9'
        };

        public static readonly char[] Chars_Big =
        {
            'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J',
            'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T',
            'U', 'V', 'W', 'X', 'Y', 'Z'
        };

        public static readonly char[] Chars_Small =
        {
            'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j',
            'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w',
            'x', 'y', 'z'
        };

        private static char[] _CharsAll;
        public static char[] CharsAll => _CharsAll ?? (_CharsAll = CharUtil.GetCharsAll());
        private static char[] _DigitsAndCharsBig;

        public static char[] DigitsAndCharsBig =>
            _DigitsAndCharsBig ?? (_DigitsAndCharsBig = CharUtil.GetDigitsAndCharsBig());

        private static char[] _DigitsAndCharsSmall;

        public static char[] DigitsAndCharsSmall =>
            _DigitsAndCharsSmall ?? (_DigitsAndCharsSmall = CharUtil.GetDigitsAndCharsSmall());

        private static char[] _DigitsAndCharsAll;

        public static char[] DigitsAndCharsAll =>
            _DigitsAndCharsAll ?? (_DigitsAndCharsAll = CharUtil.GetDigitsAndCharsAll());
    }
}