namespace CsCat
{
	public class SpellInfo
	{
		public string spell_id;
		public float cooldown_rate;
		public float cooldown_remain_duration;
		public int level;

		public float GetCooldownPct()
		{
			var cfgSpellData = CfgSpell.Instance.get_by_id(this.spell_id);
			float cooldown_duration = cfgSpellData.cooldown_duration;
			return this.cooldown_remain_duration / (cooldown_duration * this.cooldown_rate);
		}
	}
}