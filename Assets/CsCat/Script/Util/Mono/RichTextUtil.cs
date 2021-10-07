using UnityEngine;

namespace CsCat
{
    public class RichTextUtil
    {
        public static string SetColor(string s, Color color)
        {
            return string.Format(StringConst.String_Text_Color_Format, ColorUtility.ToHtmlStringRGB(color), s);
        }

        public static string SetIsBold(string s)
        {
            return string.Format(StringConst.String_Text_Bold_Format, s);
        }

        public static string SetIsItalic(string s)
        {
            return string.Format(StringConst.String_Text_Italic_Format, s);
        }

        public static string SetFontSize(string s, int fontSize)
        {
            return string.Format(StringConst.String_Text_FontSize_Format, fontSize, s);
        }
    }
}