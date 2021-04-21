using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace CsCat
{
  public class GameObjectPoolCat : UnityObjectPoolCat
  {
    private Transform root_transform;
    private Transform category_transform;


    public GameObjectPoolCat(string pool_name, GameObject prefab, string category = null) : base(pool_name, prefab,
      category)
    {
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
        return Spawn(obj => on_spawn_callback((GameObject)obj)) as GameObject;
    }

    public GameObject GetPrefab()
    {
      return this.GetPrefab<GameObject>();
    }

    public override void InitParent(Object prefab, string category)
    {
      base.InitParent(prefab, category);
      GameObject root_gameObject = GameObjectUtil.GetOrNewGameObject("Pools", null);
      root_transform = root_gameObject.transform;
      category_transform = root_transform.GetOrNewGameObject(category).transform;
    }

    public override void Despawn(object obj)
    {
      GameObject clone = obj as GameObject;
      foreach (var clone_component in clone.GetComponents<Component>())
      {
        if (clone_component is ISpawnable ispanwable)
          ispanwable.OnDespawn();
      }
      clone.SetActive(false);
      clone.transform.SetParent(category_transform);
      clone.transform.CopyFrom((prefab as GameObject).transform);
      base.Despawn(obj);
    }
  }

}