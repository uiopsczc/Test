using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CsCat
{
	public class Vector3Phase : AbstractPhase<Vector3>
	{
		public override Vector3 Lerp(AbstractPhase<Vector3> toPhase, int lerpTick)
		{
			return this.Tween(toPhase, lerpTick,
			  (fromValue, toValue, t) => Vector3.Lerp(fromValue, toValue, t));
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