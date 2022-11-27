using System;

namespace CsCat
{
	public partial class Unit
	{
		public int TakeDamage(int damageValue, Unit sourceUnit, SpellBase spell)
		{
			if (this.IsDead() || this.IsInvincible())
				return 0;
			if (this.IsCanNotBeTakeDamage())
				return 0;
			if (this.aiComp != null && this.aiComp.IsHasMethod("BeHit"))
				this.aiComp.InvokeMethod("BeHit", false, sourceUnit, damageValue);
			//以后用于计算血条收到伤害效果
			int oldHp = this.GetHp();
			SetHp(Math.Max(0, this.GetHp() - damageValue), true);
			this.OnHpChange(sourceUnit, oldHp, this.GetHp());
			this.FireEvent<Unit, Unit, SpellBase, int>(null, UnitEventNameConst.On_Unit_Hurt, sourceUnit, this, spell, damageValue);

			if (this.GetHp() <= 0)
			{
				this.FireEvent<Unit, Unit, SpellBase, int>(null, UnitEventNameConst.Before_Unit_Dead, sourceUnit, this, spell, damageValue); //回调监听
				this.OnKilled(sourceUnit, spell);
			}

			return damageValue;
		}

		//治疗
		public int Heal(int healValue, Unit sourceUnit)
		{
			if (this.IsDead() || this.IsCanNotBeHeal())
				return 0;
			int oldHp = this.GetHp();
			SetHp(Math.Min(this.GetHp() + healValue, this.GetMaxHp()), true);
			this.OnHpChange(sourceUnit, oldHp, this.GetHp());
			return healValue;
		}
	}
}