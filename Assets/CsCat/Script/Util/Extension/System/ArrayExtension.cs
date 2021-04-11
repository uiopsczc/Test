using System;
using System.Collections.Generic;

namespace CsCat
{
  public static class ArrayExtension
  {
    public static T[] EmptyIfNull<T>(this T[] self)
    {
      if (self == null)
        return new T[0];
      return self;
    }

    /// <summary>
    /// 将数组转化为List
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="self"></param>
    /// <returns></returns>
    public static List<T> ToList<T>(this T[] self)
    {
      var ls = new List<T>();
      if (!self.IsNullOrEmpty())
      {
        foreach (object t in self)
          ls.Add((T) t);
      }

      return ls;
    }

    public static void Reverse<T>(this T[] self)
    {
      Array.Reverse(self);
    }

    public static void Foreach<T>(this T[] self, Action<T> action)
    {
      foreach (var a in self)
        action(a);
    }

    public static T[] Copy<T>(this T[] self)
    {
      T[] clone = new T[self.Length];
      self.CopyTo(clone, 0);
      return clone;
    }


    /// <summary>
    /// 数组中target的Index
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="self"></param>
    /// <param name="target"></param>
    /// <returns></returns>
    public static int IndexOf<T>(this T[] self, T target)
    {
      for (int i = 0; i < self.Length; i++)
      {
        if (target.Equals(self.GetValue(i)))
          return i;
      }

      return -1;
    }


    public static T[] CopyTo<T>(this T[] self, int start_index, int len)
    {
      int length = Math.Min(len, self.Length - start_index);
      T[] target = new T[length];
      for (int i = 0; i < length; i++)
      {
        target[i] = self[start_index + i];
      }

      return target;
    }

    /// <summary>
    ///扩展数组
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="self"></param>
    public static T[] DoubleCapacity<T>(this T[] self)
    {
      int org_length = self.Length;
      int new_length = org_length * 2;
      T[] new_objects = new T[new_length];
      Array.Copy(self, new_objects, org_length);
      return new_objects;
    }

    /// <summary>
    /// 包含fromIndx，但不包含toIndx，到toIndex前一位
    /// </summary>
    public static T[] Sub<T>(this T[] self, int from_index, int to_index)
    {
      return self.ToList().Sub(from_index, to_index).ToArray();
    }

    /// <summary>
    /// 包含fromIndx到末尾
    /// </summary>
    public static T[] Sub<T>(this T[] self, int from_index)
    {
      return self.Sub(from_index, self.Length - 1);
    }

    /// <summary>
    /// 当set来使用，保持只有一个
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="c"></param>
    public static T[] Add<T>(this T[] self, T item, bool is_unqiue = false)
    {
      return self.ToList().Add(item, is_unqiue).ToArray();
    }

    public static T[] AddRange<T>(this T[] self, IEnumerable<T> collection, bool is_unqiue = false)
    {
      return self.ToList().AddRange(collection, is_unqiue).ToArray();
    }


    public static T[] AddFirst<T>(this T[] self, T o)
    {
      return self.ToList().AddFirst(o).ToArray();
    }

    public static T[] AddLast<T>(this T[] self, T o)
    {
      return self.ToList().AddLast(o).ToArray();
    }

    public static T RemoveFirst<T>(this T[] self)
    {
      List<T> list = self.ToList();
      T reuslt = list.RemoveFirst();
      return reuslt;
    }

    public static T[] Insert<T>(this T[] self, int index, T item)
    {
      if (index == self.Length)
        return self.Add(item);
      List<T> list = new List<T>(self);
      list.Insert(index, item);
      return list.ToArray();
    }

    public static T[] InsertRange<T>(this T[] self, int index, IEnumerable<T> collection)
    {
      if (index == self.Length)
        return self.AddRange(collection);
      List<T> list = new List<T>(self);
      list.InsertRange(index, collection);
      return list.ToArray();
    }


    public static T RemoveLast<T>(this T[] self)
    {
      List<T> list = self.ToList();
      T reuslt = list.RemoveLast();
      return reuslt;
    }


    /// <summary>
    /// 跟RemoveAt一样，只是有返回值
    /// </summary>
    public static T RemoveAt2<T>(this T[] self, int index)
    {
      List<T> list = self.ToList();
      T reuslt = list.RemoveAt2(index);
      return reuslt;
    }


    /// <summary>
    /// 删除list中的subList（subList必须要全部在list中）
    /// </summary>
    public static bool RemoveSub<T>(this T[] self, T[] sub_array)
    {
      List<T> list = self.ToList();
      bool reuslt = list.RemoveSub(sub_array);
      return reuslt;
    }


    /// <summary>
    /// 跟RemoveRange一样，但返回删除的元素List
    /// </summary>
    public static T[] RemoveRange2<T>(this T[] self, int index, int length)
    {
      List<T> list = self.ToList();
      T[] result = list.RemoveRange2(index, length).ToArray();
      return result;
    }

