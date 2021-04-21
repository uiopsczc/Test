using System.Collections;
using System;
using System.Collections.Generic;
using System.Text;

namespace CsCat
{
  public static class CollectionExtension
  {

    /// <summary>
    /// 转化为ToLinkedHashtable
    /// </summary>
    /// <param name="self"></param>
    /// <returns></returns>
    public static object ToLinkedHashtable2<T>(this ICollection<T> self)
    {
      if (self is IDictionary)
      {
        LinkedHashtable lht = new LinkedHashtable();
        foreach (object key in ((IDictionary)self).Keys)
        {
          object value = ((IDictionary)self)[key];
          lht.Put(key, value.ToLinkedHashtable2());
        }

        return lht;
      }
      else
      {
        ArrayList list = new ArrayList();
        foreach (object o in self)
        {
          list.Add(o.ToLinkedHashtable2());
        }

        return list;
      }
    }

    /// <summary>
    /// 将s从fromIndex开始,到toIndex（不包括toIndex）结束的元素转为字符串连接起来，元素之间用n连接（最后的元素后面不加n）
    /// 例如：object[] s={"aa","bb","cc"} split="DD" return "aaDDbbDDcc"
    /// </summary>
    public static string Join<T>(this ICollection<T> self, int from_index, int to_index, string separator)
    {
      if (from_index >= 0 && to_index <= self.Count && to_index - from_index >= 0)
      {
        var sb = new StringBuilder();
        if (to_index - from_index > 0)
        {
          int i = 0;
          var iter = self.GetEnumerator();
          while (iter.MoveNext())
          {
            string value = iter.Current.ToString();
            if (i == from_index)
            {
              sb.Append(value);
            }
            else if (i > from_index && i <= to_index)
            {
              sb.Append(separator + value);
            }

            i++;
          }
        }

        return sb.ToString();
      }

      throw new IndexOutOfRangeException();
    }

    public static string Join<T>(this ICollection<T> self, string sep)
    {
      return self.Join(0, self.Count, sep);
    }


    /// <summary>
    /// 用于不同类型转换
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="self"></param>
    /// <returns></returns>
    public static List<T> ToList<T>(this ICollection self)
    {
      List<T> result = new List<T>();
      int cur_index = -1;
      var iterator = self.GetEnumerator();
      while (iterator.MoveNext(ref cur_index))
      {
        result.Add((T)iterator.Current);
      }

      return result;
    }

    /// <summary>
    /// 用于不同类型转换
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="self"></param>
    /// <returns></returns>
    public static T[] ToArray<T>(this ICollection self)
    {
      T[] result = new T[self.Count];
      int cur_index = -1;
      var iterator = self.GetEnumerator();
      while (iterator.MoveNext(ref cur_index))
      {
        result[cur_index] = (T)iterator.Current;
      }

      return result;
    }


    public static T[] ToArray<U, T>(this ICollection<U> c, Func<U, T> func)
    {
      T[] result = new T[c.Count];
      int cur_index = -1;
      var iterator = c.GetEnumerator();
      while (iterator.MoveNext(ref cur_index))
      {
        result[cur_index] = func(iterator.Current);
      }

      return result;
    }

    public static void AddRange<T>(this ICollection<T> self, params T[] values)
    {
      foreach (T value in values)
        self.Add(value);
    }

    public static bool IsNullOrEmpty(this ICollection self)
    {
      if (self == null || self.Count == 0)
        return true;
      return false;
    }




    #region ToStrign2

    public static string ToString2(this ICollection self, bool is_fill_string_with_double_quote = false)
    {
      bool first = true;
      StringBuilder sb = new StringBuilder();

      if (self is Array)
        sb.Append("(");
      else if (self is IList)
        sb.Append("[");
      else if (self is IDictionary)
        sb.Append("{");

      if (self is IDictionary)
      {
        foreach (object key in ((IDictionary)self).Keys)
        {
          if (first)
            first = false;
          else
            sb.Append(",");
          sb.Append(key.ToString2(is_fill_string_with_double_quote));
          sb.Append(":");
          object value = ((IDictionary)self)[key];
          sb.Append(value.ToString2(is_fill_string_with_double_quote));
        }
      }
      else //list
      {
        foreach (object o in self)
        {
          if (first)
            first = false;
          else
            sb.Append(",");
          sb.Append(o.ToString2(is_fill_string_with_double_quote));
        }
      }

      if (self is Array)
        sb.Append(")");
      else if (self is IList)
        sb.Append("]");
      else if (self is IDictionary)
        sb.Append("}");

      return sb.ToString();
    }

    #endregion
  }
}