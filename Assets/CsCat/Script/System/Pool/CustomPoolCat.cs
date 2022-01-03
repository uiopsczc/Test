using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace CsCat
{
	public class CustomPoolCat : PoolCat
	{
		private Func<object> spawnFunc;

		public CustomPoolCat(string poolName, Func<object> spawnFunc) : base(poolName, (Type)null)
		{
			this.spawnFunc = spawnFunc;
		}

		protected override object _Spawn()
		{
			return this.spawnFunc();
		}
	}
}