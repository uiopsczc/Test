using System;
using System.Collections.Generic;

namespace CsCat
{
  public partial class AbstractEntity : IDespawn
  {
    public string key;
    protected bool is_has_destroyed_child; //是否【子孙】child中有要从child_key_list和children_dict中删除关联关系
    protected bool is_has_destroyed_child_component; //是否【子孙】child中有要从component_list和component_dict中删除关联关系
    protected bool is_has_destroyed_component; //是否有compoent是要从component_list和component_dict中删除关联关系
    public Cache cache = new Cache();

    protected virtual bool is_not_delete_child_relationship_immediately
      //是否不立刻将child从child_key_list和children_dict中删除关联关系
      => false;

    protected virtual bool is_not_delete_component_relationShip_immediately
      //是否不立刻将component从component_list和component_dict中删除关联关系
      => false;

    public AbstractEntity()
    {
    }


    public virtual void Init()
    {
    }

    public virtual void PostInit()
    {
    }

    public virtual void Start()
    {
    }

    public virtual void Refresh()
    {
    }


    public T GetParent<T>() where T : AbstractEntity
    {
      return (T) _parent;
    }

    void __OnDespawn_()
    {
      is_has_destroyed_child = false;
      is_has_destroyed_child_component = false;
      is_has_destroyed_component = false;
      cache.Clear();
    }
  }
}