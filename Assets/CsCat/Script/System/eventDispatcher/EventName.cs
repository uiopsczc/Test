namespace CsCat
{
  public class EventName : IDespawn
  {
    public string name;
    public object source;

    public EventName()
    {
    }
    public EventName(object source, string name)
    {
      Init(source, name);
    }


    public EventName Init(object source, string name)
    {
      this.source = source;
      this.name = name;
      return this;
    }

    public static EventName Spawn(object soruce, string name)
    {
      var result = PoolCatManagerUtil.Spawn<EventName>();
      result.Init(soruce, name);
      return result;
    }

    public static EventName Spawn(string name)
    {
      return Spawn(null, name);
    }

    public override bool Equals(object obj)
    {
      if (!(obj is EventName))
        return false;
      var other = (EventName)obj;
      if (ObjectUtil.Equals(source, other.source) && ObjectUtil.Equals(name, other.name))
        return true;
      return false;
    }

    public override int GetHashCode()
    {
      return ObjectUtil.GetHashCode(source, name);
    }

    public void OnDespawn()
    {
      this.source = null;
      this.name = null;
    }
  }
}