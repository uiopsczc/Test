namespace CsCat
{
  public class DataCenterManager : ISingleton
  {
    public EventDispatchers eventDispatchers;

    public DataCenterManager()
    {
      eventDispatchers = new EventDispatchers(this);
    }
  }
}