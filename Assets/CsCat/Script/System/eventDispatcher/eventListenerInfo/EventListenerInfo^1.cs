using System;

namespace CsCat
{
  public class EventListenerInfo<P0> : EventListenerInfoBase
  {
    public Action<P0> handler;

    public EventListenerInfo()
    {
    }

    public EventListenerInfo(EventName eventName, Action<P0> handler) : base(eventName, handler)
    {
      this.handler = handler;
    }

    public EventListenerInfo<P0> Init(EventName eventName, Action<P0> handler)
    {
      base.Init(eventName, handler);
      this.handler = handler;
      return this;
    }

    public override bool Equals(object obj)
    {
      if (!(obj is EventListenerInfo<P0>))
        return false;
      var other = (EventListenerInfo<P0>) obj;
      return ObjectUtil.Equals(this.eventName, other.eventName) && ObjectUtil.Equals(this.handler, other.handler);
    }

    public override int GetHashCode()
    {
      return ObjectUtil.GetHashCode(eventName, handler);
    }

    public override void OnDespawn()
    {
      base.OnDespawn();
      this.handler = null;
    }
  }
}