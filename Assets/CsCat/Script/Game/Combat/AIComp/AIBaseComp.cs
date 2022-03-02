using System;
using System.Collections.Generic;
using UnityEngine;

namespace CsCat
{
	public partial class AIBaseComp : TickObject
	{
		private Unit _unit;
		private float _useSkillInterval = 6; //ai攻击间隔
		private float _lastUseSkillTime;
		private int _useSkillNextIndex;

		private Vector3 _findTargetUnitPos;
		private Vector3 _findStandPos;

		public void Init(Unit unit)
		{
			base.Init();
			this._unit = unit;
			this._lastUseSkillTime = CombatUtil.GetTime();
		}

		protected override void _Update(float deltaTime = 0, float unscaledDeltaTime = 0)
		{
			base._Update(deltaTime, unscaledDeltaTime);
			this.DoBehavior(deltaTime);
		}

		public virtual void DoBehavior(float deltaTime)
		{
		}

		public (bool, bool) Attack(List<Unit> targetUnitList, bool isNoNormalAttack)
		{
			//先尝试释放技能
			if (CombatUtil.GetTime() - this._lastUseSkillTime >= this._useSkillInterval)
			{
				for (int i = 0; i < this._unit.skillIdList.Count; i++)
				{
					this._useSkillNextIndex = this._useSkillNextIndex + 1;
					if (this._useSkillNextIndex >= this._unit.skillIdList.Count)
						this._useSkillNextIndex = 0;
					var skillId = this._unit.skillIdList[this._useSkillNextIndex];
					//如果当前技能不能施放，则放下一个。
					//且保存着已经施放技能index在self.use_spell_next_index，
					//以使每个技能都有机会施放
					if (this.TryCastSkill(skillId, targetUnitList).Item1)
					{
						//成功施放了技能才记录最后一次使用技能的时间，以保证三个技能都不能施放时，
						//下一帧继续尝试施放
						this._lastUseSkillTime = CombatUtil.GetTime();
						return (true, false);
					}
				}
			}

			if (isNoNormalAttack)
				return (false, false);
			//再尝试普攻
			return this.TryNormalAttack(targetUnitList);
		}

		// 一般情况不要直接使用这个方法
		// 返回参数第二个为true时表示“不是真的成功施放了技能”，只有第一个参数返回true时，第二个参数才有意义
		// 一般情况下请不要使用第二个参数作判断
		// 第一个参数返回true时，请注意第二个参数值的返回
		public (bool, bool) TryCastSkill(string spellId, List<Unit> targetUnitList,
		  bool isCanAttackWithoutSeeingTarget = false)
		{
			if (spellId.IsNullOrWhiteSpace() || !this._unit.IsCanCastSkill())
				return (false, false);
			//如果没到释放的时机，直接就返回false
			if (!this._unit.IsTimeToCastSpell(spellId))
				return (false, false);
			var cfgSpellData = CfgSpell.Instance.GetById(spellId);
			var attackRange = cfgSpellData.range;
			var recommendTargetUnitList = Client.instance.combat.spellManager.RecommendCast(this._unit, spellId,
			  targetUnitList,
			  isCanAttackWithoutSeeingTarget);
			if (recommendTargetUnitList.IsNullOrEmpty())
				return (false, false);

			var targetUnit = recommendTargetUnitList[0];
			if (isCanAttackWithoutSeeingTarget)
			{
				//玩家可以没有见到目标就放技能
				return (this._unit.CastSpell(spellId, targetUnit, isCanAttackWithoutSeeingTarget) != null, false);
			}

			//ai需要有目标才放技能放技能
			//  如果技能填了不强制面向目标，则控制的时候不走去攻击范围，但ai还是会走去攻击范围
			if (attackRange == 0 || (isCanAttackWithoutSeeingTarget && cfgSpellData.isNotFaceToTarget) ||
				!this.IsNeedGotoAttackRange(targetUnit, attackRange))
				return (this._unit.CastSpell(spellId, targetUnit, isCanAttackWithoutSeeingTarget) != null, false);
			return (false, false);
		}

