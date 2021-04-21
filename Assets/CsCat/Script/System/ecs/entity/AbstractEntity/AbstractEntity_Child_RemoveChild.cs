using System;
using System.Collections.Generic;

namespace CsCat
{
  public partial class AbstractEntity
  {
    public AbstractEntity RemoveChild(AbstractEntity child)
    {
      if (child.IsDestroyed())
        return null;

      child.Destroy();
      if (!this.is_not_delete_child_relationship_immediately)
      {
        __RemoveChildRelationship(child);
        __DespawnChildKey(child);
        child.Despawn();
      }
      else
        __MarkHasDestroyedChild();

      return child;
    }

    public AbstractEntity RemoveChild(string child_key)
    {
      if (!key_to_child_dict.ContainsKey(child_key))
        return null;
      var child = key_to_child_dict[child_key];
      return RemoveChild(child);
    }



    public AbstractEntity RemoveChild(Type child_type)
    {
      var child = this.GetChild(child_type);
      if (child != null)
        this.RemoveChild(child);
      return child;
    }

    public T RemoveChild<T>() where T : AbstractEntity
    {
      return RemoveChild(typeof(T)) as T;
    }

    public AbstractEntity RemoveChildStrictly(Type child_type)
    {
      var child = this.GetChildStrictly(child_type);
      if (child != null)
        RemoveChild(child);
      return child;
    }

    public T RemoveChildStrictly<T>() where T : AbstractEntity
    {
      return (T)RemoveChildStrictly(typeof(T));
    }

    public AbstractEntity[] RemoveChildren(Type child_type)
    {
      var children = this.GetChildren(child_type);
      if (!children.IsNullOrEmpty())
      {
        foreach (var child in children)
          this.RemoveChild(child);
      }
      return children;
    }

    public T[] RemoveChildren<T>() where T : AbstractEntity
    {
      return (T[])RemoveChildren(typeof(T));
    }


    public AbstractEntity[] RemoveChildrenStrictly(Type child_type)
    {
      var children = this.GetChildrenStrictly(child_type);
      if (!children.IsNullOrEmpty())
      {
        foreach (var child in children)
          this.RemoveChild(child);
      }
      return children;
    }

    public T[] RemoveChildrenStrictly<T>() where T : AbstractEntity
    {
      return (T[])RemoveChildrenStrictly(typeof(T));
    }


    public void RemoveAllChildren()
    {
      var to_remove_child_key_list = PoolCatManagerUtil.Spawn<List<string>>();
      to_remove_child_key_list.Capacity = this.child_key_list.Count;
      to_remove_child_key_list.AddRange(this.child_key_list);
      foreach (var child_key in to_remove_child_key_list)
        RemoveChild(child_key);
      to_remove_child_key_list.Clear();
      PoolCatManagerUtil.Despawn(to_remove_child_key_list);
    }

    ////////////////////////////////////////////////////////////////////
    private void __MarkHasDestroyedChild()
    {
      if (!this.is_has_destroyed_child)
      {
        this.is_has_destroyed_child = true;
        _parent?.__MarkHasDestroyedChild();
      }
    }

    private void __RemoveChildRelationship(AbstractEntity child)
    {
      this.key_to_child_dict.Remove(child.key);
      this.child_key_list.Remove(child.key);
      this.type_to_childList_dict[child.GetType()].Remove(child);
    }

    private void __DespawnChildKey(AbstractEntity child)
    {
      if (child.is_key_using_parent_idPool)
      {
        child_key_idPool.Despawn(child.key);
        child.is_key_using_parent_idPool = false;
      }
    }

    public void CheckDestroyed()
    {
      //有【子孙】child中有要从child_key_list和children_dict中删除关联关系
      //或者有【子孙】child的component要从从component_list和component_dict中删除关联关系
      if (is_has_destroyed_child || is_has_destroyed_child_component)
      {
        string child_key;
        AbstractEntity child;
        for (int i = child_key_list.Count - 1; i >= 0; i--)
        {
          child_key = child_key_list[i];
          child = key_to_child_dict[child_key];
          child.CheckDestroyed();
          if (child.IsDestroyed()) //该child自身要被delete
          {
            __RemoveChildRelationship(child);
            __DespawnChildKey(child);
            child.Despawn();
          }
        }

        is_has_destroyed_child = false;
        is_has_destroyed_child_component = false;
      }

      if (this.is_has_destroyed_component)
      {
        __CheckDestroyedComponents();
        is_has_destroyed_component = false;
      }
    }
  }
}