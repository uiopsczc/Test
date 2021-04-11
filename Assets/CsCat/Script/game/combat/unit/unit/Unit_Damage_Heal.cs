using System;

namespace CsCat
{
  public partial class Unit
  {
    public int TakeDamage(int damage_value, Unit source_unit, SpellBase spell)
    {
      if (this.IsDead() || this.IsInvincible())
        return 0;
      if (this.IsCanNotBeTakeDamage())
        return 0;
      if (this.aiComp != null && this.aiComp.IsHasMethod("BeHit"))
        this.aiComp.InvokeMethod("BeHit", false, source_unit, damage_value);
      //以后用于计算血条收到伤害效果
      int old_hp = this.GetHp();
      SetHp(Math.Max(0, this.GetHp() - damage_value), true);
      this.OnHpChange(source_unit, old_hp, this.GetHp());
      this.Broadcast<Unit, Unit, SpellBase, int>(UnitEventNameConst.On_Unit_Hurt, source_unit, this, spell, damage_value);

      if (this.GetHp() <= 0)
      {
        this.Broadcast<Unit, Unit, SpellBase, int>(UnitEventNameConst.Before_Unit_Dead, source_unit, this, spell, damage_value); //回调监听
        this.OnKilled(source_unit, spell);
      }

      return damage_value;
    }

    //治疗
    public int Heal(int heal_value, Unit source_unit)
    {
      if (this.IsDead() || this.IsCanNotBeHeal())
        return 0;
      int old_hp = this.GetHp();
      SetHp(Math.Min(this.GetHp() + heal_value, this.GetMaxHp()), true);
      this.OnHpChange(source_unit, old_hp, this.GetHp());
      return heal_value;
    }
  }
}