using System;
using System.Collections.Generic;

namespace CsCat
{
	public partial class Entity : IDespawn
	{
		public Cache _cache = new Cache();
		private PoolItemIndex<Entity> _poolItemIndex;

		public Entity()
		{
		}

		public void SetPoolItemIndex(PoolItemIndex<Entity> poolItemIndex)
		{
			this._poolItemIndex = poolItemIndex;
		}

		public PoolItemIndex<Entity> GetPoolItemIndex()
		{
			return this._poolItemIndex;
		}

		public int GetId()
		{
			return this._poolItemIndex.GetIndex();
		}

		public PoolCatManager GetPoolManager()
		{
			return _poolItemIndex.GetPool().GetPoolManager();
		}

		public virtual void Start()
		{
		}

		public virtual void Refresh(bool isInit = false)
		{
		}
		

		void _OnDespawn_()
		{
		}
	}
}