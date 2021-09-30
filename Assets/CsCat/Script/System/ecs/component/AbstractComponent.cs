using System;

namespace CsCat
{
  public partial class AbstractComponent : IDespawn
  {
    public string key;
    public AbstractEntity entity;
    public bool is_key_using_parent_idPool;
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

		void __OnDespawn_()
    {
      key = null;
      entity = null;
      is_key_using_parent_idPool = false;
      cache.Clear();
    }
  }
}