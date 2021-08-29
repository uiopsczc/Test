namespace CsCat
{
//  private EventSourceImpl eventSourceImpl => cache.GetOrAddDefault(() => PoolCatManagerUtil.Spawn<EventSourceImpl>(null, null, spawn => spawn.Init(this)));
//  public ulong GetEventSourceId() => eventSourceImpl.GetEventSourceId();
//  var eventSourceImpl = this.cache.Get<EventSourceImpl>(typeof(EventSourceImpl).ToString());
//  eventSourceImpl?.Despawn();
  public interface IEventSource
  {
    ulong GetEventSourceId();
  }
}