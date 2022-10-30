using System;

namespace CsCat
{
	public partial class Component : IDespawn
	{
		private PoolItemIndex<Entity> _entityPoolItemIndex;
		protected Cache cache = new Cache();


		public Component()
		{
		}

		public void SetEntityPoolItemIndex(PoolItemIndex<Entity> entityPoolItemIndex)
		{
			this._entityPoolItemIndex = entityPoolItemIndex;
		}

		public virtual void Init()
		{
		}

		public virtual void PostInit()
		{
		}

		public Entity GetEntity()
		{
			return this.cache.GetOrAddDefault("entity", () => this._entityPoolItemIndex.GetValue());
		}
		

		void _OnDespawn_()
		{
			_entityPoolItemIndex = null;
		}
	}
}