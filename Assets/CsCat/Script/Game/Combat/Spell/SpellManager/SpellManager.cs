using System.Collections.Generic;

namespace CsCat
{
	public partial class SpellManager : TickObject
	{
		public Dictionary<string, List<SpellListenerInfo>>
		  listenerDict = new Dictionary<string, List<SpellListenerInfo>>();

		public override void Init()
		{
			base.Init();
			this.listenerDict["on_start"] = new List<SpellListenerInfo>(); //技能Start之后触发  OnSpellStart
			this.listenerDict["on_cast"] = new List<SpellListenerInfo>(); //技能OnCast之后触发  OnSpellCast
			this.listenerDict["on_hurt"] = new List<SpellListenerInfo>(); //被伤害（任何形式）  OnHurt
			this.listenerDict["on_hurt_target"] = new List<SpellListenerInfo>(); //技能Start之后触发  OnSpellStart
			this.listenerDict["on_start"] = new List<SpellListenerInfo>(); //伤害目标（任何形式） OnHurt
			this.listenerDict["be_hit"] = new List<SpellListenerInfo>(); //被技能打后  OnHit
			this.listenerDict["on_hit"] = new List<SpellListenerInfo>(); //用技能打目标后  OnHit
			this.listenerDict["on_cur_spell_hit"] = new List<SpellListenerInfo>(); //用技能打目标后只有是相同的技能才触发  OnHit
			this.listenerDict["normal_attack"] = new List<SpellListenerInfo>(); //放普攻后  OnHit
			this.listenerDict["before_dead"] = new List<SpellListenerInfo>(); //死亡前 BeforeDead
			this.listenerDict["before_hit"] = new List<SpellListenerInfo>(); //用技能打目标前  BeforeHit
			this.listenerDict["before_be_hit"] = new List<SpellListenerInfo>(); //被技能打前  BeforeHit
			this.listenerDict["on_kill_target"] = new List<SpellListenerInfo>(); //杀死目标后 OnKillTarget
			this.listenerDict["on_hp_change"] = new List<SpellListenerInfo>(); //目标血量改变时 OnHpChange
			this.listenerDict["on_missile_reach"] = new List<SpellListenerInfo>(); //当子弹到达

			this.AddListener<Unit, Unit, SpellBase>(null, SpellEventNameConst.On_Spell_Start,
			  (sourceUnit, targetUnit, spell) => this.OnSpellStart(sourceUnit, targetUnit, spell));
			this.AddListener<Unit, Unit, SpellBase>(null, SpellEventNameConst.On_Spell_Cast,
			  (sourceUnit, targetUnit, spell) => this.OnSpellCast(sourceUnit, targetUnit, spell));
			this.AddListener<Unit, EffectEntity, SpellBase>(null, SpellEventNameConst.On_Missile_Reach,
			  (sourceUnit, missileEffect, spell) => this.OnMissileReach(sourceUnit, missileEffect, spell));
			this.AddListener<Unit, Unit, int, int>(null, UnitEventNameConst.On_Unit_Hp_Change,
			  (sourceUnit, targetUnit, oldHpValue, newHpValue) =>
				this.OnHpChange(sourceUnit, targetUnit, oldHpValue, newHpValue));
			this.AddListener<Unit, Unit, SpellBase, int>(null, UnitEventNameConst.On_Unit_Hurt,
			  (sourceUnit, targetUnit, spell, damageValue) => this.OnHurt(sourceUnit, targetUnit, spell, damageValue));
			this.AddListener<Unit, Unit, SpellBase, int>(null, UnitEventNameConst.Before_Unit_Dead,
			  (sourceUnit, targetUnit, spell, damageValue) =>
				this.BeforeDead(sourceUnit, targetUnit, spell, damageValue));
			this.AddListener<Unit, Unit, SpellBase>(null, UnitEventNameConst.On_Unit_Kill_Target,
			  (sourceUnit, targetUnit, spell) => this.OnKillTarget(sourceUnit, targetUnit, spell));
			this.AddListener<Unit, Unit, SpellBase, int>(null, UnitEventNameConst.On_Unit_Hit,
			  (sourceUnit, targetUnit, spell, damageValue) => this.OnHit(sourceUnit, targetUnit, spell, damageValue));
			this.AddListener<Unit, Unit, SpellBase, int>(null, UnitEventNameConst.Before_Unit_Hit,
			  (sourceUnit, targetUnit, spell, damageValue) =>
				this.BeforeHit(sourceUnit, targetUnit, spell, damageValue));
			this.AddListener<Unit>(null, UnitEventNameConst.On_Unit_Destroy, this.OnUnitDestroy);
		}

		public SpellBase GetSpell(string guid)
		{
			return this.GetChild<SpellBase>(guid);
		}

		public void BreakSpell(string guid)
		{
			var spell = this.GetSpell(guid);
			spell?.Break();
		}

		public void RemoveSpell(string guid)
		{
			this.RemoveChild(guid);
		}

		public void OnUnitDestroy(Unit unit)
		{
			if (unit.currentAttack != null)
				this.BreakSpell(unit.currentAttack.GetGuid());
		}

		public void OnSpellAnimationFinished(SpellBase spell)
		{
			if (spell.sourceUnit.currentAttack == spell)
			{
				if (spell.sourceUnit.isNormalAttacking)
					spell.sourceUnit.NormalAttackFinish();
				spell.sourceUnit.currentAttack = null;
				spell.sourceUnit.UpdateMixedStates();
			}
		}



	}
}