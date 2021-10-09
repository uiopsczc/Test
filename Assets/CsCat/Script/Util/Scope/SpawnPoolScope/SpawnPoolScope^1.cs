using System;

namespace CsCat
{
    public class SpawnPoolScope<T> : PoolScope
    {
        private T _spawn;
        private Action<T> _onSpawnCallback;
        public T spawn
        {
            get
            {
                if (_spawn == null)
                    _spawn = PoolCatManagerUtil.Spawn(null, _onSpawnCallback);
                return _spawn;
            }
        }


        public SpawnPoolScope(Action<T> onSpawnCallback = null)
        {
            this._onSpawnCallback = onSpawnCallback;
        }
        public override void Dispose()
        {
            PoolCatManagerUtil.Despawn(_spawn);
            _spawn = default;
            base.Dispose();
        }
    }
}