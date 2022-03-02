using System.Collections.Generic;

namespace CsCat
{
	public static class SpellConst
	{
		public static Dictionary<string, string> Trigger_Type_Dict = new Dictionary<string, string>()
		{
			{"技能攻击到单位时", "onCurSpellHit"},
			{"技能开始时", "onStart"},
			{"弹道到达时", "onMissileReach"},
			{"打死目标", "onKillTarget"},
			{"被攻击时", "beHit"},
			{"死亡前", "beforeDead"},
		};

		public static Dictionary<string, string> Select_Unit_Faction_Dict = new Dictionary<string, string>()
		{
			{"敌人", "enemy"},
			{"友军", "friend"},
			{"自己", "friend"},
			{"全部", "all"},
		};
	}
}