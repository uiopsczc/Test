

using System;
using Object = UnityEngine.Object;

namespace CsCat
{
  public static partial class PoolCatManagerUtil
  {
    public static UnityObjectPoolCat AddUnityObjectPool(string pool_name, Object prefab, string category = null)
    {
      var pool = new UnityObjectPoolCat(pool_name, prefab, category);
      PoolCatManager.instance.AddPool(pool_name, pool);
      return pool;
    }

    public static UnityObjectPoolCat GetUnityObjectPool(string pool_name)
    {
      return PoolCatManager.instance.GetPool(pool_name) as UnityObjectPoolCat;
    }

    public static UnityObjectPoolCat GetOrAddUnityObjectPool(string pool_name, Object prefab, string category = null)
    {
      return PoolCatManager.instance.GetOrAddPool(typeof(UnityObjectPoolCat), pool_name,  prefab,  category) as UnityObjectPoolCat;
    }

  }
}