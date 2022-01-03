using System;

namespace CsCat
{
	public partial class AbstractComponent : IDespawn
	{
		public string key;
		public AbstractEntity entity;
		public bool isKeyUsingParentIdPool;
		protected Cache cache = new Cache();


		public AbstractComponent()
		{
		}

		public virtual void Init()
		{
		}

		public virtual void PostInit()
		{
		}

		public T GetEntity<T>() where T : AbstractEntity
		{
			return this.cache.GetOrAddDefault("entity_" + typeof(T), () => (T)this.entity);
		}

		public GameEntity GetGameEntity()
		{
			return GetEntity<GameEntity>();
		}

		void _OnDespawn_()
		{
			key = null;
			entity = null;
			isKeyUsingParentIdPool = false;
			cache.Clear();
		}
	}
}