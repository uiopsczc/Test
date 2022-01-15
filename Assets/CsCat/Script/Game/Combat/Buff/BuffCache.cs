using System.Collections;

namespace CsCat
{
	public class BuffCache
	{
		public float duration;
		public float remainDuration;
		public Unit sourceUnit;
		public SpellBase sourceSpell;
		public Hashtable argDict;

		public BuffCache(float duration, Unit sourceUnit, SpellBase sourceSpell, Hashtable argDict)
		{
			this.duration = duration;
			this.remainDuration = this.duration;
			this.sourceUnit = sourceUnit;
			this.sourceSpell = sourceSpell;
			this.argDict = argDict;
		}

	}
}



