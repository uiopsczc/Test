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
    

    public override bool Equals(object obj)
    {
      var other = obj as EventName;
      if (other == null)
        return false;
      if (((source == null && other.source == null) || (source != null && source.Equals(other.source))) &&
          ((name == null && other.name == null) || (name != null && name.Equals(other.name)))
      )
        return true;
      return false;
    }

    public override int GetHashCode()
    {
      int result = int.MinValue;
      if (source != null)
      {
        result = source.GetHashCode();
      }

      if (name == null) return result;
      if (result != int.MinValue)
        result ^= name.GetHashCode();
      else
        result = name.GetHashCode();

      return result;
    }

    public void OnDespawn()
    {
      this.source = null;
      this.name = null;
    }
  }
}