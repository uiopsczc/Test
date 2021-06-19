using System;
using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditorInternal;
#endif

namespace CsCat
{
  public static class ListExtension
  {
    public static List<T> EmptyIfNull<T>(this List<T> self)
    {
      if (self == null)
        return new List<T>();
      return self;
    }

    public static int FindIndex<T>(this IList<T> self, Func<T, bool> match)
    {
      var iterator = self.GetEnumerator();
      var cur_index = -1;
      while (iterator.MoveNext(ref cur_index))
        if (match(iterator.Current))
          return cur_index;
      return cur_index;
    }

    public static void RemoveEmpty<T>(this System.Collections.Generic.List<T> self)
    {
      for (var i = self.Count - 1; i >= 0; i--)
        if (self[i] == null)
          self.RemoveAt(i);
    }

    /// <summary>
    ///   将list[a]和list[b]交换
    /// </summary>
    public static void Swap<T>(this IList<T> self, int a, int b)
    {
      ListUtil.Swap(self, a, self, b);
    }

    //超过index或者少于0的循环index表获得
    public static T GetLooped<T>(this IList<T> self, int index)
    {
      while (index < 0) index += self.Count;
      if (index >= self.Count) index %= self.Count;
      return self[index];
    }

    //超过index或者少于0的循环index表设置
    public static void SetLooped<T>(this IList<T> self, int index, T value)
    {
      while (index < 0) index += self.Count;
      if (index >= self.Count) index %= self.Count;
      self[index] = value;
    }

    public static bool ContainsIndex(this IList self, int index)
    {
      if (index < self.Count && index >= 0)
        return true;
      return false;
    }

    /// <summary>
    ///   使其内元素单一
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="self"></param>
    /// <returns></returns>
    public static List<T> Unique<T>(this List<T> self)
    {
      var unqiue_list = new List<T>();
      for (int i = 0; i < self.Count; i++)
      {
        var element = self[i];
        if (!unqiue_list.Contains(element))
          unqiue_list.Add(element);
      }
      return unqiue_list;
    }


    public static List<T> Combine<T>(this IList<T> self, IList<T> another, bool is_unique = false)
    {
      var result = new List<T>(self);
      result.AddRange(another);
      if (is_unique)
        result = result.Unique();
      return result;
    }

    public static void Push<T>(this IList<T> self, T t)
    {
      self.Add(t);
    }

    public static T Peek<T>(this IList<T> self)
    {
      return self.Last();
    }

    public static T Pop<T>(this IList<T> self)
    {
      return self.RemoveLast();
    }


    #region 查找

    /// <summary>
    ///   第一个item
    ///   用linq
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="self"></param>
    /// <returns></returns>
    public static T First<T>(this IList<T> self)
    {
      return self[0];
    }

    /// <summary>
    ///   最后一个item
    ///   用linq
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="self"></param>
    /// <returns></returns>
    public static T Last<T>(this IList<T> self)
    {
      return self[self.Count - 1];
    }

    /// <summary>
    ///   在list中找sublist的开始位置
    /// </summary>
    /// <returns>-1表示没找到</returns>
    public static int IndexOfSub<T>(this IList<T> self, IList<T> sub_list)
    {
      var result_from_index = -1; //sublist在list中的开始位置
      for (var i = 0; i < self.Count; i++)
      {
        object o = self[i];
        if (ObjectUtil.Equals(o, sub_list[0]))
        {
          var equals = true;
          for (var j = 1; j < sub_list.Count; j++)
          {
            var o1 = sub_list[j];
            var o2 = i + j > self.Count - 1 ? default : self[i + j];
            if (!ObjectUtil.Equals(o1, o2))
            {
              equals = false;
              break;
            }
          }

          if (equals)
          {
            result_from_index = i;
            break;
          }
        }
      }

      return result_from_index;
    }

    /// <summary>
    ///   在list中只保留sublist中的元素
    /// </summary>
    public static bool RetainElementsOfSub<T>(this IList<T> self, IList<T> sub_list)
    {
      var is_modify = false;
      for (var i = self.Count - 1; i >= 0; i--)
        if (!sub_list.Contains(self[i]))
        {
          self.RemoveAt(i);
          is_modify = true;
        }

      return is_modify;
    }

    /// <summary>
    ///   包含fromIndx，但不包含toIndx，到toIndex前一位
    /// </summary>
    public static List<T> Sub<T>(this IList<T> self, int from_index, int to_index)
    {
      var al = new List<T>();
      for (var i = from_index; i <= to_index; i++)
        al.Add(self[i]);
      return al;
    }


    /// <summary>
    ///   包含fromIndx到末尾
    /// </summary>
    public static List<T> Sub<T>(this IList<T> self, int from_index)
    {
      return self.Sub(from_index, self.Count - 1);
    }

    #endregion

    #region 插入删除操作

    /// <summary>
    ///   当set来使用，保持只有一个
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="c"></param>
    public static List<T> Add<T>(this List<T> self, T item, bool is_unqiue = false)
    {
      if (is_unqiue && CheckIsDuplicate(self, item))
        return self;
      self.Add(item);
      return self;
    }

