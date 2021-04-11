using System.Collections.Generic;

namespace CsCat
{
  public partial class UnitManager
  {
    private Dictionary<string, Dictionary<string, Unit>> faction_unit_dict;
    private Dictionary<string, Dictionary<string, FactionState>> factionState_dict;

    private void InitFactionUnitDict()
    {
      this.faction_unit_dict = new Dictionary<string, Dictionary<string, Unit>>();
      foreach (var faction in FactionConst.Faction_List)
        faction_unit_dict[faction] = new Dictionary<string, Unit>();
    }

    private void InitFactionStateInfoDict()
    {
      //初始化阵营间能否攻击，加血等
      this.factionState_dict = new Dictionary<string, Dictionary<string, FactionState>>();

      this.SetFactionStateIsCanAttack(FactionConst.A_Faction, FactionConst.A_Faction, false);
      this.SetFactionStateIsCanHelp(FactionConst.A_Faction, FactionConst.A_Faction, true);
      this.SetFactionStateIsCanAttack(FactionConst.A_Faction, FactionConst.B_Faction, true);
      this.SetFactionStateIsCanHelp(FactionConst.A_Faction, FactionConst.B_Faction, false);

      this.SetFactionStateIsCanAttack(FactionConst.B_Faction, FactionConst.A_Faction, true);
      this.SetFactionStateIsCanHelp(FactionConst.B_Faction, FactionConst.A_Faction, false);
      this.SetFactionStateIsCanAttack(FactionConst.B_Faction, FactionConst.B_Faction, false);
      this.SetFactionStateIsCanHelp(FactionConst.B_Faction, FactionConst.B_Faction, true);
    }

    // 如果找敌人，找“不是自己阵营且单向阵营关系can_attack = true”
    // 如果是找自己人，找“阵营相同，或者单向阵营关系can_help = true”
    // check_state 参数：
    // 1. enemy 判断是否敌人
    // 2. friend 判断是否自己人
    public bool CheckFaction(string faction1, string faction2, string check_state)
    {
      var factionState = this.factionState_dict[faction1][faction2];
      //找敌人
      if (check_state.Equals("enemy"))
      {
        if (!faction1.Equals(faction2) && factionState.is_can_attack)
          return true;
        else
          return false;
      }
      else if (check_state.Equals("friend")) // 找自己人
      {
        if (faction1.Equals(faction2) || factionState.is_can_help)
          return true;
        else
          return false;
      }
      else // all
        return true;
    }

    public void SetFactionState(string faction1, string faction2, string state_key, object state_value,
      bool is_both_set = false)
    {
      if (!faction1.IsNullOrWhiteSpace() && !faction2.IsNullOrWhiteSpace())
      {
        this.factionState_dict.GetOrAddDefault(faction1, () => new Dictionary<string, FactionState>());
        this.factionState_dict[faction1].GetOrAddDefault(faction2, () => new FactionState());

        this.factionState_dict[faction1][faction2].SetFieldValue(state_key, state_value);

        if (is_both_set)
          SetFactionState(faction2, faction1, state_key, state_value);
      }
    }

    public void SetFactionStateIsCanAttack(string faction1, string faction2, bool is_can_attack,
      bool is_both_set = false)
    {
      this.SetFactionState(faction1, faction2, "is_can_attack", is_can_attack, is_both_set);
    }

    public void SetFactionStateIsCanHelp(string faction1, string faction2, bool is_can_help,
      bool is_both_set = false)
    {
      this.SetFactionState(faction1, faction2, "is_can_help", is_can_help, is_both_set);
    }

    public void OnUnitFactionChange(string unit_guid, string old_faction, string new_faction)
    {
      var unit = this.GetUnit(unit_guid);
      if (unit != null && !old_faction.Equals(new_faction))
      {
        this.faction_unit_dict[old_faction].Remove(unit_guid);
        this.faction_unit_dict[new_faction][unit_guid] = unit;
      }
    }

    public List<string> GetMatchFactionList(string faction, string check_scope)
    {
      List<string> faction_list = new List<string>();
      foreach (var _faction in FactionConst.Faction_List)
      {
        if (this.CheckFaction(_faction, faction, check_scope))
          faction_list.Add(_faction);
      }

      return faction_list;
    }

    public List<Unit> GetFactionUnitList(List<string> faction_list)
    {
      var faction_unit_list = new List<Unit>();
      foreach (var faction in faction_list)
      foreach (var unit in this.faction_unit_dict[faction].Values)
        faction_unit_list.Add(unit);
      return faction_unit_list;
    }

  }
}