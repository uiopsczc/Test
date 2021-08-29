namespace CsCat
{
  public class EventName : IDespawn
  {
    public string name;
    public IEventSource source;

    public EventName()
    {
    }

    public EventName( string name, IEventSource source)
    {
      Init( name, source);
    }


    public EventName Init(string name, IEventSource source)
    {
      this.source = source;
      this.name = name;
      return this;
    }


    public override bool Equals(object obj)
    {
      var other = obj as EventName;
      if (other == null)
        return false;
      return ObjectUtil.Equals(source, other.source) && ObjectUtil.Equals(name, other.name);
    }

    public override int GetHashCode()
    {
      return ObjectUtil.GetHashCode(source.GetEventSourceId(), name);
    }

    public EventName Clone()
    {
      return this.name.ToEventName(source);
    }

    public override string ToString()
    {
      return ObjectUtil.ToString(this.name, this.source);
    }

    public void OnDespawn()
    {
      this.source = null;
      this.name = null;
    }
  }
}