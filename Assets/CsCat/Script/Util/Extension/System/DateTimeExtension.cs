using System;

namespace CsCat
{
	public static class DateTimeExtension
	{
		/// <summary>
		/// 当前的时间是否是月的最后一天
		/// </summary>
		public static bool IsLastDayOfMonth(this DateTime self)
		{
			int month = self.Month;
			var check = self.AddDays(1);
			return check.Month != month;
		}

		/// <summary>
		/// 当前的时间是否是月的第一天
		/// </summary>
		public static bool IsFirstDayOfMonth(this DateTime self)
		{
			return self.Day == 1;
		}

		/// <summary>
		/// 获取指定时刻的DateTime
		/// </summary>
		/// <param name="self"></param>
		/// <param name="hour"></param>
		/// <param name="minute"></param>
		/// <param name="second"></param>
		/// <returns></returns>
		public static DateTime GetTime(this DateTime self, int hour = 0, int minute = 0, int second = 0)
		{
			return new DateTime(self.Year, self.Month, self.Day, hour, minute, second);
		}
	}
}