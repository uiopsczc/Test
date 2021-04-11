using System.Collections;
using UnityEngine;

namespace CsCat
{
  public class FloatPhase : AbstractPhase<float>
  {
    public override float Lerp(AbstractPhase<float> to_phase, int lerp_tick)
    {
      return this.Tween(to_phase, lerp_tick,
        (from_value, to_value, t) => { return Mathf.Lerp(from_value, to_value, t); });
    }

    public override void DoSave(Hashtable dict)
    {
      base.DoSave(dict);
      dict["value"] = value;
    }

    public override void DoRestore(Hashtable dict)
    {
      base.DoSave(dict);
      value = dict["value"].ToFloatOrToDefault();
    }
  }
}