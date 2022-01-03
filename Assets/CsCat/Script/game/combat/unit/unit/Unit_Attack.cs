using System;
using System.Collections.Generic;
using UnityEngine;

namespace CsCat
{
	public partial class Unit
	{
		private string last_attack_id;
		private ComboInfo normal_attack_comboInfo;
		private List<string> normal_attack_id_list;
		public List<string> skill_id_list;
		private Dictionary<string, SpellInfo> spellInfo_dict = new Dictionary<string, SpellInfo>();
		public SpellBase current_attack;
		public bool is_normal_attacking;

		//////////////////////////////////////////////////////////////////////
		// 普攻相关
		//////////////////////////////////////////////////////////////////////
		public void AddNormalAttack(string normal_attack_id)
		{
			if (normal_attack_id.IsNullOrWhiteSpace())
				return;
			normal_attack_id_list.Add(normal_attack_id);
			this.InitSpellInfo(normal_attack_id);
			this.AddPassiveBuffOfSpell(normal_attack_id);
		}

		public string GetNormalAttackId()
		{
			var next_index = this.normal_attack_comboInfo.next_index;
			if (CombatUtil.GetTime() > this.normal_attack_comboInfo.next_time ||
				!this.normal_attack_id_list.ContainsIndex(this.normal_attack_comboInfo.next_index)
			)
				next_index = 0;

			var normal_attack_id = this.normal_attack_id_list[next_index];
			return normal_attack_id;
		}


		public SpellBase NormalAttack(Unit target_unit)
		{
			var normal_attack_id = this.GetNormalAttackId();
			var normal_attack =
			  Client.instance.combat.spellManager.CastSpell(this, normal_attack_id, target_unit, null, true);
			if (normal_attack != null)
				this.last_attack_id = normal_attack_id;
			return normal_attack;
		}

		public void NormalAttackStart()
		{
			this.normal_attack_comboInfo.next_time =
			  CombatUtil.GetTime() + ComboConst.Normal_Attack_Combo_Max_Duration; // 1秒间隔触发combo
			this.normal_attack_comboInfo.next_index = this.normal_attack_comboInfo.next_index + 1;
			this.is_normal_attacking = true;
		}

		public void NormalAttackFinish()
		{
			this.normal_attack_comboInfo.next_time = CombatUtil.GetTime() + 0.2f;
			this.is_normal_attacking = true;
		}

		//////////////////////////////////////////////////////////////////////
		// 技能相关
		//////////////////////////////////////////////////////////////////////
		public void AddSkill(string skill_id)
		{
			if (skill_id.IsNullOrWhiteSpace())
				return;
			this.skill_id_list.Add(skill_id);
			this.InitSpellInfo(skill_id);
			this.AddPassiveBuffOfSpell(skill_id);
		}

		//is_control 是否是控制类技能
		public SpellBase CastSkillByIndex(int index, Unit target_unit, bool is_control)
		{
			var skill_id = this.skill_id_list[index];
			if (skill_id.IsNullOrWhiteSpace())
			{
				LogCat.error("index error ", index);
				return null;
			}

			return this.CastSpell(skill_id, target_unit, is_control);
		}

		//////////////////////////////////////////////////////////////////////
		// spell相关
		//////////////////////////////////////////////////////////////////////
		public void InitSpellInfo(string spell_id, float cooldown_pct = 0)
		{
			var spellInfo = new SpellInfo();
			this.spellInfo_dict[spell_id] = spellInfo;
			spellInfo.cooldown_rate = 1 - (this.GetCalcPropValue("技能冷却减少百分比"));
			this.SetSpellInfoCooldown(spell_id, cooldown_pct);
		}

		public float GetSpellCooldownRate(string spell_id)
		{
			var cfgSpellData = CfgSpell.Instance.get_by_id(spell_id);
			if ("普攻".Equals(cfgSpellData.type))
				return 1 / (1 + this.GetCalcPropValue("攻击速度"));
			else
				return 1 - this.GetCalcPropValue("技能冷却减少百分比");
		}

		public List<string> GetSpellIdList(string spell_id)
		{
			var cfgSpellData = CfgSpell.Instance.get_by_id(spell_id);
			if ("普攻".Equals(cfgSpellData.type))
				return this.normal_attack_id_list;
			else
				return this.skill_id_list;
		}


		public void SetSpellInfoCooldown(string spell_id, float cooldown_pct = 0)
		{
			var cfgSpellData = CfgSpell.Instance.get_by_id(spell_id);
			var spellInfo = this.spellInfo_dict[spell_id];
			spellInfo.cooldown_rate = this.GetSpellCooldownRate(spell_id);
			spellInfo.cooldown_remain_duration = cfgSpellData.cooldown_duration * spellInfo.cooldown_rate * cooldown_pct;
		}

		public void AddPassiveBuffOfSpell(string spell_id)
		{
			var cfgSpellData = CfgSpell.Instance.get_by_id(spell_id);
			var passive_buff_ids = cfgSpellData._passive_buff_ids;
			if (!passive_buff_ids.IsNullOrEmpty())
			{
				foreach (var passive_buff_id in passive_buff_ids)
					this.buffManager.AddBuff(passive_buff_id, this);
			}
		}

		public void RemoveSpell(string spell_id)
		{
			List<string> spell_id_list = this.GetSpellIdList(spell_id);
			spell_id_list.Remove(spell_id);
			this.spellInfo_dict.Remove(spell_id);
			this.RemovePassiveBuffOfSpell(spell_id);
		}

