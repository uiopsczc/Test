using System;
using UnityEngine;

namespace CsCat
{
	public class GameObjectPoolCat : UnityObjectPoolCat<GameObject>
	{
		protected Transform rootTransform;
		protected Transform categoryTransform;
		private bool isPrefabActive;

		public GameObjectPoolCat(string poolName, GameObject prefab, string category = null) : base(poolName, prefab)
		{
			if (category.IsNullOrWhiteSpace())
				category = prefab.name;
			isPrefabActive = prefab.activeSelf;
			InitParentTransform(prefab, category);
		}

		public virtual void InitParentTransform(GameObject prefab, string category)
		{
		}

		public override (PoolItem<GameObject>, PoolIndex<GameObject>) Spawn(Action<GameObject> onSpawnCallback = null)
		{
			var (poolItem, poolIndex) = base.Spawn(onSpawnCallback);
			OnSpawn(poolItem);
			return (poolItem, poolIndex);
		}

		public override GameObject SpawnValue(Action<GameObject> onSpawnCallback = null)
		{
			var(poolItem, poolIndex) = this.Spawn(onSpawnCallback);
			OnSpawnValue(poolItem, poolIndex);
			return poolItem.GetValue();
		}

		public void OnSpawn(PoolItem<GameObject> poolItem)
		{
			GameObject cloneGameObject = poolItem.GetValue();
			cloneGameObject.SetCache(PoolCatConst.Pool_Item, poolItem);
			cloneGameObject.SetActive(isPrefabActive);
			cloneGameObject.transform.CopyFrom(GetPrefab().transform);
		}

		public override void Despawn(PoolItem<GameObject> poolItem)
		{
			GameObject clone = poolItem.GetValue();
			var components = clone.GetComponents<UnityEngine.Component>();
			for (var i = 0; i < components.Length; i++)
			{
				var cloneComponent = components[i];
				var spawnable = cloneComponent as IDespawn;
				spawnable?.Despawn();
			}
			clone.SetActive(false);
			clone.transform.SetParent(categoryTransform);
			clone.transform.CopyFrom(this.prefab.transform);
			base.Despawn(poolItem);
		}
	}
}