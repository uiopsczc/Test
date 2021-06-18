using System;
using System.Collections;
using System.Collections.Generic;


namespace CsCat
{
  //里面的结构是  dict<id,List<Doer>>
  public class SubDoerUtil2
  {
    public static void DoReleaseSubDoer<T>(Doer parent_doer, string sub_doer_key, Action<T> relase_sub_doer_func = null)
      where T : Doer
    {
      //销毁
      var sub_doers = GetSubDoers<T>(parent_doer, sub_doer_key);
      for (int i = sub_doers.Length - 1; i >= 0; i--)
      {
        var sub_doer = sub_doers[i];
        relase_sub_doer_func?.Invoke(sub_doer);
        sub_doer.SetEnv(null);
        sub_doer.Destruct();
      }

      GetSubDoerDict_ToEdit(parent_doer, sub_doer_key).Clear();
    }

    /////////////////////////////////容器/////////////////////////////////
    public static T[] GetSubDoers<T>(Doer parent_doer, string sub_doer_key, string id = null,
      Func<T, bool> filter_func = null) where T : Doer
    {
      var dict = GetSubDoerDict_ToEdit(parent_doer, sub_doer_key);
      List<T> result = new List<T>();
      if (id == null)
      {
        if (filter_func == null)
        {
          foreach (var sub_doer_list in dict.Values)
          {
            foreach (var sub_doer in sub_doer_list as ArrayList)
            {
              result.Add(sub_doer as T);
            }
          }
        }
        else
        {
          foreach (var sub_doer_list in dict.Values)
          {
            foreach (var sub_doer in sub_doer_list as ArrayList)
              if (filter_func(sub_doer as T))
                result.Add(sub_doer as T);
          }
        }

        return result.ToArray();
      }

      var list = GetSubDoers_ToEdit(parent_doer, sub_doer_key, id);
      foreach (var sub_doer in list)
      {
        if (filter_func == null || filter_func(sub_doer as T))
          result.Add(sub_doer as T);
      }

      return result.ToArray();
    }

    public static Hashtable GetSubDoerDict_ToEdit(Doer parent_doer, string sub_doer_key) //进行直接修改
    {
      var dict = parent_doer.GetOrAddTmp(sub_doer_key, () => new Hashtable());
      return dict;
    }

    public static ArrayList GetSubDoers_ToEdit(Doer parent_doer, string sub_doer_key, string id) //进行直接修改
    {
      var dict = GetSubDoerDict_ToEdit(parent_doer, sub_doer_key);
      var list = dict.GetOrAddDefault(id, () => new ArrayList());
      return list;
    }

    public static T GetSubDoer<T>(Doer parent_doer, string sub_doer_key, string id_or_rid) where T : Doer
    {
      bool is_id = IdUtil.IsId(id_or_rid);
      string id = is_id ? id_or_rid : IdUtil.RidToId(id_or_rid);
      var dict = GetSubDoerDict_ToEdit(parent_doer, sub_doer_key);
      if (dict.ContainsKey(id) && !(dict[id] as ArrayList).IsNullOrEmpty())
      {
        foreach (T sub_doer in dict[id] as ArrayList)
        {
          if (is_id)
            return sub_doer;
          else
          {
            if (sub_doer.GetRid().Equals(id_or_rid))
              return sub_doer;
          }
        }
      }

      return null;
    }

    //doer中sub_doer_key的子doers
    public static bool HasSubDoers<T>(Doer parent_doer, string sub_doer_key, string id = null,
      Func<Doer, bool> filter_func = null) where T : Doer
    {
      return !GetSubDoers(parent_doer, sub_doer_key, id, filter_func).IsNullOrEmpty();
    }

    //获取doer中的sub_doer_key的子doer数量  并不是sub_doer:GetCount()累加，而是sub_doers的个数
    public static int GetSubDoersCount<T>(Doer parent_doer, string sub_doer_key, string id = null,
      Func<T, bool> filter_func = null) where T : Doer
    {
      return GetSubDoers(parent_doer, sub_doer_key, id, filter_func).Length;
    }

    // 获取doer中的sub_doer_key的子doer数量  sub_doer:GetCount()累加
    public static int GetSubDoerCount<T>(Doer parent_doer, string sub_doer_key, string id = null,
      Func<T, bool> filter_func = null) where T : Doer
    {
      var sub_doers = GetSubDoers(parent_doer, sub_doer_key, id, filter_func);
      int count = 0;
      foreach (var sub_doer in sub_doers)
      {
        count = count + sub_doer.GetCount();
      }

      return count;
    }

    public static string[] GetSubDoerIds(Doer parent_doer, string sub_doer_key)
    {
      var dict = GetSubDoerDict_ToEdit(parent_doer, sub_doer_key);
      List<string> result = new List<string>();
      foreach (string id in dict.Keys)
        result.Add(id);
      return result.ToArray();
    }