    /// <summary>
    /// 在list中删除subList中出现的元素
    /// </summary>
    public static bool RemoveElementsOfSub<T>(this T[] self, T[] sub_array)
    {
      List<T> list = self.ToList();
      bool result = list.RemoveElementsOfSub(sub_array);
      return result;
    }

    public static T[] RandomArray<T>(this T[] self, int out_count, bool is_unique, RandomManager randomManager = null,
      params float[] weights)
    {
      return self.ToList().RandomList(out_count, is_unique, randomManager, weights).ToArray();
    }

    public static T Random<T>(this T[] self, RandomManager randomManager = null, params float[] weights)
    {
      return self.RandomArray(1, false, randomManager, weights)[0];
    }


    /// <summary>
    /// 使其内元素单一
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="c"></param>
    /// <returns></returns>
    public static T[] Unique<T>(this T[] self)
    {
      List<T> list = self.ToList();
      T[] result = list.Unique().ToArray();
      return result;
    }

    public static T[] Combine<T>(this T[] self, T[] another, bool is_unique = false)
    {
      List<T> list = self.ToList();
      T[] result = list.Combine(another, is_unique).ToArray();
      return result;
    }

    /// <summary>
    /// 将多个数组合成一个数组
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <param name="arrs"></param>
    /// <returns></returns>
    public static T[] Combine<T>(this T[] self, bool is_unique, params T[][] arrs)
    {
      List<T> result = new List<T>(self);
      foreach (T[] t in arrs)
      foreach (T element in t)
        result.Add(element);
      if (is_unique)
        result = result.Unique();
      return result.ToArray();
    }

    public static bool Contains<T>(this T[] self, T target)
    {
      if (self.IndexOf(target) != -1)
        return true;
      return false;
    }

    public static bool ContainsIndex<T>(this T[] self, int index)
    {
      if (index >= 0 && index < self.Length)
        return true;
      return false;
    }

    //将self初始化为[height][width]的数组
    public static T[][] InitArrays<T>(this T[][] self, int height, int width, T default_value = default(T))
    {
      self = new T[height][];
      for (int i = 0; i < height; i++)
        self[i] = new T[width];
      if (!ObjectUtil.Equals(default_value, default(T)))
        for (int i = 0; i < height; i++)
        {
          for (int j = 0; j < width; j++)
          {
            self[i][j] = default_value;
          }
        }

      return self;
    }

    //转为左下为原点的坐标系，x增加是向右，y增加是向上（与unity的坐标系一致）
    public static T[][] ToLeftBottomBaseArrays<T>(this T[][] self)
    {
      int self_height = self.Length;
      int self_width = self[0].Length;
      int result_height = self_width;
      int result_width = self_height;
      var result = InitArrays(self, result_height, result_width);
      for (int i = 0; i < result_width; i++)
      for (int j = 0; j < result_height; j++)
        result[j][result_width - 1 - i] = self[i][j];
      return result;
    }


    public static Array Resize_Array(this Array self, int length)
    {
      Type element_type = self.GetType().GetElementType();
      Array new_array = Array.CreateInstance(element_type, length);
      Array.Copy(self, 0, new_array, 0, Math.Min(self.Length, length));
      return new_array;
    }

    public static Array Insert_Array(this Array self, int index, object value)
    {
      int new_array_length = index < self.Length ? self.Length + 1 : index + 1;

      Type element_type = self.GetType().GetElementType();
      Array new_array = Array.CreateInstance(element_type, new_array_length);
      Array.Copy(self, 0, new_array, 0, Math.Min(new_array_length, self.Length));
      new_array.SetValue(value, index);
      if (index < self.Length)
        Array.Copy(self, index, new_array, index + 1, self.Length - index);
      return new_array;
    }

    public static Array RemoveAt_Array(this Array self, int index)
    {
      Type element_type = self.GetType().GetElementType();
      Array new_array = Array.CreateInstance(element_type, self.Length - 1);
      Array.Copy(self, 0, new_array, 0, index);
      Array.Copy(self, index + 1, new_array, index, self.Length - index - 1);
      return new_array;
    }


    public static void CopyTo<T>(this T[] self, T[] dest_array, params object[] construct_args) where T : ICopyable
    {
      dest_array =new T[self.Length];
      for (int i = 0; i < self.Length; i++)
      {
        var dest_element = typeof(T).CreateInstance<T>(construct_args);
        dest_array[i]=dest_element;
        self[i].CopyTo(dest_element);
      }
    }
    public static void CopyFrom<T>(this T[] self, T[] source_array, params object[] construct_args) where T : ICopyable
    {
      Array.Resize(ref self, source_array.Length);
      for (int i = 0; i < source_array.Length; i++)
      {
        var self_element = typeof(T).CreateInstance<T>(construct_args);
        self_element.CopyFrom(source_array[i]);
        self[i] = self_element;
      }
    }

    public static T[] SortWithCompareRules<T>(this T[] self, params Comparison<T>[] compare_rules)
    {
      return CompareUtil.SortArrayWithCompareRules(self, compare_rules);
    }
  }
}