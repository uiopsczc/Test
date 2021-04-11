using System;
using System.Collections;

namespace CsCat
{
  public static class DictionaryUtil
  {
    //////////////////////////////////////////////////////////////////////
    // Diff相关
    //////////////////////////////////////////////////////////////////////
    // 必须和ApplyDiff使用
    // 以new为基准，获取new相对于old不一样的部分
    // local diff = table.GetDiff(old, new)
    //  table.ApplyDiff(old, diff)
    // 这样old的就变成和new一模一样的数据
    public static LinkedHashtable GetDiff(IDictionary old_dict, IDictionary new_dict)
    {
      var diff = new LinkedHashtable();
      foreach (DictionaryEntry dictionaryEntry in new_dict)
      {
        var new_k = dictionaryEntry.Key;
        var new_v = dictionaryEntry.Value;
        if (new_v is IDictionary)
        {
          var new_v_dict = new_v as IDictionary;
          if (new_v_dict.Count == 0 && (!old_dict.Contains(new_k) || old_dict[new_k].GetType() != new_v.GetType() ||
                                        (old_dict[new_k] is IDictionary &&
                                         (old_dict[new_k] as IDictionary).Count != 0)))
            diff[new_k] = "__{}__" + new_v.GetType();
          else if (old_dict.Contains(new_k) && old_dict[new_k] is IDictionary)
            diff[new_k] = GetDiff(old_dict[new_k] as IDictionary, new_v_dict);
          else if (!old_dict.Contains(new_k) || !new_v.Equals(old_dict[new_k]))
            diff[new_k] = CloneUtil.Clone_Deep(new_v);
        }
        else if (new_v is IList && old_dict.Contains(new_k) && old_dict[new_k] is IList)
          diff[new_k] = ListUtil.GetDiff(old_dict[new_k] as IList, new_v as IList);
        else if (!old_dict.Contains(new_k) || !new_v.Equals(old_dict[new_k]))
          diff[new_k] = new_v;
      }

      foreach (var key in old_dict.Keys)
      {
        if (!new_dict.Contains(key))
          diff[key] = "__nil__";
      }

      if (diff.Count == 0)
        diff = null;
      return diff;
    }

    // table.ApplyDiff(old, diff)
    // 将diff中的东西应用到old中
    public static void ApplyDiff(IDictionary old_dict, LinkedHashtable diff_dict)
    {
      if (diff_dict == null)
      {
        return;
      }

      foreach (DictionaryEntry dictionaryEntry in diff_dict)
      {
        var k = dictionaryEntry.Key;
        var v = dictionaryEntry.Value;
        if (v.Equals("__nil__"))
          old_dict.Remove(k);
        else if (v.ToString().StartsWith("__{}__"))
        {
          string type_string = v.ToString().Substring("__{}__".Length);
          Type type = TypeUtil.GetType(type_string);
          old_dict[k] = type.CreateInstance<object>();
        }
        else if (old_dict.Contains(k) && old_dict[k] is IDictionary && v is LinkedHashtable)
          ApplyDiff(old_dict[k] as IDictionary, v as LinkedHashtable);
        else if (old_dict.Contains(k) && old_dict[k] is IList && v is LinkedHashtable)
          old_dict[k] = ListUtil.ApplyDiff(old_dict[k] as IList, v as LinkedHashtable);
        else
          old_dict[k] = v;
      }
    }

    // 必须和ApplyDiff使用
    // 以new为基准，获取new中有，但old中没有的
    // local diff = table.GetNotExist(old, new)
    // table.ApplyDiff(old, diff)
    // 这样old就有new中的字段
    public static LinkedHashtable GetNotExist(IDictionary old_dict, IDictionary new_dict)
    {
      var diff = new LinkedHashtable();
      foreach (DictionaryEntry dictionaryEntry in new_dict)
      {
        var new_k = dictionaryEntry.Key;
        var new_v = dictionaryEntry.Value;
        if (!old_dict.Contains(new_k))
          diff[new_k] = new_v;
        else
        {
          if (new_v is IDictionary && old_dict[new_k] is IDictionary)
            diff[new_k] = GetNotExist(old_dict[new_k] as IDictionary, new_v as IDictionary);
          else if (new_v is IList && old_dict[new_k] is IList)
            diff[new_k] = ListUtil.GetNotExist(old_dict[new_k] as IList, new_v as IList);
          //其他情况不用处理
        }
      }

      return diff;
    }

    //两个table是否不一样
    public static bool IsDiff(IDictionary old_dict, IDictionary new_dict)
    {
      foreach (var key in old_dict.Keys)
      {
        if (!new_dict.Contains(key))
          return true;
      }

      foreach (DictionaryEntry dictionaryEntry in new_dict)
      {
        var new_key = dictionaryEntry.Key;
        var new_value = dictionaryEntry.Value;
        if (!old_dict.Contains(new_key))
          return false;
        if (new_value is IDictionary)
        {
          if (!(old_dict[new_key] is IDictionary))
            return false;
          if (IsDiff(old_dict[new_key] as IDictionary, new_value as IDictionary))
            return true;
        }
        else if (new_value is IList)
        {
          if (!(old_dict[new_key] is IList))
            return false;
          if (ListUtil.IsDiff(old_dict[new_key] as IList, new_value as IList))
            return true;
        }
        else if (!new_value.Equals(old_dict[new_key]))
          return true;
      }

      return false;
    }
  }
}