using System;
using System.Collections.Generic;

namespace CsCat
{
	public class PoolItemDict<T>
	{
		private readonly PoolCat<T> _pool;
		private readonly Dictionary<T, PoolItemIndex<T>> _poolItemIndexDict = new Dictionary<T, PoolItemIndex<T>>();
		public PoolItemDict(PoolCat<T> pool)
		{
			this._pool = pool;
		}

		public T Get()
		{
			var (poolItem, poolItemIndex) = this._pool.Spawn(null);
			var value = poolItem.GetValue();
			_poolItemIndexDict[value] = poolItemIndex;
			return value;
		}

		public void Remove(T key)
		{
			if (_poolItemIndexDict.TryGetValue(key, out var poolItemIndex))
			{
				_poolItemIndexDict.Remove(key);
				poolItemIndex.Despawn();
			}
		}

		public void Clear()
		{
			foreach (var keyValue in _poolItemIndexDict)
			{
				var poolItemIndex = keyValue.Value;
				poolItemIndex.Despawn();
			}
			_poolItemIndexDict.Clear();
		}
	}
}