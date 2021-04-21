using System.Collections;

namespace CsCat
{
  public partial class SpellBase
  {
    public void ___Hit(Unit source_unit, Unit target_unit, float? damage_factor = null, int? force_damage_value = null)
    {
      if (target_unit == null || target_unit.IsDead())
        return;
      this.CreateHitEffect(source_unit, target_unit);
      var (damage_value, special_effect_dict) =
        this.TakeDamage(source_unit, target_unit, damage_factor, force_damage_value);
      this.Broadcast<Unit, Unit, SpellBase, int>(UnitEventNameConst.On_Unit_Hit, source_unit, target_unit, this, damage_value);
      this.AddCombatNumber(damage_value, target_unit.GetGuid(), "physical", special_effect_dict);
      target_unit.PlayAnimation(AnimationNameConst.be_hit, null, null, null, true);
    }

    public void Hit(Unit source_unit, Unit target_unit, float? damage_factor = null, int? force_damage_value = null)
    {
      this.___Hit(source_unit, target_unit, damage_factor, force_damage_value);
    }

    public (int damage_value, Hashtable special_effect_dict) TakeDamage(Unit source_unit, Unit target_unit,
      float? damage_factor = null,
      int? force_damage_value = null)
    {
      var special_effect_dict = new Hashtable();
      if (target_unit == null || target_unit.IsDead())
        return (0, special_effect_dict);
      float _damage_factor;
      if (damage_factor != null && damage_factor.Value > 0)
        _damage_factor = damage_factor.Value;
      else
        _damage_factor = this.spellDefinition.damage_factor == 0 ? 1 : this.spellDefinition.damage_factor;
      //计算原始伤害值
      int damage_value;
      if (force_damage_value == null)
      {
        Hashtable arg_dict = new Hashtable();
        arg_dict["damage_factor"] = _damage_factor;
        arg_dict["cur_hp_pct"] = source_unit.GetHp() / (float)source_unit.GetMaxHp();
        (damage_value, special_effect_dict) = source_unit.propertyComp.CalculateOriginalDamageValue(arg_dict);
      }
      else
        damage_value = force_damage_value.Value;

      //计算减伤
      damage_value = source_unit.propertyComp.CalculateRealDamageValue(damage_value, target_unit);
      //伤害前的回调
      this.Broadcast<Unit, Unit, SpellBase, int>(UnitEventNameConst.Before_Unit_Hit, source_unit, target_unit, this, damage_value);
      Client.instance.combat.spellManager.BeforeHit(source_unit, target_unit, this, damage_value);
      //目标接收伤害
      damage_value = target_unit.TakeDamage(damage_value, source_unit, this);
      return (damage_value, special_effect_dict);
    }


    public (int heal_value, Hashtable special_effect_dict) Heal(Unit source_unit, Unit target_unit,
      int? force_damage_value = null, float? heal_factor = null)
    {
      target_unit = target_unit ?? this.target_unit;
      source_unit = source_unit ?? this.source_unit;
      Hashtable special_effect_dict = new Hashtable();
      if (target_unit == null || target_unit.IsDead())
        return (0, special_effect_dict);
      float _heal_factor;
      if (heal_factor != null)
        _heal_factor = heal_factor.Value;
      else
        _heal_factor = this.spellDefinition.damage_factor == 0 ? 1 : this.spellDefinition.damage_factor;

      int heal_value;
      if (force_damage_value != null)
        heal_value = force_damage_value.Value;
      else
      {
        Hashtable arg_dict = new Hashtable();
        arg_dict["heal_factor"] = _heal_factor;
        arg_dict["damage_type"] = this.spellDefinition.damage_type;
        (heal_value, special_effect_dict) =
          source_unit.propertyComp.CalculateOriginalHealValue(arg_dict);
      }

      heal_value = source_unit.propertyComp.CalculateRealHealValue(heal_value, target_unit);
      target_unit.Heal(heal_value, source_unit);
      return (heal_value, special_effect_dict);
    }

    //吸血
    public void SuckBlood(Unit source_unit, Unit target_unit, int? force_heal_value = null)
    {
      Heal(source_unit, target_unit, force_heal_value);
    }

  }
}