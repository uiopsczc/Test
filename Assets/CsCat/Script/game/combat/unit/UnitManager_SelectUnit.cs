using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CsCat
{
  public partial class UnitManager
  {
    public List<Unit> SelectUnit(Hashtable condition_dict)
    {
      var range_info = condition_dict.Get<Hashtable>("range_info");
      IPosition origin_iposition = condition_dict.Get<IPosition>("origin_iposition");
      var start_position = origin_iposition.GetPosition();
      var scope = condition_dict.Get<string>("scope");
      float max_distance = range_info.Get<float>("radius");
      if (range_info.Get<string>("mode").Equals("rect"))
      {
        max_distance = Math.Max(max_distance, range_info.Get<float>("height"));
        max_distance = Math.Max(max_distance, range_info.Get<float>("width"));
      }

      string order = condition_dict.Get<string>("order");
      string faction = condition_dict.Get<string>("faction");
      var candidate_list = condition_dict.Get<List<Unit>>("candidate_list");
      var is_only_attackable = condition_dict.Get<bool>("is_only_attackable");
      var is_can_select_hide_unit = condition_dict.Get<bool>("is_can_select_hide_unit");

      var target_unit_list = new List<Unit>();
      var match_faction_list = this.GetMatchFactionList(faction, scope);

      //有候选目标时，则在候选目标里选择，不考虑忽略阵营
      var check_unit_list = candidate_list != null ? candidate_list : this.GetFactionUnitList(match_faction_list);
      foreach (var unit in check_unit_list)
      {
        if (!unit.IsDestroyed() && !unit.IsDead() && this.__CheckUnit(unit, origin_iposition, range_info, faction, scope,
              is_only_attackable, is_can_select_hide_unit))
          target_unit_list.Add(unit);
      }

      if ("distance".Equals(order) && !target_unit_list.IsNullOrEmpty())
        target_unit_list.QuickSort((a, b) => { return a.Distance(origin_iposition) <= b.Distance(origin_iposition); });
      return target_unit_list;
    }


    public bool __CheckUnit(Unit unit, IPosition origin_iposition, Hashtable range_info, string faction, string scope,
      bool is_only_attackable, bool is_can_select_hide_unit = false)
    {
      if ("技能物体".Equals(unit.unitDefinition.type))
        return false;
      if (is_only_attackable)
        if (unit.IsInvincible())
          return false;

      if (!"all".Equals(scope))
      {
        if (scope.IsNullOrWhiteSpace() || !"friend".Equals(scope))
        {
          if (!this.CheckFaction(faction, unit.GetFaction(), "enemy"))
            return false;
        }
        else
        {
          if (!this.CheckFaction(faction, unit.GetFaction(), "friend"))
            return false;
        }
      }

      if (!is_can_select_hide_unit)
        if (unit.IsHide() && !unit.IsExpose())
          return false;

      if (origin_iposition == null || range_info == null)
        return false;

      if ("circle".Equals(range_info.Get<string>("mode")))
      {
        float radius = range_info.Get<float>("radius");
        if (!range_info.ContainsKey("radius") || radius < 0)
          return false;
        if (unit.Distance(origin_iposition) > radius)
          return false;
        var angle = range_info.Get<float>("angle");
        if (!range_info.ContainsKey("angle"))
          return true;
        var forward = range_info.Get<Quaternion>("rotation").Forward();
        var orgin_position = origin_iposition.GetPosition();
        var right = Quaternion.AngleAxis(90, Vector3.up) * forward;
        var dir_r = (unit.GetPosition() + (right * radius)) - orgin_position;
        var dir_l = (unit.GetPosition() + (-right * radius)) - orgin_position;
        return (Vector3.Angle(forward, dir_l) < angle / 2) || (Vector3.Angle(forward, dir_r) < angle / 2);
      }
      else if ("rect".Equals(range_info.Get<string>("mode")))
      {
        if (!range_info.ContainsKey("height") || !range_info.ContainsKey("width") ||
            range_info.Get<float>("height") < 0 || range_info.Get<float>("width") < 0)
          return false;
        if (!range_info.ContainsKey("rotation"))
          range_info["rotation"] = default(Quaternion);
        var pos = unit.GetPosition();
        var orgin_position = origin_iposition.GetPosition();
        pos = pos - orgin_position;
        pos = (range_info.Get<Quaternion>("rotation").Inverse()) * pos;
        var unit_radius = unit.GetRadius();

        if (range_info.Get<bool>("is_use_center"))
          return Math.Abs(pos.x) < range_info.Get<float>("width") / 2 + unit_radius &&
                 Math.Abs(pos.z) < range_info.Get<float>("height") / 2 + unit_radius;
        else
          return Math.Abs(pos.x) < range_info.Get<float>("width") + unit_radius &&
                 Math.Abs(pos.z) < range_info.Get<float>("height") + unit_radius;
      }

      return false;
    }
  }
}