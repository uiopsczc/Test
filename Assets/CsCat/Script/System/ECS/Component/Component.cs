using System;

namespace CsCat
{
	public partial class Component : IDespawn
	{
		private PoolObjectIndex _entityPoolObjectIndex;
		protected Cache cache = new Cache();


		public Component()
		{
		}

		public void SetEntityPoolObjectIndex(PoolObjectIndex entityPoolObjectIndex)
		{
			this._entityPoolObjectIndex = entityPoolObjectIndex;
		}

		public virtual void Init()
		{
		}

		public virtual void PostInit()
		{
		}

		public T GetEntity<T>() where T : Entity
		{
			return this.cache.GetOrAddDefault("entity_" + typeof(T), () => this._entityPoolObjectIndex.GetValue<T>());
		}

		public GameEntity GetGameEntity()
		{
			return GetEntity<GameEntity>();
		}
		

		void _OnDespawn_()
		{
			_entityPoolObjectIndex = null;
		}
	}
}