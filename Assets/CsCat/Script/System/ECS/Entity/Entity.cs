using System;
using System.Collections.Generic;

namespace CsCat
{
	public partial class Entity : IDespawn
	{
		private PoolObjectIndex _poolObjectIndex;
		public Cache cache = new Cache();

		public Entity()
		{
		}

		public void SetPoolObjectIndex(PoolObjectIndex poolObjectIndex)
		{
			this._poolObjectIndex = poolObjectIndex;
		}

		public PoolCatManager GetPoolManager()
		{
			return this._poolObjectIndex.GetPool().GetPoolManager();
		}


		public virtual void Init()
		{
		}

		public virtual void PostInit()
		{
		}

		public virtual void Start()
		{
		}

		public virtual void Refresh()
		{
		}
		

		void _OnDespawn_()
		{
		}
	}
}