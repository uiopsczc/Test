

using System.Reflection;
using UnityEngine;

namespace CsCat
{
  public class ColorUtil
  {


    public static void SetColorR(System.Object obj, float v, string member_name = "color")
    {
      SetColor(obj, member_name, ColorMode.R, v);
    }

    public static void SetColorG(System.Object obj, float v, string member_name = "color")
    {
      SetColor(obj, member_name, ColorMode.G, v);
    }

    public static void SetColorB(System.Object obj, float v, string member_name = "color")
    {
      SetColor(obj, member_name, ColorMode.B, v);
    }

    public static void SetColorA(System.Object obj, float v, string member_name = "color")
    {
      SetColor(obj, member_name, ColorMode.A, v);
    }

    public static void SetColor(System.Object obj, ColorMode rgbaMode, params float[] rgba)
    {
      SetColor(obj, "color", rgbaMode, rgba);
    }

    public static void SetColor(System.Object obj, string member_name, ColorMode rgbaMode, params float[] rgba)
    {
      FieldInfo fieldInfo = obj.GetType().GetFieldInfo(member_name, BindingFlagsConst.All);
      if (fieldInfo != null)
      {
        Color orgColor = (Color)fieldInfo.GetValue(obj);
        Color newColor = Set(orgColor, rgbaMode, rgba);
        fieldInfo.SetValue(obj, newColor);
        return;
      }

      PropertyInfo propertyInfo = obj.GetType().GetPropertyInfo(member_name, BindingFlagsConst.All);
      if (propertyInfo != null)
      {
        Color orgColor = (Color)propertyInfo.GetValue(obj, null);
        Color newColor = Set(orgColor, rgbaMode, rgba);
        propertyInfo.SetValue(obj, newColor, null);
        return;
      }
    }

    /// <summary>
    /// 修改rgba中的值，rgbaEnum任意组合
    /// </summary>
    /// <param name="color">源color</param>
    /// <param name="rgbaMode">有RGBA</param>
    /// <param name="rgba">对应设置的值，按照rgba的顺序来设置</param>
    /// <returns></returns>
    public static Color Set(Color color, ColorMode rgbaMode, params float[] rgba)
    {
      float r = color.r;
      float g = color.g;
      float b = color.b;
      float a = color.a;
      int i = 0;
      EnumUtil.GetValues<ColorMode>().ToList<ColorMode>().ForEach(
        (e) =>
        {
          if (rgbaMode.Contains(e) && i < rgba.Length)
          {
            switch (e)
            {
              case ColorMode.R:
                r = rgba[i];
                break;
              case ColorMode.G:
                g = rgba[i];
                break;
              case ColorMode.B:
                b = rgba[i];
                break;
              case ColorMode.A:
                a = rgba[i];
                break;
            }

            i++;
          }

        }
      );
      return new Color(r, g, b, a);
    }


    /// <summary>
    /// 反向值
    /// </summary>
    /// <param name="color"></param>
    /// <returns></returns>
    public static Color Inverted(Color color)
    {
      var result = Color.white - color;
      result.a = color.a;
      return result;
    }



    /// <summary>
    /// 转为HtmlStringRGB
    /// </summary>
    /// <param name="color"></param>
    /// <returns></returns>
    public static string ToHtmlStringRGB(Color color)
    {
      return ColorUtility.ToHtmlStringRGB(color);
    }

    /// <summary>
    /// 转为HtmlStringRGB
    /// </summary>
    /// <param name="color"></param>
    /// <returns></returns>
    public static string ToHtmlStringRGBA(Color color)
    {
      return ColorUtility.ToHtmlStringRGBA(color);
    }






  }
}