using System.Collections.Generic;

namespace CsCat
{
  public class FactionConst
  {
    public static string A_Faction = "A";
    public static string B_Faction = "B";
    public static string C_Faction = "C";

    private static List<string> _Faction_List;

    public static List<string> Faction_List
    {
      get
      {
        if (_Faction_List == null)
        {
          _Faction_List = new List<string>();
          _Faction_List.Add(A_Faction);
          _Faction_List.Add(B_Faction);
          _Faction_List.Add(C_Faction);
        }

        return _Faction_List;
      }
    }
  }
}