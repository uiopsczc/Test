using System;
using System.Collections.Generic;
using UnityEngine;

namespace CsCat
{
	public partial class Unit
	{
		private string _lastAttackId;
		private ComboInfo _normalAttackComboInfo;
		private List<string> _normalAttackIdList;
		private readonly Dictionary<string, SpellInfo> spellInfoDict = new Dictionary<string, SpellInfo>();

		public SpellBase currentAttack;
		public bool isNormalAttacking;
		public List<string> skillIdList;

		//////////////////////////////////////////////////////////////////////
		// 普攻相关
		//////////////////////////////////////////////////////////////////////
		public void AddNormalAttack(string normalAttackId)
		{
			if (normalAttackId.IsNullOrWhiteSpace())
				return;
			_normalAttackIdList.Add(normalAttackId);
			this.InitSpellInfo(normalAttackId);
			this.AddPassiveBuffOfSpell(normalAttackId);
		}

		public string GetNormalAttackId()
		{
			var nextIndex = this._normalAttackComboInfo.nextIndex;
			if (CombatUtil.GetTime() > this._normalAttackComboInfo.nextTime ||
				!this._normalAttackIdList.ContainsIndex(this._normalAttackComboInfo.nextIndex)
			)
				nextIndex = 0;

			var normalAttackId = this._normalAttackIdList[nextIndex];
			return normalAttackId;
		}


		public SpellBase NormalAttack(Unit targetUnit)
		{
			var normalAttackId = this.GetNormalAttackId();
			var normalAttack =
			  Client.instance.combat.spellManager.CastSpell(this, normalAttackId, targetUnit, null, true);
			if (normalAttack != null)
				this._lastAttackId = normalAttackId;
			return normalAttack;
		}

		public void NormalAttackStart()
		{
			this._normalAttackComboInfo.nextTime =
			  CombatUtil.GetTime() + ComboConst.Normal_Attack_Combo_Max_Duration; // 1秒间隔触发combo
			this._normalAttackComboInfo.nextIndex = this._normalAttackComboInfo.nextIndex + 1;
			this.isNormalAttacking = true;
		}

		public void NormalAttackFinish()
		{
			this._normalAttackComboInfo.nextTime = CombatUtil.GetTime() + 0.2f;
			this.isNormalAttacking = true;
		}

		//////////////////////////////////////////////////////////////////////
		// 技能相关
		//////////////////////////////////////////////////////////////////////
		public void AddSkill(string skillId)
		{
			if (skillId.IsNullOrWhiteSpace())
				return;
			this.skillIdList.Add(skillId);
			this.InitSpellInfo(skillId);
			this.AddPassiveBuffOfSpell(skillId);
		}

		//is_control 是否是控制类技能
		public SpellBase CastSkillByIndex(int index, Unit targetUnit, bool isControl)
		{
			var skillId = this.skillIdList[index];
			if (skillId.IsNullOrWhiteSpace())
			{
				LogCat.error("index error ", index);
				return null;
			}

			return this.CastSpell(skillId, targetUnit, isControl);
		}

		//////////////////////////////////////////////////////////////////////
		// spell相关
		//////////////////////////////////////////////////////////////////////
		public void InitSpellInfo(string spellId, float cooldownPct = 0)
		{
			var spellInfo = new SpellInfo();
			this.spellInfoDict[spellId] = spellInfo;
			spellInfo.cooldownRate = 1 - (this.GetCalcPropValue("技能冷却减少百分比"));
			this.SetSpellInfoCooldown(spellId, cooldownPct);
		}

		public float GetSpellCooldownRate(string spellId)
		{
			var cfgSpellData = CfgSpell.Instance.GetById(spellId);
			if ("普攻".Equals(cfgSpellData.type))
				return 1 / (1 + this.GetCalcPropValue("攻击速度"));
			return 1 - this.GetCalcPropValue("技能冷却减少百分比");
		}

		public List<string> GetSpellIdList(string spellId)
		{
			var cfgSpellData = CfgSpell.Instance.GetById(spellId);
			if ("普攻".Equals(cfgSpellData.type))
				return this._normalAttackIdList;
			return this.skillIdList;
		}


		public void SetSpellInfoCooldown(string spellId, float cooldownPct = 0)
		{
			var cfgSpellData = CfgSpell.Instance.GetById(spellId);
			var spellInfo = this.spellInfoDict[spellId];
			spellInfo.cooldownRate = this.GetSpellCooldownRate(spellId);
			spellInfo.cooldownRemainDuration = cfgSpellData.cooldownDuration * spellInfo.cooldownRate * cooldownPct;
		}

		public void AddPassiveBuffOfSpell(string spellId)
		{
			var cfgSpellData = CfgSpell.Instance.GetById(spellId);
			var passiveBuffIds = cfgSpellData.passiveBuffIds;
			if (!passiveBuffIds.IsNullOrEmpty())
			{
				for (var i = 0; i < passiveBuffIds.Length; i++)
				{
					var passiveBuffId = passiveBuffIds[i];
					this.buffManager.AddBuff(passiveBuffId, this);
				}
			}
		}

		public void RemoveSpell(string spellId)
		{
			List<string> spellIdList = this.GetSpellIdList(spellId);
			spellIdList.Remove(spellId);
			this.spellInfoDict.Remove(spellId);
			this.RemovePassiveBuffOfSpell(spellId);
		}

