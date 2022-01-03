namespace CsCat
{
	// t 时间
	// b 开始值
	// c 增量值  （结束值= 开始值 + 增量值）所以该值应该是（增量值 = 结束值 - 开始值）
	// d 总时长
	public partial class EaseCat
	{
		public class Strong : EaseCat
		{
			public static float EaseIn(float t, float b, float c, float d)
			{
				return c * (t /= d) * t * t * t * t + b;
			}

			public static float EaseOut(float t, float b, float c, float d)
			{
				return c * ((t = t / d - 1) * t * t * t * t + 1) + b;
			}

			public static float EaseInOut(float t, float b, float c, float d)
			{
				if ((t /= d / 2) < 1) return c / 2 * t * t * t * t * t + b;
				return c / 2 * ((t -= 2) * t * t * t * t + 2) + b;
			}

			//////////////////////////////////////////////////////////////////////////
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
		}
	}
}