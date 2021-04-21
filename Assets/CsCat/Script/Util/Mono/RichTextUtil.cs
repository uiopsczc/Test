

using UnityEngine;

namespace CsCat
{
  public class RichTextUtil
  {
    public static string SetColor(string s, Color c)
    {
      return string.Format("<color=#{0}>{1}</color>", ColorUtility.ToHtmlStringRGB(c), s);
      ;
    }

    public static string SetIsBold(string s)
    {
      return string.Format("<b>{0}</b>", s);
    }

    public static string SetIsItalic(string s)
    {
      return string.Format("<i>{0}</i>", s);
    }

    public static string SetFontSize(string s, int font_size)
    {
      return string.Format("<size=#{0}>{1}</size>", font_size, s); ;
    }
  }
}