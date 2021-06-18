

// 逻辑对象

using System;
using System.Collections.Generic;

namespace CsCat
{
  public class DoerFactory : TickObject
  {
    private Dictionary<string, Dictionary<string, DBase>> id_dict = new Dictionary<string, Dictionary<string, DBase>>();

    protected virtual string default_doer_class_path => null;

    protected virtual string GetClassPath(string id)
    {
      return default_doer_class_path;
    }

    protected virtual DBase __NewDBase(string id_or_rid)
    {
      return new DBase(id_or_rid);
    }

    protected virtual Doer __NewDoer(string id)
    {
      string class_path = GetClassPath(id);
      Type type = TypeUtil.GetType(class_path);
      var doer = this.AddChildWithoutInit(null, type) as Doer;
      return doer;
    }

    // 获得所有逻辑对象数量
    public int GetDoerCount(string id = null)
    {
      int count = 0;
      if (id == null)
      {
        foreach (var value in this.id_dict.Values)
        {
          count = count + value.Count;
        }
      }
      else
      {
        if (this.id_dict.ContainsKey(id))
          count = this.id_dict[id].Count;
      }

      return count;
    }

    //获得所有逻辑对象
    public List<Doer> GetAllDoers(string id = null)
    {
      List<Doer> result = new List<Doer>();
      if (id == null)
      {
        foreach (var value in this.id_dict.Values)
        {
          foreach (var dbase in value.Values)
          {
            result.Add(dbase.GetDoer());
          }
        }
      }
      else
      {
        if (this.id_dict.ContainsKey(id))
        {
          foreach (var dbase in this.id_dict[id].Values)
          {
            result.Add(dbase.GetDoer());
          }
        }
      }

      return result;
    }

    public Doer FindDoers(string id_or_rid)
    {
      string id = IdUtil.RidToId(id_or_rid);
      bool is_id = IdUtil.IsId(id_or_rid);
      if (id_dict.ContainsKey(id) && id_dict[id].IsNullOrEmpty())
      {
        foreach (var rid in this.id_dict[id].Keys)
        {
          var dbase = id_dict[id][rid];
          if (is_id)
            return dbase.GetDoer();
          else
          {
            if (rid.Equals(id_or_rid))
              return dbase.GetDoer();
          }
        }
      }

      return null;
    }

    // 创建逻辑对象
    public Doer NewDoer(string id_or_rid)
    {
      string id = IdUtil.RidToId(id_or_rid);
      var doer = this.__NewDoer(id);
      var dbase = this.__NewDBase(id_or_rid);
      doer.SetDBase(dbase);
      dbase.SetDoer(doer);
      doer.factory = this;
      string rid = dbase.GetRid();
      var dbase_dict = this.id_dict.GetOrAddDefault(id, () => new Dictionary<string, DBase>());
      dbase_dict[rid] = dbase;
      doer.Init();
      doer.PostInit();
      doer.SetIsEnabled(true, false);
      return doer;
    }

    // 释放逻辑对象
    public void ReleaseDoer(Doer doer)
    {
      string id = doer.GetId();
      string rid = doer.GetRid();
      if (this.id_dict.ContainsKey(id))
        this.id_dict[id].Remove(rid);
      doer.DoRelease();
      this.RemoveChild(doer.key);
    }
  }
}