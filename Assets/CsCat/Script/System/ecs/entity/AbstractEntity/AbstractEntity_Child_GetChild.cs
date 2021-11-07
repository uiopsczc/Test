using System;
using System.Collections.Generic;

namespace CsCat
{
    public partial class AbstractEntity
    {
        public AbstractEntity GetChild(string childKey)
        {
            if (!keyToChildDict.ContainsKey(childKey))
                return null;
            if (keyToChildDict[childKey].IsDestroyed())
                return null;
            return keyToChildDict[childKey];
        }

        public T GetChild<T>(string childKey) where T : AbstractEntity
        {
            return (T) GetChild(childKey);
        }

        public AbstractEntity GetChild(Type childType)
        {
            foreach (var child in ForeachChild(childType))
                return child;
            return null;
        }

        public T GetChild<T>() where T : AbstractEntity
        {
            return GetChild(typeof(T)) as T;
        }

        //效率问题引入的
        public AbstractEntity GetChildStrictly(Type child_type)
        {
            if (!this.typeToChildListDict.ContainsKey(child_type))
                return null;
            var childList = typeToChildListDict[child_type];
            for (var i = 0; i < childList.Count; i++)
            {
                var child = childList[i];
                if (!child.IsDestroyed())
                    return child;
            }

            return null;
        }

        public T GetChildStrictly<T>() where T : AbstractEntity
        {
            return GetChildStrictly(typeof(T)) as T;
        }


        public AbstractEntity[] GetChildren(Type childType)
        {
            List<AbstractEntity> list = PoolCatManagerUtil.Spawn<List<AbstractEntity>>();
            try
            {
                foreach (var child in ForeachChild(childType))
                    list.Add(child);
                return list.ToArray();
            }
            finally
            {
                list.Clear();
                PoolCatManagerUtil.Despawn(list);
            }
        }

        public T[] GetChildren<T>() where T : AbstractEntity
        {
            return (T[]) GetChildren(typeof(T));
        }


        public AbstractEntity[] GetChildrenStrictly(Type childType)
        {
            List<AbstractEntity> list = new List<AbstractEntity>();
            try
            {
                if (!this.typeToChildListDict.ContainsKey(childType))
                    return list.ToArray();
                var childList = typeToChildListDict[childType];
                for (var i = 0; i < childList.Count; i++)
                {
                    var child = childList[i];
                    if (!child.IsDestroyed())
                        list.Add(child);
                }

                return list.ToArray();
            }
            finally
            {
                list.Clear();
                PoolCatManagerUtil.Despawn(list);
            }
        }

        public T[] GetChildrenStrictly<T>() where T : AbstractEntity
        {
            return (T[]) GetChildrenStrictly(typeof(T));
        }
    }
}