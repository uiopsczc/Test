

using UnityEngine;

namespace CsCat
{
  public class RichTextUtil
  {
    public static string MakeColored(string s, Color c)
    {
      return string.Format("<color=#{0}>{1}</color>", ColorUtility.ToHtmlStringRGB(c), s);
      ;
    }

    public static string MakeBold(string s)
    {
      return string.Format("<b>{0}</b>", s);
    }

    public static string MakeItalic(string s)
    {
      return string.Format("<i>{0}</i>", s);
    }

  }
}