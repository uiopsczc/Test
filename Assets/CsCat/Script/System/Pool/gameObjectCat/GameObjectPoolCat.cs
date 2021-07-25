using System;
using UnityEngine;

namespace CsCat
{
  public class GameObjectPoolCat : UnityObjectPoolCat
  {
    protected Transform root_transfrom;
    protected Transform category_transform;

    public GameObjectPoolCat(string pool_name, GameObject prefab, string category = null) : base(pool_name, prefab,
      category)
    {
    }

    public GameObject GetPrefab()
    {
      return this.GetPrefab<GameObject>();
    }

    public override object Spawn(Action<object> on_spawn_callback = null)
    {
      GameObject clone_gameObject = base.Spawn(on_spawn_callback) as GameObject;
      clone_gameObject.SetCache(PoolCatConst.Pool_Name, this);
      clone_gameObject.SetActive(true);
      clone_gameObject.transform.CopyFrom(GetPrefab().transform);
      return clone_gameObject;
    }

    public GameObject SpawnGameObject(Action<GameObject> on_spawn_callback = null)
    {
      if (on_spawn_callback == null)
        return Spawn() as GameObject;
      else
        return Spawn(obj => on_spawn_callback((GameObject) obj)) as GameObject;
    }
    
  }
}