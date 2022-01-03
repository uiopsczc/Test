using System.Collections;
using System.Collections.Generic;

namespace CsCat
{
	public partial class SpellBase
	{
		protected void AddBuff(string buff_id, Unit target_unit, float? force_duration = null, Hashtable arg_dict = null)
		{
			if (buff_id == null)
				return;
			if (target_unit == null || target_unit.IsDead())
				return;
			arg_dict = arg_dict ?? new Hashtable();
			arg_dict["source_spell"] = this;
			target_unit.buffManager.AddBuff(buff_id, this.source_unit, force_duration, arg_dict);
		}

		public void RemoveBuff(List<Buff> buff_list, Unit unit)
		{
			if (buff_list.IsNullOrEmpty())
				return;
			unit = unit ?? this.source_unit;
			foreach (var buff in buff_list)
				unit.buffManager.RemoveBuff(buff.buff_id, null, this.GetGuid());
		}

		public void RemoveBuffById(string buff_id, Unit unit, string force_spell_guid = null)
		{
			if (unit == null || unit.IsDead())
				return;
			unit.buffManager.RemoveBuff(buff_id, this.source_unit.GetGuid(), force_spell_guid ?? this.GetGuid());
		}

		public void RemoveBuffById(List<string> buff_id_list, Unit unit, string force_spell_guid = null)
		{
			if (unit == null || unit.IsDead())
				return;
			foreach (string buff_id in buff_id_list)
				RemoveBuffById(buff_id, unit, force_spell_guid);
		}
	}
}
