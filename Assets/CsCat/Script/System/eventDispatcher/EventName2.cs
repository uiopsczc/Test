namespace CsCat
{
  public class EventName2
  {
    public string name;
    public IEventSource source;

    public EventName2( string name, IEventSource source)
    {
      Init( name, source);
    }


    public EventName2 Init(string name, IEventSource source)
    {
      this.source = source;
      this.name = name;
      return this;
    }

    public override int GetHashCode()
    {
      LogCat.warn("eeee", this.name, this.source, this.source.GetEventSourceId());
      return ObjectUtil.GetHashCode(this.name, this.source.GetEventSourceId());
    }
  }
}