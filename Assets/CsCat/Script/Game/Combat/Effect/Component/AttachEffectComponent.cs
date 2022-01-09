using UnityEngine;

namespace CsCat
{
	public class AttachEffectComponent : EffectComponent
	{
		private Transform attach_entity_transform;
		private Vector3? force_eulerAngles;
		private float sector_angle;
		public bool is_attach;

		public void Init(IPosition attach_entity_iposition,
		  Vector3? force_eulerAngles = null, float sector_angle = 0)
		{
			base.Init();
			attach_entity_iposition.SetSocketName(this.effectEntity.cfgEffectData.socket_name_1);
			this.attach_entity_transform = attach_entity_iposition.GetTransform();
			this.force_eulerAngles = force_eulerAngles;
			this.sector_angle = sector_angle;

			this.AddListener(this.GetGameEntity().eventDispatchers, ECSEventNameConst.OnAllAssetsLoadDone, OnAllAssetsLoadDone);

		}

		void OnAllAssetsLoadDone()
		{
			Attach();
		}

		void Attach()
		{
			this.ChangeAttach(true);
		}

		void DeAttach()
		{
			this.ChangeAttach(false);
		}

		void ChangeAttach(bool is_attach)
		{
			if (this.effectEntity.graphicComponent.gameObject == null)
				return;
			if (this.is_attach == is_attach)
				return;
			if (is_attach)
			{
				var socket_transform = this.attach_entity_transform;
				this.effectEntity.graphicComponent.SetParentTransform(socket_transform);
				this.effectEntity.graphicComponent.transform.localPosition = new Vector3(0, 0, 0);
				this.effectEntity.graphicComponent.transform.localRotation = Quaternion.identity;
				if (this.force_eulerAngles != null)
					this.effectEntity.graphicComponent.transform.eulerAngles = this.force_eulerAngles.Value;
				this.SetSector();
				this.is_attach = true;
			}
			else
			{
				this.effectEntity.graphicComponent.SetParentTransform(Client.instance.combat.effectManager.graphicComponent.transform);
				this.is_attach = false;
				this.effectEntity.graphicComponent.SetIsShow(false);
			}
		}

		void SetSector()
		{
			if (this.sector_angle != 0)
			{
				Transform sector_side_left = this.effectEntity.graphicComponent.transform.Find("sector/side_left");
				Transform sector_side_right = this.effectEntity.graphicComponent.transform.Find("sector/side_right");
				Transform sector_center = this.effectEntity.graphicComponent.transform.Find("sector/center");
				Material sector_center_mat = sector_center.GetComponent<MeshRenderer>().material;
				sector_center_mat.SetFloat("_AngleCos", Mathf.Cos(Mathf.Deg2Rad * (sector_angle / 2))); //扇形的角度大小
				sector_side_left.localRotation = Quaternion.Euler(0, (this.sector_angle + 2) / 2, 0); // 左边界的位置
				sector_side_right.localRotation = Quaternion.Euler(0, -(this.sector_angle + 2) / 2, 0); // 右边界的位置
			}
		}
	}
}



