using UnityEngine;

namespace CsCat
{
	public class RichTextUtil
	{
		public static string SetColor(string s, Color color)
		{
			return string.Format(StringConst.String_Format_Text_Color, ColorUtility.ToHtmlStringRGB(color), s);
		}

		public static string SetIsBold(string s)
		{
			return string.Format(StringConst.String_Format_Text_Bold, s);
		}

		public static string SetIsItalic(string s)
		{
			return string.Format(StringConst.String_Format_Text_Italic, s);
		}

		public static string SetFontSize(string s, int fontSize)
		{
			return string.Format(StringConst.String_Format_Text_FontSize, fontSize, s);
		}
	}
}