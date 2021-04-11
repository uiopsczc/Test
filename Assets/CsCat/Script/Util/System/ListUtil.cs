using System;
using System.Collections;
using System.Collections.Generic;

namespace CsCat
{
  public static class ListUtil
  {
    /// <summary>
    ///   将list1[a]和list2[b]交换
    /// </summary>
    public static void Swap<T>(IList<T> list1, int a, IList<T> list2, int b)
    {
      if (!list1[a].Equals(list2[b]))
      {
        var c = list1[a];
        list1[a] = list2[b];
        list2[b] = c;
      }
    }

    //////////////////////////////////////////////////////////////////////
    // Diff相关
    //////////////////////////////////////////////////////////////////////
    // 必须和ApplyDiff使用
    // 以new为基准，获取new相对于old不一样的部分
    // local diff = table.GetDiff(old, new)
    //  table.ApplyDiff(old, diff)
    // 这样old的就变成和new一模一样的数据
    public static LinkedHashtable GetDiff(IList old_list, IList new_list)
    {
      var diff = new LinkedHashtable();
      for (int i = new_list.Count - 1; i >= 0; i--)
      {
        var new_k = i;
        var new_v = new_list[i];
        if (new_v is IList)
        {
          var new_v_list = new_v as IList;
          if (new_v_list.Count == 0 && (!old_list.ContainsIndex(new_k) ||
                                        old_list[new_k].GetType() != new_v.GetType() ||
                                        (old_list[new_k] is IList && (old_list[new_k] as IList).Count != 0)))
            diff[new_k] = "__{}__" + new_v.GetType();
          else if (old_list.ContainsIndex(new_k) && old_list[new_k] is IList)
            diff[new_k] = GetDiff(old_list[new_k] as IList, new_v_list);
          else if (!old_list.ContainsIndex(new_k) || !new_v.Equals(old_list[new_k]))
            diff[new_k] = CloneUtil.Clone_Deep(new_v);
        }
        else if (new_v is IDictionary && old_list.ContainsIndex(new_k) && old_list[new_k] is IDictionary)
          diff[new_k] = DictionaryUtil.GetDiff(old_list[new_k] as IDictionary, new_v as IDictionary);
        else if (!old_list.ContainsIndex(new_k) || !new_v.Equals(old_list[new_k]))
          diff[new_k] = new_v;
      }

      for (int i = 0; i < old_list.Count; i++)
      {
        if (!new_list.ContainsIndex(i))
          diff[i] = "__nil__";
      }

      diff.key_list.QuickSort((a, b) => a.To<int>() >= b.To<int>());
      if (diff.Count == 0)
        diff = null;
      return diff;
    }

    // table.ApplyDiff(old, diff)
    // 将diff中的东西应用到old中
    // 重要：当为Array的时候，需要重新赋值；List的时候，可以不需要重新赋值
    public static IList ApplyDiff(IList old_list, LinkedHashtable diff_dict)
    {
      if (diff_dict == null)
      {
        return old_list;
      }

      int old_list_count = old_list.Count;
      foreach (var _k in diff_dict.Keys)
      {
        var k = _k.To<int>();
        var v = diff_dict[_k];
        if (v.Equals("__nil__"))
        {
          if (old_list is Array)
            old_list = (old_list as Array).RemoveAt_Array(k);
          else
            old_list.RemoveAt(k);


        }
        else if (v.ToString().StartsWith("__{}__"))
        {
          string type_string = v.ToString().Substring("__{}__".Length);
          Type type = TypeUtil.GetType(type_string);
          var value = type.CreateInstance<object>();
          if (k < old_list_count)
            old_list[k] = value;
          else
          {
            if (old_list is Array)
              old_list = (old_list as Array).Insert_Array(old_list_count, v);
            else
              old_list.Insert(old_list_count, v);
          }
        }
        else if (old_list.ContainsIndex(k) && old_list[k] is IList && v is LinkedHashtable)
          ApplyDiff(old_list[k] as IList, v as LinkedHashtable);
        else if (old_list.ContainsIndex(k) && old_list[k] is IDictionary && v is LinkedHashtable)
          DictionaryUtil.ApplyDiff(old_list[k] as IDictionary, v as LinkedHashtable);
        else
        {
          if (k < old_list_count)
            old_list[k] = v;
          else
          {
            if (old_list is Array)
              old_list = (old_list as Array).Insert_Array(old_list_count, v);
            else
              old_list.Insert(old_list_count, v);
          }
        }
      }

      return old_list;
    }

    // 必须和ApplyDiff使用
    // 以new为基准，获取new中有，但old中没有的
    // local diff = table.GetNotExist(old, new)
    // table.ApplyDiff(old, diff)
    // 这样old就有new中的字段
    public static LinkedHashtable GetNotExist(IList old_list, IList new_list)
    {
      var diff = new LinkedHashtable();
      for (int i = new_list.Count - 1; i >= 0; i--)
      {
        var new_k = i;
        var new_v = new_list[i];

        if (!old_list.ContainsIndex(i))
          diff[new_k] = new_k;
        else
        {
          if (old_list[new_k] is IList && new_v is IList)
            diff[new_k] = GetDiff(old_list[new_k] as IList, new_v as IList);
          else if (old_list[new_k] is IDictionary && new_v is IDictionary)
            diff[new_k] = DictionaryUtil.GetNotExist(old_list[new_k] as IDictionary, new_v as IDictionary);
          //其他情况不用处理
        }
      }

      diff.key_list.QuickSort((a, b) => a.To<int>() >= b.To<int>());
      return diff;
    }

    //两个table是否不一样
    public static bool IsDiff(IList old_list, IList new_list)
    {
      if (old_list.Count != new_list.Count)
        return true;
      for (int new_key = 0; new_key < new_list.Count; new_key++)
      {
        var new_value = new_list[new_key];
        if (new_value is IList)
        {
          if (!(old_list[new_key] is IList))
            return true;
          if (IsDiff(old_list[new_key] as IList, new_value as IList))
            return true;
        }
        else if (new_value is IDictionary)
        {
          if (!(old_list[new_key] is IDictionary))
            return true;
          if (DictionaryUtil.IsDiff(old_list[new_key] as IDictionary, new_value as IDictionary))
            return true;
        }
        else if (!new_value.Equals(old_list[new_key]))
          return true;
      }

      return false;
    }
  }
}