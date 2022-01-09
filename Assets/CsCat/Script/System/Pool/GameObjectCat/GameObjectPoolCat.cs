using System;
using UnityEngine;

namespace CsCat
{
	public class GameObjectPoolCat : UnityObjectPoolCat
	{
		protected Transform rootTransform;
		protected Transform categoryTransform;

		public GameObjectPoolCat(string poolName, GameObject prefab, string category = null) : base(poolName, prefab,
			category)
		{
		}

		public GameObject GetPrefab()
		{
			return this.GetPrefab<GameObject>();
		}

		public override object Spawn(Action<object> onSpawnCallback = null)
		{
			GameObject cloneGameObject = base.Spawn(onSpawnCallback) as GameObject;
			cloneGameObject.SetCache(PoolCatConst.Pool_Name, this);
			cloneGameObject.SetActive(true);
			cloneGameObject.transform.CopyFrom(GetPrefab().transform);
			return cloneGameObject;
		}

		public GameObject SpawnGameObject(Action<GameObject> onSpawnCallback = null)
		{
			if (onSpawnCallback == null)
				return Spawn() as GameObject;
			return Spawn(obj => onSpawnCallback((GameObject)obj)) as GameObject;
		}
	}
}