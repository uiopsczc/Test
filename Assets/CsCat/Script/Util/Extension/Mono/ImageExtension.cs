using UnityEngine;
using UnityEngine.UI;

namespace CsCat
{
  public static class ImageExtension
  {
    /// <summary>
    /// 设置图片的alpha
    /// </summary>
    /// <param name="self"></param>
    /// <param name="alpha"></param>
    public static void SetAlpha(this Image self, float alpha)
    {
      ImageUtil.SetAlpha(self, alpha);
    }

    public static void SetIsGray(this Image self, bool is_gray)
    {
      ImageUtil.SetIsGray(self, is_gray);
    }

    public static void SetColor(this Image self, Color color, bool is_not_use_color_alpha = false)
    {
      ImageUtil.SetColor(self, color, is_not_use_color_alpha);
    }
  }
}