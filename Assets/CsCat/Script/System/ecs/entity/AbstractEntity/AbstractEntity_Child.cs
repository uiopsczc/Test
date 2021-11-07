using System;
using System.Collections.Generic;

namespace CsCat
{
    public partial class AbstractEntity
    {
        protected Dictionary<string, AbstractEntity> keyToChildDict = new Dictionary<string, AbstractEntity>();

        protected Dictionary<Type, List<AbstractEntity>> typeToChildListDict =
            new Dictionary<Type, List<AbstractEntity>>(); //准确的类型


        protected List<string> childKeyList = new List<string>();
        protected List<Type> childTypeList = new List<Type>();
        protected AbstractEntity _parent;

        protected IdPool childKeyIdPool = new IdPool();
        protected bool isKeyUsingParentIdPool;

        public IEnumerable<AbstractEntity> ForeachChild()
        {
            AbstractEntity child = null;
            for (int i = 0; i < childKeyList.Count; i++)
            {
                child = GetChild(childKeyList[i]);
                if (child != null)
                    yield return child;
            }
        }

        public IEnumerable<AbstractEntity> ForeachChild(Type child_type)
        {
            AbstractEntity child = null;
            for (int i = 0; i < childKeyList.Count; i++)
            {
                child = GetChild(childKeyList[i]);
                if (child != null && child_type.IsInstanceOfType(child))
                    yield return child;
            }
        }

        public IEnumerable<T> ForeachChild<T>() where T : AbstractEntity
        {
            T child = null;
            for (int i = 0; i < childKeyList.Count; i++)
            {
                child = GetChild(childKeyList[i]) as T;
                if (child != null)
                    yield return child;
            }
        }


        void _OnDespawn_Child()
        {
            keyToChildDict.Clear();
            foreach (var childList in typeToChildListDict.Values)
            {
                childList.Clear();
                PoolCatManagerUtil.Despawn(childList);
            }

            typeToChildListDict.Clear();
            childKeyList.Clear();
            childTypeList.Clear();
            _parent = null;
            isKeyUsingParentIdPool = false;
        }
    }
}