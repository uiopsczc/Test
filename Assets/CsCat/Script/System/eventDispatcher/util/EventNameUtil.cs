namespace CsCat
{
  public static class EventNameUtil
  {
    public static EventName Spawn(string name, object soruce)
    {
      var result = PoolCatManagerUtil.SpawnEventName();
      result.Init( name, soruce);
      return result;
    }

    public static EventName Spawn(string name)
    {
      return Spawn(name,null );
    }
  }
}