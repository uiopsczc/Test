using System;
using System.Collections.Generic;

namespace CsCat
{
	public partial class Entity : IDespawn
	{
		public Cache _cache = new Cache();
		private int _id;
		private PoolCatManager _poolManager;

		public Entity()
		{
		}

		public void SetId(int id)
		{
			this._id = id;
		}

		public int GetId()
		{
			return this._id;
		}

		public void SetPoolManager(PoolCatManager poolManager)
		{
			this._poolManager = poolManager;
		}

		public PoolCatManager GetPoolManager()
		{
			return this._poolManager;
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