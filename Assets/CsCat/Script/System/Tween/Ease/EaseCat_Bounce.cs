namespace CsCat
{
	// t 时间
	// b 开始值
	// c 增量值  （结束值= 开始值 + 增量值）所以该值应该是（增量值 = 结束值 - 开始值）
	// d 总时长
	public partial class EaseCat
	{
		public class Bounce : EaseCat
		{
			public static float EaseOut(float t, float b, float c, float d)
			{
				if ((t /= d) < 1 / 2.75f)
					return c * (7.5625f * t * t) + b;
				if (t < 2 / 2.75f)
					return c * (7.5625f * (t -= 1.5f / 2.75f) * t + .75f) + b;
				if (t < 2.5 / 2.75)
					return c * (7.5625f * (t -= 2.25f / 2.75f) * t + .9375f) + b;
				return c * (7.5625f * (t -= 2.625f / 2.75f) * t + .984375f) + b;
			}

			public static float EaseIn(float t, float b, float c, float d)
			{
				return c - EaseOut(d - t, 0, c, d) + b;
			}

			public static float EaseInOut(float t, float b, float c, float d)
			{
				if (t < d / 2) return EaseIn(t * 2f, 0, c, d) * .5f + b;
				return EaseOut(t * 2f - d, 0f, c, d) * .5f + c * .5f + b;
			}


			/////////////////////////////////////////////////////////////////////////
			public static float EaseOut2(float startValue, float endValue, float pct)
			{
				return EaseOut(pct, startValue, endValue - startValue, 1);
			}

			public static float EaseIn2(float startValue, float endValue, float pct)
			{
				return EaseIn(pct, startValue, endValue - startValue, 1);
			}

			public static float EaseInOut2(float startValue, float endValue, float pct)
			{
				return EaseInOut(pct, startValue, endValue - startValue, 1);
			}
		}
	}
}