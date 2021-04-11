using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CsCat
{
  public partial class UnitManager : TickObject
  {
    private Dictionary<string, Unit> unit_dict = new Dictionary<string, Unit>();

    public override void Init()
    {
      base.Init();
      var gameObject = GameObject.Find("UnitManager") ?? new GameObject("UnitManager");
      graphicComponent.SetGameObject(gameObject,  true);
      this.InitFactionUnitDict();
      this.InitFactionStateInfoDict();

      this.AddListener<string, string>(UnitEventNameConst.On_Unit_Guid_Change, this.OnUnitGuidChange);
      this.AddListener<string, string, string>(UnitEventNameConst.On_Unit_Faction_Change, this.OnUnitFactionChange);
    }

    protected override void __Update(float deltaTime = 0, float unscaledDeltaTime = 0)
    {
      base.__Update(deltaTime, unscaledDeltaTime);
      foreach (var unit in this.unit_dict.Values)
      {
        if (!unit.IsDead() && !unit.IsDestroyed())
          unit.ReduceSpellCooldown(deltaTime);
      }
    }

    public Unit CreateUnit(Hashtable arg_dict)
    {
      string guid = arg_dict.Get<string>("guid");
      var unit_old = this.GetUnit(guid);
      if (unit_old != null)
        this.RemoveUnit(unit_old.GetGuid());
      Unit unit = this.AddChild<Unit>(guid);
      unit.Build(arg_dict);
      this.unit_dict[unit.key] = unit;
      this.faction_unit_dict[unit.GetFaction()][unit.GetGuid()] = unit;
      if (!unit.unitDefinition.ai_class_path_cs.IsNullOrWhiteSpace())
        unit.RunAI(unit.unitDefinition.ai_class_path_cs);
      return unit;
    }

    public void UpdateUnit(Unit unit, Hashtable arg_dict)
    {
      var new_guid = arg_dict.Get<string>("guid");
      var old_guid = unit.GetGuid();
      if (!new_guid.IsNullOrWhiteSpace() && !old_guid.IsNullOrWhiteSpace())
        this.Broadcast(UnitEventNameConst.On_Unit_Guid_Change, old_guid, new_guid);
      if (!new_guid.IsNullOrWhiteSpace())
        arg_dict.Remove(new_guid);
      unit.UpdateUnit(arg_dict);
    }

    public Unit GetUnit(string guid)
    {
      return this.GetChild<Unit>(guid);
    }

    public Dictionary<string, Unit> GetUnitDict()
    {
      return this.unit_dict;
    }

    public void RemoveUnit(string guid)
    {
      Unit unit = this.GetUnit(guid);
      if (unit != null)
      {
        this.RemoveChild(guid);
        this.unit_dict.Remove(guid);
        if (!unit.GetFaction().IsNullOrWhiteSpace())
          this.faction_unit_dict[unit.GetFaction()].Remove(guid);
      }
    }

    public void OnUnitGuidChange(string old_guid, string new_guid)
    {
      Unit unit = this.GetUnit(old_guid);
      if (unit != null && !old_guid.Equals(new_guid))
      {
        this.unit_dict.Remove(old_guid);
        this.key_to_child_dict.Remove(old_guid);
        int index = this.child_key_list.IndexOf(old_guid);

        this.unit_dict[new_guid] = unit;
        this.key_to_child_dict[new_guid] = unit;
        this.child_key_list[index] = new_guid;
      }
    }
  }
}