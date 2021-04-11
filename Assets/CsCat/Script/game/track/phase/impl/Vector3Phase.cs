using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CsCat
{
  public class Vector3Phase:AbstractPhase<Vector3>
  {
    public override Vector3 Lerp(AbstractPhase<Vector3> to_phase, int lerp_tick)
    {
      return this.Tween(to_phase, lerp_tick,
        (from_value, to_value, t) => { return Vector3.Lerp(from_value, to_value, t); });
    }

    public override void DoSave(Hashtable dict)
    {
      base.DoSave(dict);
      dict["value"] = value.ToString();
    }

    public override void DoRestore(Hashtable dict)
    {
      base.DoSave(dict);
      value = dict["value"].ToString().ToVector3();
    }
  }
}