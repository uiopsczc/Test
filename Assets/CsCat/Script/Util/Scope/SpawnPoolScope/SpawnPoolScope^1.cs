using System;

namespace CsCat
{
	public class SpawnPoolScope<T> : PoolScope
	{
		private T _spawn;
		private PoolItem<T> _poolItem;
		private Action<T> _onSpawnCallback;

		public T spawn
		{
			get
			{
				if (_spawn == null)
				{
					_poolItem = PoolCatManagerUtil.Spawn<T>(null, null, _onSpawnCallback);
					_spawn = _poolItem.GetValue();
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
			_poolItem.Despawn();
			_spawn = default;
			_poolItem = null;
			this._onSpawnCallback = null;
			base.Dispose();
		}
	}
}