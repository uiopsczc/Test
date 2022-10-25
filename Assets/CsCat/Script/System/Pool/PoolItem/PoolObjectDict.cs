using System;
using System.Collections.Generic;

namespace CsCat
{
	public class PoolObjectDict<SpawnType>
	{
		private readonly PoolCat<SpawnType> _pool;
		private readonly Dictionary<SpawnType, PoolItem<SpawnType>> _poolObjectDict = new Dictionary<SpawnType, PoolItem<SpawnType>>();
		public PoolObjectDict(PoolCat<SpawnType> pool)
		{
			this._pool = pool;
		}

		public SpawnType Get()
		{
			var poolObject = this._pool.Spawn(null);
			var value = poolObject.GetValue();
			_poolObjectDict[value] = poolObject;
			return value;
		}

		public void Remove(SpawnType key)
		{
			if (_poolObjectDict.TryGetValue(key, out var value))
				value.Despawn();
			_poolObjectDict.Remove(key);
		}

		public void Clear()
		{
			foreach (var keyValue in _poolObjectDict)
			{
				var value = keyValue.Value;
				value.Despawn();
			}
			_poolObjectDict.Clear();
		}
	}
}