

using System;
using Object = UnityEngine.Object;

namespace CsCat
{
  public static partial class PoolCatManagerUtil
  {
    public static CustomPoolCat AddCustomPool(string pool_name, Func<object> spawn_func)
    {
      var pool = new CustomPoolCat(pool_name, spawn_func);
      PoolCatManager.instance.AddPool(pool_name, pool);
      return pool;
    }

    public static CustomPoolCat GetCustomPool(string pool_name)
    {
      return PoolCatManager.instance.GetPool(pool_name) as CustomPoolCat;
    }

    public static CustomPoolCat GetOrAddCustomPool(string pool_name, Func<object> spawn_func)
    {
      return PoolCatManager.instance.GetOrAddPool(typeof(CustomPoolCat), pool_name, spawn_func) as CustomPoolCat;
    }

  }
}