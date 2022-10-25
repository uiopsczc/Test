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

		public override PoolItem<GameObject> Spawn(Action<GameObject> onSpawnCallback = null)
		{
			var poolObject = base.Spawn(onSpawnCallback);
			GameObject cloneGameObject = poolObject.GetValue();
			cloneGameObject.SetCache(PoolCatConst.Pool_Object, poolObject);
			cloneGameObject.SetActive(true);
			cloneGameObject.transform.CopyFrom(GetPrefab().transform);
			return poolObject;
		}

		public PoolItem<GameObject> SpawnGameObject(Action<GameObject> onSpawnCallback = null)
		{
			return onSpawnCallback == null ? Spawn() : Spawn(onSpawnCallback);
		}

		public override void Despawn(PoolItem<GameObject> poolItem)
		{
			GameObject clone = poolItem.GetValue();
			var components = clone.GetComponents<UnityEngine.Component>();
			for (var i = 0; i < components.Length; i++)
			{
				var cloneComponent = components[i];
				var spawnable = cloneComponent as IDespawn;
				spawnable?.OnDespawn();
			}
			clone.SetActive(false);
			clone.transform.SetParent(categoryTransform);
			clone.transform.CopyFrom(this.prefab.transform);
			base.Despawn(poolItem);
		}
	}
}