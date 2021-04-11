using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CsCat
{
  public class AbstractPhaseTrack<T>
  {
    public bool is_loop;
    //持续时间
    public int duration_tick;

    public List<AbstractPhase<T>> phase_list = new List<AbstractPhase<T>>();

    public T GetSnapshot(int target_tick)
    {
      if (target_tick < 0)
        throw new ArgumentException(string.Format("Tick should be zero or positive number. tick:{0}", target_tick));
      if (this.is_loop && target_tick >= this.duration_tick)
        target_tick = this.duration_tick != 0 ? target_tick % this.duration_tick : 0;
      int n = 0;
      int lerp_tick = 0;
      AbstractPhase<T> from_phase = null;
      AbstractPhase<T> to_phase = null;
      for (int i = 0; i < this.phase_list.Count; i++)
      {
        AbstractPhase<T> phase = this.phase_list[i];
        n += phase.duration_tick;
        if (n > target_tick)
        {
          from_phase = phase;
          to_phase = i + 1 < this.phase_list.Count ? this.phase_list[i + 1] : null;
          lerp_tick = target_tick - (n - phase.duration_tick);
          break;
        }
      }
      if (from_phase == null)
      {
        from_phase = this.phase_list[this.phase_list.Count - 1];
        to_phase = null;
        lerp_tick = 0;
      }
      T result = from_phase.Lerp(to_phase, lerp_tick);
      return result;
    }

    public virtual void DoSave(Hashtable dict)
    {
      duration_tick = 0;
      
      var phase_arrayList = phase_list.DoSaveList((phase, sub_dict) =>
      {
        phase.DoSave(sub_dict);
        duration_tick += phase.duration_tick;
      });

      dict["duration_tick"] = duration_tick;
      dict["is_loop"] = is_loop;
      dict["phase_arrayList"] = phase_arrayList;
    }

    public virtual void DoRestore(Hashtable dict)
    {
      phase_list.DoRestoreList(dict["phase_arrayList"] as ArrayList, (sub_dict) =>
      {
        AbstractPhase<T> phase = new AbstractPhase<T>();
        phase.DoRestore(sub_dict);
        return phase;
      });
      duration_tick = dict["duration_tick"].ToIntOrToDefault();
      is_loop = dict["is_loop"].ToBoolOrToDefault();

    }
  }
}