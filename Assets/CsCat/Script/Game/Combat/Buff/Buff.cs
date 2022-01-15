using System.Collections;
using System.Collections.Generic;

namespace CsCat
{
	public class Buff : TickObject
	{
		//因为有些buff可以同时存在多个，但效果只有一个生效
		private List<BuffCache> buffCacheList = new List<BuffCache>(); //效果不累加
		private BuffManager buffManager;
		public CfgBuffData cfgBuffData;
		private List<EffectEntity> effectList = new List<EffectEntity>(); // 一个buff可能有多个特效
		public string buffId;
		private string triggerSpellGuid;

		public void Init(BuffManager buffManager, string buffId)
		{
			base.Init();
			this.buffManager = buffManager;
			this.buffId = buffId;

			cfgBuffData = CfgBuff.Instance.get_by_id(this.buffId);

		}

		public Buff CreateBuffCache(float duration, Unit sourceUnit, SpellBase sourceSpell, Hashtable argDict)
		{
			var buffCache = new BuffCache(duration, sourceUnit, sourceSpell, argDict);
			buffCacheList.Add(buffCache);
			if (buffCacheList.Count == 1) //第一个的时候才添加
			{
				this.AddEffects();
				this.AddPropertyDict();
				this.AddTriggerSpell();
				this.buffManager.AddState(this.cfgBuffData.state);
			}

			return this;
		}

		public void RemoveBuffCache(string sourceUnitGuid, string sourceSpellGuid)
		{
			for (int i = this.buffCacheList.Count - 1; i >= 0; i--)
			{
				var buffCache = this.buffCacheList[i];
				var isThisUnit = sourceUnitGuid.IsNullOrWhiteSpace() ||
								   (buffCache.sourceUnit != null && buffCache.sourceUnit.GetGuid().Equals(sourceUnitGuid));
				var isThisSpell = sourceSpellGuid.IsNullOrWhiteSpace() ||
									(buffCache.sourceSpell != null &&
									 buffCache.sourceSpell.GetGuid().Equals(sourceSpellGuid));
				if (isThisUnit && isThisSpell)
					this.buffCacheList.RemoveAt(i);
			}

			if (this.buffCacheList.IsNullOrEmpty())
				this.buffManager.RemoveBuffByBuff(this);
		}


		protected override void _Update(float deltaTime = 0, float unscaledDeltaTime = 0)
		{
			base._Update(deltaTime, unscaledDeltaTime);
			for (var i = buffCacheList.Count - 1; i >= 0; i--)
			{
				var buffCache = buffCacheList[i];
				buffCache.remainDuration -= deltaTime;
				if (buffCache.remainDuration <= 0)
					buffCacheList.RemoveAt(i);
			}

			if (buffCacheList.Count == 0)
				buffManager.RemoveBuffByBuff(this);
		}


		private void AddEffects()
		{
			var effectIds = cfgBuffData._effect_ids;
			if (effectIds.IsNullOrEmpty())
				return;
			for (var i = 0; i < effectIds.Length; i++)
			{
				var effectId = effectIds[i];
				var cfgEffectData = CfgEffect.Instance.get_by_id(effectId);
				var effect =
					Client.instance.combat.effectManager.CreateAttachEffectEntity(effectId, this.buffManager.unit,
						cfgEffectData.duration); //TODO 如何初始化effectBase
				effectList.Add(effect);
			}
		}

		private void RemoveEffects()
		{
			for (var i = 0; i < effectList.Count; i++)
			{
				var effect = effectList[i];
				Client.instance.combat.effectManager.RemoveEffectEntity(effect.key);
			}
		}

		private void AddPropertyDict()
		{
			var newPropertyDict = DoerAttrParserUtil.ConvertTableWithTypeString(this.cfgBuffData._property_dict).ToDict<string, float>();
			if (!newPropertyDict.IsNullOrEmpty())
			{
				var propertyComp = this.buffManager.unit.propertyComp;
				propertyComp.StartChange();
				propertyComp.AddPropSet(newPropertyDict, "buff", this.GetGuid());
				propertyComp.EndChange();
			}
		}

		private void RemovePropertyDict()
		{
			var newPropertyDict = DoerAttrParserUtil.ConvertTableWithTypeString(this.cfgBuffData._property_dict).ToDict<string, float>();
			if (!newPropertyDict.IsNullOrEmpty())
			{
				var propertyComp = this.buffManager.unit.propertyComp;
				propertyComp.StartChange();
				propertyComp.RemovePropSet("buff", this.GetGuid());
				propertyComp.EndChange();
			}
		}

		private void AddTriggerSpell()
		{
			var triggerSpellId = this.cfgBuffData.trigger_spell_id;
			if (triggerSpellId.IsNullOrWhiteSpace())
				return;
			var spell = Client.instance.combat.spellManager.CastSpell(
			  this.buffCacheList[0].sourceUnit ?? this.buffManager.unit,
			  triggerSpellId, this.buffManager.unit);
			this.triggerSpellGuid = spell.GetGuid();
		}

		private void RemoveTriggerSpell()
		{
			if (!this.triggerSpellGuid.IsNullOrWhiteSpace())
			{
				var spell = Client.instance.combat.spellManager.GetSpell(this.triggerSpellGuid);
				if (spell == null)
					return;
				if (spell.IsHasMethod("OnBuffRemove"))
				{
					spell.InvokeMethod("OnBuffRemove", false, this);
					return;
				}

				Client.instance.combat.spellManager.RemoveSpell(this.triggerSpellGuid);
			}
		}

		protected override void _Destroy()
		{
			base._Destroy();
			this.RemoveEffects();
			this.RemovePropertyDict();
			this.RemoveTriggerSpell();
			this.buffManager.RemoveState(this.cfgBuffData.state);
		}
	}
}