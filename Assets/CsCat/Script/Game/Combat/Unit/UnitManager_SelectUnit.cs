using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CsCat
{
	public partial class UnitManager
	{
		public List<Unit> SelectUnit(Hashtable conditionDict)
		{
			var rangeInfo = conditionDict.Get<Hashtable>("range_info");
			IPosition originIPosition = conditionDict.Get<IPosition>("origin_iposition");
			var startPosition = originIPosition.GetPosition();
			var scope = conditionDict.Get<string>("scope");
			float maxDistance = rangeInfo.Get<float>("radius");
			if (rangeInfo.Get<string>("mode").Equals("rect"))
			{
				maxDistance = Math.Max(maxDistance, rangeInfo.Get<float>("height"));
				maxDistance = Math.Max(maxDistance, rangeInfo.Get<float>("width"));
			}

			string order = conditionDict.Get<string>("order");
			string faction = conditionDict.Get<string>("faction");
			var candidateList = conditionDict.Get<List<Unit>>("candidate_list");
			var isOnlyAttackable = conditionDict.Get<bool>("is_only_attackable");
			var isCanSelectHideUnit = conditionDict.Get<bool>("is_can_select_hide_unit");

			var targetUnitList = new List<Unit>();
			var matchFactionList = this.GetMatchFactionList(faction, scope);

			//有候选目标时，则在候选目标里选择，不考虑忽略阵营
			var checkUnitList = candidateList ?? this.GetFactionUnitList(matchFactionList);

			for (var i = 0; i < checkUnitList.Count; i++)
			{
				var unit = checkUnitList[i];
				if (!unit.IsDestroyed() && !unit.IsDead() && this.__CheckUnit(unit, originIPosition, rangeInfo, faction,
					scope,
					isOnlyAttackable, isCanSelectHideUnit))
					targetUnitList.Add(unit);
			}

			if ("distance".Equals(order) && !targetUnitList.IsNullOrEmpty())
				targetUnitList.QuickSort((a, b) => a.Distance(originIPosition) <= b.Distance(originIPosition));
			return targetUnitList;
		}


		public bool __CheckUnit(Unit unit, IPosition originIPosition, Hashtable rangeInfo, string faction, string scope,
		  bool isOnlyAttackable, bool isCanSelectHideUnit = false)
		{
			if ("技能物体".Equals(unit.cfgUnitData.type))
				return false;
			if (isOnlyAttackable)
				if (unit.IsInvincible())
					return false;

			if (!"all".Equals(scope))
			{
				if (scope.IsNullOrWhiteSpace() || !"friend".Equals(scope))
				{
					if (!this.CheckFaction(faction, unit.GetFaction(), "enemy"))
						return false;
				}
				else
				{
					if (!this.CheckFaction(faction, unit.GetFaction(), "friend"))
						return false;
				}
			}

			if (!isCanSelectHideUnit)
				if (unit.IsHide() && !unit.IsExpose())
					return false;

			if (originIPosition == null || rangeInfo == null)
				return false;

			if ("circle".Equals(rangeInfo.Get<string>("mode")))
			{
				float radius = rangeInfo.Get<float>("radius");
				if (!rangeInfo.ContainsKey("radius") || radius < 0)
					return false;
				if (unit.Distance(originIPosition) > radius)
					return false;
				var angle = rangeInfo.Get<float>("angle");
				if (!rangeInfo.ContainsKey("angle"))
					return true;
				var forward = rangeInfo.Get<Quaternion>("rotation").Forward();
				var orgPosition = originIPosition.GetPosition();
				var right = Quaternion.AngleAxis(90, Vector3.up) * forward;
				var dirR = (unit.GetPosition() + (right * radius)) - orgPosition;
				var dirL = (unit.GetPosition() + (-right * radius)) - orgPosition;
				return (Vector3.Angle(forward, dirL) < angle / 2) || (Vector3.Angle(forward, dirR) < angle / 2);
			}

			if ("rect".Equals(rangeInfo.Get<string>("mode")))
			{
				if (!rangeInfo.ContainsKey("height") || !rangeInfo.ContainsKey("width") ||
				    rangeInfo.Get<float>("height") < 0 || rangeInfo.Get<float>("width") < 0)
					return false;
				if (!rangeInfo.ContainsKey("rotation"))
					rangeInfo["rotation"] = default(Quaternion);
				var pos = unit.GetPosition();
				var orgPosition = originIPosition.GetPosition();
				pos = pos - orgPosition;
				pos = (rangeInfo.Get<Quaternion>("rotation").Inverse()) * pos;
				var unitRadius = unit.GetRadius();

				if (rangeInfo.Get<bool>("is_use_center"))
					return Math.Abs(pos.x) < rangeInfo.Get<float>("width") / 2 + unitRadius &&
					       Math.Abs(pos.z) < rangeInfo.Get<float>("height") / 2 + unitRadius;
				return Math.Abs(pos.x) < rangeInfo.Get<float>("width") + unitRadius &&
				       Math.Abs(pos.z) < rangeInfo.Get<float>("height") + unitRadius;
			}

			return false;
		}
	}
}