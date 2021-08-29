namespace CsCat
{
  public class EventListenerInfoBase : IDespawn
  {
    public EventName _eventName;
    public object _handler;

    public EventName eventName => this._eventName;

    public EventListenerInfoBase()
    {
    }

    public EventListenerInfoBase(EventName _eventName, object _handler)
    {
      Init(_eventName, _handler);
    }

    public void Init(EventName _eventName, object _handler)
    {
      this._eventName = _eventName;
      this._handler = _handler;
    }

    public override bool Equals(object obj)
    {
      var other = obj as EventListenerInfoBase;
      if (other == null)
        return false;
      return ObjectUtil.Equals(eventName, other.eventName) && ObjectUtil.Equals(_handler, other._handler);
    }

    public override int GetHashCode()
    {
      return ObjectUtil.GetHashCode(_eventName, _handler);
    }

    public virtual void OnDespawn()
    {
      this._eventName.Despawn();
      this._eventName = null;
      this._handler = null;
    }
  }
}