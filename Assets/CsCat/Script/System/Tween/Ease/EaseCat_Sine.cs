using System;

namespace CsCat
{
  // t 时间
  // b 开始值
  // c 增量值  （结束值= 开始值 + 增量值）所以该值应该是（增量值 = 结束值 - 开始值）
  // d 总时长
  public partial class EaseCat
  {
    public class Sine : EaseCat
    {
      public static float EaseIn(float t, float b, float c, float d)
      {
        return -c * (float) Math.Cos(t / d * Half_PI) + c + b;
      }

      public static float EaseOut(float t, float b, float c, float d)
      {
        return c * (float) Math.Sin(t / d * Half_PI) + b;
      }

      public static float EaseInOut(float t, float b, float c, float d)
      {
        return -c / 2 * (float) (Math.Cos(Math.PI * t / d) - 1) + b;
      }

      /////////////////////////////////////////////////////////////////////////
      public static float EaseIn2(float start_value, float end_value, float pct)
      {
        return EaseIn(pct, start_value, end_value - start_value, 1);
      }

      public static float EaseOut2(float start_value, float end_value, float pct)
      {
        return EaseOut(pct, start_value, end_value - start_value, 1);
      }

      public static float EaseInOut2(float start_value, float end_value, float pct)
      {
        return EaseInOut(pct, start_value, end_value - start_value, 1);
      }
    }
  }
}