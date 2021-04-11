using System;
namespace CsCat
{
  public class CommonUtil
  {

    public static object ConvertType(string value, Type type)
    {
      //LogCat.LogWarning(type);
      if (type == typeof(Boolean))
        return bool.Parse(value.ToLower());
      else if (type == typeof(Int32))
        return int.Parse(value);
      else if (type == typeof(Int16))
        return short.Parse(value);
      else if (type == typeof(Single))
        return float.Parse(value);
      else if (type == typeof(Double))
        return double.Parse(value);
      else if (type == typeof(Char))
        return char.Parse(value);
      else
        return value;
    }

  }
}