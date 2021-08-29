using System;

namespace CsCat
{
  public class EventListenerInfo : EventListenerInfoBase
  {
    public Action handler;

    public EventListenerInfo()
    {
    }

    public EventListenerInfo(EventName eventName, Action handler) : base(eventName, handler)
    {
      this.handler = handler;
    }

    public EventListenerInfo Init(EventName eventName, Action handler)
    {
      base.Init(eventName, handler);
      this.handler = handler;
      return this;
    }

    public EventListenerInfo Clone()
    {
      return PoolCatManagerUtil.Spawn<EventListenerInfo>().Init(eventName.Clone(), handler);
    }

    public override void OnDespawn()
    {
      base.OnDespawn();
      this.handler = null;
    }
  }
}