namespace CsCat
{
  public class YieldDefault : YieldBase
  {
    public override bool IsDone(float deltaTime)
    {
      if (!CheckIsStarted())
        return false;

      return true;
    }
  }
}