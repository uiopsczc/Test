using System;

namespace CsCat
{
	public class SpawnPoolScope<T> : PoolScope
	{
		private T _spawn;
		private PoolObject<T> poolObject;
		private Action<T> _onSpawnCallback;

		public T spawn
		{
			get
			{
				if (_spawn == null)
				{
					poolObject = PoolCatManagerUtil.Spawn<T>(null, null, _onSpawnCallback);
					_spawn = poolObject.GetValue();
				}

				return _spawn;
			}
		}


		public SpawnPoolScope(Action<T> onSpawnCallback = null)
		{
			this._onSpawnCallback = onSpawnCallback;
		}

		public override void Dispose()
		{
			poolObject.DeSpawn();
			_spawn = default;
			poolObject = null;
			this._onSpawnCallback = null;
			base.Dispose();
		}
	}
}