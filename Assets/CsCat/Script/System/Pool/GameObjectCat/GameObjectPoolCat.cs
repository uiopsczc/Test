using System;
using UnityEngine;

namespace CsCat
{
	public class GameObjectPoolCat : UnityObjectPoolCat<GameObject>
	{
		protected Transform rootTransform;
		protected Transform categoryTransform;

		public GameObjectPoolCat(string poolName, GameObject prefab, string category = null) : base(poolName, prefab)
		{
			if (category.IsNullOrWhiteSpace())
				category = prefab.name;
			InitParentTransform(prefab, category);
		}

		public virtual void InitParentTransform(GameObject prefab, string category)
		{
		}

		public override PoolObject<GameObject> Spawn(Action<GameObject> onSpawnCallback = null)
		{
			var poolObject = base.Spawn(onSpawnCallback);
			GameObject cloneGameObject = poolObject.GetValue();
			cloneGameObject.SetCache(PoolCatConst.Pool_Object, poolObject);
			cloneGameObject.SetActive(true);
			cloneGameObject.transform.CopyFrom(GetPrefab().transform);
			return poolObject;
		}

		public PoolObject<GameObject> SpawnGameObject(Action<GameObject> onSpawnCallback = null)
		{
			return onSpawnCallback == null ? Spawn() : Spawn(onSpawnCallback);
		}

		public override void DeSpawn(PoolObject<GameObject> poolObject)
		{
			GameObject clone = poolObject.GetValue();
			var components = clone.GetComponents<Component>();
			for (var i = 0; i < components.Length; i++)
			{
				var cloneComponent = components[i];
				var spawnable = cloneComponent as IDeSpawn;
				spawnable?.OnDeSpawn();
			}
			clone.SetActive(false);
			clone.transform.SetParent(categoryTransform);
			clone.transform.CopyFrom(this.prefab.transform);
			base.DeSpawn(poolObject);
		}
	}
}