    public static void AddSubDoers(Doer parent_doer, string sub_doer_key, Doer add_subDoer)
    {
      add_subDoer.SetOwner(parent_doer);
      string id = add_subDoer.GetId();
      bool can_fold = add_subDoer.IsHasMethod("CanFold") && add_subDoer.InvokeMethod<bool>("CanFold");
      var sub_doers = GetSubDoers_ToEdit(parent_doer, sub_doer_key, id);
      if (can_fold)
      {
        if (sub_doers.IsNullOrEmpty())
          sub_doers.Add(add_subDoer);
        else
        {
          (sub_doers[0] as Doer).AddCount(add_subDoer.GetCount());
          add_subDoer.SetEnv(null);
          add_subDoer.Destruct();
        }
      }
      else
        sub_doers.Add(add_subDoer);
    }

    public static T[] RemoveSubDoers<T>(Doer parent_doer, string sub_doer_key, string id, int count,
      DoerFactory sub_doer_factory) where T : Doer
    {
      var sub_doers = GetSubDoers_ToEdit(parent_doer, sub_doer_key, id);
      int current_count = 0;
      List<T> result = new List<T>();
      if (sub_doers.IsNullOrEmpty())
        return result.ToArray();
      if (count == Int32.MaxValue) //全部删除
      {
        for (int i = sub_doers.Count - 1; i >= 0; i--)
        {
          var sub_doer = sub_doers[i] as T;
          sub_doers.RemoveAt(i);
          sub_doer.SetEnv(null);
          result.Add(sub_doer);
        }

        result.Reverse();
        return result.ToArray();
      }

      bool can_fold = (sub_doers[0] as T).IsHasMethod("CanFold") && (sub_doers[0] as T).InvokeMethod<bool>("CanFold");
      for (int i = sub_doers.Count - 1; i >= 0; i--)
      {
        var sub_doer = sub_doers[i] as T;
        if (!can_fold) //不可折叠的
        {
          sub_doers.RemoveAt(i);
          sub_doer.SetEnv(null);
          current_count = current_count + 1;
          result.Add(sub_doer);
          if (current_count == count)
            return result.ToArray();
        }
        else //可折叠的
        {
          int sub_doer_count = sub_doer.GetCount();
          if (sub_doer_count > count) //有多
          {
            sub_doer.AddCount(-count);
            T clone_sub_doer = sub_doer_factory.NewDoer(sub_doer.GetId()) as T;
            clone_sub_doer.SetCount(count);
            result.Add(clone_sub_doer);
          }
          else //不够或者相等
          {
            sub_doers.RemoveAt(i);
            sub_doer.SetEnv(null);
            result.Add(sub_doer);
          }

          return result.ToArray();
        }
      }

      return result.ToArray();
    }

    public static bool CanRemoveSubDoers(Doer parent_doer, string sub_doer_key, string id, int count)
    {
      int current_count = GetSubDoerCount<Doer>(parent_doer, sub_doer_key, id, null);
      if (current_count >= count)
        return true;
      else
        return false;
    }

    public static T RemoveSubDoer<T>(Doer parent_doer, string sub_doer_key, T sub_doer) where T : Doer
    {
      var id = sub_doer.GetId();
      var sub_doers = GetSubDoers_ToEdit(parent_doer, sub_doer_key, id);
      for (int i = sub_doers.Count - 1; i >= 0; i--)
      {
        var _sub_doer = sub_doers[i] as T;
        if (_sub_doer == sub_doer)
        {
          _sub_doer.SetEnv(null);
          sub_doers.RemoveAt(i);
          return _sub_doer;
        }
      }

      return null;
    }

    public static T RemoveSubDoer<T>(Doer parent_doer, string sub_doer_key, string rid) where T : Doer
    {
      var id = IdUtil.RidToId(rid);
      var sub_doers = GetSubDoers_ToEdit(parent_doer, sub_doer_key, id);
      for (int i = sub_doers.Count - 1; i >= 0; i--)
      {
        var _sub_doer = sub_doers[i] as T;
        if (_sub_doer.GetRid().Equals(rid))
        {
          _sub_doer.SetEnv(null);
          sub_doers.RemoveAt(i);
          return _sub_doer;
        }
      }

      return null;
    }

    public static void ClearSubDoers<T>(Doer parent_doer, string sub_doer_key, Action<T> clear_sub_doer_func = null)
      where T : Doer
    {
      var dict = GetSubDoerDict_ToEdit(parent_doer, sub_doer_key);
      foreach (ArrayList sub_doer_list in dict.Values)
      {
        for (int i = sub_doer_list.Count - 1; i >= 0; i--)
        {
          var sub_doer = sub_doer_list[i] as T;
          clear_sub_doer_func?.Invoke(sub_doer);
          sub_doer.SetEnv(null);
          sub_doer.Destruct();
        }
      }

      dict.Clear();
    }
  }
}