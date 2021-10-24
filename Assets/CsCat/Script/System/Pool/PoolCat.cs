using System;
using System.Collections.Generic;

namespace CsCat
{
    public class PoolCat
    {
        /// <summary>
        /// 存放object的数组
        /// </summary>
        protected Stack<object> despawnedObjectStack = new Stack<object>();

        protected List<object> spawnedObjectList = new List<object>();
        private Type spawnType;
        public string poolName;
        private Func<object> spawnFunc;


        public PoolCat(string poolName, Type spawnType)
        {
            this.poolName = poolName;
            this.spawnType = spawnType;
        }

        public PoolCat(string poolName, Func<object> spawnFunc)
        {
            this.poolName = poolName;
            this.spawnFunc = spawnFunc;
        }

        public void InitPool(int initCount = 1, Action<object> onSpawnCallback = null)
        {
            for (int i = 0; i < initCount; i++)
                Despawn(Spawn(onSpawnCallback));
        }


        #region virtual method

        /// <summary>
        /// 子类中重写spawn中需要用到的newObject
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        protected virtual object _Spawn()
        {
            return spawnFunc != null ? spawnFunc() : Activator.CreateInstance(spawnType);
        }

        #endregion

        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public virtual object Spawn(Action<object> on_spawn_callback = null)
        {
            object spawn = null;
            spawn = despawnedObjectStack.Count > 0 ? despawnedObjectStack.Pop() : _Spawn();
            on_spawn_callback?.Invoke(spawn);
            spawnedObjectList.Add(spawn);
            return spawn;
        }

        public T Spawn<T>(Action<object> on_spawn_callback = null)
        {
            return (T) Spawn(on_spawn_callback);
        }

        public virtual void Despawn(object obj)
        {
            if (obj == null)
                return;
            if (!spawnedObjectList.Contains(obj))
            {
//        LogCat.error(string.Format("pool: {0} not contained::{1}",pool_name, obj));
                return;
            }

            despawnedObjectStack.Push(obj);

            spawnedObjectList.Remove(obj);
            (obj as IDespawn)?.OnDespawn();
        }

        public virtual void Trim()
        {
            foreach (var despawnedObject in despawnedObjectStack)
                _Trim(despawnedObject);
            despawnedObjectStack.Clear();
        }

        protected virtual void _Trim(object despawnedObject)
        {
        }

        public void DespawnAll()
        {
            for (int i = spawnedObjectList.Count - 1; i >= 0; i--)
                Despawn(spawnedObjectList[i]);
        }

        public bool IsEmpty()
        {
            if (this.spawnedObjectList.Count == 0 && despawnedObjectStack.Count == 0)
                return true;
            return false;
        }


        public int GetSpawnedCount()
        {
            return this.spawnedObjectList.Count;
        }

        public int GetDespawnedCount()
        {
            return this.despawnedObjectStack.Count;
        }

        public virtual void Destroy()
        {
            DespawnAll();
            Trim();

            spawnType = null;
            poolName = null;
            spawnFunc = null;

            despawnedObjectStack.Clear();
            spawnedObjectList.Clear();
        }
    }
}