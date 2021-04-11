using UnityEngine;

namespace CsCat
{
  public class SpawnUtil
  {
    public static GameObject Instantiate(GameObject prefab, Transform parent_transform = null)
    {
      if (prefab == null)
        return null;
      var clone = GameObject.Instantiate(prefab, parent_transform);
      return clone;
    }

    public static void Destroy(GameObject clone)
    {
      if (clone == null)
        return;
      clone.Destroy();
    }

    //冗余，不写lambda的原因是lambda会产生gc，这个不会
    public static void Destroy2(GameObject clone, Transform parent_transform)
    {
      Destroy(clone);
    }

    public static GameObjectPoolCat GetOrAddGameObjectPool(string pool_name, GameObject prefab, string category = null)
    {
      return PoolCatManagerUtil.GetOrAddGameObjectPool(pool_name, prefab, category);
    }

    public static GameObject SpawnGameObject(string pool_name, GameObject prefab, string category = null, Transform parent_transform = null)
    {
      if (prefab == null)
        return null;
      var pool = GetOrAddGameObjectPool(pool_name, prefab, category);
      var clone = pool.SpawnGameObject();
      clone.transform.SetParent(parent_transform);
      clone.transform.CopyFrom(pool.GetPrefab().transform);
      return clone;
    }

    public static void DespawnGameObject(GameObject clone, Transform parent_transform = null)
    {
      if (clone == null)
        return;
      clone.Despawn();
      if (parent_transform != null)
        clone.transform.SetParent(parent_transform);
    }

  }
}