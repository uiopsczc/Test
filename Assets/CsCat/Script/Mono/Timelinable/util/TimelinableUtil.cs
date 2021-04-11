using System.Collections.Generic;
using UnityEngine;

namespace CsCat
{
  public static class TimelinableUtil
  {

    public static GameObjectPoolCat GetPoolCatGameObject(GameObject prefab)
    {
      return SpawnUtil.GetOrAddGameObjectPool(TimelinableConst.Pool_Name, prefab, null);
    }

    public static GameObject SpawnGameObject(GameObject prefab,Transform parent_transform = null)
    {
      if (prefab == null)
        return null;
      return SpawnUtil.SpawnGameObject(TimelinableConst.Pool_Name, prefab, null, parent_transform);
    }
    public static void DespawnGameObject(GameObject clone, Transform parent_transform = null)
    {
      SpawnUtil.DespawnGameObject(clone, parent_transform);
    }
  }
}



