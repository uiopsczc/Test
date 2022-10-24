using UnityEngine;

namespace CsCat
{
	public partial class PoolCatManager
	{
		public GameObjectPoolCat AddGameObjectPool(string poolName, GameObject prefab, string category = null)
		{
			GameObjectPoolCat pool = null;
			if (!prefab.IsHasComponent<RectTransform>())
				pool = new NormalGameObjectPoolCat(poolName, prefab, category);
			else
				pool = new UIGameObjectPoolCat(poolName, prefab, category);
			this.AddPool(poolName, pool);
			return pool;
		}

		public  GameObjectPoolCat GetGameObjectPool(string poolName)
		{
			return this.GetPool(poolName) as GameObjectPoolCat;
		}

		public  GameObjectPoolCat GetOrAddGameObjectPool(string poolName, GameObject prefab,
			string category = null)
		{
			if (TryGetPool(poolName, out var pool))
				return pool as GameObjectPoolCat;
			return AddPool(poolName, AddGameObjectPool(poolName, prefab, category)) as GameObjectPoolCat;
		}
	}
}