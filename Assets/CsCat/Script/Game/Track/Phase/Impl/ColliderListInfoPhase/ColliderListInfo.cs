using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CsCat
{
	public class ColliderListInfo
	{
		public Vector3 center;

		public List<AABBBox> atkBoxList = new List<AABBBox>();

		public List<AABBBox> hitBoxList = new List<AABBBox>();


		public void DoSave(Hashtable dict)
		{
			dict["center"] = center.ToString();
			ArrayList atkBoxArrayList = atkBoxList.DoSaveList((atkBox, subDict) => atkBox.DoSave(subDict));
			ArrayList hitBoxArrayList = hitBoxList.DoSaveList((hitBox, subDict) => hitBox.DoSave(subDict));
			dict["atk_box_arrayList"] = atkBoxArrayList;
			dict["hit_box_arrayList"] = hitBoxArrayList;
		}

		public void DoRestore(Hashtable dict)
		{
			center = dict["center"].ToString().ToVector3();

			atkBoxList.DoRestoreList(dict["atk_box_arrayList"] as ArrayList, (subDict) =>
			{
				AABBBox atkBox = new AABBBox();
				atkBox.DoRestore(subDict);
				return atkBox;
			});

			hitBoxList.DoRestoreList(dict["hit_box_arrayList"] as ArrayList, (subDict) =>
			{
				AABBBox hitBox = new AABBBox();
				hitBox.DoRestore(subDict);
				return hitBox;
			});
		}
	}
}