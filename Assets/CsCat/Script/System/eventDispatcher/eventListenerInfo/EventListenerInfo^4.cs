using System;

namespace CsCat
{
  public class EventListenerInfo<P0, P1, P2, P3> : EventListenerInfoBase
  {
    public Action<P0, P1, P2, P3> handler;

    public EventListenerInfo()
    {
    }

    public EventListenerInfo(EventName eventName, Action<P0, P1, P2, P3> handler) : base(eventName, handler)
    {
      this.handler = handler;
    }

    public EventListenerInfo<P0, P1, P2, P3> Init(EventName eventName, Action<P0, P1, P2, P3> handler)
    {
      base.Init(eventName, handler);
      this.handler = handler;
      return this;
    }

    public EventListenerInfo<P0, P1, P2,P3> Clone()
    {
      return PoolCatManagerUtil.Spawn<EventListenerInfo<P0, P1, P2, P3>>().Init(eventName.Clone(), handler);
    }

    public override void OnDespawn()
    {
      base.OnDespawn();
      this.handler = null;
    }
  }
}