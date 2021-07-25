using UnityEngine;

namespace CsCat
{
  public static partial class PoolCatManagerUtil
  {
    public static GameObjectPoolCat AddGameObjectPool(string pool_name, GameObject prefab, string category = null)
    {
      GameObjectPoolCat pool = null;
      if (!prefab.IsHasComponent<RectTransform>())
        pool = new NormalGameObjectPoolCat(pool_name, prefab, category);
      else
        pool = new UIGameObjectPoolCat(pool_name, prefab, category);
      PoolCatManager.instance.AddPool(pool_name, pool);
      return pool;
    }

    public static GameObjectPoolCat GetGameObjectPool(string pool_name)
    {
      return PoolCatManager.instance.GetPool(pool_name) as GameObjectPoolCat;
    }

    public static GameObjectPoolCat GetOrAddGameObjectPool(string pool_name, GameObject prefab, string category = null)
    {
      if (!IsContainsPool(pool_name))
        AddPool(pool_name, AddGameObjectPool(pool_name, prefab, category));
      return PoolCatManager.instance.GetPool(pool_name) as GameObjectPoolCat;
    }
  }
}