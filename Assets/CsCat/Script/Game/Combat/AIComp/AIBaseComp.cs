using System;
using System.Collections.Generic;
using UnityEngine;

namespace CsCat
{
	public partial class AIBaseComp : TickObject
	{
		private Unit unit;
		private float useSkillInterval = 6; //ai攻击间隔
		private float lastUseSkillTime;
		private int useSkillNextIndex;

		private Vector3 __findTargetUnitPos;
		private Vector3 __findStandPos;

		public void Init(Unit unit)
		{
			base.Init();
			this.unit = unit;
			this.lastUseSkillTime = CombatUtil.GetTime();
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
			if (CombatUtil.GetTime() - this.lastUseSkillTime >= this.useSkillInterval)
			{
				for (int i = 0; i < this.unit.skillIdList.Count; i++)
				{
					this.useSkillNextIndex = this.useSkillNextIndex + 1;
					if (this.useSkillNextIndex >= this.unit.skillIdList.Count)
						this.useSkillNextIndex = 0;
					var skillId = this.unit.skillIdList[this.useSkillNextIndex];
					//如果当前技能不能施放，则放下一个。
					//且保存着已经施放技能index在self.use_spell_next_index，
					//以使每个技能都有机会施放
					if (this.TryCastSkill(skillId, targetUnitList).Item1)
					{
						//成功施放了技能才记录最后一次使用技能的时间，以保证三个技能都不能施放时，
						//下一帧继续尝试施放
						this.lastUseSkillTime = CombatUtil.GetTime();
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
			if (spellId.IsNullOrWhiteSpace() || !this.unit.IsCanCastSkill())
				return (false, false);
			//如果没到释放的时机，直接就返回false
			if (!this.unit.IsTimeToCastSpell(spellId))
				return (false, false);
			var cfgSpellData = CfgSpell.Instance.get_by_id(spellId);
			var attackRange = cfgSpellData.range;
			var recommendTargetUnitList = Client.instance.combat.spellManager.RecommendCast(this.unit, spellId,
			  targetUnitList,
			  isCanAttackWithoutSeeingTarget);
			if (recommendTargetUnitList.IsNullOrEmpty())
				return (false, false);

			var targetUnit = recommendTargetUnitList[0];
			if (isCanAttackWithoutSeeingTarget)
			{
				//玩家可以没有见到目标就放技能
				return (this.unit.CastSpell(spellId, targetUnit, isCanAttackWithoutSeeingTarget) != null, false);
			}

			//ai需要有目标才放技能放技能
			//  如果技能填了不强制面向目标，则控制的时候不走去攻击范围，但ai还是会走去攻击范围
			if (attackRange == 0 || (isCanAttackWithoutSeeingTarget && cfgSpellData.is_not_face_to_target) ||
				!this.IsNeedGotoAttackRange(targetUnit, attackRange))
				return (this.unit.CastSpell(spellId, targetUnit, isCanAttackWithoutSeeingTarget) != null, false);
			return (false, false);
		}

		public (bool, bool) TryNormalAttack(List<Unit> targetUnitList, bool isCanAttackWithoutSeeingTarget = false)
		{
			var attackId = this.unit.GetNormalAttackId();
			if (attackId.IsNullOrWhiteSpace() || !this.unit.IsCanNormalAttack())
				return (false, false);
			var cfgSpellData = CfgSpell.Instance.get_by_id(attackId);
			var attackRange = cfgSpellData.range;
			var recommendTargetUnitList = Client.instance.combat.spellManager.RecommendCast(this.unit, attackId,
			  targetUnitList,
			  isCanAttackWithoutSeeingTarget);
			if (recommendTargetUnitList.IsNullOrEmpty())
				return (false, false);

			var targetUnit = recommendTargetUnitList[0];
			if (isCanAttackWithoutSeeingTarget)
			{
				//玩家可以没有见到目标就放技能
				return (this.unit.NormalAttack(targetUnit) != null, false);
			}

			//ai需要有目标才放技能放技能
			//  如果有目标，但是没到释放的时机，走去攻击区域
			if (!this.unit.IsTimeToCastSpell(attackId))
			{
				this.IsNeedGotoAttackRange(targetUnit, attackRange);
				return (true, true);
			}

			if (attackRange == 0 || !this.IsNeedGotoAttackRange(targetUnit, attackRange))
			{
				this.unit.LookAt(targetUnit, AnimationNameConst.attack);
				return (this.unit.NormalAttack(targetUnit) != null, false);
			}

			return (true, true);
		}


		public bool IsNeedGotoAttackRange(Unit target_unit, float attack_range)
		{
			var unit = this.unit;
			var distance = unit.Distance(target_unit);
			if (unit.isMoveOccupy && distance <= attack_range)
				return false;
			var new_target_pos = this.__FindPlaceInAttackRange(target_unit, attack_range);
			if (new_target_pos != null)
			{
				//找到空位
				if (Vector3.Distance(new_target_pos.Value, unit.GetPosition()) < 0.01f)
				{
					unit.isMoveOccupy = true;
					unit.MoveStop();
					return false;
				}
				else
				{
					unit.Move(new_target_pos.Value);
					return true;
				}
			}
			else
			{
				// 找不到空位时，已在范围内直接可攻击，不再范围内直接朝原目标点前进
				if (distance <= attack_range)
				{
					unit.isMoveOccupy = true;
					return false;
				}

				return true;
			}
		}

		private bool __HasConflict(Vector3 pos, float radius)
		{
			foreach (var unit in Client.instance.combat.unitManager.GetUnitDict().Values)
			{
				if (unit != this.unit && unit.isMoveOccupy && unit.Distance(pos) < radius * 0.9f)
					return true;
			}

			return false;
		}

		private Vector3? __FindPlaceInAttackRange(Unit target_unit, float attack_range)
		{
			var unit = this.unit;
			var self_unit_pos = unit.GetPosition();
			var self_unit_radius = unit.GetRadius();
			var target_unit_pos = target_unit.GetPosition();
			var target_unit_radius = target_unit.GetRadius();
			var both_radius = self_unit_radius + target_unit_radius;

			//检查位置是否可以站在这个位置
			Func<Vector3, bool> __CanStand = pos =>
			{
				if (Vector3.Distance(target_unit_pos, pos) > attack_range + both_radius
			  || this.__HasConflict(pos, self_unit_radius))
					return false;
				this.__findTargetUnitPos = target_unit_pos;
				this.__findStandPos = pos;
				return true;
			};

			var distance = Vector3.Distance(self_unit_pos, target_unit_pos);
			//目前位置就可以站位
			if (distance < attack_range + both_radius && !this.__HasConflict(self_unit_pos, self_unit_radius))
				return self_unit_pos;
			if (this.__findTargetUnitPos == target_unit.GetPosition() &&
				!this.__HasConflict(this.__findStandPos, self_unit_radius))
				return this.__findStandPos;
			var base_distance = Math.Min(distance, both_radius + attack_range - 0.1f);
			var base_dir = (self_unit_pos - target_unit_pos).normalized;
			var base_pos = target_unit_pos + base_dir * base_distance;
			//测试直线走过去的位置
			if (__CanStand(base_pos))
				return base_pos;
			float angle = 0;
			//计算 delta_angle
			// 原理见图片 Assets/ 代码辅助资料 / AIComp_001.JPG
			// 已知道三边（base_distance,base_distance,self_unit_radius * 2），求角度(公式为：a ^ 2 = b ^ 2 + c ^ 2 - 2bc * cosA)
			var a = base_distance;
			var b = base_distance;
			var c = self_unit_radius * 2;
			var delta_angle = (float)Math.Acos((a * a - b * b - c * c) / (-2 * b * c)) * Mathf.Rad2Deg;
			Vector3 left_pos, right_pos;
			while (true)
			{
				angle = angle + delta_angle;
				if (angle >= 180)
					break;
				left_pos = target_unit_pos + Quaternion.AngleAxis(angle, Vector3.up) * base_dir * base_distance;
				if (__CanStand(left_pos))
					return left_pos;
				right_pos = target_unit_pos + Quaternion.AngleAxis(-angle, Vector3.up) * base_dir * base_distance;
				if (__CanStand(right_pos))
					return right_pos;
			}

			return null;
		}
	}
}