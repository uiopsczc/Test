using System.Collections.Generic;

namespace CsCat
{
	public static class StateConst
	{
		public static string Confused = "混乱";
		public static string Invincible = "无敌";
		public static string Stun = "眩晕";
		public static string Freeze = "冰冻";
		public static string Silent = "沉默";
		public static string CanNotMove = "不能移动";
		public static string CanNotAttack = "不能攻击";
		public static string CanNotBeTakeDamage = "不受伤害";
		public static string CanNotBeHeal = "不能被治疗";
		public static string ImmuneControl = "免控";
		public static string Hide = "隐身";
		public static string Expose = "反隐";

		//控制类的状态Dict
		public static Dictionary<string, bool> Control_State_Dict = new Dictionary<string, bool>()
		{
		};
	}
}