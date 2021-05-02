using System;
using System.Collections.Generic;

namespace CsCat
{
  public partial class AbstractEntity
  {
    public AbstractEntity AddChildWithoutInit(string child_key, Type child_type)
    {
      if (child_key != null && key_to_child_dict.ContainsKey(child_key))
      {
        LogCat.error("duplicate add child:{0},{1}", child_key, child_type);
        return null;
      }

      bool is_key_using_parent_idPool = child_key == null;
      if (is_key_using_parent_idPool)
      {
        child_key = child_key_idPool.Get().ToString();
        //再次检查键值
        if (key_to_child_dict.ContainsKey(child_key))
        {
          LogCat.error("duplicate add child:{0},{1}", child_key, child_type);
          return null;
        }
      }

      var child = PoolCatManagerUtil.Spawn(child_type) as AbstractEntity;
      child.key = child_key;
      child.is_key_using_parent_idPool = is_key_using_parent_idPool;
      return AddChild(child);
    }

    public T AddChildWithoutInit<T>(string child_key) where T : AbstractEntity
    {
      return AddChildWithoutInit(child_key, typeof(T)) as T;
    }

    public AbstractEntity AddChild(AbstractEntity child)
    {
      if (key_to_child_dict.ContainsKey(child.key))
      {
        LogCat.error("duplicate add child:{0}", child.key, child.GetType());
        return null;
      }

      child._parent = this;
      __AddChildRelationship(child);
      return child;
    }

    public virtual AbstractEntity AddChild(string child_key, Type child_type, params object[] init_args)
    {
      var child = AddChildWithoutInit(child_key, child_type);
      if (child == null) //没有加成功
        return null;
      child.InvokeMethod("Init", false, init_args);
      child.PostInit();
      child.SetIsEnabled(true, false);
      return child;
    }

    public T AddChild<T>(string child_key, params object[] init_args) where T : AbstractEntity
    {
      return AddChild(child_key, typeof(T), init_args) as T;
    }

    

    void __AddChildRelationship(AbstractEntity child)
    {
      key_to_child_dict[child.key] = child;
      type_to_childList_dict.GetOrAddDefault(child.GetType(), () => PoolCatManagerUtil.Spawn<List<AbstractEntity>>()).Add(child);
      child_key_list.Add(child.key);
      if (!child_type_list.Contains(child.GetType()))
        child_type_list.Add(child.GetType());
    }

    
  }
}