

// 逻辑对象

using System;
using System.Collections;
using UnityEngine;

namespace CsCat
{
  public class Doer : TickObject
  {
    private DBase dbase;
    public DoerFactory factory;

    public override void PostInit()
    {
      this.OnInit();
      base.PostInit();
    }


    ////////////////////////////DoXXX//////////////////////////////
    public virtual void Destruct()
    {
      if (this.factory != null)
        this.factory.ReleaseDoer(this);
    }

    public virtual void PrepareSave(Hashtable dict, Hashtable dict_tmp)
    {
      this.DoSave(dict, dict_tmp);
    }

    public virtual void FinishRestore(Hashtable dict, Hashtable dict_tmp)
    {
      this.DoRestore(dict, dict_tmp);
    }

    public virtual bool CheckUpgrade(string key)
    {
      return OnCheckUpgrade(key);
    }

    public virtual void Upgrade(Critter critter)
    {
    }

    //////////////////////////////OnXXX////////////////////////
    public virtual void OnInit()
    {
    }

    public void OnRelease()
    {
    }

    //存储数据事件
    public virtual void OnSave(Hashtable dict, Hashtable dict_tmp)
    {
    }

    //导出数据事件
    public virtual void OnRestore(Hashtable dict, Hashtable dict_tmp)
    {
    }

    //修改属性事件
    public void OnAttrChange(string key)
    {
    }

    //重新载入定义数据事件
    public void OnReloadCfg()
    {
    }

    public virtual bool OnCheckUpgrade(string key)
    {
      return true;
    }

    public virtual void OnUpgrade(string key)
    {
    }

    //////////////////////////////DoXXX////////////////////////
    public virtual void DoSave(Hashtable dict, Hashtable dict_tmp)
    {
      this.GetSaveData(dict);
      this.GetSaveTmpData(dict_tmp);
      this.OnSave(dict, dict_tmp);
    }

    public virtual void DoRestore(Hashtable dict, Hashtable dict_tmp)
    {
      this.OnRestore(dict, dict_tmp);
      if (dict_tmp != null)
        this.AddTmpAll(dict_tmp);
      if (dict != null)
        this.AddAll(dict);
    }

    public void NotifyAttrChange(string key)
    {
      this.OnAttrChange(key);
    }

    public virtual void DoRelease()
    {
      this.OnRelease();
    }


    //////////////////////////////GetXXX////////////////////////
    public string GetId()
    {
      return this.dbase.GetId();
    }

    public string GetRid()
    {
      return this.dbase.GetRid();
    }

    public string GetRidSeq()
    {
      return this.dbase.GetRidSeq();
    }

    public DBase GetDBase()
    {
      return this.dbase;
    }

    public T GetDBase<T>() where T : DBase
    {
      return GetDBase() as T;
    }


    public string GetShort()
    {
      return string.Format("{0}", this.GetRid());
    }

    public override string ToString()
    {
      return this.GetShort();
    }

    // 获得需存储数据
    public void GetSaveData(Hashtable dict)
    {
      if (dict == null)
        return;
      this.GetAll(dict);
    }

    // 拷贝数据到指定dict
    public void GetAll(Hashtable dict)
    {
      dict.Combine(this.dbase.db);
    }

    // 获得需存储运行时数据
    public void GetSaveTmpData(Hashtable dict)
    {
      if (dict == null)
        return;
      this.GetTmpAll(dict);
      dict.RemoveByFunc((key, value) =>
      {
        if (key.ToString().StartsWith("o_"))
          return true;
        else
          return false;
      });
    }

    public T Get<T>(string key, T default_value = default(T))
    {
      if (!this.dbase.db.ContainsKey(key))
        return default_value;
      return this.dbase.db[key].To<T>();
    }

    // 拷贝运行时数据到指定dict
    public void GetTmpAll(Hashtable dict)
    {
      dict.Combine(this.dbase.db_tmp);
    }

    public T GetTmp<T>(string key, T default_value = default(T))
    {
      if (!this.dbase.db_tmp.ContainsKey(key))
        return default_value;
      return this.dbase.db_tmp[key].To<T>();
    }
    

    

    public int GetCount()
    {
      return this.Get<int>("count");
    }

    

    // 获得物件所在环境
    public Doer GetEnv()
    {
      return GetTmp<Doer>("o_env", null);
    }

    public T GetEnv<T>() where T : class
    {
      return GetEnv() as T;
    }

