using UnityEngine;

namespace CsCat
{
  public class YieldBase : ICoroutineYield
  {
    protected bool is_started;//如果在10s的delta中创建该YieldBase，则会出现误差；所以用这个变量来让当前的帧不计算在内

    public virtual bool IsDone(float deltaTime)
    {
      return false;
    }

    protected bool CheckIsStarted()
    {
      if (!is_started)
      {
        is_started = true;
        return false;
      }
      return true;
    }
  }
}