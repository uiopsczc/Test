using UnityEngine;
using UnityEngine.UI;

namespace CsCat
{
  public static class TextExtension
  {
    public static void SetText(this Text self, string content = null, Color? color = null, int? font_size = null)
    {
      if (content != null)
        self.text = content;
      if (color.HasValue)
        self.color = color.Value;
      if (font_size.HasValue)
        self.fontSize = font_size.Value;
    }

    public static void SetIsGray(this Text self, bool is_gray)
    {
      TextUtil.SetIsGray(self, is_gray);
    }

    public static void SetAlpha(this Text self, float alpha)
    {
      TextUtil.SetAlpha(self, alpha);
    }

    public static void SetColor(this Text self, Color color, bool is_not_use_color_alpha = false)
    {
      TextUtil.SetColor(self, color, is_not_use_color_alpha);
    }
  }
}