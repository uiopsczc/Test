using System.Collections;
using UnityEngine;

namespace CsCat
{
	public class FloatPhase : AbstractPhase<float>
	{
		public override float Lerp(AbstractPhase<float> toPhase, int lerpTick)
		{
			return this.Tween(toPhase, lerpTick,
			  (fromValue, toValue, t) => Mathf.Lerp(fromValue, toValue, t));
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