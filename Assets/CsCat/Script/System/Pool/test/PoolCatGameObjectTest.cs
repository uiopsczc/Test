using UnityEngine;
using Object = UnityEngine.Object;

namespace CsCat
{
	public static class PoolCatGameObjectTest
	{
		public static GameObject Spawn(GameObject prefab)
		{
			return PoolCatManagerUtil.GetOrAddGameObjectPool("AAA", prefab, "CC").SpawnGameObject();
		}
	}
}