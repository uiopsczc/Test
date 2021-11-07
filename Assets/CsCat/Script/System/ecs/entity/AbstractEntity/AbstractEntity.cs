using System;
using System.Collections.Generic;

namespace CsCat
{
    public partial class AbstractEntity : IDespawn
    {
        public string key;
        protected bool isHasDestroyedChild; //是否【子孙】child中有要从child_key_list和children_dict中删除关联关系
        protected bool isHasDestroyedChildComponent; //是否【子孙】child中有要从component_list和component_dict中删除关联关系
        protected bool isHasDestroyedComponent; //是否有compoent是要从component_list和component_dict中删除关联关系
        public Cache cache = new Cache();

        protected virtual bool isNotDeleteChildRelationshipImmediately
            //是否不立刻将child从child_key_list和children_dict中删除关联关系
            => false;

        protected virtual bool isNotDeleteComponentRelationShipImmediately
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

        void _OnDespawn_()
        {
            isHasDestroyedChild = false;
            isHasDestroyedChildComponent = false;
            isHasDestroyedComponent = false;
            cache.Clear();
        }
    }
}