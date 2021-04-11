using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CsCat
{
  public partial class SpellManager
  {
    // is_control 是否控制类技能
    public SpellBase CastSpell(Unit source_unit, string spell_id, Unit target_unit, Hashtable instance_arg_dict = null,
      bool is_control = false)
    {
      var (is_can_cast, spellDefinition, spell_class) =
        this.CheckIsCanCast(source_unit, spell_id, target_unit, is_control);
      if (!is_can_cast)
        return null;
      //开始释放技能
      var spell = this.AddChild(null, spell_class, source_unit, spell_id, target_unit, spellDefinition,
        instance_arg_dict) as SpellBase;
      if ("正常".Equals(spellDefinition.cast_type))
      {
        //当玩家是手动操作释放技能时，技能方向就以玩家的输入为准（但如果有目标则会以目标方向释放技能，无视输入）
        //当释放的技能类型是正常的话，则需停下来施法
        if (source_unit.current_attack != null)
          this.BreakSpell(source_unit.current_attack.GetGuid());
        Quaternion? rotation = null;
        var is_not_face_to_target =
          instance_arg_dict != null
            ? instance_arg_dict.Get<bool>("is_not_face_to_target")
            : spellDefinition.is_not_face_to_target;
        var target_position = instance_arg_dict?.Get<Vector3>("position") ?? target_unit.GetPosition();
        if (target_unit != null && (!is_not_face_to_target || !is_control))
        {
          var dir = target_position - source_unit.GetPosition();
          rotation = Quaternion.LookRotation(dir);
          if (!rotation.Value.IsZero())
            source_unit.FaceTo(rotation.Value);
        }

        if (!spellDefinition.is_can_move_while_cast || !is_control)
          source_unit.MoveStop(rotation);
        source_unit.current_attack = spell;
        source_unit.UpdateMixedStates();
      }

      if ("普攻".Equals(spellDefinition.type))
        source_unit.NormalAttackStart();
      spell.Start();
      return spell;
    }


    public (bool is_can_cast, SpellDefinition spellDefinition, Type spell_class) CheckIsCanCast(Unit source_unit,
      string spell_id, Unit target_unit, bool is_control)
    {
      SpellDefinition spellDefinition = DefinitionManager.instance.GetSpellDefinition(spell_id);
      Type spell_class = null;
      if (spellDefinition == null)
      {
        LogCat.LogErrorFormat("spell_id(%d) is not exist!", spell_id);
        return (false, null, null);
      }

      if (source_unit == null || (source_unit.IsDead() && !"触发".Equals(spellDefinition.cast_type)))
        return (false, null, null);
      if (!source_unit.IsSpellCooldownOk(spell_id))
        return (false, null, null);
      if (!source_unit.CanBreakCurrentSpell(spell_id, spellDefinition))
        return (false, null, null);
      var scope = spellDefinition.target_type ?? "enemy";
      //如果是混乱则找任何可以攻击的人
      if (source_unit.IsConfused())
        scope = "all";
      var is_only_attackable = !"friend".Equals(scope);
      if (spellDefinition.is_need_target)
      {
        if (target_unit == null)
          return (false, null, null);
        Hashtable range_info = new Hashtable();
        range_info["mode"] = "circle";
        range_info["radius"] = spellDefinition.range;
        if (!Client.instance.combat.unitManager.__CheckUnit(target_unit,
          source_unit.ToUnitPosition(), range_info, source_unit.GetFaction(), scope,
          is_only_attackable))
          return (false, null, null);
      }

      spell_class = TypeUtil.GetType(spellDefinition.class_path_cs);
      if (spell_class.IsHasMethod("CheckIsCanCast") &&
          !spell_class.InvokeMethod<bool>("CheckIsCanCast", false, source_unit, spell_id, target_unit, spellDefinition,
            is_control)
      ) //静态方法CheckIsCanCast
        return (false, null, null);
      return (true, spellDefinition, spell_class);
    }

    public List<Unit> RecommendCast(Unit source_unit, string spell_id, Unit target_unit, bool is_control)
    {
      if (source_unit == null || source_unit.IsDead())
        return null;
      if (target_unit == null)
        return null;
      var spellDefinition = DefinitionManager.instance.GetSpellDefinition(spell_id);
      var spell_class = TypeUtil.GetType(spellDefinition.class_path_cs);
      if (spell_class == null)
      {
        LogCat.error("spell code is not exist: ", spellDefinition.class_path_cs);
        return null;
      }

      if (this.__IsUnitMatchCondition(source_unit, target_unit, is_control, spellDefinition, spell_class))
        return new List<Unit>() {target_unit};
      return null;
    }

    public List<Unit> RecommendCast(Unit source_unit, string spell_id, List<Unit> target_unit_list, bool is_control)
    {
      if (source_unit == null || source_unit.IsDead())
        return null;
      if (target_unit_list == null)
        return null;
      var spellDefinition = DefinitionManager.instance.GetSpellDefinition(spell_id);
      var spell_class = TypeUtil.GetType(spellDefinition.class_path_cs);
      if (spell_class == null)
      {
        LogCat.error("spell code is not exist: ", spellDefinition.class_path_cs);
        return null;
      }

      List<Unit> new_target_unit_list = new List<Unit>();
      foreach (var target_unit in target_unit_list)
      {
        if (this.__IsUnitMatchCondition(source_unit, target_unit, is_control, spellDefinition, spell_class))
          new_target_unit_list.Add(target_unit);
      }

      return new_target_unit_list;
    }

    public List<Unit> RecommendSpellRule(Unit source_unit, Unit target_unit,
      SpellDefinition spellDefinition, Vector3 origin_position, List<Unit> target_unit_list = null)
    {
      //当前敌人
      //随机x个敌人
      //生命最低的x个人敌人
      //全体敌人
      //自己
      //随机x个队友
      //生命最低的x个队友
      //全体队友
      //召唤单位
      //场上所有人(不分敌友)
      if (target_unit == null)
        return null;
      if (spellDefinition.select_unit_arg_dict.IsNullOrEmpty())
        return target_unit_list ?? new List<Unit>() {target_unit};

      var select_unit_arg_dict = DoerAttrParserUtil.ConvertTableWithTypeString(spellDefinition.select_unit_arg_dict);
      var select_unit_faction = select_unit_arg_dict.Get<string>("select_unit_faction");
      var select_unit_count = select_unit_arg_dict.GetOrGetDefault<int>("select_unit_count", () => 1000);
      var scope = SpellConst.Select_Unit_Faction_Dict[select_unit_faction];

      var range_info = new Hashtable();
      range_info["mode"] = "circle";
      range_info["radius"] = spellDefinition.range;
      var condition_dict = new Hashtable();

      condition_dict["order"] = "distance";
      condition_dict["origin"] = origin_position;
      condition_dict["faction"] = source_unit.GetFaction();
      condition_dict["scope"] = scope;
      condition_dict["range_info"] = range_info;
      target_unit_list = target_unit_list ?? Client.instance.combat.unitManager.SelectUnit(condition_dict);

      var count = select_unit_count;
      var new_target_list = new List<Unit>();
      //TODO select_unit
      //TODO select_unit
      new_target_list = target_unit_list.Sub(0, Math.Min(target_unit_list.Count, count));
      if (new_target_list.Count == 0)
        return new List<Unit>() {target_unit};
      return new_target_list;
    }

    public bool __IsUnitMatchCondition(Unit source_unit, Unit target_unit, bool is_control,
      SpellDefinition spellDefinition,
      Type spell_class)
    {
      if (target_unit.IsDead())
        return false;
      if (!source_unit.IsConfused())
      {
        if ("enemy".Equals(spellDefinition.target_type) && target_unit.IsInvincible())
          return false;
        if (spellDefinition.target_type.IsNullOrWhiteSpace() && !"all".Equals(spellDefinition.target_type))
          if (!Client.instance.combat.unitManager.CheckFaction(source_unit.GetFaction(), target_unit.GetFaction(),
            spellDefinition.target_type))
            return false;
      }

      if (!spell_class.InvokeMethod<bool>("IsUnitMatchCondition", false, source_unit, spellDefinition.id, target_unit,
        spellDefinition, is_control))
        return false;
      return true;
    }
  }
}