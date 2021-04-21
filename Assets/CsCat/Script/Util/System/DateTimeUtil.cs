using System;
using System.Globalization;

namespace CsCat
{
  public class DateTimeUtil
  {
    public static long NowTicks()
    {
      return DateTime.Now.Ticks;
    }

    public static DateTime NowDateTime()
    {
      return new DateTime(NowTicks());
    }

    /// <summary>
    /// 比较两个日期的差别
    /// </summary>
    /// <param name="compareType">返回差别的类型（年，月，日，时，分，秒）</param>
    /// <param name="isIngroed">是否忽略非比较类型部分</param>
    /// <returns></returns>
    public static float Diff(DateTime date1, DateTime date2, DateTimeType compareType,
      bool is_ingroed_not_compare_type = true)
    {
      if (is_ingroed_not_compare_type)
      {
        int year1 = date1.Year;
        int year2 = date2.Year;
        int month1 = date1.Month;
        int month2 = date2.Month;
        TimeSpan t1;
        TimeSpan t2;
        switch (compareType)
        {
          case DateTimeType.Year:
            return date1.Year - date2.Year;
          case DateTimeType.Month:
            return date1.Month - date2.Month + (date1.Year - date2.Year) * 12;
          case DateTimeType.Day:
            t1 = new TimeSpan(date1.GetTime().Ticks);
            t2 = new TimeSpan(date2.GetTime().Ticks);
            return (float)t1.Subtract(t2).TotalDays;
          case DateTimeType.Hour:
            t1 = new TimeSpan(date1.GetTime(date1.Hour).Ticks);
            t2 = new TimeSpan(date2.GetTime(date2.Hour).Ticks);
            return (float)t1.Subtract(t2).TotalHours;
          case DateTimeType.Minute:
            t1 = new TimeSpan(date1.GetTime(date1.Hour, date1.Minute).Ticks);
            t2 = new TimeSpan(date2.GetTime(date2.Hour, date1.Minute).Ticks);
            return (float)t1.Subtract(t2).TotalMinutes;
          case DateTimeType.Second:
            t1 = new TimeSpan(date1.Ticks);
            t2 = new TimeSpan(date2.Ticks);
            return (float)t1.Subtract(t2).TotalSeconds;
        }
      }
      else
      {
        TimeSpan t1 = new TimeSpan(date1.Ticks);
        TimeSpan t2 = new TimeSpan(date2.Ticks);
        switch (compareType)
        {
          case DateTimeType.Year:
            throw new Exception("不支持年比较");
          case DateTimeType.Month:
            throw new Exception("不支持月比较");
          case DateTimeType.Day:
            return (float)t1.Subtract(t2).TotalDays;
          case DateTimeType.Hour:
            return (float)t1.Subtract(t2).TotalHours;
          case DateTimeType.Minute:
            return (float)t1.Subtract(t2).TotalMinutes;
          case DateTimeType.Second:
            return (float)t1.Subtract(t2).TotalSeconds;
        }
      }

      throw new Exception("日常比较异常");
    }

    /// <summary>
    /// d1和d2是否是同一天
    /// </summary>
    /// <param name="d1"></param>
    /// <param name="d2"></param>
    /// <returns></returns>
    public static bool IsSameDay(DateTime d1, DateTime d2)
    {
      return (d1.Year == d2.Year) && (d1.Month == d2.Month) && (d1.Day == d2.Day);
    }

    /// <summary>
    /// d1和d2是否是同一个月（以月份为主，不是用天算）
    /// </summary>
    /// <param name="d1"></param>
    /// <param name="d2"></param>
    /// <returns></returns>
    public static bool IsSameMonth(DateTime d1, DateTime d2)
    {
      return (d1.Year == d2.Year) && (d1.Month == d2.Month);
    }


