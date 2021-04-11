using System.Diagnostics;
using UnityEngine;

namespace CsCat
{
  public class TimeUtil
  {
    public static long SystemTicksToSecond(long tick)
    {
      return tick / (10000000);
    }

    public static long SecondToSystemTicks(double second)
    {
      return (long) (second * 10000000);
    }

    public static TimeTable SecondToTimeTable(long seconds)
    {
      int day = (int) Mathf.Floor(seconds / (3600 * 24f));
      int hour = (int) Mathf.Floor((seconds % (3600 * 24)) / 3600f);
      int min = (int) Mathf.Floor((seconds % 3600) / 60f);
      int sec = (int) Mathf.Floor(seconds % 60f);

      return new TimeTable(day, hour, min, sec);
    }

    /// <summary>
    /// 将seconds转为hh:mm:ss
    /// 倒计时经常使用
    /// </summary>
    /// <param name="seconds"></param>
    /// <param name="h_count">小时那位需要至少保留多少位，即1的时候显不显示为01</param>
    /// <param name="is_zero_ingroe">是否小时或分钟或秒为0的时候忽视该位，高位有的话还是会保留地位的，即使低位为0</param>
    /// <returns></returns>
    public static string SecondToStringHHmmss(long seconds, int h_count = 2, bool is_zero_ingroe = false)
    {
      string result = "";
      long HH = seconds / (60 * 60);
      is_zero_ingroe = is_zero_ingroe && HH == 0 ? true : false;
      if (!is_zero_ingroe)
      {
        result += HH.ToString().FillHead(h_count, '0');
        result += ":";
      }

      long mm = (seconds % (60 * 60)) / 60;
      is_zero_ingroe = is_zero_ingroe && mm == 0 ? true : false;
      if (is_zero_ingroe)
      {
        result += mm.ToString().FillHead(2, '0');
        result += ":";
      }


      long ss = seconds % 60;
      is_zero_ingroe = is_zero_ingroe && ss == 0 ? true : false;
      if (is_zero_ingroe)
      {
        result += ss.ToString().FillHead(2, '0');
      }

      return result;


    }

    /// <summary>
    /// 以tick为单位的两个东西的相差多少秒
    /// </summary>
    /// <param name="t1"></param>
    /// <param name="t2"></param>
    /// <returns></returns>
    public static float DiffBySeconds(long t1, long t2)
    {
      float diff = (t1 - t2) / 10000000f; //
      return diff;
    }

    public static long GetNowTimestamp()
    {
      return Stopwatch.GetTimestamp();
    }



  }
}