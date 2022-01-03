using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CsCat
{
	public class BuffManager : TickObject
	{
		private Dictionary<string, int> state_dict = new Dictionary<string, int>();
		private Dictionary<string, Buff> buff_dict = new Dictionary<string, Buff>();
		public Unit unit;

		private Dictionary<string, List<Buff>>
		  buff_list_dict = new Dictionary<string, List<Buff>>(); //一个buff_id可能有多个相同的buff（不同时长，效果累加）同时存在，（效果不累加的放在buff类中处理）

		public void Init(Unit unit)
		{
			base.Init();
			this.unit = unit;
		}

		public void AddBuff(string buff_id, Unit source_unit, float? force_duration = null, Hashtable arg_dict = null)
		{
			var cfgBuffData = CfgBuff.Instance.get_by_id(buff_id);
			float duration =
			  force_duration.GetValueOrDefault(cfgBuffData.duration == 0 ? float.MaxValue : cfgBuffData.duration);
			string type_1 = cfgBuffData.type_1; // buff or debuff
			var source_spell = arg_dict.Get<SpellBase>("source_spell");
			if ("debuff".Equals(type_1) && this.unit.IsInvincible())
				return;
			if (this.unit.IsImmuneControl() && ("控制".Equals(cfgBuffData.type_2) ||
												(cfgBuffData.state.IsNullOrWhiteSpace() &&
												 StateConst.Control_State_Dict[cfgBuffData.state])))
			{
				//显示免疫
				return;
			}

			if (cfgBuffData.is_unique && HasBuff(buff_id)) // cfgBuffData.is_unique是指该buff只有一个生效
				buff_list_dict[buff_id][0].CreateBuffCache(duration, source_unit, source_spell, arg_dict);
			else
			{
				var buff = this.AddChild<Buff>(null, this, buff_id);
				buff.CreateBuffCache(duration, source_unit, source_spell, arg_dict);
				buff_dict[buff.key] = buff;
				buff_list_dict.GetOrAddDefault(buff_id, () => new List<Buff>()).Add(buff);
			}
		}

		public void RemoveBuff(List<string> buff_id_list, string source_unit_guid = null, string soruce_spell_guid = null)
		{
			foreach (var buff_id in buff_id_list)
				RemoveBuff(buff_id, source_unit_guid, soruce_spell_guid);
		}

		public void RemoveBuff(string buff_id, string source_unit_guid = null, string soruce_spell_guid = null)
		{
			this.__RemoveBuff(buff_id, source_unit_guid, soruce_spell_guid);
		}

		private void __RemoveBuff(string buff_id, string source_unit_guid, string soruce_spell_guid)
		{
			if (!this.buff_list_dict.ContainsKey(buff_id))
				return;
			for (int i = this.buff_list_dict[buff_id].Count - 1; i >= 0; i--)
				this.buff_list_dict[buff_id][i].RemoveBuffCache(source_unit_guid, soruce_spell_guid);
		}

		public void RemoveBuffByBuff(Buff buff)
		{
			buff_dict.Remove(buff.key);
			buff_list_dict[buff.buff_id].Remove(buff);
			this.RemoveChild(buff.key);
		}

		public bool HasBuff(string buff_id)
		{
			if (!buff_list_dict.ContainsKey(buff_id))
				return false;
			if (buff_list_dict[buff_id].IsNullOrEmpty())
				return false;
			return true;
		}

		public int GetBuffCount()
		{
			return this.buff_dict.Count;
		}


		public int GetDebuffCount()
		{
			int count = 0;
			foreach (var buff in this.buff_dict.Values)
			{
				if ("debuff".Equals(buff.cfgBuffData))
					count = count + 1;
			}

			return count;
		}

		public void AddState(string state_name)
		{
			if (state_name.IsNullOrWhiteSpace())
				return;
			int current_value = this.state_dict.GetOrAddDefault(state_name, () => 0);
			current_value += 1;
			this.state_dict[state_name] = current_value;

			if (current_value == 1 && this.unit != null)
			{
				//第一次添加
				if (state_name.Equals(StateConst.ImmuneControl))
					this.RemoveControlBuff();
				if (state_name.Equals(StateConst.Hide))
					this.unit.UpdateHideState();
				if (state_name.Equals(StateConst.Expose))
					this.unit.UpdateHideState();
				if (state_name.Equals(StateConst.Silent))
					this.Broadcast(null, UnitEventNameConst.On_Unit_Is_Silent_Change, this.unit, !this.HasState(StateConst.Silent),
					  this.HasState(StateConst.Silent));
				if (state_name.Equals(StateConst.Confused))
					this.Broadcast(null, UnitEventNameConst.On_Unit_Is_Confused_Change, this.unit, !this.HasState(StateConst.Confused),
					  this.HasState(StateConst.Confused));
				this.unit.UpdateMixedStates();
			}
		}

		public void RemoveState(string state_name)
		{
			if (state_name.IsNullOrWhiteSpace())
				return;

			int current_value = this.state_dict.GetOrAddDefault(state_name, () => 0);
			current_value -= 1;
			if (current_value < 0)
				LogCat.LogErrorFormat("{0} state_name = {1}", state_name, current_value);
			current_value = Mathf.Max(0, current_value);
			this.state_dict[state_name] = current_value;

			if (current_value == 0 && this.unit != null)
			{
				//第一次删除
				if (state_name.Equals(StateConst.Hide))
					this.unit.UpdateHideState();
				if (state_name.Equals(StateConst.Expose))
					this.unit.UpdateHideState();
				if (state_name.Equals(StateConst.Silent))
					this.Broadcast(null, UnitEventNameConst.On_Unit_Is_Silent_Change, this.unit, !this.HasState(StateConst.Silent),
					  this.HasState(StateConst.Silent));
				if (state_name.Equals(StateConst.Confused))
					this.Broadcast(null, UnitEventNameConst.On_Unit_Is_Confused_Change, this.unit, !this.HasState(StateConst.Confused),
					  this.HasState(StateConst.Confused));
				this.unit.UpdateMixedStates();
			}
		}

		//去掉控制类型的buff
		public void RemoveControlBuff()
		{
			foreach (var buff_guid in new List<string>(buff_dict.Keys))
			{
				var buff = this.buff_dict[buff_guid];
				if (!buff.IsDestroyed() && "控制".Equals(buff.cfgBuffData.type_2))
					this.RemoveBuff(buff_guid);
			}
		}

		public bool HasState(string state_name)
		{
			if (!state_dict.ContainsKey(state_name))
				return false;
			return state_dict[state_name] > 0;
		}


		protected override void _Destroy()
		{
			base._Destroy();
			foreach (var buff_id in new List<string>(buff_dict.Keys))
				this.RemoveBuff(buff_id);

		}
	}
}