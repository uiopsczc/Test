

using System;
using UnityEngine;

namespace CsCat
{
  public static class ColorExtension
  {
    public static Color SetR(this Color self, float v)
    {
      return ColorUtil.Set(self, ColorMode.R, v);
    }

    public static Color SetG(this Color self, float v)
    {
      return ColorUtil.Set(self, ColorMode.G, v);
    }

    public static Color SetB(this Color self, float v)
    {
      return ColorUtil.Set(self, ColorMode.B, v);
    }

    public static Color SetA(this Color self, float v)
    {
      return ColorUtil.Set(self, ColorMode.A, v);
    }


    /// <summary>
    /// 修改rgba中的值，rgbaEnum任意组合
    /// </summary>
    /// <param name="self">源color</param>
    /// <param name="rgbaMode">有RGBA</param>
    /// <param name="rgba">对应设置的值，按照rgba的顺序来设置</param>
    /// <returns></returns>
    public static Color Set(this Color self, ColorMode rgbaMode, params float[] rgba)
    {
      return ColorUtil.Set(self, rgbaMode, rgba);
    }


    /// <summary>
    /// 反向值
    /// </summary>
    /// <param name="self"></param>
    /// <returns></returns>
    public static Color Inverted(this Color self)
    {
      return ColorUtil.Inverted(self);
    }



    /// <summary>
    /// 转为HtmlStringRGB
    /// </summary>
    /// <param name="self"></param>
    /// <returns></returns>
    public static string ToHtmlStringRGB(this Color self)
    {
      return ColorUtil.ToHtmlStringRGB(self);
    }

    public static string ToHtmToHtmlStringRGBOrDefault(this Color self, string to_default_value = null,
      Color default_color = default(Color))
    {
      if (ObjectUtil.Equals(self, default_color))
        return to_default_value;
      return self.ToHtmlStringRGB();
    }

    /// <summary>
    /// 转为HtmlStringRGB
    /// </summary>
    /// <param name="self"></param>
    /// <returns></returns>
    public static string ToHtmlStringRGBA(this Color self)
    {
      return ColorUtil.ToHtmlStringRGBA(self);
    }

    public static string ToHtmlStringRGBAOrDefault(this Color self, string to_default_value = null,
      Color default_color = default(Color))
    {
      if (ObjectUtil.Equals(self, default_color))
        return to_default_value;
      return self.ToHtmlStringRGBA();
    }
    

  }
}