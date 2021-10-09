using System;

namespace CsCat
{
    public class PoolScope : IDisposable
    {
        public PoolScope()
        {
        }
        public virtual void Dispose()
        {
            PoolCatManagerUtil.Despawn(this);
        }
    }
}