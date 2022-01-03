namespace CsCat
{
	public static class CombatUtil
	{
		public static float GetTime()
		{
			return Client.instance.combat.time;
		}

		public static float GetFrame()
		{
			return Client.instance.combat.frame;
		}
	}
}