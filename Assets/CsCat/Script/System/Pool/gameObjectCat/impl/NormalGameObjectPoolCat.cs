using UnityEngine;
using Object = UnityEngine.Object;

namespace CsCat
{
  public class NormalGameObjectPoolCat : GameObjectPoolCat
  {
    public NormalGameObjectPoolCat(string pool_name, GameObject prefab, string category = null) : base(pool_name, prefab, category)
    {
    }

    public override void InitParent(Object prefab, string category)
    {
      base.InitParent(prefab, category);
      root_transfrom = GameObjectUtil.GetOrNewGameObject("Pools", null).transform;
      category_transform = root_transfrom.GetOrNewGameObject(category).transform;
    }

    public override void Despawn(object obj)
    {
      GameObject clone = obj as GameObject;
      foreach (var clone_component in clone.GetComponents<Component>())
      {
        if (clone_component is IDespawn ispanwable)
          ispanwable.OnDespawn();
      }
      clone.SetActive(false);
      clone.transform.SetParent(category_transform);
      clone.transform.CopyFrom((prefab as GameObject).transform);
      base.Despawn(obj);
    }
  }

}