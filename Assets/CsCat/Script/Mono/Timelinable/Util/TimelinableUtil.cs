using System.Collections.Generic;
using UnityEngine;

namespace CsCat
{
	public static class TimelinableUtil
	{
		public static GameObjectPoolCat GetPoolCatGameObject(GameObject prefab)
		{
			return SpawnUtil.GetOrAddGameObjectPool(TimelinableConst.Pool_Name, prefab, null);
		}

		public static GameObject SpawnGameObject(GameObject prefab, Transform parentTransform = null)
		{
			if (prefab == null)
				return null;
			return SpawnUtil.SpawnGameObject(TimelinableConst.Pool_Name, prefab, null, parentTransform);
		}

		public static void DespawnGameObject(GameObject clone, Transform parentTransform = null)
		{
			SpawnUtil.DespawnGameObject(clone, parentTransform);
		}
	}
}