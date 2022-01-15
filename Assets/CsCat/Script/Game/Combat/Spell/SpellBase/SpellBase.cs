using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CsCat
{
	public partial class SpellBase : TickObject
	{
		public Unit sourceUnit;
		public string spellId;
		public Unit targetUnit;
		public CfgSpellData cfgSpellData;
		public Hashtable instanceArgDict;

		public Vector3? originPosition;
		public Hashtable transmitArgDict;
		public Vector3 attackDir;
		public string newSpellTriggerId;
		public Hashtable argDict;
		private bool isCanMoveWhileCast;
		public bool isSpellAnimationFinished;
		public List<Hashtable> animationEventList = new List<Hashtable>();

		public void Init(Unit sourceUnit, string spellId,
		  Unit targetUnit, CfgSpellData cfgSpellData, Hashtable instanceArgDict)
		{
			base.Init();
			this.sourceUnit = sourceUnit;
			this.spellId = spellId;
			this.targetUnit = targetUnit;
			this.cfgSpellData = cfgSpellData;
			this.instanceArgDict = instanceArgDict;

			this.originPosition =
			  this.instanceArgDict.GetOrGetDefault2<Vector3>("origin_position", () => this.sourceUnit.GetPosition());
			this.transmitArgDict = this.instanceArgDict.GetOrGetDefault2("transmit_arg_dict", () => new Hashtable());
			this.attackDir = this.transmitArgDict.Get<Vector3>(attackDir);
			this.newSpellTriggerId = this.transmitArgDict.Get<string>("new_spell_trigger_id"); // 通过哪个trigger_id启动的技能

			this.argDict = DoerAttrParserUtil.ConvertTableWithTypeString(this.cfgSpellData._arg_dict);
			this.isCanMoveWhileCast = this.cfgSpellData.is_can_move_while_cast;
			this.isSpellAnimationFinished = "触发".Equals(this.cfgSpellData.cast_type);

			if (this.isCanMoveWhileCast && this.sourceUnit != null && !this.sourceUnit.IsDead())
				this.sourceUnit.SetIsMoveWithMoveAnimation(false);
			this.InitCounter();
		}

		protected override void _Update(float deltaTime, float unscaledDeltaTime)
		{
			base._Update(deltaTime, unscaledDeltaTime);
			if (!this.isSpellAnimationFinished)
			{
				//脱手了就不需要执行动画了
				//      if self.action then
				//      self.action:Update(deltaTime)
				//      else
				//      self: ProcessAnimationEvent(deltaTime)
				//      end
				this.ProcessAnimationEvent(deltaTime);
			}
			else if ("触发".Equals(this.cfgSpellData.cast_type))
				this.ProcessAnimationEvent(deltaTime);
		}

		public void RemoveSelf()
		{
			Client.instance.combat.spellManager.RemoveSpell(this.GetGuid());
		}

		protected override void _Destroy()
		{
			base._Destroy();
			if (!this.isSpellAnimationFinished)
				Client.instance.combat.spellManager.OnSpellAnimationFinished(this);
			Client.instance.combat.spellManager.RemoveListenersByObj(this);

		}

		public void AddCombatNumber(int number, string targetUnitGuid, string maxType, Hashtable argDict)
		{

		}

		public void AddCombatImage(int immuneType, string targetUnitGuid)
		{

		}

		public void AddCombatText(string strInfo, string targetUnitGuid)
		{

		}
	}
}