using System;

namespace CsCat
{
    public class CharUtil
    {
        public static char[] GetCharsAll()
        {
            return CharConst.Chars_Big.Combine(CharConst.Chars_Small);
        }

        public static char[] GetDigitsAndCharsBig()
        {
            return CharConst.Digits.Combine(CharConst.Chars_Big);
        }

        public static char[] GetDigitsAndCharsSmall()
        {
            return CharConst.Digits.Combine(CharConst.Chars_Small);
        }

        public static char[] GetDigitsAndCharsAll()
        {
            return CharConst.Digits.Combine(GetCharsAll());
        }
    }
}