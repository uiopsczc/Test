using System.Collections.Generic;
using UnityEngine;

namespace CsCat
{
	public partial class EffectManager
	{
		private HashSet<string> gameObject_pool_name_list = new HashSet<string>(); //用于销毁的时候，清理对应的gameObjectPool

		public EffectEntity CreateEffectEntity(string effect_id, Unit unit, Vector3? pos = null,
		  Vector3? eulerAngles = null)
		{
			var effectEntity = AddChild<EffectEntity>(null, effect_id, unit);
			gameObject_pool_name_list.Add((effectEntity.graphicComponent as EffectGraphicComponent)
			  .GetEffectGameObjectPoolName());
			if (pos != null)
				effectEntity.transformComponent.position = pos.Value;
			if (eulerAngles != null)
				effectEntity.transformComponent.eulerAngles = eulerAngles.Value;
			return effectEntity;
		}

		public EffectEntity CreateAttachEffectEntity(string effect_id, Unit unit, float? duration,
		  Vector3? force_eulerAngles = null, float sector_angle = 0)
		{
			var effectEntity = this.CreateEffectEntity(effect_id, unit);
			effectEntity.AddComponent<AttachEffectComponent>(null, unit.ToUnitPosition(), force_eulerAngles,
			  sector_angle);
			if (duration != null)
				effectEntity.AddComponent<DurationEffectComponent>(null, duration.Value);
			return effectEntity;
		}

		public EffectEntity CreateGroundEffectEntity(string effect_id, Unit unit, Vector3 position, Vector3 eulerAngles,
		  float duration, bool is_hide = false)
		{
			var effectEntity = this.CreateEffectEntity(effect_id, unit, position, eulerAngles);
			effectEntity.AddComponent<DurationEffectComponent>(null, duration);
			effectEntity.graphicComponent.SetIsShow(!is_hide);
			return effectEntity;
		}

		public EffectEntity CreateLineEffectEntity(string effect_id, Unit unit, IPosition source_iposition,
		  IPosition target_iposition, float speed, float acc_speed)
		{
			var effectEntity = this.CreateEffectEntity(effect_id, unit);
			effectEntity.AddComponent<LineEffectComponent>(null, source_iposition, target_iposition, 0, speed, acc_speed);
			return effectEntity;
		}


		public EffectEntity CreateSpinLineEffectEntity(string effect_id, Unit unit, Vector3 start_position,
		  Vector3 spin_dir,
		  float start_spin_angle, float spin_speed, float spin_length, Vector3 forward_dir, float forward_speed)
		{
			var effectEntity = this.CreateEffectEntity(effect_id, unit);
			effectEntity.AddComponent<SpinLineEffectComponent>(null, start_position, spin_dir, start_spin_angle, spin_speed,
			  spin_length, forward_dir,
			  forward_speed);
			return effectEntity;
		}


		public EffectEntity CreateMortarMissileEffectEntity(string effect_id, Unit unit, IPosition source_iposition,
		  IPosition target_iposition, Vector3 gravity, float start_angle)
		{
			var effect = this.CreateEffectEntity(effect_id, unit);
			effect.AddComponent<MortarEffectComponent>(null, source_iposition, target_iposition, gravity, start_angle);
			return effect;
		}
	}
}