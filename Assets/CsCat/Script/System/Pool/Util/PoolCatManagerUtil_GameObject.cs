using UnityEngine;

namespace CsCat
{
	public static partial class PoolCatManagerUtil
	{
		public static GameObjectPoolCat AddGameObjectPool(string poolName, GameObject prefab, string category = null)
		{
			GameObjectPoolCat pool = null;
			if (!prefab.IsHasComponent<RectTransform>())
				pool = new NormalGameObjectPoolCat(poolName, prefab, category);
			else
				pool = new UIGameObjectPoolCat(poolName, prefab, category);
			PoolCatManager.instance.AddPool(poolName, pool);
			return pool;
		}

		public static GameObjectPoolCat GetGameObjectPool(string poolName)
		{
			return PoolCatManager.instance.GetPool(poolName) as GameObjectPoolCat;
		}

		public static GameObjectPoolCat GetOrAddGameObjectPool(string poolName, GameObject prefab,
			string category = null)
		{
			if (TryGetPool(poolName, out var pool))
				return pool as GameObjectPoolCat;
			return AddPool(poolName, AddGameObjectPool(poolName, prefab, category)) as GameObjectPoolCat;
		}
	}
}