using System.Collections.Generic;
using UnityEngine;

namespace CsCat
{
	public partial class SpellBase
	{
		//起手特效
		public List<string> CreateHandEffect(float duration)
		{
			if (this.cfgSpellData.handEffectIds.IsNullOrEmpty())
				return null;
			var effectIds = this.cfgSpellData.handEffectIds;
			var guidList = new List<string>();
			for (var i = 0; i < effectIds.Length; i++)
			{
				var effectId = effectIds[i];
				var effect =
					Client.instance.combat.effectManager.CreateAttachEffectEntity(effectId, this.sourceUnit,
						duration);
				guidList.Add(effect.GetGuid());
			}

			return guidList;
		}

		public List<string> CreateGoEffect(float duration)
		{
			if (this.cfgSpellData.goEffectIds.IsNullOrEmpty())
				return null;
			var effectIds = this.cfgSpellData.goEffectIds;
			var guidList = new List<string>();
			for (var i = 0; i < effectIds.Length; i++)
			{
				var effectId = effectIds[i];
				var effect =
					Client.instance.combat.effectManager.CreateAttachEffectEntity(effectId, this.sourceUnit, duration);
				guidList.Add(effect.GetGuid());
			}

			return guidList;
		}

		//击中特效
		public List<string> CreateHitEffect(Unit sourceUnit, Unit targetUnit, float? duration = null,
		  float sectorAngle = 0, List<string> forceEffectIdList = null)
		{
			if (this.cfgSpellData.hitEffectIds.IsNullOrEmpty() && forceEffectIdList.IsNullOrEmpty())
				return null;
			Vector3? forceDir = null;
			if (sourceUnit != null)
			{
				var forceRotation = Quaternion.LookRotation(targetUnit.GetPosition() - sourceUnit.GetPosition());
				if (!forceRotation.IsZero())
					forceDir = forceRotation.eulerAngles;
			}

			var effectIds = forceEffectIdList != null
			  ? forceEffectIdList.ToArray()
			  : this.cfgSpellData.hitEffectIds;
			var guidList = new List<string>();
			for (var i = 0; i < effectIds.Length; i++)
			{
				var effectId = effectIds[i];
				var effect =
					Client.instance.combat.effectManager.CreateAttachEffectEntity(effectId, this.sourceUnit, duration,
						forceDir,
						sectorAngle);
				guidList.Add(effect.GetGuid());
			}

			return guidList;
		}

		//地面特效
		public List<string> CreateGroundEffect(Vector3? position, Vector3? eulerAngles, float duration,
		  List<string> forceEffectIdList = null, Vector3? forcePosition = null, bool isHide = false)
		{
			if (this.cfgSpellData.groundEffectIds.IsNullOrEmpty() && forceEffectIdList.IsNullOrEmpty())
				return null;
			Vector3 positionValue = forcePosition.GetValueOrDefault(position.GetValueOrDefault(this.sourceUnit.GetPosition()));
			Vector3 eulerAnglesValue =
			  eulerAngles.GetValueOrDefault(Quaternion.LookRotation(positionValue - this.sourceUnit.GetPosition()).GetNotZero()
				.eulerAngles);
			var effectIds = forceEffectIdList != null
			  ? forceEffectIdList.ToArray()
			  : this.cfgSpellData.groundEffectIds;
			var guidList = new List<string>();
			if (forcePosition == null)
				positionValue = Client.instance.combat.pathManager.GetGroundPos(positionValue);

			for (var i = 0; i < effectIds.Length; i++)
			{
				var effectId = effectIds[i];
				var effect = Client.instance.combat.effectManager.CreateGroundEffectEntity(effectId, this.sourceUnit,
					positionValue,
					eulerAnglesValue, duration, isHide);
				guidList.Add(effect.GetGuid());
			}

			return guidList;
		}

		//line特效
		public List<string> CreateLineEffect(IPosition targetIPosition, float speed, float accSpeed)
		{
			if (this.cfgSpellData.lineEffectIds.IsNullOrEmpty())
				return null;
			if (targetIPosition == null || !targetIPosition.IsValid())
				return null;
			var effectIds = this.cfgSpellData.lineEffectIds;
			var guidList = new List<string>();
			for (var i = 0; i < effectIds.Length; i++)
			{
				var effectId = effectIds[i];
				var effect = Client.instance.combat.effectManager.CreateLineEffectEntity(effectId, this.sourceUnit,
					this.sourceUnit.ToUnitPosition(), targetIPosition, speed, accSpeed);
				guidList.Add(effect.GetGuid());
			}

			return guidList;
		}


		public void RemoveEffect(List<string> effectGuidList)
		{
			if (effectGuidList.IsNullOrEmpty())
				return;
			for (var i = 0; i < effectGuidList.Count; i++)
			{
				var effectGuid = effectGuidList[i];
				RemoveEffect(effectGuid);
			}
		}

		public void RemoveEffect(string effectGuid)
		{
			Client.instance.combat.effectManager.RemoveEffectEntity(effectGuid);
		}
	}
}