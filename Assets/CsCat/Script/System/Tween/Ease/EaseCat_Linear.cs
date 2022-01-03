namespace CsCat
{
	// t 时间
	// b 开始值
	// c 增量值  （结束值= 开始值 + 增量值）所以该值应该是（增量值 = 结束值 - 开始值）
	// d 总时长
	public partial class EaseCat
	{
		public class Linear : EaseCat
		{
			public static float EaseNone(float t, float b, float c, float d)
			{
				return c * t / d + b;
			}

			public static float EaseIn(float t, float b, float c, float d)
			{
				return c * t / d + b;
			}

			public static float EaseOut(float t, float b, float c, float d)
			{
				return c * t / d + b;
			}

			public static float EaseInOut(float t, float b, float c, float d)
			{
				return c * t / d + b;
			}

			///////////////////////////////////////////////////////////////////////////
			public static float EaseNone2(float startValue, float endValue, float pct)
			{
				return EaseNone(pct, startValue, endValue - startValue, 1);
			}

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