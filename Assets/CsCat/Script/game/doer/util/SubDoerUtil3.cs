using System;
using System.Collections;
using System.Collections.Generic;

namespace CsCat
{
  //里面的结构是  Dict<rid,Doer>
  public class SubDoerUtil3
  {
    public static void DoReleaseSubDoer<T>(Doer parent_doer, string sub_doer_key, Action<T> relase_sub_doer_func = null)
      where T : Doer
    {
      //销毁
      var sub_doers = GetSubDoers<T>(parent_doer, sub_doer_key, null, null);
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
        foreach (var sub_doer in dict.Values)
        {
          if (filter_func == null || filter_func(sub_doer as T))
            result.Add(sub_doer as T);
        }

        return result.ToArray();
      }

      foreach (T sub_doer in dict.Values)
      {
        if (sub_doer.GetId().Equals(id))
        {
          if (filter_func == null || filter_func(sub_doer))
            result.Add(sub_doer);
        }
      }

      return result.ToArray();
    }

    public static Hashtable GetSubDoerDict_ToEdit(Doer parent_doer, string sub_doer_key) //可以直接插入删除
    {
      return parent_doer.GetOrAddTmp(sub_doer_key, () => new Hashtable());
    }


    public static bool HasSubDoers<T>(Doer parent_doer, string sub_doer_key, string id = null,
      Func<T, bool> filter_func = null) where T : Doer
    {
      return !GetSubDoers<T>(parent_doer, sub_doer_key, id, filter_func).IsNullOrEmpty();
    }

    //获取doer中的sub_doer_key的子doer数量
    public static int GetSubDoersCount<T>(Doer parent_doer, string sub_doer_key, string id = null,
      Func<T, bool> filter_func = null) where T : Doer
    {
      return GetSubDoers<T>(parent_doer, sub_doer_key, id, filter_func).Length;
    }

    public static T GetSubDoer<T>(Doer parent_doer, string sub_doer_key, string id_or_rid) where T : Doer
    {
      if (IdUtil.IsId(id_or_rid)) //id的情况
      {
        string id = id_or_rid;
        foreach (var sub_doer in GetSubDoers<T>(parent_doer, sub_doer_key, null, null))
        {
          if (sub_doer.GetId().Equals(id))
            return sub_doer;
        }

        return null;
      }
      else //rid的情况
      {
        string rid = id_or_rid;
        if (GetSubDoerDict_ToEdit(parent_doer, sub_doer_key).ContainsKey(rid))
          return GetSubDoerDict_ToEdit(parent_doer, sub_doer_key)[rid] as T;
        else
          return null;
      }
    }

    public static void ClearSubDoers<T>(Doer parent_doer, string sub_doer_key, Action<T> clear_sub_doer_func = null)
      where T : Doer
    {
      var list = GetSubDoers<T>(parent_doer, sub_doer_key, null, null);
      for (int i = list.Length - 1; i >= 0; i--)
      {
        var sub_doer = list[i];
        clear_sub_doer_func?.Invoke(sub_doer);
        sub_doer.SetEnv(null);
        sub_doer.Destruct();
      }

      GetSubDoerDict_ToEdit(parent_doer, sub_doer_key).Clear();
    }

  }
}