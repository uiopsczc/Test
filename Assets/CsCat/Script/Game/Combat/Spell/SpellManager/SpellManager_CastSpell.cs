using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CsCat
{
	public partial class SpellManager
	{
		// is_control 是否控制类技能
		public SpellBase CastSpell(Unit sourceUnit, string spellId, Unit targetUnit, Hashtable instanceArgDict = null,
		  bool isControl = false)
		{
			var (isCanCast, cfgSpellData, spellClass) =
			  this.CheckIsCanCast(sourceUnit, spellId, targetUnit, isControl);
			if (!isCanCast)
				return null;
			//开始释放技能
			var spell = this.AddChild(null, spellClass, sourceUnit, spellId, targetUnit, cfgSpellData,
			  instanceArgDict) as SpellBase;
			if ("正常".Equals(cfgSpellData.castType))
			{
				//当玩家是手动操作释放技能时，技能方向就以玩家的输入为准（但如果有目标则会以目标方向释放技能，无视输入）
				//当释放的技能类型是正常的话，则需停下来施法
				if (sourceUnit.currentAttack != null)
					this.BreakSpell(sourceUnit.currentAttack.GetGuid());
				Quaternion? rotation = null;
				var isNotFaceToTarget =
				  instanceArgDict?.Get<bool>("isNotFaceToTarget") ?? cfgSpellData.isNotFaceToTarget;
				var targetPosition = instanceArgDict?.Get<Vector3>("position") ?? targetUnit.GetPosition();
				if (targetUnit != null && (!isNotFaceToTarget || !isControl))
				{
					var dir = targetPosition - sourceUnit.GetPosition();
					rotation = Quaternion.LookRotation(dir);
					if (!rotation.Value.IsZero())
						sourceUnit.FaceTo(rotation.Value);
				}

				if (!cfgSpellData.isCanMoveWhileCast || !isControl)
					sourceUnit.MoveStop(rotation);
				sourceUnit.currentAttack = spell;
				sourceUnit.UpdateMixedStates();
			}

			if ("普攻".Equals(cfgSpellData.type))
				sourceUnit.NormalAttackStart();
			spell.Start();
			return spell;
		}


		public (bool isCanCast, CfgSpellData cfgSpellData, Type spellClass) CheckIsCanCast(Unit sourceUnit,
		  string spellId, Unit targetUnit, bool isControl)
		{
			var cfgSpellData = CfgSpell.Instance.GetById(spellId);
			Type spellClass = null;
			if (cfgSpellData == null)
			{
				LogCat.LogErrorFormat("spell_id(%d) is not exist!", spellId);
				return (false, null, null);
			}

			if (sourceUnit == null || (sourceUnit.IsDead() && !"触发".Equals(cfgSpellData.castType)))
				return (false, null, null);
			if (!sourceUnit.IsSpellCooldownOk(spellId))
				return (false, null, null);
			if (!sourceUnit.CanBreakCurrentSpell(spellId, cfgSpellData))
				return (false, null, null);
			var scope = cfgSpellData.targetType ?? "enemy";
			//如果是混乱则找任何可以攻击的人
			if (sourceUnit.IsConfused())
				scope = "all";
			var isOnlyAttackable = !"friend".Equals(scope);
			if (cfgSpellData.isNeedTarget)
			{
				if (targetUnit == null)
					return (false, null, null);
				Hashtable rangeInfo = new Hashtable {["mode"] = "circle", ["radius"] = cfgSpellData.range};
				if (!Client.instance.combat.unitManager.CheckUnit(targetUnit,
				  sourceUnit.ToUnitPosition(), rangeInfo, sourceUnit.GetFaction(), scope,
				  isOnlyAttackable))
					return (false, null, null);
			}

			spellClass = TypeUtil.GetType(cfgSpellData.classPathCs);
			if (spellClass.IsHasMethod("CheckIsCanCast") &&
				!spellClass.InvokeMethod<bool>("CheckIsCanCast", false, sourceUnit, spellId, targetUnit, cfgSpellData,
				  isControl)
			) //静态方法CheckIsCanCast
				return (false, null, null);
			return (true, cfgSpellData, spellClass);
		}

		public List<Unit> RecommendCast(Unit sourceUnit, string spellId, Unit targetUnit, bool isControl)
		{
			if (sourceUnit == null || sourceUnit.IsDead())
				return null;
			if (targetUnit == null)
				return null;
			var cfgSpellData = CfgSpell.Instance.GetById(spellId);
			var spellClass = TypeUtil.GetType(cfgSpellData.classPathCs);
			if (spellClass == null)
			{
				LogCat.error("spell code is not exist: ", cfgSpellData.classPathCs);
				return null;
			}

			return this._IsUnitMatchCondition(sourceUnit, targetUnit, isControl, cfgSpellData, spellClass) ? new List<Unit>() { targetUnit } : null;
		}

		public List<Unit> RecommendCast(Unit sourceUnit, string spellId, List<Unit> targetUnitList, bool isControl)
		{
			if (sourceUnit == null || sourceUnit.IsDead())
				return null;
			if (targetUnitList == null)
				return null;
			var cfgSpellData = CfgSpell.Instance.GetById(spellId);
			var spellClass = TypeUtil.GetType(cfgSpellData.classPathCs);
			if (spellClass == null)
			{
				LogCat.error("spell code is not exist: ", cfgSpellData.classPathCs);
				return null;
			}

			List<Unit> newTargetUnitList = new List<Unit>();
			for (var i = 0; i < targetUnitList.Count; i++)
			{
				var targetUnit = targetUnitList[i];
				if (this._IsUnitMatchCondition(sourceUnit, targetUnit, isControl, cfgSpellData, spellClass))
					newTargetUnitList.Add(targetUnit);
			}

			return newTargetUnitList;
		}

		public List<Unit> RecommendSpellRule(Unit sourceUnit, Unit targetUnit,
		  CfgSpellData cfgSpellData, Vector3 originPosition, List<Unit> targetUnitList = null)
		{
			//当前敌人
			//随机x个敌人
			//生命最低的x个人敌人
			//全体敌人
			//自己
			//随机x个队友
			//生命最低的x个队友
			//全体队友
			//召唤单位
			//场上所有人(不分敌友)
			if (targetUnit == null)
				return null;
			if (cfgSpellData._selectUnitArgDict.IsNullOrEmpty())
				return targetUnitList ?? new List<Unit>() { targetUnit };

			var selectUnitArgDict = DoerAttrParserUtil.ConvertTableWithTypeString(cfgSpellData._selectUnitArgDict);
			var selectUnitFaction = selectUnitArgDict.Get<string>("selectUnitFaction");
			var selectUnitCount = selectUnitArgDict.GetOrGetDefault2<int>("selectUnitCount", () => 1000);
			var scope = SpellConst.Select_Unit_Faction_Dict[selectUnitFaction];

			var rangeInfo = new Hashtable();
			rangeInfo["mode"] = "circle";
			rangeInfo["radius"] = cfgSpellData.range;
			var conditionDict = new Hashtable();

			conditionDict["order"] = "distance";
			conditionDict["origin"] = originPosition;
			conditionDict["faction"] = sourceUnit.GetFaction();
			conditionDict["scope"] = scope;
			conditionDict["rangeInfo"] = rangeInfo;
			targetUnitList = targetUnitList ?? Client.instance.combat.unitManager.SelectUnit(conditionDict);

			var count = selectUnitCount;
			var newTargetList = new List<Unit>();
			//TODO select_unit
			//TODO select_unit
			newTargetList = targetUnitList.Sub(0, Math.Min(targetUnitList.Count, count));
			return newTargetList.Count == 0 ? new List<Unit>() { targetUnit } : newTargetList;
		}

		public bool _IsUnitMatchCondition(Unit sourceUnit, Unit targetUnit, bool isControl,
		  CfgSpellData cfgSpellData,
		  Type spellClass)
		{
			if (targetUnit.IsDead())
				return false;
			if (!sourceUnit.IsConfused())
			{
				if ("enemy".Equals(cfgSpellData.targetType) && targetUnit.IsInvincible())
					return false;
				if (cfgSpellData.targetType.IsNullOrWhiteSpace() && !"all".Equals(cfgSpellData.targetType))
					if (!Client.instance.combat.unitManager.CheckFaction(sourceUnit.GetFaction(), targetUnit.GetFaction(),
					  cfgSpellData.targetType))
						return false;
			}

			if (!spellClass.InvokeMethod<bool>("IsUnitMatchCondition", false, sourceUnit, cfgSpellData.id, targetUnit,
			  cfgSpellData, isControl))
				return false;
			return true;
		}
	}
}