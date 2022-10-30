using System;
using System.Collections.Generic;

namespace CsCat
{
	public partial class PoolCat<T>:IPoolCat
	{
		/// <summary>
		/// 存放item的List
		/// </summary>
		private readonly List<PoolItem<T>> _poolItemList = new List<PoolItem<T>>();
		/// <summary>
		/// 存放item对应在pool中的index
		/// </summary>
		private Dictionary<T, int> _valueToPoolItemIndexDict;
		private Dictionary<T, int> valueToPoolItemIndexDict => _valueToPoolItemIndexDict ?? (_valueToPoolItemIndexDict = new Dictionary<T, int>());
		private string _poolName;
		private PoolCatManager _poolManager;
		private Func<T> _spawnFunc;



		public PoolCat(string poolName)
		{
			this._poolName = poolName;
		}

		public PoolCat(string poolName, Func<T> spawnFunc)
		{
			this._poolName = poolName;
			this._spawnFunc = spawnFunc;
		}

		public string GetPoolName()
		{
			return this._poolName;
		}

		public void SetPoolManager(PoolCatManager poolManager)
		{
			this._poolManager = poolManager;
		}

		public PoolCatManager GetPoolManager()
		{
			return this._poolManager;
		}

		public void InitPool(int initCount = 1, Action<T> onSpawnCallback = null)
		{
			for (int i = 0; i < initCount; i++)
			{
				var (poolItem, poolItemIndex) = Spawn(onSpawnCallback);
				Despawn(poolItem);
			}
		}

		public PoolItem<T> GetPoolItemAtIndex(int index)
		{
			return this._poolItemList[index];
		}
	}
}