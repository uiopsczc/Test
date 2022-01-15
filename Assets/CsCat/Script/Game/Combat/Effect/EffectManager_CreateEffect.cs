using System.Collections.Generic;
using UnityEngine;

namespace CsCat
{
	public partial class EffectManager
	{
		private HashSet<string> gameObjectPoolNameList = new HashSet<string>(); //用于销毁的时候，清理对应的gameObjectPool

		public EffectEntity CreateEffectEntity(string effectId, Unit unit, Vector3? pos = null,
		  Vector3? eulerAngles = null)
		{
			var effectEntity = AddChild<EffectEntity>(null, effectId, unit);
			gameObjectPoolNameList.Add((effectEntity.graphicComponent as EffectGraphicComponent)
			  .GetEffectGameObjectPoolName());
			if (pos != null)
				effectEntity.transformComponent.position = pos.Value;
			if (eulerAngles != null)
				effectEntity.transformComponent.eulerAngles = eulerAngles.Value;
			return effectEntity;
		}

		public EffectEntity CreateAttachEffectEntity(string effectId, Unit unit, float? duration,
		  Vector3? forceEulerAngles = null, float sectorAngle = 0)
		{
			var effectEntity = this.CreateEffectEntity(effectId, unit);
			effectEntity.AddComponent<AttachEffectComponent>(null, unit.ToUnitPosition(), forceEulerAngles,
			  sectorAngle);
			if (duration != null)
				effectEntity.AddComponent<DurationEffectComponent>(null, duration.Value);
			return effectEntity;
		}

		public EffectEntity CreateGroundEffectEntity(string effectId, Unit unit, Vector3 position, Vector3 eulerAngles,
		  float duration, bool isHide = false)
		{
			var effectEntity = this.CreateEffectEntity(effectId, unit, position, eulerAngles);
			effectEntity.AddComponent<DurationEffectComponent>(null, duration);
			effectEntity.graphicComponent.SetIsShow(!isHide);
			return effectEntity;
		}

		public EffectEntity CreateLineEffectEntity(string effectId, Unit unit, IPosition sourceIPosition,
		  IPosition targetIPosition, float speed, float accSpeed)
		{
			var effectEntity = this.CreateEffectEntity(effectId, unit);
			effectEntity.AddComponent<LineEffectComponent>(null, sourceIPosition, targetIPosition, 0, speed, accSpeed);
			return effectEntity;
		}


		public EffectEntity CreateSpinLineEffectEntity(string effectId, Unit unit, Vector3 startPosition,
		  Vector3 spinDir,
		  float startSpinAngle, float spinSpeed, float spinLength, Vector3 forwardDir, float forwardSpeed)
		{
			var effectEntity = this.CreateEffectEntity(effectId, unit);
			effectEntity.AddComponent<SpinLineEffectComponent>(null, startPosition, spinDir, startSpinAngle, spinSpeed,
			  spinLength, forwardDir,
			  forwardSpeed);
			return effectEntity;
		}


		public EffectEntity CreateMortarMissileEffectEntity(string effectId, Unit unit, IPosition sourceIPosition,
		  IPosition targetIPosition, Vector3 gravity, float startAngle)
		{
			var effect = this.CreateEffectEntity(effectId, unit);
			effect.AddComponent<MortarEffectComponent>(null, sourceIPosition, targetIPosition, gravity, startAngle);
			return effect;
		}
	}
}