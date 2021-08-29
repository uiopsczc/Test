using System;

namespace CsCat
{
  public partial class AbstractComponent : IDespawn,IEventSource
  {
    public string key;
    public AbstractEntity entity;
    public bool is_key_using_parent_idPool;
    protected Cache cache = new Cache();

    private EventSourceImpl eventSourceImpl => cache.GetOrAddDefault(() => PoolCatManagerUtil.Spawn<EventSourceImpl>( null, spawn => spawn.Init(this)));
    public ulong GetEventSourceId() => eventSourceImpl.GetEventSourceId();

    public AbstractComponent()
    {
      GetEventSourceId();
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

    void __OnDespawn_()
    {
      key = null;
      entity = null;
      is_key_using_parent_idPool = false;
      var eventSourceImpl = this.cache.Get<EventSourceImpl>(typeof(EventSourceImpl).ToString());
      eventSourceImpl?.Despawn();
      cache.Clear();
    }
  }
}