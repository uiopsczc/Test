using System;
using UnityEngine;

namespace CsCat
{
	public static class DoubleExtension
	{
		public static byte[] ToBytes(this double self, bool isNetOrder = false)
		{
			byte[] data = BitConverter.GetBytes(self);
			if (isNetOrder)
				Array.Reverse(data);
			return data;
		}


		//是否是defalut, 默认是与float.MaxValue比较
		public static bool IsDefault(this double self, bool isMin = false)
		{
			return isMin ? self == double.MinValue : self == double.MaxValue;
		}

		//得到百分比
		public static double GetPercent(this double self, double minValue, double maxValue, bool isClamp = true)
		{
			if (isClamp)
			{
				if (self < minValue)
					self = minValue;
				else if (self > maxValue)
					self = maxValue;
			}

			double offset = self - minValue;
			return offset / (maxValue - minValue);
		}

		public static bool IsInRange(this double self, double minValue, double maxValue,
			bool isMinValueIncluded = false,
			bool isMaxValueIncluded = false)
		{
			return !(self < minValue) && !(self > maxValue) &&
				   ((self != minValue || isMinValueIncluded) && (self != maxValue || isMaxValueIncluded));
		}

		//将v Round四舍五入snap_soze的倍数的值
		//Rounds value to the closest multiple of snap_soze.
		public static double Snap(this double v, double snapSize)
		{
			return Math.Round(v / snapSize) * snapSize;
		}

		public static double Snap2(this double v, double snapSize)
		{
			return Math.Round(v * snapSize) / snapSize;
		}

		public static double Minimum(this double self, double minimum)
		{
			return Math.Max(self, minimum);
		}

		public static double Maximum(this double self, double maximum)
		{
			return Math.Min(self, maximum);
		}
	}
}