using System.Collections;

namespace CsCat
{
	public partial class SpellBase
	{
		public void _Hit(Unit sourceUnit, Unit targetUnit, float? damageFactor = null, int? forceDamageValue = null)
		{
			if (targetUnit == null || targetUnit.IsDead())
				return;
			this.CreateHitEffect(sourceUnit, targetUnit);
			var (damageValue, specialEffectDict) =
			  this.TakeDamage(sourceUnit, targetUnit, damageFactor, forceDamageValue);
			this.FireEvent<Unit, Unit, SpellBase, int>(null, UnitEventNameConst.On_Unit_Hit, sourceUnit, targetUnit, this, damageValue);
			this.AddCombatNumber(damageValue, targetUnit.GetGuid(), "physical", specialEffectDict);
			targetUnit.PlayAnimation(AnimationNameConst.BeHit, null, null, null, true);
		}

		public void Hit(Unit sourceUnit, Unit targetUnit, float? damageFactor = null, int? forceDamageValue = null)
		{
			this._Hit(sourceUnit, targetUnit, damageFactor, forceDamageValue);
		}

		public (int damageValue, Hashtable specialEffectDict) TakeDamage(Unit sourceUnit, Unit targetUnit,
		  float? damageFactor = null,
		  int? forceDamageValue = null)
		{
			var specialEffectDict = new Hashtable();
			if (targetUnit == null || targetUnit.IsDead())
				return (0, specialEffectDict);
			float damageFactorValue;
			if (damageFactor != null && damageFactor.Value > 0)
				damageFactorValue = damageFactor.Value;
			else
				damageFactorValue = this.cfgSpellData.damageFactor == 0 ? 1 : this.cfgSpellData.damageFactor;
			//计算原始伤害值
			int damageValue;
			if (forceDamageValue == null)
			{
				Hashtable argDict = new Hashtable
				{
					["damageFactor"] = damageFactorValue,
					["curHpPct"] = sourceUnit.GetHp() / (float)sourceUnit.GetMaxHp()
				};
				(damageValue, specialEffectDict) = sourceUnit.propertyComp.CalculateOriginalDamageValue(argDict);
			}
			else
				damageValue = forceDamageValue.Value;

			//计算减伤
			damageValue = sourceUnit.propertyComp.CalculateRealDamageValue(damageValue, targetUnit);
			//伤害前的回调
			this.FireEvent<Unit, Unit, SpellBase, int>(null, UnitEventNameConst.Before_Unit_Hit, sourceUnit, targetUnit, this, damageValue);
			Client.instance.combat.spellManager.BeforeHit(sourceUnit, targetUnit, this, damageValue);
			//目标接收伤害
			damageValue = targetUnit.TakeDamage(damageValue, sourceUnit, this);
			return (damageValue, specialEffectDict);
		}


		public (int healValue, Hashtable specialEffectDict) Heal(Unit sourceUnit, Unit targetUnit,
		  int? forceDamageValue = null, float? healFactor = null)
		{
			targetUnit = targetUnit ?? this.targetUnit;
			sourceUnit = sourceUnit ?? this.sourceUnit;
			Hashtable specialEffectDict = new Hashtable();
			if (targetUnit == null || targetUnit.IsDead())
				return (0, specialEffectDict);
			float healFactorValue;
			if (healFactor != null)
				healFactorValue = healFactor.Value;
			else
				healFactorValue = this.cfgSpellData.damageFactor == 0 ? 1 : this.cfgSpellData.damageFactor;

			int healValue;
			if (forceDamageValue != null)
				healValue = forceDamageValue.Value;
			else
			{
				Hashtable argDict = new Hashtable
				{
					["healFactor"] = healFactorValue,
					["damageType"] = this.cfgSpellData.damageType
				};
				(healValue, specialEffectDict) =
				  sourceUnit.propertyComp.CalculateOriginalHealValue(argDict);
			}

			healValue = sourceUnit.propertyComp.CalculateRealHealValue(healValue, targetUnit);
			targetUnit.Heal(healValue, sourceUnit);
			return (healValue, specialEffectDict);
		}

		//吸血
		public void SuckBlood(Unit sourceUnit, Unit targetUnit, int? forceHealValue = null)
		{
			Heal(sourceUnit, targetUnit, forceHealValue);
		}

	}
}