    /// <summary>
    /// 将DateTime用pattern模式转换后用string来返回
    /// </summary>
    /// <param name="pattern"></param>
    /// <param name="d"></param>
    /// <returns></returns>
    public static string GetDateTime(string pattern, DateTime d)
    {
      switch (pattern.ToLower())
      {
        case "date":
          return d.ToString("yyyy-MM-dd");
        case "time":
          return d.ToString("HH:mm:ss");
        case "datetime":
          return d.ToString("yyyy-MM-dd HH:mm:ss");
      }

      return d.ToString(pattern);
    }

    /// <summary>
    /// 将ms转为DateTime，用pattern模式转换后用string来返回
    /// </summary>
    /// <param name="pattern"></param>
    /// <param name="ms"></param>
    /// <returns></returns>
    public static string GetDateTime(string pattern, long ms)
    {
      return GetDateTime(pattern, new DateTime(ms));
    }

    /// <summary>
    /// 将当前时间用pattern模式转换后用string来返回
    /// </summary>
    /// <param name="pattern"></param>
    /// <returns></returns>
    public static string GetDateTime(string pattern)
    {
      return GetDateTime(pattern, NowDateTime());
    }

    /// <summary>
    /// 将当前时间用yyyy-MM-dd HH:mm:ss模式转换后用string来返回
    /// </summary>
    /// <returns></returns>
    public static string GetDateTime()
    {
      return GetDateTime("datetime");
    }

    /// <summary>
    /// 将t秒转换为？天？小时？分？秒
    /// </summary>
    /// <param name="t">秒</param>
    /// <returns></returns>
    public static string GetTimeString(int t)
    {
      long sec = t % 60; //0-59秒
      long min = (t / 60) % 60; //0-59分钟
      long hour = (t / 3600) % 24; //0-24小时
      long day = t / (3600 * 24); //天

      return (day > 0L ? day + "天" : "") + ((hour > 0L) || (day > 0L) ? hour + "小时" : "") +
             ((min > 0L) || (hour > 0L) || (day > 0L) ? min + "分" : "") + sec + "秒";
    }

    /// <summary>
    /// 将date增加days天，返回long
    /// </summary>
    /// <param name="date"></param>
    /// <param name="add"></param>
    /// <returns></returns>
    public static long AddDay(DateTime date, int add_days)
    {
      return date.Ticks + 86400000L * add_days;
    }

    /// <summary>
    /// 将现在时间添加days天，返回long
    /// </summary>
    /// <param name="add_days"></param>
    /// <returns></returns>
    public static long AddDay(int add_days)
    {
      return AddDay(DateTimeUtil.NowDateTime(), add_days);
    }

    /// <summary>
    /// date1（yyyy-MM-dd）比date2(yyyy-MM-dd)多?天
    /// </summary>
    /// <param name="date1"></param>
    /// <param name="date2"></param>
    /// <returns>天</returns>
    public static int DayDiff(string date1, string date2)
    {
      DateTime d1 = ParseDateTime(date1, "yyyy-MM-dd");
      DateTime d2 = ParseDateTime(date2, "yyyy-MM-dd");
      return DayDiff(d1, d2);
    }

    /// <summary>
    /// date1比date2多?天
    /// </summary>
    /// <param name="date1"></param>
    /// <param name="date2"></param>
    /// <returns></returns>
    public static int DayDiff(DateTime date1, DateTime date2)
    {
      return DayDiff(date1.Ticks, date2.Ticks);
    }

    /// <summary>
    /// date1(long)比date2(long)多?天
    /// </summary>
    /// <param name="date1"></param>
    /// <param name="date2"></param>
    /// <returns></returns>
    public static int DayDiff(long date1, long date2)
    {
      return (int)((date1 - date2) / 86400000L);
    }

    /// <summary>
    /// date1和date2相差多少个月（以月份为主，不是用天算）
    /// </summary>
    /// <param name="date1"></param>
    /// <param name="date2"></param>
    /// <returns></returns>
    public static int MonthDiff(DateTime date1, DateTime date2)
    {
      int year1 = date1.Year;
      int year2 = date2.Year;
      int month1 = date1.Month;
      int month2 = date2.Month;
      if (year1 == year2)
        return month1 - month2;
      return month1 - month2 + (year1 - year2) * 12;
    }