		public void RemovePassiveBuffOfSpell(string spell_id)
		{
			var cfgSpellData = CfgSpell.Instance.get_by_id(spell_id);
			var passive_buff_ids = cfgSpellData._passive_buff_ids;
			if (!passive_buff_ids.IsNullOrEmpty())
			{
				foreach (var passive_buff_id in passive_buff_ids)
					this.buffManager.RemoveBuff(passive_buff_id, this.GetGuid());
			}
		}

		//替换单位技能
		public void ReplaceSpell(string old_spell_id, string new_spell_id, bool is_reset_cooldown_remain_duration = false)
		{
			var spell_id_list = this.GetSpellIdList(old_spell_id);
			int index = spell_id_list.IndexOf(old_spell_id);
			spell_id_list[index] = new_spell_id;

			//更新cooldown
			float cooldown_cur_pct = 0;
			if (!is_reset_cooldown_remain_duration)
				cooldown_cur_pct = this.spellInfo_dict[old_spell_id].GetCooldownPct();
			this.InitSpellInfo(new_spell_id, cooldown_cur_pct);
			this.spellInfo_dict.Remove(old_spell_id);

			//删除原技能被动buff
			this.RemovePassiveBuffOfSpell(old_spell_id);
			// 添加新技能被动buff
			this.AddPassiveBuffOfSpell(new_spell_id);
		}

		//改变技能CD
		private void OnSpellCooldownRateChange()
		{
			foreach (var spell_id in this.spellInfo_dict.Keys)
			{
				var spellInfo = spellInfo_dict[spell_id];
				var cfgSpellData = CfgSpell.Instance.get_by_id(spell_id);
				var cooldown_old_rate = spellInfo.cooldown_rate;
				var cooldown_duration = cfgSpellData.cooldown_duration;
				if (cooldown_duration > 0)
				{
					var new_rate = this.GetSpellCooldownRate(spell_id);
					if (spellInfo.cooldown_remain_duration <= 0)
						spellInfo.cooldown_remain_duration = 0;
					else
					{
						//var cooldown_cur_pct = spellInfo.cooldown_remain_duration / (cooldown_duration * cooldown_last_rate)
						//var cooldown_remain_duration = cooldown_cur_pct * (cooldown_duration * new_rate)
						spellInfo.cooldown_remain_duration = spellInfo.cooldown_remain_duration * new_rate / cooldown_old_rate;
					}

					spellInfo.cooldown_rate = new_rate;
				}
			}
		}

		public void ReduceSpellCooldown(float deltaTime)
		{
			foreach (var spellInfo in this.spellInfo_dict.Values)
			{
				if (spellInfo.cooldown_remain_duration > 0)
					spellInfo.cooldown_remain_duration = Math.Max(0, spellInfo.cooldown_remain_duration - deltaTime);
			}
		}

		public bool IsSpellCooldownOk(string spell_id)
		{
			if (this.spellInfo_dict[spell_id].cooldown_remain_duration == 0)
				return true;
			return false;
		}


		//is_control 是否是控制类技能
		public SpellBase CastSpell(string spell_id, Unit target_unit, bool is_control)
		{
			var spell = Client.instance.combat.spellManager.CastSpell(this, spell_id, target_unit, null, is_control);
			if (spell != null)
				this.last_attack_id = spell_id;
			return spell;
		}

		public bool CanBreakCurrentSpell(string new_spell_id, CfgSpellData new_cfgSpellData = null)
		{
			if (this.current_attack == null)
				return true;

			new_cfgSpellData = new_cfgSpellData ?? CfgSpell.Instance.get_by_id(new_spell_id);
			if (("法术".Equals(new_cfgSpellData.type) && "普攻".Equals(this.current_attack.cfgSpellData.type)) //法术可以打断普攻
				|| "触发".Equals(new_cfgSpellData.cast_type))
				return true;
			else
				return this.current_attack.is_past_break_time;
		}

		//检查是否到时间可以放技能1、是否能打断当前技能2、技能cd是否到
		public bool IsTimeToCastSpell(string spell_id)
		{
			var cfgSpellData = CfgSpell.Instance.get_by_id(spell_id);
			if (!this.CanBreakCurrentSpell(spell_id, cfgSpellData))
				return false;
			if (!this.IsSpellCooldownOk(spell_id))
				return false;
			return true;
		}

		public bool IsInSpellRange(Unit target, string spell_id)
		{
			var cfgSpellData = CfgSpell.Instance.get_by_id(spell_id);
			if (cfgSpellData.range == 0)
				return false;
			return cfgSpellData.range >= this.Distance(target);
		}

		public bool IsInSpellRange(Transform target, string spell_id)
		{
			var cfgSpellData = CfgSpell.Instance.get_by_id(spell_id);
			if (cfgSpellData.range == 0)
				return false;
			return cfgSpellData.range >= this.Distance(target);
		}

		public bool IsInSpellRange(Vector3 target, string spell_id)
		{
			var cfgSpellData = CfgSpell.Instance.get_by_id(spell_id);
			if (cfgSpellData.range == 0)
				return false;
			return cfgSpellData.range >= this.Distance(target);
		}

		public bool IsInSpellRange(IPosition target_iposition, string spell_id)
		{
			var cfgSpellData = CfgSpell.Instance.get_by_id(spell_id);
			if (cfgSpellData.range == 0)
				return false;
			return cfgSpellData.range >= this.Distance(target_iposition);
		}
	}
}