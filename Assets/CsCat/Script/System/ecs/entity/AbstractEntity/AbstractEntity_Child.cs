using System;
using System.Collections.Generic;

namespace CsCat
{
  public partial class AbstractEntity
  {
    protected Dictionary<string, AbstractEntity> key_to_child_dict = new Dictionary<string, AbstractEntity>();

    protected Dictionary<Type, List<AbstractEntity>> type_to_childList_dict =
      new Dictionary<Type, List<AbstractEntity>>(); //准确的类型

    protected List<string> child_key_list = new List<string>();
    protected List<Type> child_type_list = new List<Type>();
    protected AbstractEntity _parent;

    protected IdPool child_key_idPool = new IdPool();
    protected bool is_key_using_parent_idPool;

    public IEnumerable<AbstractEntity> ForeachChild()
    {
      AbstractEntity child = null;
      for (int i = 0; i < child_key_list.Count; i++)
      {
        child = GetChild(child_key_list[i]);
        if (child != null)
          yield return child;
      }
    }

    public IEnumerable<AbstractEntity> ForeachChild(Type child_type)
    {
      AbstractEntity child = null;
      for (int i = 0; i < child_type_list.Count; i++)
      {
        var _child_type = child_type_list[i];
        if (child_type.IsAssignableFrom(_child_type))
        {
          var childList = type_to_childList_dict[_child_type];
          for (int m = 0; m < childList.Count; m++)
          {
            child = childList[m];
            if (!child.IsDestroyed())
              yield return child;
          }
        }
      }
    }

    public IEnumerable<T> ForeachChild<T>() where T : AbstractEntity
    {
      T child = null;
      for (int i = 0; i < child_type_list.Count; i++)
      {
        var _child_type = child_type_list[i];
        if (typeof(T).IsAssignableFrom(_child_type))
        {
          var childList = type_to_childList_dict[_child_type];
          for (int m = 0; m < childList.Count; m++)
          {
            child = (T)childList[m];
            if (!child.IsDestroyed())
              yield return child;
          }
        }
      }
    }
    
    


    void __OnDespawn_Child()
    {
      key_to_child_dict.Clear();
      foreach (var childList in type_to_childList_dict.Values)
      {
        childList.Clear();
        PoolCatManagerUtil.Despawn(childList);
      }
      type_to_childList_dict.Clear();
      child_key_list.Clear();
      child_type_list.Clear();
      _parent = null;
      is_key_using_parent_idPool = false;
    }
  }
}