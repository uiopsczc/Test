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

    public virtual void OnDespawn()
    {
      this._eventName.Despawn();
      this._eventName = null;
      this._handler = null;
    }
  }
}