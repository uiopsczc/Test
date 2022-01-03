namespace CsCat
{
	// t 时间
	// b 开始值
	// c 增量值  （结束值= 开始值 + 增量值）所以该值应该是（增量值 = 结束值 - 开始值）
	// d 总时长
	public partial class EaseCat
	{
		public class Back : EaseCat
		{
			public static float EaseIn(float t, float b, float c, float d)
			{
				var s = 1.70158f;
				return EaseIn(t, b, c, d, s);
			}

			public static float EaseOut(float t, float b, float c, float d)
			{
				var s = 1.70158f;
				return EaseOut(t, b, c, d, s);
			}

			public static float EaseInOut(float t, float b, float c, float d)
			{
				var s = 1.70158f;
				return EaseInOut(t, b, c, d, s);
			}


			public static float EaseIn(float t, float b, float c, float d, float s)
			{
				return c * (t /= d) * t * ((s + 1) * t - s) + b;
			}

			public static float EaseOut(float t, float b, float c, float d, float s)
			{
				return c * ((t = t / d - 1) * t * ((s + 1) * t + s) + 1) + b;
			}

			public static float EaseInOut(float t, float b, float c, float d, float s)
			{
				if ((t /= d / 2) < 1) return c / 2 * (t * t * (((s *= 1.525f) + 1) * t - s)) + b;
				return c / 2 * ((t -= 2) * t * (((s *= 1.525f) + 1) * t + s) + 2) + b;
			}

			////////////////////////////////////////////////////////////////////////////////////
			public static float EaseIn2(float startValue, float endValue, float pct)
			{
				return EaseIn(pct, startValue, endValue - startValue, 1);
			}

			public static float EaseOut2(float startValue, float endValue, float pct)
			{
				return EaseOut(pct, startValue, endValue - startValue, 1);
			}

			public static float EaseInOut2(float startValue, float endValue, float pct)
			{
				return EaseInOut(pct, startValue, endValue - startValue, 1);
			}


			public static float EaseIn2(float startValue, float endValue, float pct, float s)
			{
				return EaseIn(pct, startValue, endValue - startValue, 1, s);
			}

			public static float EaseOut2(float startValue, float endValue, float pct, float s)
			{
				return EaseOut(pct, startValue, endValue - startValue, 1, s);
			}

			public static float EaseInOut2(float startValue, float endValue, float pct, float s)
			{
				return EaseInOut(pct, startValue, endValue - startValue, 1, s);
			}
		}
	}
}