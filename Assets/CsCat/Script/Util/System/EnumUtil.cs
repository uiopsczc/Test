using System;
using System.Collections.Generic;

namespace CsCat
{
  public class EnumUtil
  {
    public static int GetCount<T>()
    {
      return GetNames<T>().Count;
    }

    public static List<string> GetNames<T>()
    {
      return Enum.GetNames(typeof(T)).ToList();
    }
    public static string GetName<T>(int i)
    {
      return GetNames<T>()[i];
    }

    public static List<T> GetValues<T>()
    {
      return ((T[])Enum.GetValues(typeof(T))).ToList();
    }

    public static T GetValue<T>(int i)
    {
      return GetValues<T>()[i];
    }

    public static List<int> GetInts<T>()
    {
      var result = new List<int>();
      foreach (var e in GetValues<T>())
        result.Add(Convert.ToInt32(e));
      return result;
    }


    


    public static bool IsEnum<T>()
    {
      var enum_type = typeof(T);
      if (!enum_type.IsEnum) return false;
      return true;
    }
  }
}