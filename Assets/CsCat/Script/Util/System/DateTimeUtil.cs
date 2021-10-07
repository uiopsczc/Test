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
        public static float Diff(DateTime dateTime1, DateTime dateTime2, DateTimeType compareType,
            bool isIngroedNotCompareType = true)
        {
            if (isIngroedNotCompareType)
            {
                TimeSpan timeSpan1;
                TimeSpan timeSpan2;
                switch (compareType)
                {
                    case DateTimeType.Year:
                        return dateTime1.Year - dateTime2.Year;
                    case DateTimeType.Month:
                        return dateTime1.Month - dateTime2.Month + (dateTime1.Year - dateTime2.Year) * 12;
                    case DateTimeType.Day:
                        timeSpan1 = new TimeSpan(dateTime1.GetTime().Ticks);
                        timeSpan2 = new TimeSpan(dateTime2.GetTime().Ticks);
                        return (float) timeSpan1.Subtract(timeSpan2).TotalDays;
                    case DateTimeType.Hour:
                        timeSpan1 = new TimeSpan(dateTime1.GetTime(dateTime1.Hour).Ticks);
                        timeSpan2 = new TimeSpan(dateTime2.GetTime(dateTime2.Hour).Ticks);
                        return (float) timeSpan1.Subtract(timeSpan2).TotalHours;
                    case DateTimeType.Minute:
                        timeSpan1 = new TimeSpan(dateTime1.GetTime(dateTime1.Hour, dateTime1.Minute).Ticks);
                        timeSpan2 = new TimeSpan(dateTime2.GetTime(dateTime2.Hour, dateTime1.Minute).Ticks);
                        return (float) timeSpan1.Subtract(timeSpan2).TotalMinutes;
                    case DateTimeType.Second:
                        timeSpan1 = new TimeSpan(dateTime1.Ticks);
                        timeSpan2 = new TimeSpan(dateTime2.Ticks);
                        return (float) timeSpan1.Subtract(timeSpan2).TotalSeconds;
                }
            }
            else
            {
                TimeSpan timeSpan1 = new TimeSpan(dateTime1.Ticks);
                TimeSpan timeSpan2 = new TimeSpan(dateTime2.Ticks);
                switch (compareType)
                {
                    case DateTimeType.Year:
                        throw new Exception("不支持年比较");
                    case DateTimeType.Month:
                        throw new Exception("不支持月比较");
                    case DateTimeType.Day:
                        return (float) timeSpan1.Subtract(timeSpan2).TotalDays;
                    case DateTimeType.Hour:
                        return (float) timeSpan1.Subtract(timeSpan2).TotalHours;
                    case DateTimeType.Minute:
                        return (float) timeSpan1.Subtract(timeSpan2).TotalMinutes;
                    case DateTimeType.Second:
                        return (float) timeSpan1.Subtract(timeSpan2).TotalSeconds;
                }
            }

            throw new Exception("日常比较异常");
        }

        /// <summary>
        /// d1和d2是否是同一天
        /// </summary>
        /// <param name="dateTime1"></param>
        /// <param name="dateTime2"></param>
        /// <returns></returns>
        public static bool IsSameDay(DateTime dateTime1, DateTime dateTime2)
        {
            return (dateTime1.Year == dateTime2.Year) && (dateTime1.Month == dateTime2.Month) && (dateTime1.Day == dateTime2.Day);
        }

        /// <summary>
        /// d1和d2是否是同一个月（以月份为主，不是用天算）
        /// </summary>
        /// <param name="dateTime1"></param>
        /// <param name="dateTime2"></param>
        /// <returns></returns>
        public static bool IsSameMonth(DateTime dateTime1, DateTime dateTime2)
        {
            return (dateTime1.Year == dateTime2.Year) && (dateTime1.Month == dateTime2.Month);
        }


        /// <summary>
        /// 将DateTime用pattern模式转换后用string来返回
        /// </summary>
        /// <param name="pattern"></param>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static string GetDateTime(string pattern, DateTime dateTime)
        {
            switch (pattern.ToLower())
            {
                case StringConst.String_date:
                    return dateTime.ToString(StringConst.String_yyyy_MM_dd);
                case StringConst.String_time:
                    return dateTime.ToString(StringConst.String_HH_mm_ss);
                case StringConst.String_datetime:
                    return dateTime.ToString(StringConst.String_yyyy_MM_dd_HH_mm_ss);
            }

            return dateTime.ToString(pattern);
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
            return GetDateTime(StringConst.String_datetime);
        }

        /// <summary>
        /// 将t秒转换为？天？小时？分？秒
        /// </summary>
        /// <param name="seconds">秒</param>
        /// <returns></returns>
        public static string GetTimeString(int seconds)
        {
            long sec = seconds % 60; //0-59秒
            long min = (seconds / 60) % 60; //0-59分钟
            long hour = (seconds / 3600) % 24; //0-24小时
            long day = seconds / (3600 * 24); //天

            return (day > 0L ? day + StringConst.String_Day_CN : StringConst.String_Empty) + ((hour > 0L) || (day > 0L) ? hour + StringConst.String_Hour_CN : StringConst.String_Empty) +
                   ((min > 0L) || (hour > 0L) || (day > 0L) ? min + StringConst.String_Minute_CN : StringConst.String_Empty) + sec + StringConst.String_Second_CN;
        }

        /// <summary>
        /// 将date增加days天，返回long
        /// </summary>
        /// <param name="date"></param>
        /// <param name="add"></param>
        /// <returns></returns>
        public static long AddDay(DateTime date, int addDays)
        {
            return date.Ticks + 86400000L * addDays;
        }

        /// <summary>
        /// 将现在时间添加days天，返回long
        /// </summary>
        /// <param name="addDays"></param>
        /// <returns></returns>
        public static long AddDay(int addDays)
        {
            return AddDay(DateTimeUtil.NowDateTime(), addDays);
        }

        /// <summary>
        /// date1（yyyy-MM-dd）比date2(yyyy-MM-dd)多?天
        /// </summary>
        /// <param name="date1"></param>
        /// <param name="date2"></param>
        /// <returns>天</returns>
        public static int DayDiff(string date1, string date2)
        {
            DateTime d1 = ParseDateTime(date1, StringConst.String_yyyy_MM_dd);
            DateTime d2 = ParseDateTime(date2, StringConst.String_yyyy_MM_dd);
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
            return (int) ((date1 - date2) / 86400000L);
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
            return year1 == year2 ? month1 - month2 : month1 - month2 + (year1 - year2) * 12;
        }

        /// <summary>
        /// 将s用pattern模式转换为DateTime，转换失败时返回默认值dv
        /// </summary>
        /// <param name="s"></param>
        /// <param name="pattern"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static DateTime ParseDateTime(string s, string pattern, DateTime defaultValue)
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
                // ignored
            }

            return defaultValue;
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
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static DateTime ParseDateTime(string s, DateTime defaultValue)
        {
            return ParseDateTime(s, null, defaultValue);
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
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static bool IsFirstDayOfMonth(DateTime dateTime)
        {
            return dateTime.Day == 1;
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
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static bool IsLastDayOfMonth(DateTime dateTime)
        {
            int month = dateTime.Month;
            var check = dateTime.AddDays(1);
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
        /// <param name="minValue"></param>
        /// <param name="maxValue"></param>
        /// <returns></returns>
        public static bool IsBetween(DateTime dateTime, DateTime minValue, DateTime maxValue)
        {
            return minValue.CompareTo(dateTime) == -1 && dateTime.CompareTo(maxValue) == -1;
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
            DateTime lastDayOfMonthEnd = new DateTime(dateTime.Year, dateTime.Month, 1).AddMonths(1)
                .Subtract(new TimeSpan(0, 0, 0, 0, 1));
            return new DateTime(lastDayOfMonthEnd.Year, lastDayOfMonthEnd.Month, lastDayOfMonthEnd.Day);
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
                result = endDayOfWeek < result.DayOfWeek
                    ? result.AddDays(7 - (result.DayOfWeek - endDayOfWeek))
                    : result.AddDays(endDayOfWeek - result.DayOfWeek);

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
            DateTime lastDayOfYearEnd = FirstDayOfYear(dateTime).AddYears(1).Subtract(new TimeSpan(0, 0, 0, 0, 1));
            return new DateTime(lastDayOfYearEnd.Year, lastDayOfYearEnd.Month, lastDayOfYearEnd.Day);
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
        public static bool IsWeeken(DateTime dateTime, params DayOfWeek[] weekens)
        {
            if (weekens == null)
            {
                weekens = new DayOfWeek[2];
                weekens[0] = DayOfWeek.Saturday;
                weekens[1] = DayOfWeek.Sunday;
            }

            foreach (DayOfWeek weeken in weekens)
            {
                if (weeken == dateTime.DayOfWeek)
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
                age--;

            return age;
        }
    }
}