using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CsCat
{
	public class Vector2Phase : AbstractPhase<Vector2>
	{
		public override Vector2 Lerp(AbstractPhase<Vector2> toPhase, int lerpTick)
		{
			return this.Tween(toPhase, lerpTick,
			  (fromValue, toValue, t) => Vector2.Lerp(fromValue, toValue, t));
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