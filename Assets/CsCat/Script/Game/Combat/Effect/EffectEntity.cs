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
			cfgEffectData = CfgEffect.Instance.GetById(this.effectId);
			graphicComponent.SetPrefabPath(cfgEffectData.prefabPath);
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
				this.TransformInfoComponent.position = position.Value;
			if (eulerAngles != null)
				this.TransformInfoComponent.eulerAngles = eulerAngles.Value;
			if (scale != null)
				this.TransformInfoComponent.scale = scale.Value;
		}


		protected override void _Destroy()
		{
			base._Destroy();
		}
	}
}