using Object = UnityEngine.Object;

namespace CsCat
{
  public class UnityObjectPoolCat : PoolCat
  {
    public Object prefab;

    public UnityObjectPoolCat(string pool_name, Object prefab, string category = null) : base(pool_name,
      prefab.GetType())
    {
      this.prefab = prefab;
      if (category.IsNullOrWhiteSpace())
        category = prefab.name;
      InitParent(prefab, category);
    }

    public T GetPrefab<T>() where T : Object
    {
      return prefab as T;
    }

    public virtual void InitParent(Object prefab, string category)
    {
    }

    protected override object __Spawn()
    {
      Object clone = Object.Instantiate(prefab);
      clone.name = prefab.name;
      return clone;
    }

    protected override void __Trim(object despawned_object)
    {
      base.__Trim(despawned_object);
      (despawned_object as Object).Destroy();
    }

    public override void Destroy()
    {
      base.Destroy();
    }
  }
}

