namespace CsCat
{
  public class EventSourceImpl:IDespawn
  {
    private ulong eventSourceId;
    private IEventSource eventSouce;

    public EventSourceImpl()
    {
    }

    public void Init(IEventSource eventSouce)
    {
      this.eventSouce = eventSouce;
      this.eventSourceId = EventSourceManager.instance.AddEventSource(this.eventSouce);
    }

    public ulong GetEventSourceId()
    {
      return this.eventSourceId;
    }

    public void Destroy()
    {
      OnDespawn();
    }

    public void OnDespawn()
    {
      EventSourceManager.instance.RemoveEventSource(this.eventSouce);
      eventSouce = null;
      eventSourceId = 0;
    }
  }
}