    public static List<T> AddRange<T>(this List<T> self, IEnumerable<T> collection, bool is_unqiue = false)
    {
      foreach (var e in collection)
      {
        if (is_unqiue && self.Contains(e))
          continue;
        self.Add(e);
      }

      return self;
    }

    private static bool CheckIsDuplicate<T>(this IList<T> self, T item)
    {
      if (self.Contains(item)) return true;
      return false;
    }


    public static System.Collections.Generic.List<T> AddFirst<T>(this System.Collections.Generic.List<T> self, T o, bool is_unqiue = false)
    {
      if (is_unqiue && CheckIsDuplicate(self, o))
        return self;
      self.Insert(0, o);
      return self;
    }

    public static System.Collections.Generic.List<T> AddLast<T>(this System.Collections.Generic.List<T> self, T o, bool is_unqiue = false)
    {
      if (is_unqiue && CheckIsDuplicate(self, o))
        return self;
      self.Insert(self.Count, o);
      return self;
    }


    public static System.Collections.Generic.List<T> AddUnique<T>(this System.Collections.Generic.List<T> self, T o)
    {
      return AddLast(self, o, true);
    }


    public static T RemoveFirst<T>(this IList<T> self)
    {
      var t = self[0];
      self.RemoveAt(0);
      return t;
    }


    public static T RemoveLast<T>(this IList<T> self)
    {
      var o = self[self.Count - 1];
      self.RemoveAt(self.Count - 1);
      return o;
    }

    /// <summary>
    ///   跟RemoveAt一样，只是有返回值
    /// </summary>
    public static T RemoveAt2<T>(this System.Collections.Generic.List<T> self, int index)
    {
      var t = self[index];
      self.RemoveAt(index);
      return t;
    }


    /// <summary>
    ///   跟Remove一样，只是有返回值(是否删除掉)
    /// </summary>
    /// <param name="self"></param>
    /// <param name="o"></param>
    /// <returns></returns>
    public static bool Remove2<T>(this IList<T> self, T o)
    {
      if (!self.Contains(o))
        return false;
      self.Remove(o);
      return true;
    }

    /// <summary>
    ///   删除list中的所有元素
    /// </summary>
    /// <param name="self"></param>
    /// <returns></returns>
    public static void RemoveAll<T>(this IList<T> self)
    {
      self.RemoveRange2(0, self.Count);
    }

    /// <summary>
    ///   删除list中的subList（subList必须要全部在list中）
    /// </summary>
    public static bool RemoveSub<T>(this IList<T> self, IList<T> sub_list)
    {
      var from_index = self.IndexOfSub(sub_list);
      if (from_index == -1)
        return false;
      var is_modify = false;
      for (var i = from_index + sub_list.Count - 1; i >= from_index; i--)
      {
        self.RemoveAt(i);
        if (!is_modify)
          is_modify = true;
      }

      return is_modify;
    }

    /// <summary>
    ///   跟RemoveRange一样，但返回删除的元素List
    /// </summary>
    public static System.Collections.Generic.List<T> RemoveRange2<T>(this IList<T> self, int index, int length)
    {
      var result = new System.Collections.Generic.List<T>();
      var last_index = index + length - 1 <= self.Count - 1 ? index + length - 1 : self.Count - 1;
      for (var i = last_index; i >= index; i--)
      {
        result.Add(self[i]);
        self.RemoveAt(i);
      }

      result.Reverse();

      return result;
    }


    /// <summary>
    ///   在list中删除subList中出现的元素
    /// </summary>
    public static bool RemoveElementsOfSub<T>(this IList<T> self, IList<T> sub_list)
    {
      var is_modify = false;
      for (var i = self.Count - 1; i >= 0; i++)
        if (sub_list.Contains(self[i]))
        {
          self.Remove(self[i]);
          is_modify = true;
        }

      return is_modify;
    }

    #endregion

    #region Random 随机

    public static List<T> RandomList<T>(this List<T> self, int out_count, bool is_unique,
      RandomManager randomManager = null, params float[] weights)
    {
      randomManager = randomManager ?? Client.instance.randomManager;
      return randomManager.RandomList(self, out_count, is_unique, weights.ToList());
    }

    public static T Random<T>(this List<T> self, RandomManager randomManager = null, params float[] weights)
    {
      return self.RandomList(1, false, randomManager, weights)[0];
    }

    #endregion


    //如：list.BubbleSort((a, b)=>return a.count <= b.count)
    //则是将count由小到大排序，注意比较大小时不要漏掉等于号，否则相等时也进行排序，则排序不稳定
    public static void BubbleSort<T>(this IList<T> self, Func<T, T, bool> compare_func)
    {
      SortUtil.BubbleSort(self, compare_func);
    }

    public static void BubbleSort(this IList self, Func<object, object, bool> compare_func)
    {
      SortUtil.BubbleSort(self, compare_func);
    }

    public static void BubbleSortWithCompareRules<T>(this IList<T> self, params Comparison<T>[] compare_rules)
    {
      SortUtil.BubbleSortWithCompareRules(self, compare_rules);
    }

