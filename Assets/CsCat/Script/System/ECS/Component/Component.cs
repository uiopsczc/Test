using System;

namespace CsCat
{
	public partial class Component : IDespawn
	{
		private int _entityId;
		protected Cache cache = new Cache();


		public Component()
		{
		}

		public void SetEntityId(int entityId)
		{
			this._entityId = entityId;
		}

		public virtual void Init()
		{
		}

		public virtual void PostInit()
		{
		}

		public Entity GetEntity()
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