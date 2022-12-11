using Object = UnityEngine.Object;

namespace CsCat
{
	public class UnityObjectPoolCat<T> : PoolCat<T> where T: Object
	{
		public T prefab;

		public UnityObjectPoolCat(string poolName, T prefab) : base(poolName)
		{
			this.prefab = prefab;
		}

		public T GetPrefab()
		{
			return prefab;
		}

		protected override T _Spawn()
		{
			T clone = Object.Instantiate(prefab);
			clone.name = prefab.name;
			return clone;
		}

		protected override void _OnDestroy(T value)
		{
			value.Destroy();
			base._OnDestroy(value);
		}
	}
}