    public static void BubbleSortWithCompareRules(this IList self, params Comparison<object>[] compare_rules)
    {
      SortUtil.BubbleSortWithCompareRules(self, compare_rules);
    }

    //如：list.QuickSort((a, b)=>return a.count <= b.count)
    //则是将count由小到大排序，注意比较大小时不要漏掉等于号，否则相等时也进行排序，则排序不稳定
    public static void QuickSort<T>(this IList<T> self, Func<T, T, bool> compare_func)
    {
      SortUtil.QuickSort(self, compare_func);
    }

    public static void QuickSort(this IList self, Func<object, object, bool> compare_func)
    {
      SortUtil.QuickSort(self, compare_func);
    }

    public static void QuickSortWithCompareRules<T>(this IList<T> self, params Comparison<T>[] compare_rules)
    {
      SortUtil.QuickSortWithCompareRules(self, compare_rules);
    }

    public static void QuickSortWithCompareRules(this IList self, params Comparison<object>[] compare_rules)
    {
      SortUtil.QuickSortWithCompareRules(self, compare_rules);
    }

    //////////////////////////////////////////////////////////////////////
    // Diff相关
    //////////////////////////////////////////////////////////////////////
    // 必须和ApplyDiff使用
    // 以new为基准，获取new相对于old不一样的部分
    // local diff = table.GetDiff(old, new)
    //  table.ApplyDiff(old, diff)
    // 这样old的就变成和new一模一样的数据
    public static LinkedHashtable GetDiff(this IList old_list, IList new_list)
    {
      return ListUtil.GetDiff(old_list, new_list);
    }

    // table.ApplyDiff(old, diff)
    // 将diff中的东西应用到old中
    // 重要：当为Array的时候，需要重新赋值；List的时候，可以不需要重新赋值
    public static IList ApplyDiff(this IList old_list, LinkedHashtable diff_dict)
    {
      return ListUtil.ApplyDiff(old_list, diff_dict);
    }

    // 必须和ApplyDiff使用
    // 以new为基准，获取new中有，但old中没有的
    // local diff = table.GetNotExist(old, new)
    // table.ApplyDiff(old, diff)
    // 这样old就有new中的字段
    public static LinkedHashtable GetNotExist(this IList old_list, IList new_list)
    {
      return ListUtil.GetNotExist(old_list, new_list);
    }

    //两个table是否不一样
    public static bool IsDiff(this IList old_list, IList new_list)
    {
      return ListUtil.IsDiff(old_list, new_list);
    }

    public static void CopyTo<T>(this IList<T> self, IList<T> dest_list, params object[] construct_args)
      where T : ICopyable
    {
      dest_list.Clear();
      for (int i = 0; i < self.Count; i++)
      {
        var dest_element = typeof(T).CreateInstance<T>(construct_args);
        dest_list.Add(dest_element);
        self[i].CopyTo(dest_element);
      }
    }

    public static void CopyFrom<T>(this IList<T> self, IList<T> source_list, params object[] construct_args)
      where T : ICopyable
    {
      self.Clear();
      for (int i = 0; i < source_list.Count; i++)
      {
        var self_element = typeof(T).CreateInstance<T>(construct_args);
        self.Add(self_element);
        self_element.CopyFrom(source_list[i]);
      }
    }

    public static ArrayList DoSaveList<T>(this IList<T> self, Action<T, Hashtable> doSave_callback)
    {
      ArrayList result = new ArrayList();
      for (int i = 0; i < self.Count; i++)
      {
        Hashtable element_dict = new Hashtable();
        result.Add(element_dict);
        var element = self[i];
        doSave_callback(element, element_dict);
      }
      return result;
    }

    public static void DoRestoreList<T>(this IList<T> self, ArrayList arrayList, Func<Hashtable, T> doRestore_callback)
    {
      for (int i = 0; i < arrayList.Count; i++)
      {
        var element_dict = arrayList[i] as Hashtable;
        T element = doRestore_callback(element_dict);
        self.Add(element);
      }
    }

    public static void SortWithCompareRules<T>(this List<T> self, params Comparison<T>[] compare_rules)
    {
      CompareUtil.SortListWithCompareRules(self, compare_rules);
    }

    #region 各种To ToXXX

    /// <summary>
    ///   变为对应的ArrayList
    /// </summary>
    /// <param name="self"></param>
    /// <returns></returns>
    public static ArrayList ToArrayList(this IList self)
    {
      var al = new ArrayList();
      foreach (var o in self)
        al.Add(o);
      return al;
    }

    public static T[] ToArray<T>(this ICollection<T> self)
    {
      var result = new T[self.Count];
      self.CopyTo(result, 0);
      return result;
    }
#if UNITY_EDITOR
    public static void ToReorderableList(this IList to_reorder_list, ref ReorderableList _reorderableList)
    {
      ReorderableListUtil.ToReorderableList(to_reorder_list, ref _reorderableList);
    }
#endif

    #endregion
  }
}