    /// <summary>
    /// 将s用pattern模式转换为DateTime，转换失败时返回默认值dv
    /// </summary>
    /// <param name="s"></param>
    /// <param name="pattern"></param>
    /// <param name="dv"></param>
    /// <returns></returns>
    public static DateTime ParseDateTime(string s, string pattern, DateTime dv)
    {
      try
      {
        if (!string.IsNullOrEmpty(pattern))
        {
          CultureInfo provider = CultureInfo.InvariantCulture;
          return DateTime.ParseExact(s, pattern, provider);
        }
      }
      catch
      {
      }

      return dv;
    }

    /// <summary>
    /// 将s用pattern模式转换为DateTime，转换失败时返回当前时间
    /// </summary>
    /// <param name="s"></param>
    /// <param name="pattern"></param>
    /// <returns></returns>
    public static DateTime ParseDateTime(string s, string pattern)
    {
      return ParseDateTime(s, pattern, new DateTime());
    }

    /// <summary>
    /// 将s转换为DateTime，转换失败时返回默认值dv
    /// </summary>
    /// <param name="s"></param>
    /// <param name="dv"></param>
    /// <returns></returns>
    public static DateTime ParseDateTime(string s, DateTime dv)
    {
      return ParseDateTime(s, null, dv);
    }

    /// <summary>
    /// 将s转换为DateTime，转换失败时返回当前时间
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    public static DateTime ParseDateTime(string s)
    {
      return ParseDateTime(s, null, new DateTime());
    }

    /// <summary>
    /// d是否是月的第一天
    /// </summary>
    /// <param name="d"></param>
    /// <returns></returns>
    public static bool IsFirstDayOfMonth(DateTime d)
    {
      return d.Day == 1;
    }

    /// <summary>
    /// 当前的时间是否是月的第一天
    /// </summary>
    /// <returns></returns>
    public static bool IsFirstDayOfMonth()
    {
      return IsFirstDayOfMonth(DateTimeUtil.NowDateTime());
    }

    /// <summary>
    /// d是否是月的最后一天
    /// </summary>
    /// <param name="d"></param>
    /// <returns></returns>
    public static bool IsLastDayOfMonth(DateTime d)
    {
      int month = d.Month;
      var check = d.AddDays(1);
      return check.Month != month;
    }

    /// <summary>
    /// 当前的时间是否是月的最后一天
    /// </summary>
    /// <returns></returns>
    public static bool IsLastDayOfMonth()
    {
      return IsLastDayOfMonth(DateTimeUtil.NowDateTime());
    }

    /// <summary>
    /// 是否在给定的时间范围内
    /// </summary>
    /// <param name="dateTime"></param>
    /// <param name="min_value"></param>
    /// <param name="max_value"></param>
    /// <returns></returns>
    public static bool IsBetween(DateTime dateTime, DateTime min_value, DateTime max_value)
    {
      return min_value.CompareTo(dateTime) == -1 && dateTime.CompareTo(max_value) == -1;
    }

    /// <summary>
    /// 23:59:59:999
    /// </summary>
    /// <param name="dateTime"></param>
    /// <returns></returns>
    public static DateTime End(DateTime dateTime)
    {
      return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, 23, 59, 59, 999);
    }

