using System.Collections.Generic;

namespace CsCat
{
  public partial class SpellManager : TickObject
  {
    public Dictionary<string, List<SpellListenerInfo>>
      listener_dict = new Dictionary<string, List<SpellListenerInfo>>();

    public override void Init()
    {
      base.Init();
      this.listener_dict["on_start"] = new List<SpellListenerInfo>(); //技能Start之后触发  OnSpellStart
      this.listener_dict["on_cast"] = new List<SpellListenerInfo>(); //技能OnCast之后触发  OnSpellCast
      this.listener_dict["on_hurt"] = new List<SpellListenerInfo>(); //被伤害（任何形式）  OnHurt
      this.listener_dict["on_hurt_target"] = new List<SpellListenerInfo>(); //技能Start之后触发  OnSpellStart
      this.listener_dict["on_start"] = new List<SpellListenerInfo>(); //伤害目标（任何形式） OnHurt
      this.listener_dict["be_hit"] = new List<SpellListenerInfo>(); //被技能打后  OnHit
      this.listener_dict["on_hit"] = new List<SpellListenerInfo>(); //用技能打目标后  OnHit
      this.listener_dict["on_cur_spell_hit"] = new List<SpellListenerInfo>(); //用技能打目标后只有是相同的技能才触发  OnHit
      this.listener_dict["normal_attack"] = new List<SpellListenerInfo>(); //放普攻后  OnHit
      this.listener_dict["before_dead"] = new List<SpellListenerInfo>(); //死亡前 BeforeDead
      this.listener_dict["before_hit"] = new List<SpellListenerInfo>(); //用技能打目标前  BeforeHit
      this.listener_dict["before_be_hit"] = new List<SpellListenerInfo>(); //被技能打前  BeforeHit
      this.listener_dict["on_kill_target"] = new List<SpellListenerInfo>(); //杀死目标后 OnKillTarget
      this.listener_dict["on_hp_change"] = new List<SpellListenerInfo>(); //目标血量改变时 OnHpChange
      this.listener_dict["on_missile_reach"] = new List<SpellListenerInfo>(); //当子弹到达

      this.AddListener<Unit, Unit, SpellBase>(null, SpellEventNameConst.On_Spell_Start,
        (source_unit, target_unit, spell) => this.OnSpellStart(source_unit, target_unit, spell));
      this.AddListener<Unit, Unit, SpellBase>(null, SpellEventNameConst.On_Spell_Cast,
        (source_unit, target_unit, spell) => this.OnSpellCast(source_unit, target_unit, spell));
      this.AddListener<Unit, EffectEntity, SpellBase>(null, SpellEventNameConst.On_Missile_Reach,
        (source_unit, missileEffect, spell) => this.OnMissileReach(source_unit, missileEffect, spell));
      this.AddListener<Unit, Unit, int, int>(null,UnitEventNameConst.On_Unit_Hp_Change,
        (source_unit, target_unit, old_hp_value, new_hp_value) =>
          this.OnHpChange(source_unit, target_unit, old_hp_value, new_hp_value));
      this.AddListener<Unit, Unit, SpellBase, int>(null, UnitEventNameConst.On_Unit_Hurt,
        (source_unit, target_unit, spell, damage_value) => this.OnHurt(source_unit, target_unit, spell, damage_value));
      this.AddListener<Unit, Unit, SpellBase, int>(null, UnitEventNameConst.Before_Unit_Dead,
        (source_unit, target_unit, spell, damage_value) =>
          this.BeforeDead(source_unit, target_unit, spell, damage_value));
      this.AddListener<Unit, Unit, SpellBase>(null, UnitEventNameConst.On_Unit_Kill_Target,
        (source_unit, target_unit, spell) => this.OnKillTarget(source_unit, target_unit, spell));
      this.AddListener<Unit, Unit, SpellBase, int>(null, UnitEventNameConst.On_Unit_Hit,
        (source_unit, target_unit, spell, damage_value) => this.OnHit(source_unit, target_unit, spell, damage_value));
      this.AddListener<Unit, Unit, SpellBase, int>(null, UnitEventNameConst.Before_Unit_Hit,
        (source_unit, target_unit, spell, damage_value) =>
          this.BeforeHit(source_unit, target_unit, spell, damage_value));
      this.AddListener<Unit>(null, UnitEventNameConst.On_Unit_Destroy, this.OnUnitDestroy);
    }

    public SpellBase GetSpell(string guid)
    {
      return this.GetChild<SpellBase>(guid);
    }

    public void BreakSpell(string guid)
    {
      var spell = this.GetSpell(guid);
      if (spell != null)
        spell.Break();
    }

    public void RemoveSpell(string guid)
    {
      this.RemoveChild(guid);
    }

    public void OnUnitDestroy(Unit unit)
    {
      if (unit.current_attack != null)
        this.BreakSpell(unit.current_attack.GetGuid());
    }
    
    public void OnSpellAnimationFinished(SpellBase spell)
    {
      if (spell.source_unit.current_attack == spell)
      {
        if (spell.source_unit.is_normal_attacking)
          spell.source_unit.NormalAttackFinish();
        spell.source_unit.current_attack = null;
        spell.source_unit.UpdateMixedStates();
      }
    }



  }
}