    // 拥有者，如发放任务的npc
    public Doer GetOwner()
    {
      return GetTmp<Doer>("o_owner");
    }

    public Vector2 GetPos2()
    {
      var pos = Get<Hashtable>("pos2");
      if (pos == null)
        return Vector2Const.Default;
      return new Vector2(pos.Get<float>("x"), pos.Get<float>("y"));
    }

    public Vector3 GetPos3()
    {
      var pos = Get<Hashtable>("pos3");
      if (pos == null)
        return Vector3Const.Default;
      return new Vector3(pos.Get<float>("x"), pos.Get<float>("y"), pos.Get<float>("z"));
    }

    public string GetBelong()
    {
      return GetTmp("belong", "");
    }
    //////////////////////////////SetXXXX////////////////////////

    public void SetDBase(DBase dbase)
    {
      this.dbase = dbase;
    }

    public void Set(string key, object value)
    {
      this.dbase.db[key] = value;
    }

    public void SetTmp(string key, object value)
    {
      this.dbase.db_tmp[key] = value;
    }

    public void SetCount(int value)
    {
      Set("count", value);
    }

    public void SetEnv(object env)
    {
      SetTmp("o_env", env);
    }

    //拥有者，如发放任务的npc
    public void SetOwner(Doer owner)
    {
      SetTmp("o_owner", owner);
    }

    public void SetPos2(float x, float y)
    {
      var pos = new Hashtable();
      pos["x"] = x;
      pos["y"] = y;
      Set("pos2", pos);
    }

    public void SetPos3(float x, float y, float z)
    {
      var pos = new Hashtable();
      pos["x"] = x;
      pos["y"] = y;
      pos["z"] = z;
      Set("pos3", pos);
    }

    public void SetBelong(string belong)
    {
      SetTmp("belong", belong);
    }
    //////////////////////////////AddXXXX////////////////////////

    public void AddAll(Hashtable dict)
    {
      this.dbase.db.Combine(dict);
    }

    public void Add(string key, float add_value)
    {
      if (!this.dbase.db.ContainsKey(key))
        this.dbase.db[key] = 0;
      this.dbase.db[key] = this.dbase.db[key].To<float>() + add_value;
    }

    public void Add(string key, int add_value)
    {
      if (!this.dbase.db.ContainsKey(key))
        this.dbase.db[key] = 0;
      this.dbase.db[key] = this.dbase.db[key].To<int>() + add_value;
    }

    public void Add(string key, string add_value)
    {
      if (!this.dbase.db.ContainsKey(key))
        this.dbase.db[key] = "";
      this.dbase.db[key] = this.dbase.db[key].To<string>() + add_value;
    }

    public void AddCount(int add_value)
    {
      this.Add("count", add_value);
    }

    public void AddTmp(string key, float add_value)
    {
      if (!this.dbase.db_tmp.ContainsKey(key))
        this.dbase.db_tmp[key] = 0;
      this.dbase.db_tmp[key] = this.dbase.db_tmp[key].To<float>() + add_value;
    }

    public void AddTmp(string key, string add_value)
    {
      if (!this.dbase.db_tmp.ContainsKey(key))
        this.dbase.db_tmp[key] = "";
      this.dbase.db_tmp[key] = this.dbase.db_tmp[key].To<string>() + add_value;
    }

    public void AddTmpAll(Hashtable dict)
    {
      this.dbase.db_tmp.Combine(dict);
    }

    /////////////////////////////Remove/////////////////////////////
    public T Remove<T>(string key)
    {
      T result = default(T);
      if (dbase.db.ContainsKey(key))
      {
        result = (T)dbase.db[key];
        dbase.db.Remove(key);
      }

      return result;
    }

    public T RemoveTmp<T>(string key)
    {
      T result = default(T);
      if (dbase.db_tmp.ContainsKey(key))
      {
        result = (T)dbase.db_tmp[key];
        dbase.db_tmp.Remove(key);
      }

      return result;
    }

    /////////////////////////////GetOrAddXXXX/////////////////////////////
    public T GetOrAddTmp<T>(string key, Func<T> add_func)
    {
      return this.dbase.db_tmp.GetOrAddDefault(key, add_func);
    }

    public T GetOrAdd<T>(string key, Func<T> add_func)
    {
      return this.dbase.db.GetOrAddDefault(key, add_func);
    }
  }
}