    /// <summary>
    /// 00:00:00:000
    /// </summary>
    /// <param name="dateTime"></param>
    /// <returns></returns>
    public static DateTime Begin(DateTime dateTime)
    {
      return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day);
    }

    /// <summary>
    /// 获取月份的第一天
    /// </summary>
    /// <param name="dateTime"></param>
    /// <returns></returns>
    public static DateTime FirstDayOfMonth(DateTime dateTime)
    {
      return new DateTime(dateTime.Year, dateTime.Month, 1);
    }

    /// <summary>
    /// 获取月份的最后一天
    /// </summary>
    /// <param name="dateTime"></param>
    /// <returns></returns>
    public static DateTime LastDayOfMonth(DateTime dateTime)
    {
      DateTime lastDayOfMonth_End = new DateTime(dateTime.Year, dateTime.Month, 1).AddMonths(1)
        .Subtract(new TimeSpan(0, 0, 0, 0, 1));
      return new DateTime(lastDayOfMonth_End.Year, lastDayOfMonth_End.Month, lastDayOfMonth_End.Day);
    }

    /// <summary>
    /// 获取一周中第一天
    /// </summary>
    /// <param name="dateTime"></param>
    /// <param name="startDayOfWeek"></param>
    /// <returns></returns>
    public static DateTime FirstDayOfWeek(DateTime dateTime, DayOfWeek startDayOfWeek = DayOfWeek.Monday)
    {
      return LastDayOfWeek(dateTime, startDayOfWeek).Subtract(new TimeSpan(6, 0, 0, 0, 0));
    }

    /// <summary>
    /// 获取一周中最后一天
    /// </summary>
    /// <param name="dateTime"></param>
    /// <param name="startDayOfWeek">一周开始的第一天</param>
    /// <returns></returns>
    public static DateTime LastDayOfWeek(DateTime dateTime, DayOfWeek startDayOfWeek = DayOfWeek.Monday)
    {
      DateTime result = dateTime;
      DayOfWeek endDayOfWeek = startDayOfWeek - 1;
      if (endDayOfWeek < 0)
        endDayOfWeek = DayOfWeek.Saturday;

      if (result.DayOfWeek != endDayOfWeek)
      {
        if (endDayOfWeek < result.DayOfWeek)
          result = result.AddDays(7 - (result.DayOfWeek - endDayOfWeek));
        else
          result = result.AddDays(endDayOfWeek - result.DayOfWeek);
      }

      return result;

    }

    /// <summary>
    /// 获取一年中第一天
    /// </summary>
    /// <param name="dateTime"></param>
    /// <returns></returns>
    public static DateTime FirstDayOfYear(DateTime dateTime)
    {
      return new DateTime(dateTime.Year, 1, 1);
    }

    /// <summary>
    /// 获取一年中最后一天
    /// </summary>
    /// <param name="dateTime"></param>
    /// <returns></returns>
    public static DateTime LastDayOfYear(DateTime dateTime)
    {
      DateTime lastDayOfYear_End = FirstDayOfYear(dateTime).AddYears(1).Subtract(new TimeSpan(0, 0, 0, 0, 1));
      return new DateTime(lastDayOfYear_End.Year, lastDayOfYear_End.Month, lastDayOfYear_End.Day);
    }

    public static bool IsAM(DateTime dateTime)
    {
      return dateTime.TimeOfDay < new DateTime(2000, 1, 1, 12, 0, 0).TimeOfDay;
    }


    public static bool IsPM(DateTime dateTime)
    {
      return dateTime.TimeOfDay > new DateTime(2000, 1, 1, 12, 0, 0).TimeOfDay;
    }

    /// <summary>
    /// 是否是周末
    /// </summary>
    /// <param name="this"></param>
    /// <returns></returns>
    public static bool IsWeeken(DateTime dateTtime, params DayOfWeek[] weekens)
    {
      if (weekens == null)
      {
        weekens = new DayOfWeek[2];
        weekens[0] = DayOfWeek.Saturday;
        weekens[1] = DayOfWeek.Sunday;
      }

      foreach (DayOfWeek weeken in weekens)
      {
        if (weeken == dateTtime.DayOfWeek)
          return true;
      }

      return false;
    }


    /// <summary>
    /// 获取年龄
    /// </summary>
    /// <param name="birthDay"></param>
    /// <returns></returns>
    public static int GetAge(DateTime birthDay)
    {
      var today = DateTime.Today;
      var age = today.Year - birthDay.Year;

      if (birthDay > today.AddYears(-age))
      {
        age--;
      }

      return age;
    }




  }
}