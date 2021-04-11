using System;

namespace CsCat
{
  // t 时间
  // b 开始值
  // c 增量值  （结束值= 开始值 + 增量值）所以该值应该是（增量值 = 结束值 - 开始值）
  // d 总时长
  public partial class EaseCat
  {
    public class Expo : EaseCat
    {
      public static float EaseIn(float t, float b, float c, float d)
      {
        return t == 0 ? b : c * (float) Math.Pow(2, 10 * (t / d - 1)) + b - c * 0.001f;
      }

      public static float EaseOut(float t, float b, float c, float d)
      {
        return t == d ? b + c : c * (float) (-Math.Pow(2, -10 * t / d) + 1) + b;
      }

      public static float EaseInOut(float t, float b, float c, float d)
      {
        if (t == 0) return b;
        if (t == d) return b + c;
        if ((t /= d / 2) < 1) return c / 2 * (float) Math.Pow(2, 10 * (t - 1)) + b;
        return c / 2 * (float) (-Math.Pow(2, -10 * --t) + 2) + b;
      }

      /////////////////////////////////////////////////////////////////////////
      public static float EaseIn2(float start_value, float end_value, float pct)
      {
        return EaseIn(start_value, end_value - start_value, pct, 1);
      }

      public static float EaseOut2(float start_value, float end_value, float pct)
      {
        return EaseOut(start_value, end_value - start_value, pct, 1);
      }

      public static float EaseInOut2(float start_value, float end_value, float pct)
      {
        return EaseInOut(start_value, end_value - start_value, pct, 1);
      }
    }
  }
}