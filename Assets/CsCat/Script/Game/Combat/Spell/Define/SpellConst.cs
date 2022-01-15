using System.Collections.Generic;

namespace CsCat
{
	public static class SpellConst
	{
		public static Dictionary<string, string> Trigger_Type_Dict = new Dictionary<string, string>()
		{
			{"技能攻击到单位时", "on_cur_spell_hit"},
			{"技能开始时", "on_start"},
			{"弹道到达时", "on_missile_reach"},
			{"打死目标", "on_kill_target"},
			{"被攻击时", "be_hit"},
			{"死亡前", "before_dead"},
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