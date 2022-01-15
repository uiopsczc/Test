using System.Collections;
using UnityEngine;

namespace CsCat
{
	public class ColliderListInfoPhase : AbstractPhase<ColliderListInfo>
	{
		public override ColliderListInfo Lerp(AbstractPhase<ColliderListInfo> toPhase, int lerpTick)
		{
			return this.Tween(toPhase, lerpTick,
				(fromValue, toValue, t) =>
				{
					ColliderListInfo colliderListInfo = new ColliderListInfo();
					colliderListInfo.center = Vector3.Lerp(fromValue.center, toValue.center, t);
					colliderListInfo.atkBoxList = fromValue.atkBoxList;
					colliderListInfo.hitBoxList = fromValue.hitBoxList;
					return colliderListInfo;
				});
		}

		public override void DoSave(Hashtable dict)
		{
			base.DoSave(dict);
			dict["value"] = new Hashtable();
			value.DoSave(dict["value"] as Hashtable);
		}

		public override void DoRestore(Hashtable dict)
		{
			base.DoSave(dict);
			value.DoRestore(dict["value"] as Hashtable);
		}
	}
}