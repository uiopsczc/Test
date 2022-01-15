using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CsCat
{
	public class BuffManager : TickObject
	{
		private Dictionary<string, int> stateDict = new Dictionary<string, int>();
		private Dictionary<string, Buff> buffDict = new Dictionary<string, Buff>();
		public Unit unit;

		private Dictionary<string, List<Buff>>
		  buffListDict = new Dictionary<string, List<Buff>>(); //一个buff_id可能有多个相同的buff（不同时长，效果累加）同时存在，（效果不累加的放在buff类中处理）

		public void Init(Unit unit)
		{
			base.Init();
			this.unit = unit;
		}

		public void AddBuff(string buffId, Unit sourceUnit, float? forceDuration = null, Hashtable argDict = null)
		{
			var cfgBuffData = CfgBuff.Instance.get_by_id(buffId);
			float duration =
			  forceDuration.GetValueOrDefault(cfgBuffData.duration == 0 ? float.MaxValue : cfgBuffData.duration);
			string type1 = cfgBuffData.type_1; // buff or debuff
			var sourceSpell = argDict.Get<SpellBase>("source_spell");
			if ("debuff".Equals(type1) && this.unit.IsInvincible())
				return;
			if (this.unit.IsImmuneControl() && ("控制".Equals(cfgBuffData.type_2) ||
												(cfgBuffData.state.IsNullOrWhiteSpace() &&
												 StateConst.Control_State_Dict[cfgBuffData.state])))
			{
				//显示免疫
				return;
			}

			if (cfgBuffData.is_unique && HasBuff(buffId)) // cfgBuffData.is_unique是指该buff只有一个生效
				buffListDict[buffId][0].CreateBuffCache(duration, sourceUnit, sourceSpell, argDict);
			else
			{
				var buff = this.AddChild<Buff>(null, this, buffId);
				buff.CreateBuffCache(duration, sourceUnit, sourceSpell, argDict);
				buffDict[buff.key] = buff;
				buffListDict.GetOrAddDefault(buffId, () => new List<Buff>()).Add(buff);
			}
		}

		public void RemoveBuff(List<string> buffIdList, string sourceUnitGuid = null, string sourceSpellGuid = null)
		{
			for (var i = 0; i < buffIdList.Count; i++)
			{
				var buffId = buffIdList[i];
				RemoveBuff(buffId, sourceUnitGuid, sourceSpellGuid);
			}
		}

		public void RemoveBuff(string buffId, string sourceUnitGuid = null, string sourceSpellGuid = null)
		{
			this.__RemoveBuff(buffId, sourceUnitGuid, sourceSpellGuid);
		}

		private void __RemoveBuff(string buffId, string sourceUnitGuid, string sourceSpellGuid)
		{
			if (!this.buffListDict.ContainsKey(buffId))
				return;
			for (int i = this.buffListDict[buffId].Count - 1; i >= 0; i--)
				this.buffListDict[buffId][i].RemoveBuffCache(sourceUnitGuid, sourceSpellGuid);
		}

		public void RemoveBuffByBuff(Buff buff)
		{
			buffDict.Remove(buff.key);
			buffListDict[buff.buffId].Remove(buff);
			this.RemoveChild(buff.key);
		}

		public bool HasBuff(string buffId)
		{
			if (!buffListDict.ContainsKey(buffId))
				return false;
			if (buffListDict[buffId].IsNullOrEmpty())
				return false;
			return true;
		}

		public int GetBuffCount()
		{
			return this.buffDict.Count;
		}


		public int GetDebuffCount()
		{
			int count = 0;
			foreach (var buff in this.buffDict.Values)
			{
				if ("debuff".Equals(buff.cfgBuffData))
					count = count + 1;
			}

			return count;
		}

		public void AddState(string stateName)
		{
			if (stateName.IsNullOrWhiteSpace())
				return;
			int currentValue = this.stateDict.GetOrAddDefault(stateName, () => 0);
			currentValue += 1;
			this.stateDict[stateName] = currentValue;

			if (currentValue == 1 && this.unit != null)
			{
				//第一次添加
				if (stateName.Equals(StateConst.ImmuneControl))
					this.RemoveControlBuff();
				if (stateName.Equals(StateConst.Hide))
					this.unit.UpdateHideState();
				if (stateName.Equals(StateConst.Expose))
					this.unit.UpdateHideState();
				if (stateName.Equals(StateConst.Silent))
					this.Broadcast(null, UnitEventNameConst.On_Unit_Is_Silent_Change, this.unit, !this.HasState(StateConst.Silent),
					  this.HasState(StateConst.Silent));
				if (stateName.Equals(StateConst.Confused))
					this.Broadcast(null, UnitEventNameConst.On_Unit_Is_Confused_Change, this.unit, !this.HasState(StateConst.Confused),
					  this.HasState(StateConst.Confused));
				this.unit.UpdateMixedStates();
			}
		}

		public void RemoveState(string stateName)
		{
			if (stateName.IsNullOrWhiteSpace())
				return;

			int currentValue = this.stateDict.GetOrAddDefault(stateName, () => 0);
			currentValue -= 1;
			if (currentValue < 0)
				LogCat.LogErrorFormat("{0} state_name = {1}", stateName, currentValue);
			currentValue = Mathf.Max(0, currentValue);
			this.stateDict[stateName] = currentValue;

			if (currentValue == 0 && this.unit != null)
			{
				//第一次删除
				if (stateName.Equals(StateConst.Hide))
					this.unit.UpdateHideState();
				if (stateName.Equals(StateConst.Expose))
					this.unit.UpdateHideState();
				if (stateName.Equals(StateConst.Silent))
					this.Broadcast(null, UnitEventNameConst.On_Unit_Is_Silent_Change, this.unit, !this.HasState(StateConst.Silent),
					  this.HasState(StateConst.Silent));
				if (stateName.Equals(StateConst.Confused))
					this.Broadcast(null, UnitEventNameConst.On_Unit_Is_Confused_Change, this.unit, !this.HasState(StateConst.Confused),
					  this.HasState(StateConst.Confused));
				this.unit.UpdateMixedStates();
			}
		}

		//去掉控制类型的buff
		public void RemoveControlBuff()
		{
			var list = new List<string>(buffDict.Keys);
			for (var i = 0; i < list.Count; i++)
			{
				var buffGuid = list[i];
				var buff = this.buffDict[buffGuid];
				if (!buff.IsDestroyed() && "控制".Equals(buff.cfgBuffData.type_2))
					this.RemoveBuff(buffGuid);
			}
		}

		public bool HasState(string stateName)
		{
			return stateDict.ContainsKey(stateName) && stateDict[stateName] > 0;
		}


		protected override void _Destroy()
		{
			base._Destroy();
			var ids = new List<string>(buffDict.Keys);
			for (var i = 0; i < ids.Count; i++)
			{
				var buffId = ids[i];
				this.RemoveBuff(buffId);
			}
		}
	}
}