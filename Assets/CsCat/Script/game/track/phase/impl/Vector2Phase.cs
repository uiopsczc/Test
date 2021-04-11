using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CsCat
{
  public class Vector2Phase: AbstractPhase<Vector2>
  {
    public override Vector2 Lerp(AbstractPhase<Vector2> to_phase, int lerp_tick)
    {
      return this.Tween(to_phase, lerp_tick,
        (from_value, to_value, t) => { return Vector2.Lerp(from_value, to_value, t); });
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