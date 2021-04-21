using System;
using System.Collections;

namespace CsCat
{
  public class AbstractPhase<T>
  {
    protected T value;

    //持续时间
    public int duration_tick;


    //lerp_tick的范围在[0, 这个this的duration_tick]之间
    public T Tween(AbstractPhase<T> to_phase, int lerp_tick, Func<T, T, float, T> lerp_callback)
    {
      T from_value = value;
      if (to_phase == null)
        return from_value;
      T to_value = to_phase.value;
      return lerp_callback(from_value, to_value, (float)lerp_tick / duration_tick);
    }

    public virtual T Lerp(AbstractPhase<T> to_phase, int lerp_tick)
    {
      return default(T);
    }

    public virtual void DoSave(Hashtable dict)
    {
      dict["duration_tick"] = duration_tick;
    }

    public virtual void DoRestore(Hashtable dict)
    {
      duration_tick = dict["duration_tick"].ToIntOrToDefault();
    }
  }
}