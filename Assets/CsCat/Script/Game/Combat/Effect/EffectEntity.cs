using UnityEngine;

namespace CsCat
{
	//创建后需要调用CreateEffect
	public class EffectEntity : GameObjectEntity
	{
		public string effectId;
		public CfgEffectData cfgEffectData;
		public Unit unit;


		public virtual void Init(string effectId, Unit unit)
		{
			base.Init();
			this.effectId = effectId;
			this.unit = unit;
			cfgEffectData = CfgEffect.Instance.get_by_id(this.effectId);
			graphicComponent.SetPrefabPath(cfgEffectData.prefab_path);
		}

		protected override GraphicComponent CreateGraphicComponent()
		{
			return this.AddComponent<EffectGraphicComponent>(null,
			  Client.instance.combat.effectManager.resLoadComponent);
		}


		public override void OnAllAssetsLoadDone()
		{
			base.OnAllAssetsLoadDone();
			graphicComponent.SetIsShow(true);
		}


		public virtual void OnEffectReach()
		{
			this.parent.RemoveChild(this.key);
		}

		public virtual void OnNoRemainDuration()
		{
			this.parent.RemoveChild(this.key);
		}

		public void ApplyToTransformComponent(Vector3? position, Vector3? eulerAngles, Vector3? scale = null)
		{
			if (position != null)
				this.transformComponent.position = position.Value;
			if (eulerAngles != null)
				this.transformComponent.eulerAngles = eulerAngles.Value;
			if (scale != null)
				this.transformComponent.scale = scale.Value;
		}


		protected override void _Destroy()
		{
			base._Destroy();
		}
	}
}