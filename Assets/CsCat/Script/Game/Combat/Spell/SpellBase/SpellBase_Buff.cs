using System.Collections;
using System.Collections.Generic;

namespace CsCat
{
	public partial class SpellBase
	{
		protected void AddBuff(string buffId, Unit targetUnit, float? forceDuration = null, Hashtable argDict = null)
		{
			if (buffId == null)
				return;
			if (targetUnit == null || targetUnit.IsDead())
				return;
			argDict = argDict ?? new Hashtable();
			argDict["sourceSpell"] = this;
			targetUnit.buffManager.AddBuff(buffId, this.sourceUnit, forceDuration, argDict);
		}

		public void RemoveBuff(List<Buff> buffList, Unit unit)
		{
			if (buffList.IsNullOrEmpty())
				return;
			unit = unit ?? this.sourceUnit;
			for (var i = 0; i < buffList.Count; i++)
			{
				var buff = buffList[i];
				unit.buffManager.RemoveBuff(buff.buffId, null, this.GetGuid());
			}
		}

		public void RemoveBuffById(string buffId, Unit unit, string forceSpellGuid = null)
		{
			if (unit == null || unit.IsDead())
				return;
			unit.buffManager.RemoveBuff(buffId, this.sourceUnit.GetGuid(), forceSpellGuid ?? this.GetGuid());
		}

		public void RemoveBuffById(List<string> buffIdList, Unit unit, string forceSpellGuid = null)
		{
			if (unit == null || unit.IsDead())
				return;
			for (var i = 0; i < buffIdList.Count; i++)
			{
				string buffId = buffIdList[i];
				RemoveBuffById(buffId, unit, forceSpellGuid);
			}
		}
	}
}
