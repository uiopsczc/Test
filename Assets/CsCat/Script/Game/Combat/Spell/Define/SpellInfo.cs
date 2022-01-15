namespace CsCat
{
	public class SpellInfo
	{
		public string spellId;
		public float cooldownRate;
		public float cooldownRemainDuration;
		public int level;

		public float GetCooldownPct()
		{
			var cfgSpellData = CfgSpell.Instance.get_by_id(this.spellId);
			float cooldownDuration = cfgSpellData.cooldown_duration;
			return this.cooldownRemainDuration / (cooldownDuration * this.cooldownRate);
		}
	}
}