using System;

namespace CsCat
{
    public class CharUtil
    {
        public static char[] GetCharsAll()
        {
            return CharConst.Chars_Big.AddRange(CharConst.Chars_Small);
        }

        public static char[] GetDigitsAndCharsBig()
        {
            return CharConst.Digits.AddRange(CharConst.Chars_Big);
        }

        public static char[] GetDigitsAndCharsSmall()
        {
            return CharConst.Digits.AddRange(CharConst.Chars_Small);
        }

        public static char[] GetDigitsAndCharsAll()
        {
            return CharConst.Digits.AddRange(CharConst.CharsAll);
        }
    }
}