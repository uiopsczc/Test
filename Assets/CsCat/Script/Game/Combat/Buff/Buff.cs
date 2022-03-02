using System.Collections;
using System.Collections.Generic;

namespace CsCat
{
	public class Buff : TickObject
	{
		//因为有些buff可以同时存在多个，但效果只有一个生效
		private readonly List<BuffCache> _buffCacheList = new List<BuffCache>(); //效果不累加
		private BuffManager _buffManager;
		private readonly List<EffectEntity> _effectList = new List<EffectEntity>(); // 一个buff可能有多个特效
		private string triggerSpellGuid;

		public CfgBuffData cfgBuffData;
		public string buffId;
		

		public void Init(BuffManager buffManager, string buffId)
		{
			base.Init();
			this._buffManager = buffManager;
			this.buffId = buffId;

			cfgBuffData = CfgBuff.Instance.GetById(this.buffId);

		}

		public Buff CreateBuffCache(float duration, Unit sourceUnit, SpellBase sourceSpell, Hashtable argDict)
		{
			var buffCache = new BuffCache(duration, sourceUnit, sourceSpell, argDict);
			_buffCacheList.Add(buffCache);
			if (_buffCacheList.Count == 1) //第一个的时候才添加
			{
				this.AddEffects();
				this.AddPropertyDict();
				this.AddTriggerSpell();
				this._buffManager.AddState(this.cfgBuffData.state);
			}

			return this;
		}

		public void RemoveBuffCache(string sourceUnitGuid, string sourceSpellGuid)
		{
			for (int i = this._buffCacheList.Count - 1; i >= 0; i--)
			{
				var buffCache = this._buffCacheList[i];
				var isThisUnit = sourceUnitGuid.IsNullOrWhiteSpace() ||
								   (buffCache.sourceUnit != null && buffCache.sourceUnit.GetGuid().Equals(sourceUnitGuid));
				var isThisSpell = sourceSpellGuid.IsNullOrWhiteSpace() ||
									(buffCache.sourceSpell != null &&
									 buffCache.sourceSpell.GetGuid().Equals(sourceSpellGuid));
				if (isThisUnit && isThisSpell)
					this._buffCacheList.RemoveAt(i);
			}

			if (this._buffCacheList.IsNullOrEmpty())
				this._buffManager.RemoveBuffByBuff(this);
		}


		protected override void _Update(float deltaTime = 0, float unscaledDeltaTime = 0)
		{
			base._Update(deltaTime, unscaledDeltaTime);
			for (var i = _buffCacheList.Count - 1; i >= 0; i--)
			{
				var buffCache = _buffCacheList[i];
				buffCache.remainDuration -= deltaTime;
				if (buffCache.remainDuration <= 0)
					_buffCacheList.RemoveAt(i);
			}

			if (_buffCacheList.Count == 0)
				_buffManager.RemoveBuffByBuff(this);
		}


		private void AddEffects()
		{
			var effectIds = cfgBuffData._effectIds;
			if (effectIds.IsNullOrEmpty())
				return;
			for (var i = 0; i < effectIds.Length; i++)
			{
				var effectId = effectIds[i];
				var cfgEffectData = CfgEffect.Instance.GetById(effectId);
				var effect =
					Client.instance.combat.effectManager.CreateAttachEffectEntity(effectId, this._buffManager.unit,
						cfgEffectData.duration); //TODO 如何初始化effectBase
				_effectList.Add(effect);
			}
		}

		private void RemoveEffects()
		{
			for (var i = 0; i < _effectList.Count; i++)
			{
				var effect = _effectList[i];
				Client.instance.combat.effectManager.RemoveEffectEntity(effect.key);
			}
		}

		private void AddPropertyDict()
		{
			var newPropertyDict = DoerAttrParserUtil.ConvertTableWithTypeString(this.cfgBuffData._propertyDict).ToDict<string, float>();
			if (!newPropertyDict.IsNullOrEmpty())
			{
				var propertyComp = this._buffManager.unit.propertyComp;
				propertyComp.StartChange();
				propertyComp.AddPropSet(newPropertyDict, "buff", this.GetGuid());
				propertyComp.EndChange();
			}
		}

		private void RemovePropertyDict()
		{
			var newPropertyDict = DoerAttrParserUtil.ConvertTableWithTypeString(this.cfgBuffData._propertyDict).ToDict<string, float>();
			if (!newPropertyDict.IsNullOrEmpty())
			{
				var propertyComp = this._buffManager.unit.propertyComp;
				propertyComp.StartChange();
				propertyComp.RemovePropSet("buff", this.GetGuid());
				propertyComp.EndChange();
			}
		}

		private void AddTriggerSpell()
		{
			var triggerSpellId = this.cfgBuffData.triggerSpellId;
			if (triggerSpellId.IsNullOrWhiteSpace())
				return;
			var spell = Client.instance.combat.spellManager.CastSpell(
			  this._buffCacheList[0].sourceUnit ?? this._buffManager.unit,
			  triggerSpellId, this._buffManager.unit);
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
			this._buffManager.RemoveState(this.cfgBuffData.state);
		}
	}
}