		public void RemovePassiveBuffOfSpell(string spellId)
		{
			var cfgSpellData = CfgSpell.Instance.GetById(spellId);
			var passiveBuffIds = cfgSpellData.passiveBuffIds;
			if (!passiveBuffIds.IsNullOrEmpty())
			{
				for (var i = 0; i < passiveBuffIds.Length; i++)
				{
					var passiveBuffId = passiveBuffIds[i];
					this.buffManager.RemoveBuff(passiveBuffId, this.GetGuid());
				}
			}
		}

		//替换单位技能
		public void ReplaceSpell(string oldSpellId, string newSpellId, bool isResetCooldownRemainDuration = false)
		{
			var spellIdList = this.GetSpellIdList(oldSpellId);
			int index = spellIdList.IndexOf(oldSpellId);
			spellIdList[index] = newSpellId;

			//更新cooldown
			float cooldownCurPct = 0;
			if (!isResetCooldownRemainDuration)
				cooldownCurPct = this.spellInfoDict[oldSpellId].GetCooldownPct();
			this.InitSpellInfo(newSpellId, cooldownCurPct);
			this.spellInfoDict.Remove(oldSpellId);

			//删除原技能被动buff
			this.RemovePassiveBuffOfSpell(oldSpellId);
			// 添加新技能被动buff
			this.AddPassiveBuffOfSpell(newSpellId);
		}

		//改变技能CD
		private void OnSpellCooldownRateChange()
		{
			foreach (var keyValue in this.spellInfoDict)
			{
				var spellId = keyValue.Key;
				var spellInfo = spellInfoDict[spellId];
				var cfgSpellData = CfgSpell.Instance.GetById(spellId);
				var cooldownOldRate = spellInfo.cooldownRate;
				var cooldownDuration = cfgSpellData.cooldownDuration;
				if (cooldownDuration > 0)
				{
					var newRate = this.GetSpellCooldownRate(spellId);
					if (spellInfo.cooldownRemainDuration <= 0)
						spellInfo.cooldownRemainDuration = 0;
					else
					{
						//var cooldown_cur_pct = spellInfo.cooldown_remain_duration / (cooldown_duration * cooldown_last_rate)
						//var cooldown_remain_duration = cooldown_cur_pct * (cooldown_duration * new_rate)
						spellInfo.cooldownRemainDuration = spellInfo.cooldownRemainDuration * newRate / cooldownOldRate;
					}

					spellInfo.cooldownRate = newRate;
				}
			}
		}

		public void ReduceSpellCooldown(float deltaTime)
		{
			foreach (var keyValue in this.spellInfoDict)
			{
				var spellInfo = keyValue.Value;
				if (spellInfo.cooldownRemainDuration > 0)
					spellInfo.cooldownRemainDuration = Math.Max(0, spellInfo.cooldownRemainDuration - deltaTime);
			}
		}

		public bool IsSpellCooldownOk(string spell_id)
		{
			return this.spellInfoDict[spell_id].cooldownRemainDuration == 0;
		}


		//is_control 是否是控制类技能
		public SpellBase CastSpell(string spellId, Unit targetUnit, bool isControl)
		{
			var spell = Client.instance.combat.spellManager.CastSpell(this, spellId, targetUnit, null, isControl);
			if (spell != null)
				this._lastAttackId = spellId;
			return spell;
		}

		public bool CanBreakCurrentSpell(string newSpellId, CfgSpellData newCfgSpellData = null)
		{
			if (this.currentAttack == null)
				return true;

			newCfgSpellData = newCfgSpellData ?? CfgSpell.Instance.GetById(newSpellId);
			if (("法术".Equals(newCfgSpellData.type) && "普攻".Equals(this.currentAttack.cfgSpellData.type)) //法术可以打断普攻
				|| "触发".Equals(newCfgSpellData.castType))
				return true;
			return this.currentAttack.isPastBreakTime;
		}

		//检查是否到时间可以放技能1、是否能打断当前技能2、技能cd是否到
		public bool IsTimeToCastSpell(string spellId)
		{
			var cfgSpellData = CfgSpell.Instance.GetById(spellId);
			if (!this.CanBreakCurrentSpell(spellId, cfgSpellData))
				return false;
			if (!this.IsSpellCooldownOk(spellId))
				return false;
			return true;
		}

		public bool IsInSpellRange(Unit target, string spellId)
		{
			var cfgSpellData = CfgSpell.Instance.GetById(spellId);
			if (cfgSpellData.range == 0)
				return false;
			return cfgSpellData.range >= this.Distance(target);
		}

		public bool IsInSpellRange(Transform target, string spellId)
		{
			var cfgSpellData = CfgSpell.Instance.GetById(spellId);
			if (cfgSpellData.range == 0)
				return false;
			return cfgSpellData.range >= this.Distance(target);
		}

		public bool IsInSpellRange(Vector3 target, string spellId)
		{
			var cfgSpellData = CfgSpell.Instance.GetById(spellId);
			if (cfgSpellData.range == 0)
				return false;
			return cfgSpellData.range >= this.Distance(target);
		}

		public bool IsInSpellRange(IPosition targetIPosition, string spellId)
		{
			var cfgSpellData = CfgSpell.Instance.GetById(spellId);
			if (cfgSpellData.range == 0)
				return false;
			return cfgSpellData.range >= this.Distance(targetIPosition);
		}
	}
}