using Object = UnityEngine.Object;

namespace CsCat
{
	public class UnityObjectPoolCat : PoolCat
	{
		public Object prefab;

		public UnityObjectPoolCat(string poolName, Object prefab, string category = null) : base(poolName,
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

		protected override object _Spawn()
		{
			Object clone = Object.Instantiate(prefab);
			clone.name = prefab.name;
			return clone;
		}

		protected override void _Trim(object despawnedObject)
		{
			base._Trim(despawnedObject);
			(despawnedObject as Object).Destroy();
		}

		public override void Destroy()
		{
			base.Destroy();
		}
	}
}

