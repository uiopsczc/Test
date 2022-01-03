using System;

namespace CsCat
{
	// t 时间
	// b 开始值
	// c 增量值  （结束值= 开始值 + 增量值）所以该值应该是（增量值 = 结束值 - 开始值）
	// d 总时长
	public partial class EaseCat
	{
		public class Elastic : EaseCat
		{
			public static float EaseIn(float t, float b, float c, float d)
			{
				float a = 0;
				var p = d * .3f;
				return EaseIn(t, b, c, d, a, p);
			}

			public static float EaseOut(float t, float b, float c, float d)
			{
				float a = 0;
				var p = d * .3f;
				return EaseOut(t, b, c, d, a, p);
			}

			public static float EaseInOut(float t, float b, float c, float d)
			{
				float a = 0;
				var p = d * (.3f * 1.5f);
				return EaseInOut(t, b, c, d, a, p);
			}


			public static float EaseIn(float t, float b, float c, float d, float a, float p)
			{
				float s;
				if (t == 0)
					return b;
				if ((t /= d) == 1)
					return b + c;
				if (p == 0)
					p = d * .3f;
				if (a < Math.Abs(c))
				{
					a = c;
					s = p / 4;
				}
				else
				{
					s = p / MathConst.Two_PI * (float)Math.Asin(c / a);
				}

				return -(a * (float)Math.Pow(2, 10 * (t -= 1)) *
						 (float)Math.Sin((t * d - s) * MathConst.Two_PI / p)) + b;
			}

			public static float EaseOut(float t, float b, float c, float d, float a, float p)
			{
				float s;
				if (t == 0)
					return b;
				if ((t /= d) == 1)
					return b + c;
				if (p == 0)
					p = d * .3f;
				if (a < Math.Abs(c))
				{
					a = c;
					s = p / 4;
				}
				else
				{
					s = p / MathConst.Two_PI * (float)Math.Asin(c / a);
				}

				return a * (float)Math.Pow(2, -10 * t) * (float)Math.Sin((t * d - s) * MathConst.Two_PI / p) + c + b;
			}

			public static float EaseInOut(float t, float b, float c, float d, float a, float p)
			{
				float s;
				if (t == 0)
					return b;
				if ((t /= d / 2) == 2)
					return b + c;
				if (p == 0)
					p = d * (.3f * 1.5f);
				if (a < Math.Abs(c))
				{
					a = c;
					s = p / 4;
				}
				else
				{
					s = p / MathConst.Two_PI * (float)Math.Asin(c / a);
				}

				if (t < 1)
					return -.5f * (a * (float)Math.Pow(2, 10 * (t -= 1)) *
								   (float)Math.Sin((t * d - s) * MathConst.Two_PI / p)) + b;
				return a * (float)Math.Pow(2, -10 * (t -= 1)) * (float)Math.Sin((t * d - s) * MathConst.Two_PI / p) *
					   .5f + c +
					   b;
			}


			///////////////////////////////////////////////////////////////////////////////////////////
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


			public static float EaseIn2(float startValue, float endValue, float pct, float a, float p)
			{
				return EaseIn(pct, startValue, endValue - startValue, 1, a, p);
			}

			public static float EaseOut2(float startValue, float endValue, float pct, float a, float p)
			{
				return EaseOut(pct, startValue, endValue - startValue, 1, a, p);
			}

			public static float EaseInOut2(float startValue, float endValue, float pct, float a, float p)
			{
				return EaseOut(pct, startValue, endValue - startValue, 1, a, p);
			}
		}
	}
}