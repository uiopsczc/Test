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
			var cfgSpellData = CfgSpell.Instance.GetById(this.spellId);
			float cooldownDuration = cfgSpellData.cooldownDuration;
			return this.cooldownRemainDuration / (cooldownDuration * this.cooldownRate);
		}
	}
}