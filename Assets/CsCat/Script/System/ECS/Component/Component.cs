using System;

namespace CsCat
{
	public partial class Component : IDespawn
	{
		private PoolItemIndex<Entity> _entityPoolItemIndex;
		protected Cache _cache = new Cache();


		public Component()
		{
		}

		public void SetEntityPoolItemIndex(PoolItemIndex<Entity> entityPoolItemIndex)
		{
			this._entityPoolItemIndex = entityPoolItemIndex;
		}

		public void DoInit(params object[] args)
		{
			_PreInit();
			this.InvokeMethod("_Init", false, args);
			_PostInit();
		}

		protected virtual void _PreInit()
		{
		}

		protected virtual void _Init()
		{
		}

		protected virtual void _PostInit()
		{
		}

		public Entity GetEntity()
		{
			return this._cache.GetOrAddDefault("entity", () => this._entityPoolItemIndex.GetValue());
		}

		public T GetComponent<T>() where T:Component
		{
			return this.GetEntity().GetComponentStrictly<T>();
		}
		

		void _OnDespawn_()
		{
			_entityPoolItemIndex = null;
		}
	}
}