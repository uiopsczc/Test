using System;

namespace CsCat
{
    public class PoolScope<T> : IDisposable
    {
        public T spawn;

        public PoolScope(string poolName = null, Action<T> onSpawnCallback = null)
        {
            spawn = PoolCatManagerUtil.Spawn(poolName, onSpawnCallback);
        }
        public virtual void Dispose()
        {
            PoolCatManagerUtil.Despawn(spawn);
            spawn = default;
        }
    }
}