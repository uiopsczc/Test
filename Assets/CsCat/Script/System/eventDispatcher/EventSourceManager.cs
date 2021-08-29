using System.Collections.Generic;

namespace CsCat
{
  public class EventSourceManager : ISingleton
  {
    public static EventSourceManager instance => SingletonFactory.instance.Get<EventSourceManager>();

    private IdPool idPool = (IdPool) PoolCatManagerUtil.AddPool("EventSourceIdPool", new IdPool());
    private List<IEventSource> eventSourceList = new List<IEventSource>();

    public ulong AddEventSource(IEventSource eventSource)
    {
      eventSourceList.Add(eventSource);
      return idPool.Get();
    }

    public void RemoveEventSource(IEventSource eventSource)
    {
      eventSourceList.Remove(eventSource);
      idPool.Despawn(eventSource.GetEventSourceId());
    }
  }
}