		public (bool, bool) TryNormalAttack(List<Unit> targetUnitList, bool isCanAttackWithoutSeeingTarget = false)
		{
			var attackId = this._unit.GetNormalAttackId();
			if (attackId.IsNullOrWhiteSpace() || !this._unit.IsCanNormalAttack())
				return (false, false);
			var cfgSpellData = CfgSpell.Instance.GetById(attackId);
			var attackRange = cfgSpellData.range;
			var recommendTargetUnitList = Client.instance.combat.spellManager.RecommendCast(this._unit, attackId,
			  targetUnitList,
			  isCanAttackWithoutSeeingTarget);
			if (recommendTargetUnitList.IsNullOrEmpty())
				return (false, false);

			var targetUnit = recommendTargetUnitList[0];
			if (isCanAttackWithoutSeeingTarget)
			{
				//玩家可以没有见到目标就放技能
				return (this._unit.NormalAttack(targetUnit) != null, false);
			}

			//ai需要有目标才放技能放技能
			//  如果有目标，但是没到释放的时机，走去攻击区域
			if (!this._unit.IsTimeToCastSpell(attackId))
			{
				this.IsNeedGotoAttackRange(targetUnit, attackRange);
				return (true, true);
			}

			if (attackRange == 0 || !this.IsNeedGotoAttackRange(targetUnit, attackRange))
			{
				this._unit.LookAt(targetUnit, AnimationNameConst.attack);
				return (this._unit.NormalAttack(targetUnit) != null, false);
			}

			return (true, true);
		}


		public bool IsNeedGotoAttackRange(Unit targetUnit, float attackRange)
		{
			var unit = this._unit;
			var distance = unit.Distance(targetUnit);
			if (unit.isMoveOccupy && distance <= attackRange)
				return false;
			var newTargetPos = this._FindPlaceInAttackRange(targetUnit, attackRange);
			if (newTargetPos != null)
			{
				//找到空位
				if (Vector3.Distance(newTargetPos.Value, unit.GetPosition()) < 0.01f)
				{
					unit.isMoveOccupy = true;
					unit.MoveStop();
					return false;
				}

				unit.Move(newTargetPos.Value);
				return true;
			}

			// 找不到空位时，已在范围内直接可攻击，不再范围内直接朝原目标点前进
			if (distance <= attackRange)
			{
				unit.isMoveOccupy = true;
				return false;
			}

			return true;
		}

		private bool _IsHasConflict(Vector3 pos, float radius)
		{
			foreach (var keyValue in Client.instance.combat.unitManager.GetUnitDict())
			{
				var unit = keyValue.Value;
				if (unit != this._unit && unit.isMoveOccupy && unit.Distance(pos) < radius * 0.9f)
					return true;
			}
			return false;
		}

		private Vector3? _FindPlaceInAttackRange(Unit targetUnit, float attackRange)
		{
			var unit = this._unit;
			var selfUnitPos = unit.GetPosition();
			var selfUnitRadius = unit.GetRadius();
			var targetUnitPos = targetUnit.GetPosition();
			var targetUnitRadius = targetUnit.GetRadius();
			var radiusOfBoth = selfUnitRadius + targetUnitRadius;

			//检查位置是否可以站在这个位置
			bool _IsCanStand(Vector3 pos)
			{
				if (Vector3.Distance(targetUnitPos, pos) > attackRange + radiusOfBoth || this._IsHasConflict(pos, selfUnitRadius)) return false;
				this._findTargetUnitPos = targetUnitPos;
				this._findStandPos = pos;
				return true;
			}

			var distance = Vector3.Distance(selfUnitPos, targetUnitPos);
			//目前位置就可以站位
			if (distance < attackRange + radiusOfBoth && !this._IsHasConflict(selfUnitPos, selfUnitRadius))
				return selfUnitPos;
			if (this._findTargetUnitPos == targetUnit.GetPosition() &&
				!this._IsHasConflict(this._findStandPos, selfUnitRadius))
				return this._findStandPos;
			var baseDistance = Math.Min(distance, radiusOfBoth + attackRange - 0.1f);
			var baseDir = (selfUnitPos - targetUnitPos).normalized;
			var basePos = targetUnitPos + baseDir * baseDistance;
			//测试直线走过去的位置
			if (_IsCanStand(basePos))
				return basePos;
			float angle = 0;
			//计算 delta_angle
			// 原理见图片 Assets/ 代码辅助资料 / AIComp_001.JPG
			// 已知道三边（base_distance,base_distance,self_unit_radius * 2），求角度(公式为：a ^ 2 = b ^ 2 + c ^ 2 - 2bc * cosA)
			var a = baseDistance;
			var b = baseDistance;
			var c = selfUnitRadius * 2;
			var deltaAngle = (float)Math.Acos((a * a - b * b - c * c) / (-2 * b * c)) * Mathf.Rad2Deg;
			Vector3 leftPos, rightPos;
			while (true)
			{
				angle = angle + deltaAngle;
				if (angle >= 180)
					break;
				leftPos = targetUnitPos + Quaternion.AngleAxis(angle, Vector3.up) * baseDir * baseDistance;
				if (_IsCanStand(leftPos))
					return leftPos;
				rightPos = targetUnitPos + Quaternion.AngleAxis(-angle, Vector3.up) * baseDir * baseDistance;
				if (_IsCanStand(rightPos))
					return rightPos;
			}

			return null;
		}
	}
}