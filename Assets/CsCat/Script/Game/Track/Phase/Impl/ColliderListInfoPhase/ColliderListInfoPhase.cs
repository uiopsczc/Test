using System.Collections;
using UnityEngine;

namespace CsCat
{
	public class ColliderListInfoPhase : AbstractPhase<ColliderListInfo>
	{
		public override ColliderListInfo Lerp(AbstractPhase<ColliderListInfo> to_phase, int lerp_tick)
		{
			return this.Tween(to_phase, lerp_tick,
			  (from_value, to_value, t) =>
			  {
				  ColliderListInfo colliderListInfo = new ColliderListInfo();
				  colliderListInfo.center = Vector3.Lerp(from_value.center, to_value.center, t);
				  colliderListInfo.atk_box_list = from_value.atk_box_list;
				  colliderListInfo.hit_box_list = from_value.hit_box_list;
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