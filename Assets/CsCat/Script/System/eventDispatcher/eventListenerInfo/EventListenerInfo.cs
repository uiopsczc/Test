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


    public override bool Equals(object obj)
    {
      if (!(obj is EventListenerInfo))
        return false;
      var other = (EventListenerInfo)obj;
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