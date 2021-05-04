using System;
using System.Text;

namespace CsCat
{
  public class ObjectUtil
  {
    /// <summary>
    /// o1是否和o2相等
    /// </summary>
    public new static bool Equals(object o1, object o2)
    {
      if (o1 == null)
        return o2 == null;
      return o1.Equals(o2);
    }

    /// <summary>
    /// o1和o2比较大小
    /// </summary>
    public static int Compares(object o1, object o2)
    {
      if (o1 == o2)
        return 0;
      if (o1 != null && o2 == null)
        return 1;
      if (o1 == null && o2 != null)
        return -1;
      var comparable = o1 as IComparable;
      if (comparable != null)
        return comparable.CompareTo(o2);
      var comparable1 = o2 as IComparable;
      if (comparable1 != null)
        return comparable1.CompareTo(o1);
      return o1.ToString().CompareTo(o2.ToString());
    }


    public static int GetHashCode(params object[] objs)
    {
      int result = int.MinValue;
      bool foundFirstNotNullObject = false;
      foreach (var obj in objs)
      {
        if (obj != null)
        {
          if (!foundFirstNotNullObject)
          {
            result = obj.GetHashCode();
            foundFirstNotNullObject = true;
          }
          else
            result ^= obj.GetHashCode();
        }
      }

      return result;
    }

    /// <summary>
    /// 交换两个object
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="a"></param>
    /// <param name="b"></param>
    public static void Swap<T>(ref T a, ref T b)
    {
      T c = b;
      b = a;
      a = c;
    }

    public static string ToString(params object[] objs)
    {
      StringBuilder stringBuilder = new StringBuilder();
      foreach (var obj in objs)
        stringBuilder.Append(obj+" ");
      if (stringBuilder.Length > 0)
        stringBuilder.Remove(stringBuilder.Length - 1, 1);
      return stringBuilder.ToString();
    }

    public static string ToString2(params object[] objs)
    {
      StringBuilder stringBuilder = new StringBuilder();
      foreach (var obj in objs)
        stringBuilder.Append(obj.ToString2() + " ");
      if (stringBuilder.Length > 0)
        stringBuilder.Remove(stringBuilder.Length - 1, 1);
      return stringBuilder.ToString();
    }
  }
}