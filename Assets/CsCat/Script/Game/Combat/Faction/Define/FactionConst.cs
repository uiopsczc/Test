using System.Collections.Generic;

namespace CsCat
{
	public class FactionConst
	{
		public static string A_Faction = "A";
		public static string B_Faction = "B";
		public static string C_Faction = "C";

		private static List<string> _Faction_List;

		public static List<string> Faction_List => _Faction_List ?? (_Faction_List = new List<string> {A_Faction, B_Faction, C_Faction});
	}
}