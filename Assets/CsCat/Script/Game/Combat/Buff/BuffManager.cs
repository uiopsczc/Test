using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CsCat
{
	public class BuffManager : TickObject
	{
		private readonly Dictionary<string, int> _stateDict = new Dictionary<string, int>();
		private readonly Dictionary<string, Buff> _buffDict = new Dictionary<string, Buff>();

		public Unit unit;

		private readonly Dictionary<string, List<Buff>>
		  _buffListDict = new Dictionary<string, List<Buff>>(); //一个buff_id可能有多个相同的buff（不同时长，效果累加）同时存在，（效果不累加的放在buff类中处理）

		public void Init(Unit unit)
		{
			base.Init();
			this.unit = unit;
		}

		public void AddBuff(string buffId, Unit sourceUnit, float? forceDuration = null, Hashtable argDict = null)
		{
			var cfgBuffData = CfgBuff.Instance.GetById(buffId);
			float duration =
			  forceDuration.GetValueOrDefault(cfgBuffData.duration == 0 ? float.MaxValue : cfgBuffData.duration);
			string type1 = cfgBuffData.type1; // buff or debuff
			var sourceSpell = argDict.Get<SpellBase>("sourceSpell");
			if ("debuff".Equals(type1) && this.unit.IsInvincible())
				return;
			if (this.unit.IsImmuneControl() && ("控制".Equals(cfgBuffData.type2) ||
												(cfgBuffData.state.IsNullOrWhiteSpace() &&
												 StateConst.Control_State_Dict[cfgBuffData.state])))
			{
				//显示免疫
				return;
			}

			if (cfgBuffData.isUnique && IsHasBuff(buffId)) // cfgBuffData.is_unique是指该buff只有一个生效
				_buffListDict[buffId][0].CreateBuffCache(duration, sourceUnit, sourceSpell, argDict);
			else
			{
				var buff = this.AddChild<Buff>(null, this, buffId);
				buff.CreateBuffCache(duration, sourceUnit, sourceSpell, argDict);
				_buffDict[buff.key] = buff;
				_buffListDict.GetOrAddDefault(buffId, () => new List<Buff>()).Add(buff);
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
			this._RemoveBuff(buffId, sourceUnitGuid, sourceSpellGuid);
		}

		private void _RemoveBuff(string buffId, string sourceUnitGuid, string sourceSpellGuid)
		{
			if (!this._buffListDict.ContainsKey(buffId))
				return;
			for (int i = this._buffListDict[buffId].Count - 1; i >= 0; i--)
				this._buffListDict[buffId][i].RemoveBuffCache(sourceUnitGuid, sourceSpellGuid);
		}

		public void RemoveBuffByBuff(Buff buff)
		{
			_buffDict.Remove(buff.key);
			_buffListDict[buff.buffId].Remove(buff);
			this.RemoveChild(buff.key);
		}

		public bool IsHasBuff(string buffId)
		{
			if (!_buffListDict.ContainsKey(buffId))
				return false;
			return !_buffListDict[buffId].IsNullOrEmpty();
		}

		public int GetBuffCount()
		{
			return this._buffDict.Count;
		}


		public int GetDebuffCount()
		{
			int count = 0;
			foreach (var keyValue in _buffDict)
			{
				var buff = keyValue.Value;
				if ("debuff".Equals(buff.cfgBuffData.type1))
					count = count + 1;
			}

			return count;
		}

		public void AddState(string stateName)
		{
			if (stateName.IsNullOrWhiteSpace())
				return;
			int currentValue = this._stateDict.GetOrAddDefault(stateName, () => 0);
			currentValue += 1;
			this._stateDict[stateName] = currentValue;

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
					this.Broadcast<Unit, bool, bool>(null, UnitEventNameConst.On_Unit_Is_Silent_Change, this.unit, !this.IsHasState(StateConst.Silent),
					  this.IsHasState(StateConst.Silent));
				if (stateName.Equals(StateConst.Confused))
					this.Broadcast<Unit, bool, bool>(null, UnitEventNameConst.On_Unit_Is_Confused_Change, this.unit, !this.IsHasState(StateConst.Confused),
					  this.IsHasState(StateConst.Confused));
				this.unit.UpdateMixedStates();
			}
		}

		public void RemoveState(string stateName)
		{
			if (stateName.IsNullOrWhiteSpace())
				return;

			int currentValue = this._stateDict.GetOrAddDefault(stateName, () => 0);
			currentValue -= 1;
			if (currentValue < 0)
				LogCat.LogErrorFormat("{0} stateName = {1}", stateName, currentValue);
			currentValue = Mathf.Max(0, currentValue);
			this._stateDict[stateName] = currentValue;

			if (currentValue == 0 && this.unit != null)
			{
				//第一次删除
				if (stateName.Equals(StateConst.Hide))
					this.unit.UpdateHideState();
				if (stateName.Equals(StateConst.Expose))
					this.unit.UpdateHideState();
				if (stateName.Equals(StateConst.Silent))
					this.Broadcast<Unit, bool, bool>(null, UnitEventNameConst.On_Unit_Is_Silent_Change, this.unit, !this.IsHasState(StateConst.Silent),
					  this.IsHasState(StateConst.Silent));
				if (stateName.Equals(StateConst.Confused))
					this.Broadcast<Unit, bool, bool>(null, UnitEventNameConst.On_Unit_Is_Confused_Change, this.unit, !this.IsHasState(StateConst.Confused),
					  this.IsHasState(StateConst.Confused));
				this.unit.UpdateMixedStates();
			}
		}

		//去掉控制类型的buff
		public void RemoveControlBuff()
		{
			var list = new List<string>(_buffDict.Keys);
			for (var i = 0; i < list.Count; i++)
			{
				var buffGuid = list[i];
				var buff = this._buffDict[buffGuid];
				if (!buff.IsDestroyed() && "控制".Equals(buff.cfgBuffData.type2))
					this.RemoveBuff(buffGuid);
			}
		}

		public bool IsHasState(string stateName)
		{
			return _stateDict.ContainsKey(stateName) && _stateDict[stateName] > 0;
		}


		protected override void _Destroy()
		{
			base._Destroy();
			var ids = new List<string>(_buffDict.Keys);
			for (var i = 0; i < ids.Count; i++)
			{
				var buffId = ids[i];
				this.RemoveBuff(buffId);
			}
		}
	}
}