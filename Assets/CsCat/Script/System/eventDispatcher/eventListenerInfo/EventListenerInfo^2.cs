using System;

namespace CsCat
{
  public class EventListenerInfo<P0, P1> : EventListenerInfoBase
  {
    public Action<P0, P1> handler;

    public EventListenerInfo()
    {
    }

    public EventListenerInfo(EventName eventName, Action<P0, P1> handler) : base(eventName, handler)
    {
      this.handler = handler;
    }


    public EventListenerInfo<P0, P1> Init(EventName eventName, Action<P0, P1> handler)
    {
      base.Init(eventName, handler);
      this.handler = handler;
      return this;
    }

    public EventListenerInfo<P0,P1> Clone()
    {
      return PoolCatManagerUtil.Spawn<EventListenerInfo<P0, P1>>().Init(eventName.Clone(), handler);
    }


    public override void OnDespawn()
    {
      base.OnDespawn();
      this.handler = null;
    }
  }
}