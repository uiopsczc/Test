

using UnityEngine;

namespace CsCat
{
  public static partial class PoolCatManagerUtil
  {
    public static GameObjectPoolCat AddGameObjectPool(string pool_name, GameObject prefab, string category = null)
    {
      var pool = new GameObjectPoolCat(pool_name, prefab, category);
      PoolCatManager.instance.AddPool(pool_name, pool);
      return pool;
    }

    public static GameObjectPoolCat GetGameObjectPool(string pool_name)
    {
      return PoolCatManager.instance.GetPool(pool_name) as GameObjectPoolCat;
    }

    public static GameObjectPoolCat GetOrAddGameObjectPool(string pool_name, GameObject prefab, string category = null)
    {
      return PoolCatManager.instance.GetOrAddPool(typeof(GameObjectPoolCat), pool_name,prefab, category) as GameObjectPoolCat;
    }

  }
}