namespace CsCat
{
  public static class EventNameUtil
  {
    public static EventName Spawn(object soruce, string name)
    {
      var result = PoolCatManagerUtil.SpawnEventName();
      result.Init(soruce, name);
      return result;
    }

    public static EventName Spawn(string name)
    {
      return Spawn(null, name);
    }
  }
}